namespace CardsEngine;

public class TargetMonsterLife : Expression, INumericalExpression
{
    public TargetMonsterLife(Position position) : base(position) { }

    public double Evaluate(Game game, MonsterCard playerMonster, MonsterCard targetMonster)
    {
        return targetMonster.lifePoints;
    }
}