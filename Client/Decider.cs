﻿using SnakeBattle.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class Decider : IDecider
    {
        static uint counter = 0;

        public SnakeAction MakeMove(GameBoard gameBoard)
        {
            counter++;
            return ClosestApple(gameBoard);
        }

        private SnakeAction RandomMove(GameBoard gameBoard)
        {
            var random = new Random();
            var currentPosition = gameBoard.GetMyHead();
            do
            {
                var direction = (Direction)random.Next(Enum.GetValues(typeof(Direction)).Length - 1);
                BoardPoint nextPosition = currentPosition.Value;
                switch (direction)
                {
                    case Direction.Down:
                        nextPosition = currentPosition.Value.ShiftTop();
                        break;
                    case Direction.Left:
                        nextPosition = currentPosition.Value.ShiftRight();
                        break;
                    case Direction.Right:
                        nextPosition = currentPosition.Value.ShiftLeft();
                        break;
                    case Direction.Up:
                        nextPosition = currentPosition.Value.ShiftBottom();
                        break;
                    case Direction.Stop:
                        continue;
                        break;
                }
                if (gameBoard.IsBadThingAt(nextPosition))
                    continue;
                return new SnakeAction(false, direction);
            } while (false);
            var act = random.Next() % 2 == 0;

            return new SnakeAction(false, Direction.Right);
        }

        private SnakeAction ClosestApple(GameBoard gameBoard)
        {
            Graph graph = GraphCreator.Create(gameBoard);
            var myPosition = gameBoard.GetMyHead().Value;

            var weights = graph.Dijkstra(graph[myPosition]);
            var applePoints = gameBoard.GetApples();
            BoardPoint best = applePoints.FirstOrDefault();
            double bestWeight = double.MaxValue;
            foreach (var point in applePoints)
            {
                if (weights[graph[point].NodeNumber] < bestWeight)
                {
                    bestWeight = weights[graph[point].NodeNumber];
                    best = point;
                }
            }
            var bestNode = graph.Dijkstra(graph[myPosition], graph[best]);
            if (myPosition.ShiftBottom() == bestNode.BoardPoint)
                return new SnakeAction(false, Direction.Down);
            if (myPosition.ShiftLeft() == bestNode.BoardPoint)
                return new SnakeAction(false, Direction.Left);
            if (myPosition.ShiftRight() == bestNode.BoardPoint)
                return new SnakeAction(false, Direction.Right);
            if (myPosition.ShiftTop() == bestNode.BoardPoint)
                return new SnakeAction(false, Direction.Up);

            return new SnakeAction(false, Direction.Up);
        }
    }
}
