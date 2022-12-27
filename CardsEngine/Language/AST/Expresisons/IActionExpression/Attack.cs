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
        targetMonster.UpdateLifePoints(-1 * (int)(playerMonster.attackPoints * (1.0 + (targetMonster.WeaknessValue(playerMonster.type) * (1.0 / 10)))));
    }
}