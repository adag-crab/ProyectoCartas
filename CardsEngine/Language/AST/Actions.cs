namespace CardsEngine;

public class Actions : ASTNode
{
    public List<IActionExpression> expressions;
    public Actions(Position position) : base(position)
    {
        expressions = new List<IActionExpression>();
    }
    public void Activate(Game game, MonsterCard playerMonster, MonsterCard targetMonster)
    {
        foreach (IActionExpression expression in expressions) expression.Activate(game, playerMonster, targetMonster);
    }
}