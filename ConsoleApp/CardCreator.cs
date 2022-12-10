using CardsEngine;
namespace ConsoleApp;

public static class CardCreator
{
    
    public static void CardMain()
    {
        Console.Clear();

        Console.WriteLine("1. Crear Mounstro \n2. Crear Carta \n3. Volver al Menu Principal ");

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
                Console.WriteLine("esa opcion es un mojon");
                string a = Console.ReadLine();
                break;
        }

    }

    public static void MonsterCreator()
    {

        Console.WriteLine("Creando mounstro");

        Console.WriteLine("Cual sera el nombre del mounstro");
        string name = Console.ReadLine();
        Console.WriteLine("Cual sera el tipo");

        //imprimir opciones de enum

        string type = Console.ReadLine();
        Console.WriteLine("Cuales seran sus puntos de ataque");
        int attackPoints = int.Parse(Console.ReadLine());
        Console.WriteLine("Cuales seran sus puntos de vida");
        int lifePoints = int.Parse(Console.ReadLine());

        MonsterCard monsterCard = new MonsterCard(0, name, Engine.Types.planta, "", "", "", "Normal", attackPoints, lifePoints);

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
                Console.WriteLine("esa opcion es un mojon");
                string a = Console.ReadLine();
                break;
        }
    }

    public static void PowerCardCreator()
    {

        Console.WriteLine("Creando Carta de Poder");

        Console.WriteLine("Cual sera el nombre de la carta");
        string name = Console.ReadLine();
        Console.WriteLine("Cual sera el tipo");

        //imprimir opciones de enum  
        //type distinto a mounstro

        string type = Console.ReadLine();
        Console.WriteLine("La descripcion publica de la carta");
        string publicDescription = Console.ReadLine();

        
        Console.WriteLine("Escriba el codigo de la carta");
        string programmerDescription = "";
        while (true)
        {
            string s = Console.ReadLine();

            if (s == "")
                break;

            programmerDescription += s+"\n";
        }

        Console.WriteLine("Defina el costo de energia de esta carta");
        int activationEnergy = int.Parse(Console.ReadLine());

        PowerCard powerCard = new PowerCard(0, name, Engine.Types.enano, publicDescription,programmerDescription, "", activationEnergy);

        Engine.PowerCardsDataBase.Add(powerCard);
        Engine.SavePowerCard(powerCard);

        Console.WriteLine(name + "Ha sido creada con exito");
        Console.WriteLine("1. Crear otro Carta \n2. Volver atras");
        string option = Console.ReadLine();
        switch (option)
        {
            case "1":
                Console.Clear();
                PowerCardCreator();
                CardMain();
                break;
            default:
                Console.Clear();
                Console.WriteLine("esa opcion es un mojon");
                string a = Console.ReadLine();
                break;
        }

    }


}