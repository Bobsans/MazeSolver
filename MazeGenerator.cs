using System;
using System.Collections.Generic;
using System.Linq;

namespace MazeSolver;

public class MazeGenerator {
    readonly int _width;
    readonly int _height;
    readonly Random _random;

    public Maze Maze { get; }

    public event Action<MazeCell> CellProcessed;

    public MazeGenerator(int width, int height) {
        _width = width == 0 ? 1 : width;
        _height = height == 0 ? 1 : height;
        Maze = new Maze(_width, _height);
        _random = new Random();

        for (int x = 0; x < _width; x++) {
            for (int y = 0; y < _height; y++) {
                Maze[x, y] = new MazeCell(x, y, Maze);
            }
        }
    }

    public void Generate() {
        Stack<(MazeCell, Direction)> stack = new();
        stack.Push((Maze[0, 0], Direction.START));

        while (stack.Count > 0) {
            InfoChannel.Publish($"Generating [stack: {stack.Count}]");
            (MazeCell cell, Direction direction) = stack.Pop();
            VisitCell(cell, direction, stack);
        }

        InfoChannel.Publish("");
    }

    void VisitCell(MazeCell cell, Direction move, Stack<(MazeCell, Direction)> stack) {
        List<Direction> availableMoves = new();


        if (!cell.Top.IsFake && !cell.Top.IsVisited) {
            availableMoves.Add(Direction.UP);
        } else if (!cell.IsVisited && move != Direction.DOWN) {
            cell.WallTop = cell.Top.WallBottom = true;
        }

        if (!cell.Left.IsFake && !cell.Left.IsVisited) {
            availableMoves.Add(Direction.LEFT);
        } else if (!cell.IsVisited && move != Direction.RIGHT) {
            cell.WallLeft = cell.Left.WallRight = true;
        }

        if (!cell.Bottom.IsFake && !cell.Bottom.IsVisited) {
            availableMoves.Add(Direction.DOWN);
        } else if (!cell.IsVisited && move != Direction.UP) {
            cell.WallBottom = cell.Bottom.WallTop = true;
        }

        if (!cell.Right.IsFake && !cell.Right.IsVisited) {
            availableMoves.Add(Direction.RIGHT);
        } else if (!cell.IsVisited && move != Direction.LEFT) {
            cell.WallRight = cell.Right.WallLeft = true;
        }

        cell.IsVisited = true;

        if (cell.X == 0 && cell.Y == 0) {
            cell.IsStart = true;
            cell.WallTop = false;
        }

        if (cell.X == _width - 1 && cell.Y == _height - 1) {
            cell.IsGoal = true;
            cell.WallBottom = false;
        }

        CellProcessed?.Invoke(cell);

        foreach (Direction direction in availableMoves.OrderBy(_ => _random.Next())) {
            switch (direction) {
                case Direction.START:
                    break;
                case Direction.UP:
                    if (!cell.Top.IsFake) {
                        stack.Push((cell.Top, Direction.UP));
                    }

                    break;
                case Direction.RIGHT:
                    if (!cell.Right.IsFake) {
                        stack.Push((cell.Right, Direction.RIGHT));
                    }

                    break;
                case Direction.DOWN:
                    if (!cell.Bottom.IsFake) {
                        stack.Push((cell.Bottom, Direction.DOWN));
                    }

                    break;
                case Direction.LEFT:
                    if (!cell.Left.IsFake) {
                        stack.Push((cell.Left, Direction.LEFT));
                    }

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
