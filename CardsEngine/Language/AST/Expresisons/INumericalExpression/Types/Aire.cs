namespace CardsEngine;

public class Aire : Expression, INumericalExpression
{
    public Aire(Position position) : base(position) { }

    public double Evaluate(Game game, MonsterCard playerMonster, MonsterCard targetMonster)
    {
        return (double)Card.Types.Aire;
    }
}