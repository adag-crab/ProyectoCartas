/*
 * Terminar la parte vidual del creador de cartas
 * Imprimir en pantalla los types (probar toString)
 * Implmentar sistemas de tipos
 * Implementar id (final de la aslista)
 * implementar el ide
*/


using CardsEngine;
namespace ConsoleApp;

public static class CardCreator
{
    public static void CardMain()
    {
        Console.Clear();

        Console.WriteLine("1. Crear Mounstro \n2. Crear Carta \n3. Volver al Menu Principal");

        string option = Console.ReadLine();

        switch (option)
        {
            case "1":
                Console.Clear();
                MonsterCreator();
                break;
            case "2":
                Console.Clear();
                PowerCardCreator();
                break;
            case "3":
                Console.Clear();
                break;
            default:
                Console.Clear();
                Console.WriteLine("Opcion incorrecta vuelva a seleccionar");
                Console.ReadLine();
                CardMain();
                break;
        }

    }

    public static void MonsterCreator()
    {
        Console.WriteLine("Creando mounstro");

        Console.WriteLine("Cual sera el nombre del mounstro");
        string name = Console.ReadLine();

        Program.MessagePrinter messagePrinter = (game) =>
        {
            Console.Clear();
            Console.WriteLine("Cual sera el tipo");

            for (int i = 0; i < 5; i++) //Hay que modificar el valor segun la cantidad de elementos que tenga el enum
            {
                Console.WriteLine(i + 1 + ". " + (Card.Types)i);
            }
        };

        int opcion = Program.OptionValidator((1, 6), messagePrinter);

        messagePrinter = (game) =>
        {
            Console.Clear();
            Console.WriteLine("Cuales seran sus puntos de ataque");
        };
        int attackPoints = Program.OptionValidator((0, int.MaxValue), messagePrinter);
        messagePrinter = (game) =>
        {
            Console.Clear();
            Console.WriteLine("Cuales seran sus puntos de vida");
        };
        
        int lifePoints = Program.OptionValidator((0, int.MaxValue), messagePrinter);

        MonsterCard monsterCard = new MonsterCard(0, name, (Card.Types)(opcion - 1), "", "", Card.States.Normal, attackPoints, lifePoints);

        Engine.MonsterCardsDataBase.Add(monsterCard);
        Engine.SaveMonsterCard(monsterCard);

        Console.WriteLine(name + " Ha sido creado con exito");

        Console.WriteLine("1. Crear otro Mounstro \n2. Volver atras");
        string option = Console.ReadLine();
        switch (option)
        {
            case "1":
                Console.Clear();
                MonsterCreator();
                break;
            case "2":
                Console.Clear();
                CardMain();
                break;
            default:
                Console.Clear();
                Console.WriteLine("Opcion incorrecta vuelva a seleccionar");
                string a = Console.ReadLine();
                break;
        }
    }

    public static void PowerCardCreator()
    {
        Console.WriteLine("Creando Carta de Poder");

        Console.WriteLine("Cual sera el nombre de la carta");
        string name = Console.ReadLine();

        Console.Clear();
        Console.WriteLine("La descripcion publica de la carta");
        string publicDescription = Console.ReadLine();

        string code = IDE.StartIDE(); //Aqui se escribe y procesa el codigo de l acrta para lueo guararlo solo si esta bien
        Console.WriteLine(code);
        
        Program.MessagePrinter messagePrinter = (game) =>
        {
            Console.Clear();
            Console.WriteLine("Defina el costo de energia de esta carta");
        };
        int activationEnergy = Program.OptionValidator((0, int.MaxValue), messagePrinter);

        PowerCard powerCard = new PowerCard(0, name, publicDescription, code, "", activationEnergy);
        Console.WriteLine(powerCard.code);

        Engine.PowerCardsDataBase.Add(powerCard);
        Engine.SavePowerCard(powerCard);

        Console.WriteLine(name + " Ha sido creada con exito");
        Console.WriteLine("1. Crear otro Carta \n2. Volver atras");
        
        string option = Console.ReadLine();
        
        switch (option)
        {
            case "1":
                Console.Clear();
                PowerCardCreator();
                CardMain();
                break;
            case "2":
                Console.Clear();
                CardMain();
                break;
            default:
                Console.Clear();
                Console.WriteLine("Opcion incorrecta vuelva a seleccionar");
                string a = Console.ReadLine();
                break;
        }
    }
}