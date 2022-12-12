namespace CardsEngine;

public class Expression : ASTNode
{
    public Expression(Position position) : base(position){}
}

public interface IConditionalExpression
{
    public bool Evaluate();
}
public interface IActionExpression
{
	public void Activate(MonsterCard playerMonster, MonsterCard targetMonster);
}	
