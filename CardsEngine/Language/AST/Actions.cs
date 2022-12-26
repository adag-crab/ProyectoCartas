namespace CardsEngine;

public class Actions : ASTNode
{
    public List<IActionExpression> expressions;
    public Actions(Position position) : base(position)
    {
        expressions = new List<IActionExpression>();
    }
    public void Activate()
    {
        
    }
}