namespace CardsEngine;

//Modificar el orde n de los if del parser

class Parser
{
    int tokenIndex;
    List<Token> tokens;
    public List<Error> errors { get; private set; }

    public Parser(List<Token> tokens)
    {
        this.tokenIndex = 0;
        this.tokens = tokens;
        this.errors = new List<Error>();
    }
    public Script TryParseScript()
    {
        List<string> keyWords = TokenCodes.GetKeyWords();
        List<string> symbols = TokenCodes.GetSymbols();

        Script script = new Script(this.tokens[this.tokenIndex].pos);

        Conditions conditions = TryParseConditions();

        Actions actions = TryParseActions(keyWords);

        if (this.tokenIndex < this.tokens.Count && this.tokens[this.tokenIndex].tokenCode != "EOF")
        {
            this.errors.Add(new Error(this.tokens[this.tokenIndex].pos, "No se esperaba mas código"));
        }

        script.conditions = conditions;
        script.actions = actions;

        return errors.Count == 0 ? script : null;
    }

    Conditions TryParseConditions()
    {
        Conditions conditions = new Conditions(tokens[this.tokenIndex].pos);

        if (!CheckToken("Conditions"))
        {
            this.errors.Add(new Error(this.tokens[GetIndex()].pos, "Se esperaba el keyword: Conditions"));
        }
        else UpdateIndex();

        if (!CheckToken("{"))
        {
            this.errors.Add(new Error(this.tokens[GetIndex()].pos, "Se esperaba: {, despues de Conditions"));
        }
        else UpdateIndex();

        while (IsExpectedToken())
        {
            IConditionalExpression condition = TryParseCondition();

            if (condition != null && condition.Left != null && condition.Right != null) conditions.expressions.Add(condition);
            else
            {
                this.errors.Add(new Error(this.tokens[GetIndex()].pos, "No se pudo parsear una condicion valida"));
                UpdateIndex();
            }
        }

        if (!CheckToken("}"))
        {
            this.errors.Add(new Error(tokens[this.tokenIndex].pos, "Se esperaba: }, despues de Conditions"));
        }
        else UpdateIndex();

        return conditions;
    }

    Actions TryParseActions(List<string> keyWords)
    {
        Actions actions = new Actions(this.tokens[GetIndex()].pos);

        if (!CheckToken("Actions"))
        {
            this.errors.Add(new Error(this.tokens[GetIndex()].pos, "Se esperaba el keyword: Actions"));
        }

        while (!CheckToken("EOF") && !CheckToken("Actions"))
        {
            UpdateIndex();
        }

        UpdateIndex();

        if (!CheckToken("{"))
        {
            this.errors.Add(new Error(tokens[GetIndex()].pos, "Se esperaba: {, despues de Actions"));
        }
        else UpdateIndex();

        while (IsExpectedToken())//añadir mas posibles errores
        {
            IActionExpression action = TryParseAction(keyWords);

            if (action != null) actions.expressions.Add(action);

            UpdateIndex();
        }

        if (!CheckToken("}"))
        {
            this.errors.Add(new Error(this.tokens[GetIndex()].pos, "Se esperaba: }, despues de Actions"));
        }
        else UpdateIndex();

        return actions;
    }

    IConditionalExpression TryParseCondition()
    {
        IConditionalExpression condition = null;
        INumericalExpression? left = ParseExpression();

        if (left == null)
        {
            this.errors.Add(new Error(this.tokens[GetIndex()].pos, "No se pudo parsear una expresion aritmetica"));

            while (!CheckToken("EOF") && GetCondition(this.tokens[GetIndex()]) == null)
            {
                UpdateIndex();
            }
        }

        condition = GetCondition(this.tokens[GetIndex()]);

        if (condition != null) {
            UpdateIndex();
        }
        else this.errors.Add(new Error(this.tokens[GetIndex()].pos, "Se esperaba un operador de comparacion"));

        INumericalExpression? right = ParseExpression();
        
        if(right == null)
        {
            this.errors.Add(new Error(this.tokens[GetIndex()].pos, "No se pudo parsear una expresion aritmetica"));
        }

        condition.Left = left;
        condition.Right = right;

        return condition;
    }

