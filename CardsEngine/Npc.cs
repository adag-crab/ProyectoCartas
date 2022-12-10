namespace CardsEngine;

public class Npc
{
   public int playerNumber { get; private set; }
   
    public Npc (int playerNumber)
    {
        this.playerNumber = playerNumber;
    }


    public void PlayTurn(Game game)
    {
        while(game.board.hands[playerNumber].Count!=0)
        {
            PlayOneCard(game);
        }
    }

    public void PlayOneCard(Game game)
    {
      game.PlayCard(SelectCardToPlay(game), SelectTarget(game), 1);
    }

    public PowerCard SelectCardToPlay(Game game)
    {
      
     PowerCard card = game.board.hands[playerNumber].FirstOrDefault();
      
        return card;
    }

    public int SelectTarget(Game game)
    {
        int targetPlayer = -1;

        for (int i = 0; i < game.players.Length; i++)
        {
            if (i != playerNumber && !game.losers[i])
            {
                targetPlayer = i;
            }
        }

        return targetPlayer;
    }

}