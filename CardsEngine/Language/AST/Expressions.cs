namespace CardsEngine;

public class Expression : ASTNode
{
    public Expression(Position position) : base(position){}
}

public interface INumericalExpression
{
    public double Evaluate(Game game, MonsterCard playerMonster, MonsterCard targetMonster);
}

public interface IAritmeticalExpression : INumericalExpression
{
    INumericalExpression Left { get; set; }
    INumericalExpression Right { get; set; }
}

public interface IConditionalExpression
{
    INumericalExpression Left { get; set; }
    INumericalExpression Right { get; set; }
    public bool Evaluate(Game game, MonsterCard playerMonster, MonsterCard targetMonster);
}

public interface IActionExpression
{
    public bool NeedsParameters(); //para saber si voy a tener que parsear una expresion dentro de los parentesis
    public INumericalExpression Parameter { get; set; } //el resultao de evaluar la expresion en los parentesis
    public void Activate(Game game, MonsterCard playerMonster, MonsterCard targetMonster);// es generico en T porq a veces necesito que el metodo d evuelva algo
}	
