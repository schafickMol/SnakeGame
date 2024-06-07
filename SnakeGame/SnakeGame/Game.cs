using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Spectre.Console;
using Spectre.Console.Rendering;

namespace SnakeGame2
{
    class Game
    {
        public Snake snake;
        public Apple apple;
        private int score;
        private ConsoleKey direction = ConsoleKey.RightArrow;

        public async Task Run()
        {
            InitializeGame();

            var gameLoop = GameLoop();
            var inputTask = HandleInput();

            await Task.WhenAny(gameLoop, inputTask);
        }

        private void InitializeGame()
        {
            snake = new Snake();
            apple = new Apple();
            apple.GenerateNewApple();

            Console.Beep(440, 500);
        }

        private async Task GameLoop()
        {
            while (true)
            {
                DrawBoard();
                snake.MoveSnake(direction);

                if (CollidesWithWall() || snake.CollidesWithSelf())
                {
                    EndGame();
                    break;
                }

                if (snake.Head == apple.Position)
                {
                    score++;
                    apple.GenerateNewApple();
                    snake.Grow();
                }

                await Task.Delay(200);
            }
        }

        private async Task HandleInput()
        {
            while (true)
            {
                var key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.LeftArrow && direction != ConsoleKey.RightArrow)
                {
                    direction = key;
                }
                else if (key == ConsoleKey.RightArrow && direction != ConsoleKey.LeftArrow)
                {
                    direction = key;
                }
                else if (key == ConsoleKey.UpArrow && direction != ConsoleKey.DownArrow)
                {
                    direction = key;
                }
                else if (key == ConsoleKey.DownArrow && direction != ConsoleKey.UpArrow)
                {
                    direction = key;
                }

                await Task.Delay(10);
            }
        }

        private void DrawBoard()
        {
            AnsiConsole.Clear();
            AnsiConsole.Render(new Rule("Snake Game")
                .Centered()
                .RuleStyle(Style.Parse("yellow")));

            var grid = new Grid().Collapse();
            for (var x = 0; x < 30; x++)
            {
                grid.AddColumn(new GridColumn().NoWrap());
            }

            for (var y = 0; y < 20; y++)
            {
                var row = new List<IRenderable>();
                for (var x = 0; x < 30; x++)
                {
                    if (x == 0 || x == 29 || y == 0 || y == 19)
                    {
                        row.Add(new Markup("[white on black]█[/]"));
                    }
                    else if (snake.Contains((x, y)))
                    {
                        row.Add(new Markup("[green]■[/]"));
                    }
                    else if (apple.Position == (x, y))
                    {
                        row.Add(new Markup("[red]■[/]"));
                    }
                    else
                    {
                        row.Add(new Markup(" "));
                    }
                }
                grid.AddRow(row.ToArray());
            }

            AnsiConsole.Render(grid);
            AnsiConsole.MarkupLine($"[bold yellow]Puntaje: {score}[/]");
        }

        private bool CollidesWithWall()
        {
            var head = snake.Head;
            return head.X < 0 || head.X >= 30 || head.Y < 0 || head.Y >= 20;
        }

        private void EndGame()
        {
            Console.Clear();
            Console.WriteLine($"¡Perdiste! Puntaje: {score}");
            Console.WriteLine("¿Deseas jugar de nuevo? (S/N)");

            var response = Console.ReadKey(true).KeyChar;
            if (response == 'S' || response == 's')
            {
                Run().Wait();
            }
            else
            {
                Environment.Exit(0);
            }
        }
    }
}
