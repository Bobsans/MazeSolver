using System.Collections.Generic;

namespace MazeSolver;

public class SmartMazeSolver : MazeSolver {
    public SmartMazeSolver(Maze maze) : base(maze) {}

    public override void Solve() {
        Stack<(MazeCell, Direction)> stack = new();
        stack.Push((Maze[0, 0], Direction.START));

        while (stack.Count > 0) {
            InfoChannel.Publish($"Solving [stack: {stack.Count}]");
            (MazeCell cell, Direction direction) = stack.Peek();
            ProcessCell(cell, direction, stack);
        }

        InfoChannel.Publish("");
    }

    void ProcessCell(MazeCell cell, Direction move, Stack<(MazeCell, Direction)> stack) {
        cell.State = CellState.POTENTIAL;
        
        if (cell.IsGoal) {
            while (stack.Count > 0) {
                (MazeCell _cell, Direction _) = stack.Pop();
                
                if (_cell.State == CellState.POTENTIAL) {
                    _cell.State = CellState.RIGHT;
                }

                OnCellProcessed(_cell);
                InfoChannel.Publish($"Solving [stack: {stack.Count}]");
            }

            InfoChannel.Publish("");
            return;
        }

        int ways = 4;
        List<(MazeCell, Direction)> toPush = new();
        
        if (move != Direction.DOWN) {
            if (cell.WallTop || cell.Top.State == CellState.WRONG) {
                ways--;
            } else if (!cell.Top.IsFake) {
                toPush.Add((cell.Top, Direction.UP));
            }
        }

        if (move != Direction.RIGHT) {
            if (cell.WallLeft || cell.Left.State == CellState.WRONG) {
                ways--;
            } else if (!cell.Left.IsFake) {
                toPush.Add((cell.Left, Direction.LEFT));
            }
        }

        if (move != Direction.UP) {
            if (cell.WallBottom || cell.Bottom.State == CellState.WRONG) {
                ways--;
            } else if (!cell.Bottom.IsFake) {
                toPush.Add((cell.Bottom, Direction.DOWN));
            }
        }

        if (move != Direction.LEFT) {
            if (cell.WallRight || cell.Right.State == CellState.WRONG) {
                ways--;
            } else if (!cell.Right.IsFake) {
                toPush.Add((cell.Right, Direction.RIGHT));
            }
        }

        if (ways < 2) {
            cell.State = CellState.WRONG;
            stack.Pop();
        }

        foreach ((MazeCell, Direction) tuple in toPush) {
            stack.Push(tuple);
        }

        OnCellProcessed(cell);
    }
}
