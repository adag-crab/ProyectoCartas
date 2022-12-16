using CardsEngine;
namespace ConsoleApp;

public static class DeckCreator
{
    public static Deck[] CreateAllDecks(bool[] players)
    {
        Deck[] decks = new Deck[players.Length];

        for (int i = 0; i < players.Length; i++)
        {
            Console.WriteLine("Creando Deck Jugador " + (i + 1));

            if(players[i])
            decks[i] = CreateDeck();
            else
            decks[i] = CreateNpcDeck();
        }

        return decks;
    }


    public static Deck CreateDeck()
    {
        MonsterCard[] monsterCards = new MonsterCard[3];
        PowerCard[] powerCards = new PowerCard[12];
        Dictionary<PowerCard, int> asociations = new Dictionary<PowerCard, int>();

        for (int i = 0; i < monsterCards.Length; i++)
        {
            Console.Clear();
            Console.WriteLine("Elija su monstruo " + (i + 1));

            for (int j = 1; j <= Engine.MonsterCardsDataBase.Count; j++)
            {
                Console.WriteLine(j + " Name: " + Engine.MonsterCardsDataBase[j - 1].name + " Type: " + Engine.MonsterCardsDataBase[j - 1].type.ToString() + " Atk: " + Engine.MonsterCardsDataBase[j - 1].attackPoints + " HP: " + Engine.MonsterCardsDataBase[j - 1].lifePoints);
            }

            int m = int.Parse(Console.ReadLine());//arreglar aqui

            
            monsterCards[i] = Engine.MonsterCardsDataBase[m-1].Clone();

            Console.Clear();
            Console.WriteLine("Elija sus 12 cartas");

            for (int j = 1; j <= Engine.PowerCardsDataBase.Count; j++)
            {
                Console.WriteLine(j + " Name: " + Engine.PowerCardsDataBase[j - 1].name + " Type: " + Engine.PowerCardsDataBase[j - 1].type.ToString());
            }


            for (int j = 0; j < 4; j++)
            {
                int p = int.Parse(Console.ReadLine());

                powerCards[i * 4 + j] = Engine.PowerCardsDataBase[p-1].Clone();
                asociations.Add(powerCards[i * 4 + j], i);
            }
        }

        return new Deck(monsterCards, powerCards, asociations);
    }
    public static Deck CreateNpcDeck()
    {
        MonsterCard[] monsterCards = new MonsterCard[3];
        PowerCard[] powerCards = new PowerCard[12];
        Dictionary<PowerCard, int> asociations = new Dictionary<PowerCard, int>();


        Random random = new Random();

        for (int i = 0; i < monsterCards.Length; i++)
        {
            monsterCards[i] = Engine.MonsterCardsDataBase[random.Next(0, Engine.MonsterCardsDataBase.Count)].Clone();

            for (int j = 0; j < monsterCards.Length; j++)
            {
                powerCards[i * 4 + j] = Engine.PowerCardsDataBase[random.Next(0, Engine.PowerCardsDataBase.Count)].Clone();
                asociations.Add(powerCards[i * 4 + j], i);
            }
        }

        return new Deck(monsterCards, powerCards, asociations);
    }
}