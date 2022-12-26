namespace CardsEngine;

public class Error
{
    public Position pos { get; private set; }
    public string message { get; private set; }

    public Error(Position pos, string message)
    {
        this.pos = pos;
        this.message = message;
    }
}