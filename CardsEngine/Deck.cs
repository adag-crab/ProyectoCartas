namespace CardsEngine;

public class Deck
{
    public MonsterCard[] monsters { get; private set; }
    public PowerCard[] powers { get; private set; }
    public Dictionary<PowerCard, int> associations { get; private set; }

    public Deck( MonsterCard[] monsters, PowerCard[] powers , Dictionary<PowerCard,int > associations)
    {
        this.monsters = monsters;
        this.powers = powers;
        this.associations = associations;
    }
}