using System;
using SnakeBattle.Api;

namespace Client
{
    public class SnakeBattleClient : SnakeBattleBase
    {
        private IDecider decider;

        public SnakeBattleClient(string serverAddress) : base(serverAddress)
        {
        }

        protected override string DoMove(GameBoard gameBoard)
        {
            //Just print current state (gameBoard) to console
            Console.Clear();
            //Console.SetCursorPosition(0, 0);
            gameBoard.PrintBoard();

            var action = decider.MakeMove(gameBoard).ToString();
            Console.WriteLine(action);
            return action;
        }

        public void InitiateExit()
        {
            ShouldExit = true;
        }

        public void Run(IDecider decider)
        {
            Connect();
            this.decider = decider;
        }
    }
}
