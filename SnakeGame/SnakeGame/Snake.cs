using System;
using System.Collections.Generic;
using System.Linq;

namespace SnakeGame2
{
    class Snake
    {
        public List<(int X, int Y)> Body { get; private set; }
        public (int X, int Y) Head => Body.First();

        public Snake()
        {
            Body = new List<(int X, int Y)>
            {
                (15, 10)
            };
        }

        public void MoveSnake(ConsoleKey key)
        {
            var newHead = key switch
            {
                ConsoleKey.LeftArrow => (Head.X - 1, Head.Y),
                ConsoleKey.RightArrow => (Head.X + 1, Head.Y),
                ConsoleKey.UpArrow => (Head.X, Head.Y - 1),
                ConsoleKey.DownArrow => (Head.X, Head.Y + 1),
                _ => Head
            };

            Body.Insert(0, newHead);
            Body.RemoveAt(Body.Count - 1);
        }

        public bool CollidesWithSelf()
        {
            return Body.Skip(1).Any(segment => segment == Head);
        }

        public void Grow()
        {
            Body.Add(Body.Last());
        }

        public bool Contains((int X, int Y) position)
        {
            return Body.Contains(position);
        }
    }
}
