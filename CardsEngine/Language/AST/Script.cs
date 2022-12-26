namespace CardsEngine;

public class Script : ASTNode
{
    public Conditions? conditions;
    public Actions actions;
    public Script(Position pos) : base(pos){}
}