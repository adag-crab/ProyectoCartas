using CardsEngine;
namespace ConsoleApp;

public static class DeckCreator
{
    public static Deck[] CreateAllDecks(int players, int npc)
    {
        Deck[] decks = new Deck[players + npc];

        for (int i = 0; i < players; i++)
        {
            Console.WriteLine("Creando Deck Jugador " + (i + 1));

            decks[i] = CreateDeck();
        }

        for (int i = players; i < players + npc; i++)
        {
            Console.WriteLine("Creando Deck Jugador " + (i + 1));

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

            int m = int.Parse(Console.ReadLine());

            
            monsterCards[i] = Engine.MonsterCardsDataBase[m-1];

            Console.Clear();
            Console.WriteLine("Elija sus 12 cartas");

            for (int j = 1; j <= Engine.PowerCardsDataBase.Count; j++)
            {
                Console.WriteLine(j + " Name: " + Engine.PowerCardsDataBase[j - 1].name + " Type: " + Engine.PowerCardsDataBase[j - 1].type.ToString());
            }


            for (int j = 0; j < 4; j++)
            {
                int p = int.Parse(Console.ReadLine());

                powerCards[i * 4 + j] = Engine.PowerCardsDataBase[p-1];
                asociations.Add(Engine.PowerCardsDataBase[p-1], i);
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
            monsterCards[i] = Engine.MonsterCardsDataBase[random.Next(0, Engine.MonsterCardsDataBase.Count)];

            for (int j = 0; j < monsterCards.Length; j++)
            {
                powerCards[i * 4 + j] = Engine.PowerCardsDataBase[random.Next(0, Engine.PowerCardsDataBase.Count)];
                asociations.Add(powerCards[i * 4 + j], i);
            }
        }

        return new Deck(monsterCards, powerCards, asociations);
    }
}