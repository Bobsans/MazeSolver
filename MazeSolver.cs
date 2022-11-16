using System;

namespace MazeSolver; 

public abstract class MazeSolver {
    protected Maze Maze { get; }

    public event Action<MazeCell> CellProcessed;

    protected MazeSolver(Maze maze) {
        Maze = maze;
    }

    protected void OnCellProcessed(MazeCell cell) => CellProcessed?.Invoke(cell);

    public abstract void Solve();
}
