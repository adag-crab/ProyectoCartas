using CardsEngine;
namespace ConsoleApp;

public class Program
{
    public static void Main()
    {
        Engine.LoadCards();

        while (true)
        {
            Console.Clear();
            Console.WriteLine("Seleccione una opcion:\n1. Inciar Duelo \n2. Crear Cartas\n3. Salir");
            string option = Console.ReadLine();
            switch (option)
            {
                case "1":
                    MessagePrinter messagePrinter = (game) =>
                    {
                        Console.Clear();
                        Console.WriteLine("Cuantos Jugadores Reales");
                    };
                    int realplayers = Program.OptionValidator((0, int.MaxValue), messagePrinter);

                    messagePrinter = (game) =>
                    {
                        Console.Clear();
                        Console.WriteLine("Cuantos Jugadores Npc");
                    };
                    int npc = Program.OptionValidator((0, int.MaxValue), messagePrinter);

                    bool[] players = new bool[realplayers + npc];

                    for (int i = 0; i < realplayers; i++)
                    {
                        players[i] = true;
                    }

                    StarNewGame(players, DeckCreator.CreateAllDecks(players));
                    break;

                case "2":
                    CardCreator.CardMain();
                    Console.Clear();
                    break;

                case "3":
                    return;

                default:
                    Console.Clear();
                    Console.WriteLine("Opcion incorrecta vuelva a seleccionar");
                    string a = Console.ReadLine();
                    break;
            }
        }
    }

    public static void StarNewGame(bool[] players, Deck[] decks)
    {
        Game game = new Game(players, 200, decks);

        int winner = -1;
        while (winner == -1)
        {
            StartTurn(game);
            winner = Engine.PlayerWins(game.losers); // para saber si alguine gano me devuelve el indice del jugador que lo hizo sino devuelve -1
        }

        GameOver(winner);
    }

    public static void StartTurn(Game game)
    {
        Engine.UpdateMonsterStates(game);
        while (game.NextPlayer())
        {
            if (!game.losers[game.currentPlayer])
            {
                game.UpdateEnergy(50, game.currentPlayer);
                Engine.ActionDraw(game, 1);

                ShowBoard(game);
                Play(game);

                //Console.WriteLine(game.npcs.Count);
                //Console.WriteLine(player);

                if (Engine.PlayerWins(game.losers) != -1)
                {
                    return;
                }
            }
        }

        game.UpdateTurn();
    }

