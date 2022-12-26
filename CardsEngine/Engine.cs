using System.Text.Json;

namespace CardsEngine;

public static class Engine
{
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
        string[] directory = Directory.GetFiles(Path.Join("..", Path.Join("..", Path.Join("..", Path.Join("..", Path.Join("Cartas", "MonsterCards"))))));
        foreach (string card in directory)
        {
            string json = File.ReadAllText(card);
            MonsterCard deserializedCard = JsonSerializer.Deserialize<MonsterCard>(json);
            MonsterCardsDataBase.Add(deserializedCard);
        }

        directory = Directory.GetFiles(Path.Join("..", Path.Join("..", Path.Join("..", Path.Join("..", Path.Join("Cartas", "PowerCards"))))));
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

    public static List<int> GetInitialHand(Deck deck)
    {
        List<int> hand = new List<int>();

        for (int i = 0; i < 3; i++)
        {
            hand.Add(Draw(deck));
        }

        return hand;
    }

    public static int Draw(Deck deck)
    {
        Random random = new Random();
        return random.Next(deck.powers.Length);
    }

    public static int PlayerWins(bool[] losers)
    {
        int stillPlaying = 0;
        int winner = -1;

        for (int i = 0; i < losers.Length; i++)
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
        for (int monster = 0; monster < 3; monster++)
        {
            if (monsters[player, monster].lifePoints != 0)
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

    //Funciones para las acciones del lenguaje
    public static void ActionDraw(Game game, int amount) 
    {
        for(int i = 0; i < amount; i++)
        {
            game.board.hands[game.currentPlayer].Add(Draw(game.decks[game.currentPlayer]));
        }
    }

    public static T[] Clone<T>(T[] array)
    {
        T[] newArray = new T[array.Length];

        for (int i = 0; i < array.Length; i++)
        {
            newArray[i] = array[i];
        }

        return newArray;
    }
}

