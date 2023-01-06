namespace CardsEngine;

public class Fuego : Expression, INumericalExpression
{
    public Fuego(Position position) : base(position) { }

    public double Evaluate(Game game, MonsterCard playerMonster, MonsterCard targetMonster)
    {
        return (double)Card.Types.Fuego;
    }
}