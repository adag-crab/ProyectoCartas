namespace CardsEngine;
/*
 * Clase que controla el flujo del juego:
 *  El orden de los turnos
 *  EL estado del tablero
 */

public class Game
{
    public bool[] players { get; private set; } //true (real Players) False NPC
    public Deck[] decks { get; private set; }

    public int turn { get; private set; }
    public int currentPlayer { get; private set; }

    public int[] energyPoints { get; private set; }
    public bool[] losers { get; private set; }

    public Board board { get; private set; }
    public List<Npc> npcs { get; private set; }

    public Game(bool[] players, int energyPoints, Deck[] decks)
    { // inciar el juego 
        this.players = players;
        this.decks = decks;

        this.turn = 1;
        this.currentPlayer = -1;

        this.losers = new bool[players.Length];
        this.energyPoints = new int[players.Length]; // incializar los puntos de energia de cada juagdor 

        for (int playerIndex = 0; playerIndex < players.Length; playerIndex++) this.energyPoints[playerIndex] = energyPoints;

        this.board = new Board(players.Length, decks);
        this.npcs = new List<Npc>();

        for (int playerIndex = 0; playerIndex < players.Length; playerIndex++)
        {
            if (!players[playerIndex])
            {
                npcs.Add(new Npc(playerIndex));
            }
        }


    }

    public Game Clone()
    {
        Game newGame = new Game(this.players, 200, this.decks);


        Deck[] newDecks = new Deck[this.decks.Length];

        for (int i = 0; i < this.decks.Length; i++)
        {
            newDecks[i] = this.decks[i].Clone();
        }

        newGame.energyPoints = Engine.Clone<int>(this.energyPoints);
        newGame.losers = Engine.Clone<bool>(this.losers);
        newGame.npcs = Engine.Clone<Npc>(this.npcs.ToArray()).ToList<Npc>(); //no se si hay que clonar los npc

        newGame.turn = this.turn;
        newGame.SetPlayer(this.currentPlayer);
        newGame.board = this.board.Clone(decks);
        newGame.decks = newDecks;
        return newGame;
    }

    public bool PlayCard(int handIndex, int player, int targetPlayer) // Annadir el hand index para eliminarla para no eliminar el primero que aparezca
    {


        PowerCard power = this.decks[player].powers[this.board.hands[player][handIndex]];

        // Seleccionar al monstruo al que se ataca pues siempre se ataca al primero de la foramcion del enemigo

        int targetMonster = 0;

        while (Engine.MonsterDied(targetPlayer, targetMonster, board.monsters) && targetMonster < 2)
        {
            targetMonster++;
        }

        // Interpretacion del codigo de la carta

        if (!CardCodeProcessor.ProcessCard(this, power, this.board.monsters[player, this.decks[player].associations[power]], this.board.monsters[targetPlayer, targetMonster]))
        {
            return false;
        }
        else
        {
            // Comprobar si el monstruo se murio

            if (Engine.MonsterDied(targetPlayer, targetMonster, board.monsters))
            {
                board.monsters[targetPlayer, targetMonster].state = "Muerto";
            }

            UpdateEnergy(-1 * power.activationEnergy, player);
            board.hands[player].RemoveAt(handIndex);
        }

        return true;
    }

    public void UpdateTurn()
    {
        this.turn++;
        this.currentPlayer = -1;
    }

    public bool NextPlayer()
    {
        this.currentPlayer++;
        return this.currentPlayer < this.players.Length;
    }

    public void SetPlayer(int newPlayerIndex)
    {
        if(newPlayerIndex < this.players.Length && newPlayerIndex >= 0) this.currentPlayer = newPlayerIndex;
    }
    public void TurnDraw()
    {
        board.hands[currentPlayer].Add(Engine.Draw(decks[currentPlayer]));
        board.hands[currentPlayer].Add(Engine.Draw(decks[currentPlayer]));
    }

    public void UpdateEnergy(int amount, int player)
    {
        energyPoints[player] += amount;
    }

    public void UpdateLosers(int player)
    {
        losers[player] = true;
    }

    public bool CanPlay(PowerCard cardToPlay, int player)
    {
        return cardToPlay.activationEnergy < this.energyPoints[player];
    }
}