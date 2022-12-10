namespace CardsEngine;

class Reader
{
    public void Parser(string code, MonsterCard PlayerMonster, MonsterCard TargetMonster)
    {
        char[] spliters = {'\n'};
        string[] parsedCode = code.Split(spliters);

        

        for(int i = 0; i < parsedCode.Length; i++)
            parsedCode[i] = parsedCode[i].Trim();

        List<string> effects = Effects(parsedCode);

        for(int i = 0; i < effects.Count; i++) {

            switch (effects[i])
            {
                case "Attack()":
                    IActionExpresion a = new Attack();
                    a.Activate(PlayerMonster, TargetMonster);
                    break;
            }
        }

    }
    
    



   

    //lista de efectos a ejecutar 
    
    public static List<string> Effects(string[] parsedcode)
    {
        List<string> effects = new List<string>();
        
        for(int i = 0; i < parsedcode.Length; i++)
        {
            if(parsedcode[i] == "Effects:")
            {
               for(int j = i+1; j < parsedcode.Length; j++)
               {
                    effects.Add(parsedcode[j]);
               }
                break;
            }
        }

        return effects;
    }

}

