namespace CardsEngine;

public abstract class Card
{ 
	public int id { get; set; }    // para que tener id si el diccionario guarda el id
	public string name { get; set; }
	public string publicDescription { get; set; }

	public Card(int id, string name, string publicDescription)
    {
		this.id = id;
		this.name = name;
		this.publicDescription = publicDescription;
    }

	public enum Types
	{
		Planta,
		Agua,
		Fuego,
		Aire,
		Tierra
	};

	public enum States
	{
		Muerto,
		Normal,
		Envenenado
	};
}

public class PowerCard : Card
{
	public string code { get; private set; }
	public int activationEnergy { get; private set;}

	public PowerCard(int id, string name, string publicDescription, string code, int ActivationEnergy) : base(id, name, publicDescription)
	{
		this.code = code;
		this.activationEnergy = ActivationEnergy;
	}

	/*public PowerCard(PowerCard other): base(other.id, other.name, other.type, other.publicDescription, other.programmerDescription, other.imageURL)
    {
		this.activationEnergy = other.activationEnergy;
    */

	public PowerCard Clone()
    {
		return new PowerCard(
			this.id,
			this.name,
			this.publicDescription,
			this.code,
			this.activationEnergy);	
    }
}

public class MonsterCard : Card
{
	public Card.States state { get; set; }
	public Card.Types type { get; set; }
	public int attackPoints { get; private set; }
	public int lifePoints { get; private set; }
	public MonsterCard(int id, string name, Card.Types type, string publicDescription, Card.States state, int attackPoints, int lifePoints) : base(id, name, publicDescription)
	{
		this.state = state;
		this.type = type;
		this.attackPoints = attackPoints;
		this.lifePoints = lifePoints;
	}

	/*public MonsterCard(MonsterCard other) : base(other.id, other.name, other.type, other.publicDescription, other.programmerDescription, other.imageURL)
    {
		this.state = other.state;
		this.attackPoints = other.attackPoints;
		this.lifePoints = other.lifePoints;
	}*/
	public void UpdateLifePoints(int points)
	{
		lifePoints += points;
		if (lifePoints < 0) lifePoints = 0;
	}

	public int WeaknessValue(Card.Types otherType)
	{
		int[,] weaknessMatrix = { {  0,  1, -1, -1,  1},
								  { -1,  0,  1,  0,  1},
								  {  1, -1,  1,  0, -1},
								  {  1,  0,  0,  0,  1},
								  {  1,  0,  1,  0,  0} };
		return weaknessMatrix[(int) otherType, (int) this.type];
		
	}

	public MonsterCard Clone()
	{
		return new MonsterCard(
			this.id,
			this.name,
			this.type,
			this.publicDescription,
			this.state,
			this.attackPoints,
			this.lifePoints);
	}
}


