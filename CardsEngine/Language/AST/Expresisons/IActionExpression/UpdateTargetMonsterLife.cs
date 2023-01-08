namespace CardsEngine;
public class UpdateTargetMonsterLife : Expression, IActionExpression
{
    public UpdateTargetMonsterLife(Position position) : base(position) { }
    public bool NeedsParameters()
    {
        return true;
    }
    public INumericalExpression Parameter { get; set; }
    public void Activate(Game game, MonsterCard playerMonster, MonsterCard targetMonster)
    {
        playerMonster.UpdateLifePoints((int)Parameter.Evaluate(game, playerMonster, targetMonster));
    }
}