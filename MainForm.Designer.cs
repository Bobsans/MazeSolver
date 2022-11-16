namespace MazeSolver {
	partial class MainForm {
		/// <summary>
		/// Требуется переменная конструктора.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Освободить все используемые ресурсы.
		/// </summary>
		/// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
		protected override void Dispose(bool disposing) {   
			if(disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Код, автоматически созданный конструктором форм Windows

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.components = new System.ComponentModel.Container();
			this.controlPanel = new System.Windows.Forms.Panel();
			this.smoothBox = new System.Windows.Forms.CheckBox();
			this.sizeBox = new System.Windows.Forms.TextBox();
			this.sizeLabel = new System.Windows.Forms.Label();
			this.speedLabel = new System.Windows.Forms.Label();
			this.solverLabel = new System.Windows.Forms.Label();
			this.speedBox = new System.Windows.Forms.TextBox();
			this.solveButton = new System.Windows.Forms.Button();
			this.generateButton = new System.Windows.Forms.Button();
			this.solveAlgorythmBox = new System.Windows.Forms.ComboBox();
			this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.controlPanel.SuspendLayout();
			this.contextMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// controlPanel
			// 
			this.controlPanel.Controls.Add(this.smoothBox);
			this.controlPanel.Controls.Add(this.sizeBox);
			this.controlPanel.Controls.Add(this.sizeLabel);
			this.controlPanel.Controls.Add(this.speedLabel);
			this.controlPanel.Controls.Add(this.solverLabel);
			this.controlPanel.Controls.Add(this.speedBox);
			this.controlPanel.Controls.Add(this.solveButton);
			this.controlPanel.Controls.Add(this.generateButton);
			this.controlPanel.Controls.Add(this.solveAlgorythmBox);
			this.controlPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.controlPanel.Location = new System.Drawing.Point(0, 0);
			this.controlPanel.Name = "controlPanel";
			this.controlPanel.Size = new System.Drawing.Size(701, 30);
			this.controlPanel.TabIndex = 0;
			// 
			// smoothBox
			// 
			this.smoothBox.AutoSize = true;
			this.smoothBox.Checked = true;
			this.smoothBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.smoothBox.Location = new System.Drawing.Point(623, 6);
			this.smoothBox.Name = "smoothBox";
			this.smoothBox.Size = new System.Drawing.Size(62, 17);
			this.smoothBox.TabIndex = 7;
			this.smoothBox.Text = "Smooth";
			this.smoothBox.UseVisualStyleBackColor = true;
			this.smoothBox.CheckedChanged += new System.EventHandler(this.SmoothBox_CheckedChanged);
			// 
			// sizeBox
			// 
			this.sizeBox.Location = new System.Drawing.Point(529, 5);
			this.sizeBox.Name = "sizeBox";
			this.sizeBox.Size = new System.Drawing.Size(88, 20);
			this.sizeBox.TabIndex = 6;
			// 
			// sizeLabel
			// 
			this.sizeLabel.AutoSize = true;
			this.sizeLabel.Location = new System.Drawing.Point(493, 8);
			this.sizeLabel.Name = "sizeLabel";
			this.sizeLabel.Size = new System.Drawing.Size(30, 13);
			this.sizeLabel.TabIndex = 5;
			this.sizeLabel.Text = "Size:";
			this.sizeLabel.Click += new System.EventHandler(this.SizeLabel_Click);
			// 
			// speedLabel
			// 
			this.speedLabel.AutoSize = true;
			this.speedLabel.Location = new System.Drawing.Point(378, 8);
			this.speedLabel.Name = "speedLabel";
			this.speedLabel.Size = new System.Drawing.Size(41, 13);
			this.speedLabel.TabIndex = 4;
			this.speedLabel.Text = "Speed:";
			// 
			// solverLabel
			// 
			this.solverLabel.AutoSize = true;
			this.solverLabel.Location = new System.Drawing.Point(165, 8);
			this.solverLabel.Name = "solverLabel";
			this.solverLabel.Size = new System.Drawing.Size(40, 13);
			this.solverLabel.TabIndex = 3;
			this.solverLabel.Text = "Solver:";
			// 
			// speedBox
			// 
			this.speedBox.Location = new System.Drawing.Point(425, 5);
			this.speedBox.Name = "speedBox";
			this.speedBox.Size = new System.Drawing.Size(62, 20);
			this.speedBox.TabIndex = 2;
			// 
			// solveButton
			// 
			this.solveButton.Location = new System.Drawing.Point(84, 3);
			this.solveButton.Name = "solveButton";
			this.solveButton.Size = new System.Drawing.Size(75, 23);
			this.solveButton.TabIndex = 1;
			this.solveButton.Text = "Solve";
			this.solveButton.UseVisualStyleBackColor = true;
			this.solveButton.Click += new System.EventHandler(this.SolveButton_Click);
			// 
			// generateButton
			// 
			this.generateButton.Location = new System.Drawing.Point(3, 3);
			this.generateButton.Name = "generateButton";
			this.generateButton.Size = new System.Drawing.Size(75, 23);
			this.generateButton.TabIndex = 0;
			this.generateButton.Text = "Generate";
			this.generateButton.UseVisualStyleBackColor = true;
			this.generateButton.Click += new System.EventHandler(this.GenerateButton_Click);
			// 
			// solveAlgorythmBox
			// 
			this.solveAlgorythmBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.solveAlgorythmBox.Location = new System.Drawing.Point(211, 4);
			this.solveAlgorythmBox.Name = "solveAlgorythmBox";
			this.solveAlgorythmBox.Size = new System.Drawing.Size(161, 21);
			this.solveAlgorythmBox.TabIndex = 0;
			// 
			// contextMenu
			// 
			this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.saveToolStripMenuItem });
			this.contextMenu.Name = "contextMenu";
			this.contextMenu.ShowImageMargin = false;
			this.contextMenu.Size = new System.Drawing.Size(74, 26);
			// 
			// saveToolStripMenuItem
			// 
			this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
			this.saveToolStripMenuItem.Size = new System.Drawing.Size(73, 22);
			this.saveToolStripMenuItem.Text = "Save";
			this.saveToolStripMenuItem.Click += new System.EventHandler(this.SaveToolStripMenuItem_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(701, 701);
			this.ContextMenuStrip = this.contextMenu;
			this.Controls.Add(this.controlPanel);
			this.DoubleBuffered = true;
			this.Name = "MainForm";
			this.Text = "Maze Solver";
			this.ResizeEnd += new System.EventHandler(this.MainForm_ResizeEnd);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.MazePanel_Paint);
			this.controlPanel.ResumeLayout(false);
			this.controlPanel.PerformLayout();
			this.contextMenu.ResumeLayout(false);
			this.ResumeLayout(false);
		}

		private System.Windows.Forms.CheckBox smoothBox;

		private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;

		private System.Windows.Forms.TextBox sizeBox;

		private System.Windows.Forms.Label sizeLabel;

		private System.Windows.Forms.Label solverLabel;

		private System.Windows.Forms.TextBox speedBox;
		private System.Windows.Forms.Label speedLabel;

		private System.Windows.Forms.Button generateButton;
		private System.Windows.Forms.Button solveButton;
		private System.Windows.Forms.ComboBox solveAlgorythmBox;

		private System.Windows.Forms.Panel controlPanel;
		private System.Windows.Forms.ContextMenuStrip contextMenu;

		#endregion
	}
}

