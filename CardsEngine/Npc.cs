namespace CardsEngine;

public class Npc
{
    public int playerNumber { get; private set; }

    public static int[] BetterCombination = new int [10];

    public static int Life = int.MaxValue;

    public static int BetterTarget = -1;

    public Npc(int playerNumber)
    {
        this.playerNumber = playerNumber;
    }



    public void PlayTurn(Game game)
    {
        Potencia(game.board.hands[playerNumber].Count,game,playerNumber);

        for(int i = 0; i < BetterCombination.Length; i++)
        {
           // Console.WriteLine(BetterCombination[i]);

            game.PlayCard(game.board.hands[playerNumber][BetterCombination[i] - i], playerNumber, 0);
        }
    }


    public static void Potencia(int cardsQtt, Game game, int playerNumber)
    {
        for (int i = 0; i <= cardsQtt; i++)
        {
            Potencia(0, new int[i], 0, cardsQtt,game,playerNumber, game.energyPoints[playerNumber]);
        }
    }
    public static void Potencia(int pos, int[] CardsCombination, int auxPos, int cardsQtt, Game game, int playerNumber, int energyRemain )
    {
        if(pos == CardsCombination.Length)
        {
            CardsPermutation(CardsCombination,game,playerNumber);
        }
        else
        {
            for (int i = 0;i < cardsQtt;i++)
            {
                if (game.board.hands[playerNumber][i].activationEnergy < energyRemain)
                {
                    energyRemain -= game.board.hands[playerNumber][i].activationEnergy;
                    CardsCombination[pos] = i;
                    Potencia(pos + 1, CardsCombination, i + 1, cardsQtt, game, playerNumber, energyRemain);
                   // Console.WriteLine(game.board.hands[playerNumber].Count);
                    //Console.WriteLine(i);
                    energyRemain += game.board.hands[playerNumber][i].activationEnergy;
                }
            }
        }
    }

    public static void CardsPermutation(int[] CardsCombination, Game game, int playerNumber)
    {
        CardsPermutation(0, new int[CardsCombination.Length], new bool[CardsCombination.Length], CardsCombination,game, playerNumber);
    }
    public static void CardsPermutation(int pos, int[] permutation, bool[] get, int[] combination, Game game, int playerNumber)
    {
        if (pos == permutation.Length)
        {
            (int, int) RemainLifeTarget = EvaluateBetterplay(game, permutation, playerNumber);

            if(RemainLifeTarget.Item1 < Life)
            {
                Life = RemainLifeTarget.Item1;
                BetterTarget = RemainLifeTarget.Item2;
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
                    CardsPermutation(pos + 1, permutation, get, combination,game,playerNumber);
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
                Game NewGame = game.Clone() as Game;

                Console.WriteLine(NewGame.board.hands[1].Count);
                Console.WriteLine(game.board.hands[1].Count);


                MonsterCard[] AdversaryMonster = NewGame.decks[playerNumber].monsters;

                for(int j = 0; j < cards.Length; j++)
                {
                    NewGame.PlayCard(NewGame.board.hands[playerNumber][cards[j]], playerNumber, i);
                }

                int TotalLife = AdversaryMonster[0].lifePoints + AdversaryMonster[1].lifePoints + AdversaryMonster[2].lifePoints;
                
                if(TotalLife < globalLife)
                {
                    globalLife = TotalLife;
                    target = i;
                }
            }
        }


        return (globalLife, target);
    }

}