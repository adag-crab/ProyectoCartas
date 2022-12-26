namespace CardsEngine;

public class PlayerMonsterLife : Expression, INumericalExpression
{
    public PlayerMonsterLife(Position position) : base(position) { }

    public double Evaluate(Game game, MonsterCard playerMonster, MonsterCard targetMonster)
    {
        return playerMonster.lifePoints;
    }
}