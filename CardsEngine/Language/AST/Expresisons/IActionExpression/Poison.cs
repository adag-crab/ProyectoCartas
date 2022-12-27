namespace CardsEngine;
public class Poison : Expression, IActionExpression
{
    public Poison(Position position) : base(position) { }
    public bool NeedsParameters()
    {
        return false;
    }
    public INumericalExpression Parameter { get; set; }
    public void Activate(Game game, MonsterCard playerMonster, MonsterCard targetMonster)
    {
        targetMonster.state = Card.States.Envenenado;
    }
}