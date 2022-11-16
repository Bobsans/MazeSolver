using System.Collections.Generic;

namespace MazeSolver; 

public class SimpleMazeSolver : MazeSolver {
    public SimpleMazeSolver(Maze maze) : base(maze) {}

    public override void Solve() {
        Stack<MazeCell> stack = new();
        for (int x = 0; x < Maze.Width; x++) {
            for (int y = 0; y < Maze.Height; y++) {
                stack.Push(Maze[x, y]);
                while (stack.Count > 0) {
                    ProcessCell(stack.Pop(), stack);
                    InfoChannel.Publish($"Solving [stack: {stack.Count}]");
                }
            }
        }

        InfoChannel.Publish("");
    }

    void ProcessCell(MazeCell cell, Stack<MazeCell> stack) {
        int blockedWays = 0;

        if (cell.WallTop || cell.Top.State == CellState.WRONG) {
            blockedWays += 1;
        }

        if (cell.WallRight || cell.Right.State == CellState.WRONG) {
            blockedWays += 1;
        }

        if (cell.WallBottom || cell.Bottom.State == CellState.WRONG) {
            blockedWays += 1;
        }

        if (cell.WallLeft || cell.Left.State == CellState.WRONG) {
            blockedWays += 1;
        }

        cell.State = CellState.POTENTIAL;

        switch (blockedWays) {
            case 4:
                cell.State = CellState.WRONG;
                break;
            case 3: {
                cell.State = CellState.WRONG;

                if (!cell.WallTop) {
                    stack.Push(cell.Top);
                }

                if (!cell.WallLeft) {
                    stack.Push(cell.Left);
                }

                if (!cell.WallBottom) {
                    stack.Push(cell.Bottom);
                }

                if (!cell.WallRight) {
                    stack.Push(cell.Right);
                }

                break;
            }
            default:
                cell.State = CellState.RIGHT;
                break;
        }

        OnCellProcessed(cell);
    }
}
