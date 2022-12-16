namespace CardsEngine;

public class Deck
{
    public MonsterCard[] monsters { get; private set; }
    public PowerCard[] powers { get; private set; }
    public Dictionary<PowerCard, int> associations { get; private set; }

    public Deck(MonsterCard[] monsters, PowerCard[] powers , Dictionary<PowerCard,int > associations)
    {
        this.monsters = monsters;
        this.powers = powers;
        this.associations = associations;
    }

    public Deck Clone()
    {
        MonsterCard[] newMonsters = new MonsterCard[this.monsters.Length];

        for (int i = 0; i < monsters.Length; i++)
        {
            newMonsters[i] = this.monsters[i].Clone();
        }

        PowerCard[] newPowers = new PowerCard[this.powers.Length];
        Dictionary<PowerCard, int> newAsociation = new Dictionary<PowerCard, int>(); //esta mal

        for(int i =0; i<newPowers.Length; i++)
        {
            newPowers[i] = this.powers[i].Clone();


        }

        return new Deck(newMonsters, newPowers, newAsociation);
    }    

}