    IActionExpression TryParseAction(List<string> keyWords)
    {
        IActionExpression? action = null;

        if (CheckToken("Conditions") ||
            CheckToken("Actions") ||
            !keyWords.Contains(this.tokens[GetIndex()].tokenCode)
           )
        {
            this.errors.Add(new Error(this.tokens[GetIndex()].pos, this.tokens[GetIndex()].tokenCode + " no es una acción valida"));
        }
        else
        {
            string actionTokenCode = this.tokens[GetIndex()].tokenCode; // aqui devuelo el error de arriba si sale null y ya
            action = GetAction(this.tokens[GetIndex()]);
            UpdateIndex();

            if (!CheckToken("("))
            {
                this.errors.Add(new Error(this.tokens[GetIndex()].pos, "Se esperaba: (, despues de " + actionTokenCode));
            }
            else UpdateIndex();
            if (action.NeedsParameters())
            {
                action.Parameter = ParseExpression();
                if(action.Parameter == null) this.errors.Add(new Error(this.tokens[GetIndex()].pos, "No se pudo parsear un expresion aritmetica"));
            }//verificar error aqui con lo del parentesis
            if (!CheckToken(")"))
            {
                this.errors.Add(new Error(this.tokens[GetIndex()].pos, "Se esperaba: ), despues de " + actionTokenCode));
            }
        }

        return action;
    }
    INumericalExpression GetVariable(Token VariableNameToken)
    {
        INumericalExpression variable = null;

        if (VariableNameToken.tokenCode == TokenCodes.PlayerMonsterLife)
        {
            variable = new PlayerMonsterLife(VariableNameToken.pos);
        }
        if (VariableNameToken.tokenCode == TokenCodes.TargetMonsterLife)
        {
            variable = new TargetMonsterLife(VariableNameToken.pos);
        }

        return variable;
    }
    IConditionalExpression GetCondition(Token ConditionToken)
    {
        IConditionalExpression condition = null;

        if (ConditionToken.tokenCode == "==")
        {
            condition = new Equal(ConditionToken.pos);
        }
        if (ConditionToken.tokenCode == ">=")
        {
            condition = new BiggerOrEqualThan(ConditionToken.pos);
        }
        if (ConditionToken.tokenCode == "<=")
        {
            condition = new LowerOrEqualThan(ConditionToken.pos);
        }
        if (ConditionToken.tokenCode == ">")
        {
            condition = new BiggerThan(ConditionToken.pos);
        }
        if (ConditionToken.tokenCode == "<")
        {
            condition = new LowerThan(ConditionToken.pos);
        }

        return condition;
    }

    IActionExpression GetAction(Token actionToken)
    {
        IActionExpression action = null;

        if (actionToken.tokenCode == "Attack")
        {
            action = new Attack(actionToken.pos);
        }
        if (actionToken.tokenCode == "Draw")
        {
            action = new Draw(actionToken.pos);
        }
        if(actionToken.tokenCode == "Poison")
        {
            action = new Poison(actionToken.pos);
        }
        if(actionToken.tokenCode == "Heal")
        {
            action = new Heal(actionToken.pos);
        }

        return action;
    }

    bool IsExpectedToken()
    {
        if (CheckToken("}") ||
            CheckToken("Actions") ||
            CheckToken("EOF")
            ) return false;

        return true;
    }

    bool CheckToken(string expectedTokenCode)
    {
        return this.tokenIndex < this.tokens.Count && this.tokens[this.tokenIndex].tokenCode == expectedTokenCode;
    }

    int GetIndex()
    {
        return this.tokenIndex >= this.tokens.Count ? this.tokens.Count - 1 : this.tokenIndex;
    }

    void UpdateIndex()
    {
        if (this.tokenIndex < this.tokens.Count - 1) this.tokenIndex++;
        if (this.tokenIndex >= this.tokens.Count) this.tokenIndex = this.tokens.Count - 1;
    }
    void SetIndexAt(int newIndex)
    {
        if (newIndex < this.tokens.Count)
        {
            this.tokenIndex = newIndex;
        }
    }

    #region AritmeticalExpressionParser

    INumericalExpression? ParseExpression()
    {
        return ParseExpressionLv1(null);
    }

