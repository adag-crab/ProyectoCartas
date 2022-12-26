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
        PowerCard[] newPowers = new PowerCard[this.powers.Length];
        Dictionary<PowerCard, int> newAsociation = new Dictionary<PowerCard, int>(); //esta mal

        for (int monsterIndex = 0; monsterIndex < this.monsters.Length; monsterIndex++)
        {
            newMonsters[monsterIndex] = this.monsters[monsterIndex].Clone();
        }

        for(int powerIndex = 0; powerIndex < this.powers.Length; powerIndex++)
        {
            newPowers[powerIndex] = this.powers[powerIndex].Clone();
            newAsociation.Add(newPowers[powerIndex], this.associations[this.powers[powerIndex]]);
        }


        return new Deck(newMonsters, newPowers, newAsociation);
    }    
}
