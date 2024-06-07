using System;
using System.Media;
using Spectre.Console;

namespace SnakeGame
{
    class Program
    {
        static SoundPlayer player;

        static void Main(string[] args)
        {
            player = new SoundPlayer("C:\\Users\\chafm\\source\\repos\\SnakeGame2\\SnakeGame2/musica_Menu.wav");

            player.PlayLooping();

            ShowMainMenu();
        }

        static void ShowMainMenu()
        {
            while (true)
            {
                Console.Clear();
                AnsiConsole.Write(
                    new Panel(new Markup("[bold yellow]Bienvenido a Snake Game[/]"))
                        .Border(BoxBorder.Double)
                        .Header("Menú Principal", Justify.Center));

                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[green]Seleccione una opción[/]")
                        .AddChoices("Jugar", "Ayuda", "Salir"));

                switch (choice)
                {
                    case "Jugar":
                        player.Stop();

                        var game = new Game();
                        game.Run().Wait();

                        player.PlayLooping();
                        break;
                    case "Ayuda":
                        ShowHelp();
                        break;
                    case "Salir":
                        player.Stop();
                        Environment.Exit(0);
                        break;
                }
            }
        }

        static void ShowHelp()
        {
            Console.Clear();
            AnsiConsole.Write(
                new Panel(new Markup("[bold yellow]Instrucciones del Juego[/]\n\n" +
                                     "Use las teclas de flecha para mover la serpiente.\n" +
                                     "Coma la manzana para ganar puntos.\n" +
                                     "Evite chocar con las paredes y con usted mismo.\n\n" +
                                     "Presione cualquier tecla para regresar al menú."))
                    .Border(BoxBorder.Double)
                    .Header("Ayuda", Justify.Center));
            Console.ReadKey(true);
        }
    }
}
