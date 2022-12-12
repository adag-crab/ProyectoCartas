namespace CardsEngine;
public class Attack : Expression, IActionExpression
{
    public Attack(Position position) : base(position) { }
    public void Activate(MonsterCard playerMonster, MonsterCard targetMonster)
    {
        targetMonster.UpdateLifePoints(-1 * playerMonster.attackPoints);
    }
}