namespace CardsEngine;

public class BiggerOrEqualThan : Expression, IConditionalExpression
{
    public INumericalExpression Left { get; set; }
    public INumericalExpression Right { get; set; }
    public BiggerOrEqualThan(Position pos) : base(pos) { }

    public bool Evaluate(Game game, MonsterCard playerMonster, MonsterCard targetMonster)
    {
        return Left.Evaluate(game, playerMonster, targetMonster) >= Right.Evaluate(game, playerMonster, targetMonster);
    }
} 