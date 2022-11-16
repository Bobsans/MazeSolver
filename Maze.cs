using System;

namespace MazeSolver;

public class Maze {
    MazeCell[,] Cells { get; }
    public int Width { get; }
    public int Height { get; }

    public MazeCell this[int x, int y] {
        get => Cells[x, y];
        set => Cells[x, y] = value;
    }

    public Maze(int width, int height) {
        Width = width;
        Height = height;
        Cells = new MazeCell[width, height];
    }

    public MazeCell Cell(int x, int y, MazeCell @default = null) {
        if (x < 0 || x > Width - 1 || y < 0 || y > Height - 1) {
            if (@default != null) {
                return @default;
            }

            throw new IndexOutOfRangeException($"Cell at coords {x}:{y} not found!");
        }

        return Cells[x, y];
    }
}
