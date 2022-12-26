namespace CardsEngine;

public class Num : Expression, INumericalExpression
{
    double Value;
    
    public Num(Position pos, double value) : base(pos) 
    { 
        this.Value = value;
    }

    public double Evaluate(Game game, MonsterCard playerMonster, MonsterCard targetMonster)
    {
        return Value;
    }
}