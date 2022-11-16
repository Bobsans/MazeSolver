using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MazeSolver;

public partial class MainForm : Form {
    const int DEFAULT_UPDATE_FRAME = 500;

    readonly object drawLock = new();
    Maze _maze;
    Bitmap bitmap;
    InterpolationMode interpolationMode = InterpolationMode.Low;

    public MainForm() {
        InitializeComponent();
        solveAlgorythmBox.Items.AddRange(new object[] {
            nameof(SmartMazeSolver),
            nameof(SimpleMazeSolver)
        });
        solveAlgorythmBox.SelectedIndex = 0;
        Task.Run(RefreshInfo);
        speedBox.Text = DEFAULT_UPDATE_FRAME.ToString();
        sizeBox.Text = $@"{ClientRectangle.Width}x{ClientRectangle.Height - controlPanel.Height}";
        Icon = Icon.ExtractAssociatedIcon(Assembly.GetExecutingAssembly().Location);
    }

    void MazePanel_Paint(object sender, PaintEventArgs e) {
        if (bitmap != null) {
            lock (drawLock) {
                e.Graphics.InterpolationMode = interpolationMode;
                int destX = bitmap.Width == ClientRectangle.Width ? 0 : -1;
                int destY = bitmap.Height == ClientRectangle.Height - controlPanel.Height ? 0 : -1;
                e.Graphics.DrawImage(
                    bitmap,
                    new RectangleF(0, controlPanel.Height + 0, ClientRectangle.Width, ClientRectangle.Height - controlPanel.Height),
                    new RectangleF(destX, destY, bitmap.Width, bitmap.Height),
                    GraphicsUnit.Pixel
                );
            }
        }
    }

    void MainForm_ResizeEnd(object sender, EventArgs e) {
        if (Width % 2 == 0) {
            Width += 1;
        }

        if (Height % 2 == 1) {
            Height += 1;
        }

        RefreshView();
    }

    void GenerateButton_Click(object sender, EventArgs e) {
        ChangeControlsState(false);
        Task.Run(GenerateMaze).ContinueWith(_ => ChangeControlsState(true));
    }

