namespace CardsEngine;
class Reader
{
    List<string> symbols = TokenCodes.GetSymbols();
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

        if (pattern.Length > codeByLines[this.line].Length - this.column) return false;

        for (int i = this.column; i < this.column + pattern.Length; i++)
        {
            if (pattern[i - this.column] != codeByLines[this.line][i])
            {
                return false;
            }
        }

        this.column += pattern.Length;
        CheckEOL();

        return true;
    }
    public Token TryReadNumber() ///agregar error de dos puntos en n mismo numero
    {
        Position pos = new Position(this.line, this.column);
        string result = "";
        bool isFloat = false;

        if (char.IsDigit(codeByLines[this.line][this.column]))
        {
            result += codeByLines[this.line][this.column];
            this.column++;

            while (!CheckEOL() && (char.IsDigit(codeByLines[this.line][this.column]) || codeByLines[this.line][this.column] == '.'))
            {
                if (codeByLines[this.line][this.column] == '.')
                {
                    if (!isFloat)
                    {
                        isFloat = true;
                    }
                    else
                    {
                        //Aqui va el error no puede tener dos comas
                        break;
                    }
                }
                result += codeByLines[this.line][this.column];
                this.column++;
            }
        }

        return (result != "") ? new Token(result, pos) : null;
    }
    public Token Read()
    {
        Position pos = new Position(line, column);
        string result = "";

        //while (CanRead(this.column == pos.column))
        while (!CheckEOL() && !this.symbols.Contains(codeByLines[this.line][this.column].ToString()))
        {
            result += codeByLines[this.line][this.column];
            this.column++;
        }

        if (this.column == pos.column) return null;

        return new Token(result, pos);
    }
    public bool CanReadIdentifier(bool isFirstCharacter)
    {
        return (codeByLines[this.line][this.column] == '_' || (isFirstCharacter ? char.IsLetter(codeByLines[this.line][this.column]) : char.IsLetterOrDigit(codeByLines[this.line][this.column])) || CheckEOL());
    }
    public bool CanReadNumber(Position pos, bool isFloat)
    {
        if (!isFloat && codeByLines[pos.line][pos.column] == '.')
        {

        }
        return char.IsDigit(codeByLines[pos.line][pos.column]) || (!isFloat && codeByLines[pos.line][pos.column] == '.');
    }
    public bool isWhiteSpace()
    {
        if (codeByLines[this.line][this.column] == ' ')
        {
            this.column++;
            CheckEOL();
            return true;
        }
        return false;
    }
    public bool CheckEOL()
    {
        if (this.column >= codeByLines[this.line].Length)
        {
            this.line++;
            this.column = 0;
            return true;
        }

        return false;
    }
    public bool CheckEOF()
    {
        return this.line >= codeByLines.Length;
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