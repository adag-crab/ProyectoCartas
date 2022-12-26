namespace CardsEngine;
public class Draw : Expression, IActionExpression
{
    public Draw(Position pos) : base(pos) { }
    public bool NeedsParameters()
    {
       return true;
    }
    public INumericalExpression Parameter { get; set; }
    public void Activate(Game game, MonsterCard playerMonster, MonsterCard targetMonster)
    {
        Engine.ActionDraw(game, (int) Parameter.Evaluate(game, playerMonster, targetMonster));
    }
}