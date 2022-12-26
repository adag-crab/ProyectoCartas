namespace CardsEngine;

public class Conditions : ASTNode
{
    public List<IConditionalExpression>? expressions { get; set; }
    public Conditions(Position position) : base(position)
    {
        expressions = new List<IConditionalExpression>();
    }

    public bool Evaluate()
    {
        bool result = true;

        foreach (IConditionalExpression expression in expressions) //Agregar saber cual condicion no se cumple y porq
        {
            result = result && expression.Evaluate();          
        }
        
        return result;
    }
}