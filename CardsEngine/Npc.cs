namespace CardsEngine;

public class NPC
{
    public int playerNumber { get; private set; }

    public static int[] BetterCombination = new int[10]; //hacer una lista

    public static int Life = int.MaxValue;

    public static int BetterTarget = -1;

    public NPC(int playerNumber)
    {
        this.playerNumber = playerNumber;
    }

    public (int[], int) PlayTurn(Game game)
    {
        Potencia(game.board.hands[playerNumber].Count, game, playerNumber);

        return (BetterCombination, BetterTarget);
    }


    public static void Potencia(int cardsQtt, Game game, int playerNumber)
    {
        for (int i = 0; i <= cardsQtt; i++)
        {
            Potencia(0, new int[i], 0, cardsQtt, game, playerNumber, game.energyPoints[playerNumber]);
        }
    }
    public static void Potencia(int pos, int[] CardsCombination, int auxPos, int cardsQtt, Game game, int playerNumber, int energyRemain)
    {
        if (pos == CardsCombination.Length)
        {
            CardsPermutation(CardsCombination, game, playerNumber);
        }
        else
        {
            for (int i = auxPos; i < cardsQtt; i++)
            {
                int activationEnergy = game.decks[playerNumber].powers[game.board.hands[playerNumber][i]].activationEnergy;
                if (activationEnergy < energyRemain)
                {
                    energyRemain -= activationEnergy;
                    CardsCombination[pos] = i;
                    Potencia(pos + 1, CardsCombination, i + 1, cardsQtt, game, playerNumber, energyRemain);
                    energyRemain += activationEnergy;
                }
            }
        }
    }

    public static void CardsPermutation(int[] CardsCombination, Game game, int playerNumber)
    {
        CardsPermutation(0, new int[CardsCombination.Length], CardsCombination, new bool[CardsCombination.Length], game, playerNumber);
    }
    public static void CardsPermutation(int pos, int[] permutation, int[] combination, bool[] get, Game game, int playerNumber)
    {
        if (pos == permutation.Length)
        {
            (int, int) RemainLifeTarget = EvaluateBetterplay(game, permutation, playerNumber);

            if (RemainLifeTarget.Item1 < Life)
            {
                Life = RemainLifeTarget.Item1;
                BetterTarget = RemainLifeTarget.Item2;
                BetterCombination = permutation;
            }

        }
        else
        {
            for (int i = 0; i < permutation.Length; i++)
            {
                if (!get[i])
                {
                    get[i] = true;
                    permutation[pos] = combination[i];
                    CardsPermutation(pos + 1, permutation, combination, get, game, playerNumber);
                    get[i] = false;
                }
            }
        }
    }




    private static (int, int) EvaluateBetterplay(Game game, int[] cards, int playerNumber)   // vida restante, target
    {
        int globalLife = int.MaxValue;
        int target = -1;


        for (int i = 0; i < game.players.Length; i++)
        {
            if (i != playerNumber)
            {
                Game NewGame = game.Clone();
                
                for (int j = 0; j < cards.Length; j++)
                {
                    NewGame.PlayCard(cards[j], playerNumber, i);
                    
                    if (game.losers[i])
                        return (0, i);

                    for (int k = j + 1; k < cards.Length; k++)
                        if (cards[j] < cards[k]) cards[k]--;

                }

                int TotalLife = NewGame.board.monsters[i, 0].lifePoints + NewGame.board.monsters[i, 1].lifePoints + NewGame.board.monsters[i, 2].lifePoints;

                if (TotalLife < globalLife)
                {
                    globalLife = TotalLife;
                    target = i;
                }
            }
        }

        return (globalLife, target);
    }

}
