namespace CardsEngine;
public class Purify : Expression, IActionExpression
{
    public Purify(Position position) : base(position) { }
    public bool NeedsParameters()
    {
        return false;
    }
    public INumericalExpression Parameter { get; set; }
    public void Activate(Game game, MonsterCard playerMonster, MonsterCard targetMonster)
    {
        playerMonster.state = Card.States.Normal;
    }
}