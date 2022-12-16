namespace CardsEngine;

public interface IClonable
{
	public object Clone();
}
public abstract class Card
{ 
	public int id { get; set; }    // para que tener id si el diccionario guarda el id
	public string name { get; set; }
	public Engine.Types type { get; set; }
	public string publicDescription { get; set; }
	public string programmerDescription { get; set; }
	public string imageURL { get; set; }
	public Card(int id, string name, Engine.Types type, string publicDescription, string programmerDescription, string imageURL)
    {
		this.id = id;
		this.name = name;
		this.type = type;
		this.publicDescription = publicDescription;
		this.programmerDescription = programmerDescription;
		this.imageURL = imageURL;
    }
}
public class PowerCard : Card
{
	public int activationEnergy { get; private set;}
	public PowerCard(int id, string name, Engine.Types type, string publicDescription, string programmerDescription, string imageURL, int ActivationEnergy) : base(id, name, type, publicDescription, programmerDescription, imageURL)
	{
		this.activationEnergy = ActivationEnergy;
	}
	
	public PowerCard Clone()
    {
		return new PowerCard(
			this.id,
			this.name,
			this.type,
			this.publicDescription,
			this.programmerDescription,
			this.imageURL,
			this.activationEnergy);	
    }
}

public class MonsterCard : Card
{
	public string state { get; set; }
	public int attackPoints { get; private set; }
	public int lifePoints { get; private set; }
	public MonsterCard(int id, string name, Engine.Types type, string publicDescription, string programmerDescription, string imageURL, string state, int attackPoints, int lifePoints): base(id, name, type, publicDescription, programmerDescription, imageURL)
    {
		this.state = state;
		this.attackPoints = attackPoints;
		this.lifePoints = lifePoints;
    }
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
			this.programmerDescription,
			this.imageURL,
			this.state,
			this.attackPoints,
			this.lifePoints);
	}
}


