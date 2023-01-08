namespace CardsEngine;

public class PlayerHandSize : Expression, INumericalExpression
{
    public PlayerHandSize(Position position) : base(position) { }

    public double Evaluate(Game game, MonsterCard playerMonster, MonsterCard targetMonster)
    {
        return (double) game.board.hands[game.currentPlayer].Count;
    }
}