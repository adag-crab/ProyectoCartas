namespace CardsEngine;
class Tokenizer
{
    public List<Token> tokens { get; private set; }
    public List<Error> errors { get; private set; }

    Reader reader;

    public Tokenizer(Reader reader)
    {
        this.errors = new List<Error>();
        this.tokens = new List<Token>();
        this.reader = reader;
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
                    tokens.Add(new Token(tokenCode, pos, TokenType.keyword));
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
                        tokens.Add(new Token(tokenCode, pos, TokenType.symbol));
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
                        errors.Add(new Error(token.pos, "Expresión desconocida: " + token.tokenCode));
                    }
                }
            }
        }

        this.tokens.Add(new Token("EOF", reader.eof, TokenType.eof));
        
        errors.AddRange(reader.errors);
        
        return this.tokens;
    }
}

class Token
{
    public Position pos { get; private set; }
    public string tokenCode { get; private set; }
    public TokenType type { get; private set; }

    public Token(string tokenCode, Position pos, TokenType type)
    {
        this.tokenCode = tokenCode;
        this.pos = pos;
        this.type = type;
    }
}

static class TokenCodes
{
    //KeyWords
    public static string Conditions = "Conditions";
    public static string Actions = "Actions";

    //Variables
    public static string PlayerMonsterLife = "PlayerMonsterLife";
    public static string TargetMonsterLife = "TargetMonsterLife";

    //Actions
    public static string Attack = "Attack";
    public static string Draw = "Draw";
    public static string Poison = "Poison";

    //Comparisson symbols
    public static string Equal = "==";
    public static string BiggerThan = ">";
    public static string LowerThan = "<";
    public static string BiggerOrEqualThan = ">=";
    public static string LowerOrEqualThan = "<=";

    //Aritmetical symbols
    public static string Add = "+";
    public static string Sub = "-";
    public static string Div = "/";
    public static string Mul = "*";
    
    //Brackets
    public static string OpenBracket = "(";
    public static string ClosedBracket = ")";
    public static string OpenCurlyBraces = "{";
    public static string ClosedCurlyBraces = "}";

    public static List<string> GetKeyWords()
    {
        List<string> keyWords = new List<string>();

        keyWords.Add(Conditions);
        keyWords.Add(Actions);

        keyWords.Add(PlayerMonsterLife);
        keyWords.Add(TargetMonsterLife);

        keyWords.Add(Attack);
        keyWords.Add(Draw);
        keyWords.Add(Poison);

        return keyWords;
    }
    public static List<string> GetSymbols()
    {
        List<string> symbols = new List<string>();

        symbols.Add(Equal);
        symbols.Add(BiggerThan);
        symbols.Add(LowerThan);
        symbols.Add(BiggerOrEqualThan);
        symbols.Add(LowerOrEqualThan);

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

public class Position
{
    public int line;
    public int column;
    public Position(int line, int column)
    {
        this.line = line;
        this.column = column;
    }
}

public enum TokenType 
{ 
    symbol,
    number,
    keyword,
    eof,
    unknown
}
