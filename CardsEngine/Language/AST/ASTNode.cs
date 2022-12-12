namespace CardsEngine;

public abstract class ASTNode
{
    public Position position { get; private set; }
    public ASTNode(Position position)
    {
        this. position = position;
    }
}
