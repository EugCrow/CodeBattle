using SnakeBattle.Api;

namespace Client
{
    public interface IDecider
    {
        SnakeAction MakeMove(GameBoard gameBoard);
    }
}