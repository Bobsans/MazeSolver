namespace MazeSolver;

public class MazeCell {
    static MazeCell Fake { get; } = new(-1, -1, null) {
        IsVisited = true,
        WallTop = true,
        WallRight = true,
        WallBottom = true,
        WallLeft = true,
        State = CellState.FAKE
    };

    Maze Maze { get; }

    public int X { get; }
    public int Y { get; }

    public bool IsVisited;
    public bool WallTop;
    public bool WallBottom;
    public bool WallRight;
    public bool WallLeft;
    public bool IsStart = false;
    public bool IsGoal = false;
    public CellState State = CellState.EMPTY;

    public bool IsFake => State == CellState.FAKE;

    public MazeCell(int x, int y, Maze parent) {
        X = x;
        Y = y;
        Maze = parent;
    }

    public MazeCell Top => Maze.Cell(X, Y - 1, Fake);
    public MazeCell Bottom => Maze.Cell(X, Y + 1, Fake);
    public MazeCell Left => Maze.Cell(X - 1, Y, Fake);
    public MazeCell Right => Maze.Cell(X + 1, Y, Fake);
}

public enum CellState {
    EMPTY,
    POTENTIAL,
    RIGHT,
    WRONG,
    FAKE
}