    void SolveButton_Click(object sender, EventArgs e) {
        if (bitmap == null) {
            MessageBox.Show(@"You need to generate maze before solve it!", @"Stupid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        MazeSolver solver;
        switch (solveAlgorythmBox.SelectedItem) {
            case nameof(SimpleMazeSolver):
                solver = new SimpleMazeSolver(_maze);
                break;
            case nameof(SmartMazeSolver):
                solver = new SmartMazeSolver(_maze);
                break;
            default:
                MessageBox.Show(@"Invalid solver selected!", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
        }

        ChangeControlsState(false);
        Task.Run(() => SolveMaze(solver)).ContinueWith(_ => ChangeControlsState(true));
    }

    void SaveToolStripMenuItem_Click(object sender, EventArgs e) {
        if (bitmap != null) {
            SaveFileDialog dialog = new() { Filter = @"PNG image (*.png)|*.png" };
            if (dialog.ShowDialog() == DialogResult.OK) {
                bitmap.Save(dialog.FileName, ImageFormat.Png);
            }
        } else {
            MessageBox.Show(@"Maze is not generated", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    void SmoothBox_CheckedChanged(object sender, EventArgs e) {
        interpolationMode = smoothBox.Checked ? InterpolationMode.Low : InterpolationMode.NearestNeighbor;
        RefreshView();
    }

    void SizeLabel_Click(object sender, EventArgs e) {
        sizeBox.Text = $@"{ClientRectangle.Width}x{ClientRectangle.Height - controlPanel.Height}";
    }

    void ChangeControlsState(bool state) {
        try {
            Control[] controls = { generateButton, solveButton, solveAlgorythmBox, speedBox, sizeBox };
            foreach (Control control in controls) {
                if (control.InvokeRequired) {
                    control.Invoke((MethodInvoker)(() => control.Enabled = state));
                } else {
                    control.Enabled = state;
                }
            }
        } catch {
            // ignored
        }
    }

    void SetPixel(int x, int y, Color color) {
        lock (drawLock) {
            bitmap.SetPixel(x, y, color);
        }
    }

    void RefreshView() {
        try {
            Invoke((MethodInvoker)Invalidate);
        } catch {
            // ignored
        }
    }

    async void RefreshInfo() {
        while (true) {
            try {
                Invoke((MethodInvoker)(() => {
                    if (InfoChannel.Changed) {
                        string info = InfoChannel.Info;
                        if (!string.IsNullOrEmpty(info)) {
                            Text = @"MazeSolver :: " + info;
                        } else {
                            Text = @"MazeSolver";
                        }
                    }
                }));
            } catch (ObjectDisposedException) {
                break;
            } catch {
                // ignored
            }

            await Task.Delay(100);
        }
    }

    void GenerateMaze() {
        Size size = GetSize();

        lock (drawLock) {
            bitmap = new Bitmap(size.Width, size.Height);
        }

        int updateFrame = GetUpdateFrame();
        int width = (size.Width - 1) / 2;
        int height = (size.Height - 1) / 2;
        int counter = 0;

        MazeGenerator generator = new(width, height);
        generator.CellProcessed += cell => {
            int imgX = cell.X * 2 + 1;
            int imgY = cell.Y * 2 + 1;
            Color color = Color.Black;

            SetPixel(imgX - 1, imgY - 1, color);
            SetPixel(imgX - 1, imgY + 1, color);
            SetPixel(imgX + 1, imgY - 1, color);
            SetPixel(imgX + 1, imgY + 1, color);

            if (cell.WallTop) {
                SetPixel(imgX, imgY - 1, color);
            }

            if (cell.WallRight) {
                SetPixel(imgX + 1, imgY, color);
            }

            if (cell.WallBottom) {
                SetPixel(imgX, imgY + 1, color);
            }

            if (cell.WallLeft) {
                SetPixel(imgX - 1, imgY, color);
            }

            if (counter++ % updateFrame == 0) {
                RefreshView();
            }
        };

        generator.Generate();
        _maze = generator.Maze;

        RefreshView();
    }

    void SolveMaze(MazeSolver solver) {
        int counter = 0;
        int updateFrame = GetUpdateFrame();

        solver.CellProcessed += cell => {
            int imgX = cell.X * 2 + 1;
            int imgY = cell.Y * 2 + 1;

            if (cell.State != CellState.EMPTY) {
                SetPixel(imgX, imgY, GetCellColor(cell));

                if (!cell.WallTop) {
                    SetPixel(imgX, imgY - 1, GetCellColor(cell.Top));
                }

                if (!cell.WallRight) {
                    SetPixel(imgX + 1, imgY, GetCellColor(cell.Right));
                }

                if (!cell.WallBottom) {
                    SetPixel(imgX, imgY + 1, GetCellColor(cell.Bottom));
                }

                if (!cell.WallLeft) {
                    SetPixel(imgX - 1, imgY, GetCellColor(cell.Left));
                }

                if (cell.IsStart) {
                    SetPixel(imgX, imgY - 1, GetCellColor(cell));
                }

                if (cell.IsGoal) {
                    SetPixel(imgX, imgY + 1, GetCellColor(cell));
                }
            }

            if (counter++ % updateFrame == 0) {
                RefreshView();
            }
        };
        solver.Solve();

        RefreshView();
    }

    static Color GetCellColor(MazeCell cell) {
        switch (cell.State) {
            case CellState.WRONG:
                return Color.FromArgb(255, 247, 0, 28);
            case CellState.RIGHT:
                return Color.FromArgb(255, 107, 177, 66);
            case CellState.POTENTIAL:
                return Color.FromArgb(255, 35, 75, 248);
            default:
                return Color.Transparent;
        }
    }

    int GetUpdateFrame() {
        try {
            return int.Parse(speedBox.Text);
        } catch {
            MessageBox.Show(@"Invalid update frame. Use default.", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            speedBox.Text = DEFAULT_UPDATE_FRAME.ToString();
            return DEFAULT_UPDATE_FRAME;
        }
    }

    Size GetSize() {
        try {
            int[] parts = sizeBox.Text.Split('x').Select(int.Parse).ToArray();
            return new Size(parts[0], parts[1]);
        } catch {
            MessageBox.Show(@"Invalid size. Use default.", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Size result = new(ClientRectangle.Width, ClientRectangle.Height - controlPanel.Height);
            speedBox.Text = $@"{result.Width}x{result.Height}";
            return result;
        }
    }
}
