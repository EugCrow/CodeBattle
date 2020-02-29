using SnakeBattle.Api;
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
            return RandomMove(gameBoard);
        }

        private SnakeAction RandomMove(GameBoard gameBoard)
        {

            var random = new Random();
            do
            {
                var direction = (Direction)random.Next(Enum.GetValues(typeof(Direction)).Length);
                var currentPosition = gameBoard.GetMyHead();
                BoardPoint nextPosition = currentPosition.Value;
                switch (direction)
                {
                    case Direction.Down:
                        nextPosition = currentPosition.Value.ShiftBottom();
                        break;
                    case Direction.Left:
                        nextPosition = currentPosition.Value.ShiftLeft();
                        break;
                    case Direction.Right:
                        nextPosition = currentPosition.Value.ShiftRight();
                        break;
                    case Direction.Up:
                        nextPosition = currentPosition.Value.ShiftTop();
                        break;
                    case Direction.Stop:
                        continue;
                        break;
                }
                if (gameBoard.IsBarrierAt(nextPosition))
                    continue;
                return new SnakeAction(false, direction);
            } while (false);
            var act = random.Next() % 2 == 0;

            return new SnakeAction(false, Direction.Right);
        }
    }
}