    public static void Play(Game game)
    {
        if (!game.players[game.currentPlayer])    //chequear si quien juega es un npc
        {
            int RealPlayers = game.players.Length - game.npcs.Count;

            (int[], int) npcCardsToPlay = game.npcs[game.currentPlayer - RealPlayers].PlayTurn(game);

            int[] cardsToPlay = npcCardsToPlay.Item1;
            int targetPlayer = npcCardsToPlay.Item2;

            for (int i = 0; i < cardsToPlay.Length; i++)
            {
                if (!game.losers[targetPlayer])
                {
                    
                    Console.WriteLine("Presione enter para la siguiente jugada");
                    Console.ReadLine();

                    if (!game.PlayCard(cardsToPlay[i], game.currentPlayer, targetPlayer))
                    {
                        Console.WriteLine("No se cumplieron las condiciones para activar el efecto de la carta");
                        Console.ReadLine();
                    }
                    ShowBoard(game);

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
            }
        }
        else
        {
            while (true) // Ver como va ha ser la parte visual y la logica de juego
            {
                MessagePrinter messagePrinter;
                int option;

                while (true)
                {
                    messagePrinter = (game) =>
                    {
                        ShowBoard(game);
                        Console.WriteLine("Selecione la carta");
                    };

                    option = Program.OptionValidator((0, game.board.hands[game.currentPlayer].Count), messagePrinter, game);
                    if (option == 0) break;

                    messagePrinter = (game) =>
                    {
                        ShowBoard(game);
                        Console.WriteLine("Descripcion de la carta:\n" + game.decks[game.currentPlayer].powers[game.board.hands[game.currentPlayer][option - 1]].publicDescription);
                        Console.WriteLine("1. Para jugarla 0. Para seleccionar otra");
                    };

                    int option2 = Program.OptionValidator((0, 1), messagePrinter, game);
                    if (option2 == 1) break;
                }

                if (option == 0) break;

                PowerCard cardToPLay = game.decks[game.currentPlayer].powers[game.board.hands[game.currentPlayer][option - 1]];
                if (game.CanPlay(cardToPLay, game.currentPlayer))
                {
                    messagePrinter = (game) =>
                    {
                        ShowBoard(game);
                        Console.WriteLine("A que jugador vas a atacar?");
                    };

                    int targetPlayer = Program.OptionValidator((0, game.players.Length - 1), messagePrinter, game);

                    if (game.currentPlayer != targetPlayer)
                    {
                        if (!game.losers[targetPlayer])
                        {
                            if (!game.PlayCard(option - 1, game.currentPlayer, targetPlayer))
                            {
                                Console.WriteLine("No se cumplieron las condiciones para activar el efecto de la carta");
                                Console.ReadLine();
                            }
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
                        ShowBoard(game);
                    }
                    else
                    {
                        Console.WriteLine("No te puedes atacar a ti mismo");
                        Console.ReadLine();
                    }
                }
                else
                {
                    Console.WriteLine("No tienes energia suficiente");
                }
            }
        }
    }

    public static void ShowBoard(Game game)
    {
        Console.Clear();
        Console.WriteLine("Energy Points: " + game.energyPoints[game.currentPlayer] + "\tTurno: " + game.turn + "\tJugador: " + game.currentPlayer);
        int currentI = 0;

        while (currentI < game.players.Length)
        {
            int _auxCurrentI = currentI;
            for (int i = 0; i < 2 && currentI < game.players.Length; i++)
            {
                Console.Write("Player: " + currentI + "\t\t\t\t\t\t\t");
                currentI++;
            }
            currentI = _auxCurrentI;

            Console.WriteLine();
            for (int j = 0; j < 3; j++)
            {
                _auxCurrentI = currentI;
                for (int i = 0; i < 2 && currentI < game.players.Length; i++)
                {
                    Console.Write(game.board.monsters[currentI, j].name + " Tipo: " + game.board.monsters[currentI, j].type + " Estado: " + game.board.monsters[currentI, j].state + " HP: " + game.board.monsters[currentI, j].lifePoints + " ATK: " + game.board.monsters[currentI, j].attackPoints + "\t\t");
                    currentI++;
                }
                currentI = _auxCurrentI;
                Console.WriteLine();
            }
            Console.WriteLine();

            currentI++;
            if (currentI < game.players.Length) currentI++;

        }

        Console.WriteLine();

        for (int i = 0; i < game.board.hands[game.currentPlayer].Count; i++)
        {
            Console.WriteLine(i + 1 + " " + game.decks[game.currentPlayer].powers[game.board.hands[game.currentPlayer][i]].name + " (asociada a: " + game.decks[game.currentPlayer].associations[game.decks[game.currentPlayer].powers[game.board.hands[game.currentPlayer][i]]] + ") Costo de energia: " + game.decks[game.currentPlayer].powers[game.board.hands[game.currentPlayer][i]].activationEnergy);
        }

        Console.WriteLine();
    }

    public delegate void MessagePrinter(Game? gmae);

    public static int OptionValidator((int, int) range, MessagePrinter messagePrinter, Game? game = null)
    {
        messagePrinter(game); //Para poder actualizar la pantalla cada vez que se ejecute el metodo 

        string readLine = Console.ReadLine();
        int option;

        if (!int.TryParse(readLine, out option) || option < range.Item1 || option > range.Item2)
        {
            Console.WriteLine("Su opcion es incorrecta, vuelva a selseccionar");
            Console.ReadLine();

            return OptionValidator(range, messagePrinter, game);
        }
        return option;
    }
    public static void GameOver(int winner)
    {

    }
}