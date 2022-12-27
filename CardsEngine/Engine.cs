using System.Text.Json;

namespace CardsEngine;

public static class Engine
{
    public static List<MonsterCard> MonsterCardsDataBase = new List<MonsterCard>();
    public static List<PowerCard> PowerCardsDataBase = new List<PowerCard>();

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

    public static int Draw(Deck deck, int player = -1, Game game = null)
    {
        Random random = new Random();

        int randomCard = random.Next(deck.powers.Length);
        if (game != null)
        {
            while (game.board.monsters[player, game.decks[player].associations[game.decks[player].powers[randomCard]]].state == Card.States.Muerto)
            {
                randomCard = random.Next(deck.powers.Length);
            }
        }
        return randomCard;
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

    public static void DeleteDeadMonsterPowerCards(int player, Game game)
    {
        List<int> _auxHand = new List<int>();
        foreach (int PowerCardIndex in game.board.hands[player])
        {
            if (game.board.monsters[player, game.decks[player].associations[game.decks[player].powers[PowerCardIndex]]].state != Card.States.Muerto)
            {
                _auxHand.Add(PowerCardIndex);
            }
        }
        game.board.hands[player] = _auxHand;
    }
    public static void UpdateMonsterStates(Game game)
    {
        for(int i = 0; i < game.players.Length; i++)
        {
            for(int j = 0; j < 3; j++)
            {
                if(game.board.monsters[i,j].state == Card.States.Envenenado)
                {
                    game.board.monsters[i, j].UpdateLifePoints(-1 * ((int) (game.board.monsters[i, j].lifePoints * 3.0 / 100) < 1 ? 1 : (int)(game.board.monsters[i, j].lifePoints * 3.0 / 100)));
                }
            }
        }
    }

    public static void GameOver(int winner)
    {

    }


    //Funciones para las acciones del lenguaje
    public static void ActionDraw(Game game, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            game.board.hands[game.currentPlayer].Add(Draw(game.decks[game.currentPlayer], game.currentPlayer, game));
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

