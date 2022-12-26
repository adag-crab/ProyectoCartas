namespace CardsEngine;
public static class IDE 
{
    public static string StartIDE()
    {
        string code = "";
        Script script;
        List<Error> errors = null;

        do
        {
            Console.Clear();
            Console.WriteLine("IDE: Escribe el codigo de tu carta");
            if (errors != null)
            {
                Console.WriteLine("Codigo anterior: \n");
                Console.WriteLine(code);

                Console.WriteLine("Errores: \n");
                foreach (Error error in errors)
                {
                    Console.WriteLine("Error en: (línea " + error.pos.line + " columna: " + error.pos.column + ") mensaje: \"" + error.message + "\"");
                }
            }

            code = "";
            Console.WriteLine("Escriba el codigo de la carta");
            while (true)
            {
                string s = Console.ReadLine();

                if (s == "")
                    break;

                code += s + "\n";
            }
        }while (!CardCodeProcessor.ProcessCode(code, out script, out errors));
        
        return code;
    }
}
