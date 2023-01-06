namespace CardsEngine;

public class Planta : Expression, INumericalExpression
{
    public Planta(Position position) : base(position) { }

    public double Evaluate(Game game, MonsterCard playerMonster, MonsterCard targetMonster)
    {
        return (double) Card.Types.Planta;
    }
}