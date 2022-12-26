using System.Text.Json;

namespace CardsEngine;


public class a
{
    public MonsterCard[] monsterCards { get; set; }
}

public static class Engine {

    public static List<MonsterCard> MonsterCardsDataBase = new List<MonsterCard>();
    public static List<PowerCard> PowerCardsDataBase = new List<PowerCard>();

    public enum Types
    {
        planta,
        elfo,
        enano,
        humano,
        mago
    };
    public enum States
    {
        muerto,
        envenenado,
        dormido,
        euforico,
        normal
    };


    public static void LoadCards()
    {
        string[] directory = Directory.GetFiles(Path.Join("..", Path.Join("..", Path.Join("..",  Path.Join("..",Path.Join("Cartas", "MonsterCards"))))));
        foreach(string card in directory)
        {
            string json = File.ReadAllText(card);
            MonsterCard deserializedCard = JsonSerializer.Deserialize<MonsterCard>(json);
            MonsterCardsDataBase.Add(deserializedCard);
        }

        directory = Directory.GetFiles(Path.Join("..", Path.Join("..", Path.Join("..",  Path.Join("..",Path.Join("Cartas", "PowerCards"))))));
        foreach (string card in directory)
        {

            string json = File.ReadAllText(card);
            PowerCard deserializedCard = JsonSerializer.Deserialize<PowerCard>(json);
            PowerCardsDataBase.Add(deserializedCard);
        }
    }

    public static void SaveMonsterCard(MonsterCard card)
    {
        string json = JsonSerializer.Serialize(card);
        string path = Path.Join("..", Path.Join("..", Path.Join("..", Path.Join("..", Path.Join("Cartas", Path.Join("MonsterCards", card.name + ".Json"))))));
        File.WriteAllText(path, json);
    }

    public static void SavePowerCard(PowerCard card)
    {
        string json = JsonSerializer.Serialize(card);
        string path = Path.Join("..", Path.Join("..", Path.Join("..", Path.Join("..", Path.Join("Cartas", Path.Join("PowerCards", card.name + ".Json"))))));
        File.WriteAllText(path, json);
    }

        /*public static void LoadCards()
        {
            string monsterCardsJson = File.ReadAllText(Path.Join("..", Path.Join("..", Path.Join("..", Path.Join("..",Path.Join("CardsEngine", "MonsterCards.json"))))));
            string powerCardsJson = File.ReadAllText(Path.Join("..", Path.Join("..", Path.Join("..", Path.Join("..", Path.Join("CardsEngine", "PowerCards.json"))))));


            a monsterCards = JsonSerializer.Deserialize<a>(monsterCardsJson);
            /// PowerCard[] powerCards = JsonSerializer.Deserialize<PowerCard[]>(powerCardsJson);   

            a aasd = (a)monsterCards;


            for(int i = 0; i < aasd.monsterCards.Length ; i++)
            {
                MonsterCardsDataBase.Add(aasd.monsterCards[i]);
            }
          /*  for(int i = 0; i < powerCards.Length; i++)
            {
                PowerCardsDataBase.Add(powerCards[i]);
            }
        }
        public static void UpdateMonsterCardsJson()
        {
            string monsterCardsJson = JsonSerializer.Serialize(MonsterCardsDataBase.ToArray());
            File.WriteAllText("../../../../CardsEngine/MonsterCards.json", monsterCardsJson);
            Console.WriteLine("Listo");
            Console.ReadLine();
        }
        public static void UpdatePowerCardsJson()
        {
            string powerCardsJson = JsonSerializer.Serialize(PowerCardsDataBase.ToArray());
            File.WriteAllText(Path.Join("..", "PowerCards.json"), powerCardsJson);
        }*/
        public static List<PowerCard> GetInitialHand(Deck deck)
    {
        List<PowerCard> hand = new List<PowerCard>();

        for(int i = 0; i < 3; i++)
        {
            hand.Add(Draw(deck));
        }

        return hand;
    }
    public static PowerCard Draw(Deck deck)
    {
        Random random = new Random();
        return deck.powers[random.Next(deck.powers.Length)];
    }
    
    public static int PlayerWins(bool[] losers)
    {
        int stillPlaying = 0;
        int winner = -1;

        for(int i = 0; i < losers.Length; i++)
        {
            if (!losers[i])
            {
                winner = i;
                stillPlaying++;
            }
        }

        if (stillPlaying != 1) return -1;
        else return winner;
    }
    public static bool PlayerLose(int player, MonsterCard[,] monsters)
    {
        for(int monster = 0; monster < 3; monster++)
        {
            if(monsters[player, monster].lifePoints != 0)
            {
                return false;
            }
        }

        return true;
    } 
    public static bool MonsterDied(int targetPlayer, int targetMonster, MonsterCard[,] monsters)
    {
        if (monsters[targetPlayer, targetMonster].lifePoints <= 0) return true;
        return false;
    }
    public static void GameOver(int winner)
    {

    }
    /*public Attack(MonsterCard )
    {

    }*/
}

