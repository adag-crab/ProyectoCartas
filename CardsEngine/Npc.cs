namespace CardsEngine;

public class NPC
{
    public int playerNumber { get; private set; }
    public int[] betterCombination = new int[0];
    public double qualityOfPLay = 0;
    public int betterTarget = -1;

    public NPC(int playerNumber)
    {
        this.playerNumber = playerNumber;
    }

    public (int[], int) PlayTurn(Game game)
    {
        this.betterCombination = new int[0];
        this.qualityOfPLay = 0;
        this.betterTarget = -1;

        Potencia(game.board.hands[playerNumber].Count, game, playerNumber);

        return (this.betterCombination, this.betterTarget);
    }

    public void Potencia(int cardsQtt, Game game, int playerNumber)
    {
        for (int i = 0; i <= cardsQtt; i++)
        {
            Potencia(0, new int[i], 0, cardsQtt, game, playerNumber, game.energyPoints[playerNumber]);
        }
    }

    public void Potencia(int pos, int[] cardsCombination, int auxPos, int cardsQtt, Game game, int playerNumber, int energyRemain)
    {
        if (pos == cardsCombination.Length)
        {
            CardsPermutation(cardsCombination, game, playerNumber);
        }
        else
        {
            for (int i = auxPos; i < cardsQtt; i++)
            {
                int activationEnergy = game.decks[playerNumber].powers[game.board.hands[playerNumber][i]].activationEnergy;
                if (activationEnergy < energyRemain)
                {
                    energyRemain -= activationEnergy;
                    cardsCombination[pos] = i;
                    Potencia(pos + 1, cardsCombination, i + 1, cardsQtt, game, playerNumber, energyRemain);
                    energyRemain += activationEnergy;
                }
            }
        }
    }

    public void CardsPermutation(int[] cardsCombination, Game game, int playerNumber)
    {
        CardsPermutation(0, new int[cardsCombination.Length], cardsCombination, new bool[cardsCombination.Length], game, playerNumber);
    }

    public void CardsPermutation(int pos, int[] permutation, int[] combination, bool[] get, Game game, int playerNumber)
    {
        if (pos == permutation.Length)
        {
            (double, int) QualityTarget = EvaluateBetterplay(game, permutation, playerNumber);

            if (QualityTarget.Item1 > qualityOfPLay)
            {
                qualityOfPLay = QualityTarget.Item1;
                betterTarget = QualityTarget.Item2;
                betterCombination = permutation;
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

    private (double, int) EvaluateBetterplay(Game game, int[] cards, int playerNumber)   // QUALITY, target
    {
        double quality = 0;
        int target = -1;

        for (int i = 0; i < game.players.Length; i++)
        {
            if (i != playerNumber)
            {
                Game newGame = game.Clone();
                /*
                 *Las estadisticas a analizar son: Vida del oponente, vida delm player, energia del player, cartas en la mano
                 */
                int[] statics = new int[4];  // estadisticas antes de hacer la jugada
                
                statics[0] = newGame.board.monsters[i, 0].lifePoints + newGame.board.monsters[i, 1].lifePoints + newGame.board.monsters[i, 2].lifePoints;
                statics[1] = newGame.board.monsters[playerNumber, 0].lifePoints + newGame.board.monsters[playerNumber, 1].lifePoints + newGame.board.monsters[playerNumber, 2].lifePoints;
                statics[2] = newGame.energyPoints[playerNumber];
                statics[3] = newGame.board.hands[playerNumber].Count;

                for (int j = 0; j < cards.Length; j++)
                {
                    newGame.PlayCard(cards[j], playerNumber, i);
                    
                    if (game.losers[i])
                        return (0, i);

                    for (int k = j + 1; k < cards.Length; k++)
                        if (cards[j] < cards[k]) cards[k]--;

                }

                int[] newStatics = new int[4]; // estadisticas despues de haver la jugada

                newStatics[0] = newGame.board.monsters[i, 0].lifePoints + newGame.board.monsters[i, 1].lifePoints + newGame.board.monsters[i, 2].lifePoints;
                newStatics[1] = newGame.board.monsters[playerNumber, 0].lifePoints + newGame.board.monsters[playerNumber, 1].lifePoints + newGame.board.monsters[playerNumber, 2].lifePoints;
                newStatics[2] = newGame.energyPoints[playerNumber];
                newStatics[3] = newGame.board.hands[playerNumber].Count;
                
                if (EvaluateStatics(statics,newStatics) > quality)
                {
                    quality = EvaluateStatics(statics,newStatics);
                    target = i;
                }
            }
        }

        return (quality, target);
    }

    private double EvaluateStatics(int[] statics, int[] newStatics)
    {
        double quality = 0; // maxima puntuacion sera 10 
        int[] points = { 5, 2, 2, 1 }; // 5 puntos para la vida del target, 2 para vida del player actual y para su enegia, 1 para cartas en la mano 

        quality += (double)(statics[0] - newStatics[0]) / statics[0] * points[0];
        
        for (int i = 1; i < statics.Length; i++)
        {
            if(newStatics[i] > statics[i])
            {
                quality += (double)(newStatics[i] - statics[i]) / statics[i] * points[i];
            }
        }

        return quality;
    }

}
