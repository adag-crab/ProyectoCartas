namespace CardsEngine;

public abstract class Card
{ 
	public int id { get; set; }    // para que tener id si el diccionario guarda el id
	public string name { get; set; }
	public Engine.Types type { get; set; }
	public string publicDescription { get; set; }
	public string imageURL { get; set; }
	public Card(int id, string name, Engine.Types type, string publicDescription, string imageURL)
    {
		this.id = id;
		this.name = name;
		this.type = type;
		this.publicDescription = publicDescription;
		this.imageURL = imageURL;
    }
}

public class PowerCard : Card
{
	public string code { get; private set; }
	public int activationEnergy { get; private set;}

	public PowerCard(int id, string name, Engine.Types type, string publicDescription, string code, string imageURL, int ActivationEnergy) : base(id, name, type, publicDescription, imageURL)
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
			this.type,
			this.publicDescription,
			this.code,
			this.imageURL,
			this.activationEnergy);	
    }
}

public class MonsterCard : Card
{
	public string state { get; set; }
	public int attackPoints { get; private set; }
	public int lifePoints { get; private set; }
	public MonsterCard(int id, string name, Engine.Types type, string publicDescription, string imageURL, string state, int attackPoints, int lifePoints): base(id, name, type, publicDescription, imageURL)
    {
		this.state = state;
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
		if(lifePoints < 0) lifePoints = 0;
    }

	public MonsterCard Clone()
	{
		return new MonsterCard(
			this.id,
			this.name,
			this.type,
			this.publicDescription,
			this.imageURL,
			this.state,
			this.attackPoints,
			this.lifePoints);
	}
}


