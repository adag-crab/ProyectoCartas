namespace CardsEngine;

class Program
{
    public static void Main()
    {
        Deck[] decks = new Deck[2];
        Dictionary<Card, MonsterCard> asociations = new Dictionary<Card, MonsterCard>();
        MonsterCard[] monsterCards = new MonsterCard[3];
        Card[] cards = new Card[12];

        for(int i = 0; i < 12; i++)
           cards[i]= new Card(i, "poder"+i, Engine.Types.mago, "", "", "");

        for(int j = 0; j < 3; j++)
           monsterCards[j] = new MonsterCard(j, "Pedro picapiedras" + j, Engine.Types.humano, "", "", "", Engine.States.euforico, 110, 200);

        decks[0] = new Deck(monsterCards, cards, asociations);
        decks[1] = new Deck(monsterCards, cards, asociations);

        Game Start = new Game(2, 10, decks);

        for(int i = 0; i < 2; i++)
        {
            for(int j = 0; j < 3; j++)
            {
                Console.WriteLine(Start.board.monsters[i, j].Item1.id + " " + Start.board.monsters[i, j].Item1.name + " " + Start.board.monsters[i, j].Item2);
            }
            for(int j = 0; j < 5; j++)
            {
                Console.WriteLine(Start.board.hands[i][j].id + " " + Start.board.hands[i][j].name);
            }
        }
    }
}
