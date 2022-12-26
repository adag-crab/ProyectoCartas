namespace CardsEngine;

public class Position
{
    public int line { get; private set; }
    public int column { get; private set; }

    public Position(int line, int column)
    {
        this.line = line;
        this.column = column;
    }
}