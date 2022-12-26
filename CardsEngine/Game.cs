namespace CardsEngine;
/*
 * Clase que controla el flujo del juego:
 *  El orden de los turnos
 *  EL estado del tablero
 */

public class Game
{
    
    public Deck[] decks { get; private set; }
    public int playersAmount { get; private set; }
    public int turn { get; private set; }
    public int[] playersLife { get; private set; }
    public int[] energyPoints { get; private set; }
    public Board board { get; private set; }
    public bool[] losers { get; private set; }


    public Game(int playersAmount, int energyPoints, Deck[] decks)
    { // inciar el juego 
        this.playersAmount = playersAmount;
        this.turn = 1;
        this.decks = decks;

        this.energyPoints = new int[playersAmount]; // incializar los puntos de energia de cada juagdor 
        for (int i = 0; i < playersAmount; i++) this.energyPoints[i] = energyPoints;

        //this.playersLife = new int[playersAmount];
        //for (int i = 0; i < playersAmount; i++) this.playersLife[i] = playersLife;

        this.board = new Board(playersAmount, decks);
        losers = new bool[playersAmount];
    }
    public void PlayCard(PowerCard power, int player, int targetPlayer)
    {
        int targetMonster = 0;
        while(Engine.MonsterDied(targetPlayer, targetMonster, board.monsters))
        {
            targetMonster++;
        }

        Reader a = new Reader();
        a.Parser(power.programmerDescription, board.monsters[player, 0], board.monsters[targetPlayer, targetMonster]);

        if (Engine.MonsterDied(targetPlayer, targetMonster, board.monsters))
        {
            board.monsters[targetPlayer, targetMonster].state = "Muerto";
        }
        UpdateEnergy(-1 * power.activationEnergy, player);
        board.hands[player].Remove(power);
    }
    public void UpdateTurn()
    {
        turn++;
    }
    public void TurnDraw(int player)
    {
        board.hands[player].Add(Engine.Draw(decks[player]));
        board.hands[player].Add(Engine.Draw(decks[player]));
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

public class Board
{
    public List<PowerCard>[] hands { get; private set; }
    public MonsterCard[,] monsters { get; private set; }

    public Board(int playersAmount, Deck[] decks)
    {
        this.hands = new List<PowerCard>[playersAmount];
        this.monsters = new MonsterCard[playersAmount, 3];

        for (int playerIndex = 0; playerIndex < playersAmount; playerIndex++){ //poner los mosntruos en el campo
            for(int monsterIndex = 0; monsterIndex < 3; monsterIndex++) {
                monsters[playerIndex,monsterIndex] = decks[playerIndex].monsters[monsterIndex]; 
            }
            hands[playerIndex] = Engine.GetInitialHand(decks[playerIndex]);  // reparte la mano incicial
        }
    }
}