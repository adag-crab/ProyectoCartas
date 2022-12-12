namespace CardsEngine;
class Tokenizer
{
    public List<Token> tokens { get; private set; }
    Reader reader;
    public Tokenizer(Reader reader)
    {
        this.reader = reader;
        tokens = new List<Token>();
    }

    public List<Token> Tokenize()
    {
        List<string> keyWords = TokenCodes.GetKeyWords();
        List<string> symbols = TokenCodes.GetSymbols();

        while (!reader.CheckEOF())
        {
            bool read = false;

            if (reader.isWhiteSpace())
            {
                continue;
            }

            foreach (string tokenCode in keyWords)
            {
                Position pos;
                if (reader.TryReadToken(tokenCode, out pos))
                {
                    tokens.Add(new Token(tokenCode, pos));
                    read = true;
                    break;
                }
            }
            if (!read)
            {
                foreach (string tokenCode in symbols)
                {
                    Position pos;
                    if (reader.TryReadToken(tokenCode, out pos))
                    {
                        tokens.Add(new Token(tokenCode, pos));
                        read = true;
                        break;
                    }
                }
            }
            if (!read)
            {
                Token token = reader.TryReadNumber();
                if (token != null)
                {
                    this.tokens.Add(token);
                    read = true;
                }
                else
                {
                    token = reader.Read();
                    if (token != null)
                    {
                        this.tokens.Add(token);
                        //agregar error de unknown
                    }
                }
            }
        }
        return this.tokens;
    }
}
class Token
{
    public Position pos { get; private set; }
    public string tokenCode { get; private set; }
    public Token(string tokenCode, Position pos)
    {
        this.tokenCode = tokenCode;
        this.pos = pos;
    }
}

static class TokenCodes
{
    public static string Conditions = "Conditions";
    public static string Actions = "Actions";
    public static string Attack = "Attack";

    public static string Add = "+";
    public static string Sub = "-";
    public static string Div = "/";
    public static string Mul = "*";
    public static string OpenBracket = "(";
    public static string ClosedBracket = ")";
    public static string OpenCurlyBraces = "{";
    public static string ClosedCurlyBraces = "}";

    public static List<string> GetKeyWords()
    {
        List<string> keyWords = new List<string>();

        keyWords.Add(Conditions);
        keyWords.Add(Actions);
        keyWords.Add(Attack);

        return keyWords;
    }
    public static List<string> GetSymbols()
    {
        List<string> symbols = new List<string>();

        symbols.Add(Add);
        symbols.Add(Sub);
        symbols.Add(Div);
        symbols.Add(Mul);
        symbols.Add(OpenBracket);
        symbols.Add(ClosedBracket);
        symbols.Add(OpenCurlyBraces);
        symbols.Add(ClosedCurlyBraces);

        return symbols;
    }
}

class Position
{
    public int line;
    public int column;
    public Position(int line, int column)
    {
        this.line = line;
        this.column = column;
    }
}