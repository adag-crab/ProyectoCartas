namespace CardsEngine;

public class Tierra : Expression, INumericalExpression
{
    public Tierra(Position position) : base(position) { }

    public double Evaluate(Game game, MonsterCard playerMonster, MonsterCard targetMonster)
    {
        return (double)Card.Types.Tierra;
    }
}