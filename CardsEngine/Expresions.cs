namespace CardsEngine;

public interface IActionExpresion
{
	public void Activate(MonsterCard playerMonster, MonsterCard targetMonster);
}	

public class Attack: IActionExpresion
{
 	public void Activate(MonsterCard playerMonster, MonsterCard targetMonster)
    {
 		targetMonster.UpdateLifePoints(-1 * playerMonster.attackPoints);		
    }
}