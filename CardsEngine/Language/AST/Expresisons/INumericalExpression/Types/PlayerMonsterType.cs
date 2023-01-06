namespace CardsEngine;

public class PlayerMonsterType : Expression, INumericalExpression
{
    public PlayerMonsterType(Position position) : base(position) { }

    public double Evaluate(Game game, MonsterCard playerMonster, MonsterCard targetMonster)
    {
        return (double) playerMonster.type;
    }
}