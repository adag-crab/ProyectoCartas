using CardsEngine;
namespace ConsoleApp;

public class Program
{
    public static void Main()
    {
        Engine.LoadCards();

        while (true)
        {
            Deck[] decks = new Deck[2];

            for (int i = 0; i < decks.Length; i++)
            {
                Dictionary<PowerCard, int> asociations = new Dictionary<PowerCard, int>();
                MonsterCard[] a = new MonsterCard[3];
                PowerCard[] powerCards = new PowerCard[12];

                

                for (int j = 0; j < 3; j++)
                {
                    a[j] = Engine.MonsterCardsDataBase[j];

                    

                    for (int k = 0; k < 4; k++)
                    {
                        powerCards[j * 4 + k] = Engine.PowerCardsDataBase[j * 4 + k];
                        asociations.Add(Engine.PowerCardsDataBase[j * 4 + k], i);
                    }
                }

                decks[i] = new Deck(a,powerCards, asociations);

            }
            Console.Clear();
            Console.WriteLine("Seleccione una opcion:\n1. Inciar Duelo \n2. Crear Cartas\n3. Salir");
            string option = Console.ReadLine();
            switch (option)
            {
                case "1":
                    Console.Clear();
                    Console.WriteLine("Cuantos Jugadores Reales");
                    int players = int.Parse(Console.ReadLine());
                    Console.WriteLine("Cuantos Jugadores Npc");
                    int npc = int.Parse(Console.ReadLine());

                    StarNewGame(players + npc, decks /*DeckCreator.CreateAllDecks(players, npc)*/);
                    break;
                case "2":
                    CardCreator.CardMain();
                    Console.Clear();
                    break;
                case "3":
                    return;
                default:
                    Console.Clear();
                    Console.WriteLine("esa opcion es un mojon");
                    string a = Console.ReadLine();
                    break;
            }
        }
    }
    public static void StarNewGame(int playersAmount, Deck[] decks)
    {
        Game game = new Game(playersAmount, 200, decks);
        
        int winner = -1;
        while (winner == -1)
        {
            StartTurn(game);
            winner = Engine.PlayerWins(game.losers); // para saber si alguine gano me devuelve el indice del jugador que lo hizo sino devuelve -1
        }

        Engine.GameOver(winner);
    }
    public static void StartTurn(Game game)
    {
        for (int player = 0; player < game.playersAmount; player++)
        {
            if (!game.losers[player])
            {
                game.UpdateEnergy(3,player);
                game.TurnDraw(player);
                ShowBoard(game, player);

                Play(player, game);
                if (Engine.PlayerWins(game.losers) != -1)
                {
                    return;
                }
            }
        }
        game.UpdateTurn();
    }

    public static void Play(int player, Game game)
    {
        while (true) // Ver como va ha ser la parte visual y la logica de juego
        {
            Console.WriteLine("Selecione la carta");
            int option = int.Parse(Console.ReadLine());

            if (option == 0) break;

            PowerCard cardToPLay = game.board.hands[player][option-1];
            if (game.CanPlay(cardToPLay, player))
            {

                Console.WriteLine("A que jugador vas a atacar?");
                int targetPlayer = int.Parse(Console.ReadLine());
                if (player != targetPlayer)
                {
                    if (!game.losers[targetPlayer])
                    {
                        game.PlayCard(cardToPLay, player, targetPlayer);

                        if (Engine.PlayerLose(targetPlayer, game.board.monsters))
                        {
                            game.UpdateLosers(targetPlayer);
                            Console.WriteLine("El player: " + targetPlayer + " perdio");
                            Console.ReadLine();
                        }
                        if (Engine.PlayerWins(game.losers) != -1)
                        {
                            return;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Ese jugador perdio no lo puedes atacar");
                        Console.ReadLine();
                    }
                    ShowBoard(game, 0);
                }
                else
                {
                    Console.WriteLine("No te puedes atacar a ti mismo");
                }
            }
            else
            {
                Console.WriteLine("No tienes energia suficiente");
            }
        }
    }

    public static void ShowBoard(Game game, int player)
    {
        Console.Clear();
        Console.WriteLine("Energy Points: " + game.energyPoints[player] + "\tTurno: " + game.turn + "\tJugador: " + player);
        for (int j = 0; j < 3; j++)
        {
            for (int i = 0; i < game.playersAmount; i++)
            {
                Console.Write(game.board.monsters[i, j].name + " HP: " + game.board.monsters[i, j].lifePoints + " ATK: " + game.board.monsters[i, j].attackPoints +  "\t\t");
            }
            Console.WriteLine();
        }
        for(int i = 0; i < game.board.hands[player].Count; i++)
        {
            Console.WriteLine(i+1 + " " + game.board.hands[player][i].name);
        }
;
    }
}