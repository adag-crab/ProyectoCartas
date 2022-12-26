namespace CardsEngine;
public class Attack : Expression, IActionExpression
{
    public Attack(Position position) : base(position) { }
    public bool NeedsParameters()
    {
        return false;
    }
    public INumericalExpression Parameter { get; set; }
    public void Activate(Game game, MonsterCard playerMonster, MonsterCard targetMonster)
    {
        targetMonster.UpdateLifePoints(-1 * playerMonster.attackPoints);
    }
}