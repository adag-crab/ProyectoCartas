namespace CardsEngine;
public class UpdateEnergy : Expression, IActionExpression
{
    public UpdateEnergy(Position pos) : base(pos) { }
    public bool NeedsParameters()
    {
        return true;
    }
    public INumericalExpression Parameter { get; set; }
    public void Activate(Game game, MonsterCard playerMonster, MonsterCard targetMonster)
    {
        game.UpdateEnergy((int)Parameter.Evaluate(game, playerMonster, targetMonster), game.currentPlayer);
    }
}