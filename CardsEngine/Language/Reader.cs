namespace CardsEngine;

class Reader
{
    string code;
    string[] codeByLines;
    int line;
    int column;
    public Reader(string code)
    {
        this.code = code;
        this.codeByLines = code.Split("\n");
        this.line = 0;
        this.column = 0;
    }

    public bool TryReadToken(string pattern, out Position pos)
    {
        pos = new Position(line, column);
        for(int i = this.column; i < this.column + pattern.Length; i++)
        {
            if(pattern[i - this.column] != codeByLines[this.line][i])
            {
                return false;
            }
        }
        return true;
    }
    public Token Read()
    {
        Position pos = new Position(line, column);
        string result = "";
        int _auxColumn = this.column;
        while(CanRead(this.column == _auxColumn))
        {
            result += codeByLines[this.line][_auxColumn];
            _auxColumn++;
        }

        return new Token(result, pos);
    }

    public Token ReadNumber(Position pos)
    {
        string result = "";
        int _auxColumn = pos.column;
        bool isFloat = false;

        if (char.IsDigit(codeByLines[pos.line][pos.column]))
        {
            result += codeByLines[pos.line][pos.column];
            _auxColumn++;

            while(CanReadNumber(new Position(pos.line, _auxColumn), isFloat)){
                result += codeByLines[pos.line][pos.column];
            }
        }
        return new Token(result, pos);
    }
    public bool CanRead(bool isFirstCharacter)
    {
        return (codeByLines[this.line][this.column] == '_' || isFirstCharacter ? char.IsLetter(codeByLines[this.line][this.column]) : char.IsLetterOrDigit(codeByLines[this.line][this.column])); 
    }
    public bool CanReadNumber(Position pos, bool isFirstDigit)
    {
        return char.IsDigit(codeByLines[pos.line][pos.column]);
    }
    public bool EOL(Position pos)
    {
        return (pos.column >= codeByLines[pos.line].Length);   
    }
    
    /*public bool TryReadConditions(int line, int column)
    {
        string pattern = "Conditions";
        for (int i = column; i < column + pattern.Length; i++)
        {
            if (pattern[i - column] != codeByLines[line][i])
            {
                return false;
            }
        }

        return true;
    }*/
}

class Tokenizer
{
    public List<Token> tokens { get; private set; }
    Reader reader;
    public Tokenizer(Reader reader)
    {
        this.reader = reader;
        tokens = new List<Token>();
    }

    public List<Token> Tokenize() {
        List<string> keyWords = TokenCodes.GetKeyWords();
        List<string> symbols = TokenCodes.GetSymbols();

        while (true)
        {
            bool read = false;
            foreach(string tokenCode in keyWords)
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
                foreach(string tokenCode in symbols)
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