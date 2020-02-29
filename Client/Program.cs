using System;
using SnakeBattle.Api;

namespace Client
{
    class Program
    {
        const string SERVER_ADDRESS = "http://codingdojo2020.westeurope.cloudapp.azure.com/codenjoy-contest/board/player/j7koydkba973ovsryolc?code=5169101146561309444";

        static void Main(string[] args)
        {
            var client = new SnakeBattleClient(SERVER_ADDRESS);
            client.Run(DoRun);

            Console.ReadKey();
            client.InitiateExit();
        }

        static int counter = 0;

        private static SnakeAction DoRun(GameBoard gameBoard)
        {
            if (counter == 0)
                return new SnakeAction(false, Direction.Down);
            return new SnakeAction(false, Direction.Stop);
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