    INumericalExpression? ParseExpressionLv1(INumericalExpression left)
    {
        INumericalExpression? _auxLeft = ParseExpressionLv2(left);
        return ParseExpressionLv1_(_auxLeft);
    }

    INumericalExpression? ParseExpressionLv1_(INumericalExpression left)
    {
        INumericalExpression? exp = ParseAdd(left);

        if (exp != null) return exp;

        exp = ParseSub(left);

        if (exp != null) return exp;

        return left;
    }

    INumericalExpression? ParseExpressionLv2(INumericalExpression left)
    {
        INumericalExpression? _auxLeft = ParseExpressionLv3(left);
        return ParseExpressionLv2_(_auxLeft);

    }

    INumericalExpression? ParseExpressionLv2_(INumericalExpression left)
    {
        INumericalExpression? exp = ParseMul(left);

        if (exp != null) return exp;

        exp = ParseDiv(left);

        if (exp != null) return exp;

        return left;
    }

    INumericalExpression? ParseExpressionLv3(INumericalExpression left)
    {
        INumericalExpression? exp = ParseNum();
        if (exp != null) return exp;

        exp = ParseVariable();
        if (exp != null) return exp;

        return null;
    }

    INumericalExpression? ParseAdd(INumericalExpression? left)
    {

        if (left == null || this.tokens[GetIndex()].tokenCode != TokenCodes.Add) return null;

        IAritmeticalExpression? add = new Add(this.tokens[GetIndex()].pos);

        add.Left = left;

        int _auxIndex = GetIndex();

        UpdateIndex();

        INumericalExpression? right = ParseExpressionLv2(null);

        if (right == null)
        {
            SetIndexAt(_auxIndex);
            return null;
        }

        add.Right = right;

        return ParseExpressionLv1_(add);
    }

    INumericalExpression? ParseSub(INumericalExpression? left)
    {
        if (left == null || this.tokens[GetIndex()].tokenCode != TokenCodes.Sub) return null;

        IAritmeticalExpression? sub = new Sub(this.tokens[GetIndex()].pos);

        sub.Left = left;

        int _auxIndex = GetIndex();

        UpdateIndex();

        INumericalExpression? right = ParseExpressionLv2(null);

        if (right == null)
        {
            SetIndexAt(_auxIndex);
            return null;
        }

        sub.Right = right;

        return ParseExpressionLv1_(sub);
    }

    INumericalExpression? ParseMul(INumericalExpression left)
    {
        if (left == null || this.tokens[GetIndex()].tokenCode != TokenCodes.Mul) return null;

        IAritmeticalExpression? mul = new Mul(this.tokens[GetIndex()].pos);

        mul.Left = left;

        int _auxIndex = GetIndex();

        UpdateIndex();

        INumericalExpression? right = ParseExpressionLv3(null);

        if (right == null)
        {
            SetIndexAt(_auxIndex);
            return null;
        }

        mul.Right = right;

        return ParseExpressionLv2_(mul);
    }

    INumericalExpression? ParseDiv(INumericalExpression left)
    {
        if (left == null || this.tokens[GetIndex()].tokenCode != TokenCodes.Div) return null;

        IAritmeticalExpression? div = new Div(this.tokens[GetIndex()].pos);

        div.Left = left;

        int _auxIndex = GetIndex();

        UpdateIndex();

        INumericalExpression? right = ParseExpressionLv3(null);

        if (right == null)
        {
            SetIndexAt(_auxIndex);
            return null;
        }

        div.Right = right;

        return ParseExpressionLv2_(div);
    }

    INumericalExpression? ParseNum()
    {
        if (this.tokens[GetIndex()].type == TokenType.number)
        {
            INumericalExpression num = new Num(this.tokens[GetIndex()].pos, double.Parse(this.tokens[GetIndex()].tokenCode));

            UpdateIndex();
            return num;
        }

        return null;
    }
    INumericalExpression? ParseVariable()
    {
        if (this.tokens[GetIndex()].type == TokenType.keyword)
        {
            INumericalExpression num = GetVariable(this.tokens[GetIndex()]);

            if (num == null) return null;

            UpdateIndex();
            return num;
        }

        return null;
    }

    #endregion

}