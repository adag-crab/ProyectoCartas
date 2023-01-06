namespace CardsEngine;

public class TargetMonsterType : Expression, INumericalExpression
{
    public TargetMonsterType(Position position) : base(position) { }

    public double Evaluate(Game game, MonsterCard playerMonster, MonsterCard targetMonster)
    {
        Console.ReadLine();
        return (double) targetMonster.type;
    }
}