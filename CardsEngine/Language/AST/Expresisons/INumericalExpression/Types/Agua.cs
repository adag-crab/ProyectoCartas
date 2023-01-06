namespace CardsEngine;

public class Agua : Expression, INumericalExpression
{
    public Agua(Position position) : base(position) { }

    public double Evaluate(Game game, MonsterCard playerMonster, MonsterCard targetMonster)
    {
        return (double)Card.Types.Agua;
    }
}