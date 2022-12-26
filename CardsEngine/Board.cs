namespace CardsEngine;

public class Board
{
    public List<int>[] hands { get; private set; }
    public MonsterCard[,] monsters { get; private set; }

    public Board(int playersAmount, Deck[] decks)
    {
        this.hands = new List<int>[playersAmount];
        this.monsters = new MonsterCard[playersAmount, 3];

        for (int playerIndex = 0; playerIndex < playersAmount; playerIndex++)
        { //poner los mosntruos en el campo
            for (int monsterIndex = 0; monsterIndex < 3; monsterIndex++)
            {
                monsters[playerIndex, monsterIndex] = decks[playerIndex].monsters[monsterIndex];
            }
            hands[playerIndex] = Engine.GetInitialHand(decks[playerIndex]);  // reparte la mano incicial
        }
    }

    public Board Clone(Deck[] decks)
    {
        Board newBoard = new Board(this.hands.Length, decks);

        List<int>[] newHands = new List<int>[this.hands.Length];
        MonsterCard[,] newMonsters = new MonsterCard[decks.Length, 3];

        for (int playerIndex = 0; playerIndex < this.hands.Length; playerIndex++)
        {
            newHands[playerIndex] = Engine.Clone<int>(this.hands[playerIndex].ToArray()).ToList();

            for (int monsterIndex = 0; monsterIndex < 3; monsterIndex++)
            {
                newMonsters[playerIndex, monsterIndex] = newBoard.monsters[playerIndex, monsterIndex].Clone();
            }
        }

        newBoard.hands = newHands;
        newBoard.monsters = newMonsters;

        return newBoard;
    }
}