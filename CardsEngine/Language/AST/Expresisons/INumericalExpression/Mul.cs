namespace CardsEngine;

public class Mul : Expression, IAritmeticalExpression
{
    public INumericalExpression Left { get; set; }
    public INumericalExpression Right { get; set; }

    public Mul(Position pos) : base(pos) { }
    public double Evaluate(Game game, MonsterCard playerMonster, MonsterCard targetMonster)
    {
        return (double)(Left.Evaluate(game, playerMonster, targetMonster) * Right.Evaluate(game, playerMonster, targetMonster));
    }
}