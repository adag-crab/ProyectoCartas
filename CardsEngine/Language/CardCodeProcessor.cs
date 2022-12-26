namespace CardsEngine;

public static class CardCodeProcessor
{
    public static bool ProcessCard(Game game, PowerCard power, MonsterCard playerMonster, MonsterCard targetMonster)
    {
        Script script;
        List<Error> errors;

        if (ProcessCode(power.code, out script, out errors))
        {
            if(script.conditions.Evaluate(game, playerMonster, targetMonster))
            {
                script.actions.Activate(game, playerMonster, targetMonster);
                return true;
            }
        }

        foreach (Error error in errors)
        {
            Console.WriteLine(error.message);
        }

        return false;
    }

    public static bool ProcessCode(string code, out Script script, out List<Error> errors) //Compilacion del codigo de la carta construye el ast y devuelve los errores en caso de existir
    {
        Reader reader = new Reader(code);
        Tokenizer tokenizer = new Tokenizer(reader);
        tokenizer.Tokenize();

        Parser parser = new Parser(tokenizer.tokens);
        script = parser.TryParseScript();

        errors = new List<Error>();

        if (tokenizer.errors.Count == 0 && parser.errors.Count == 0) return true;

        foreach (Error error in tokenizer.errors)
        {
            errors.Add(error);
        }
        foreach (Error error in parser.errors)
        {
            errors.Add(error);
        }

        return false;
    }
}
