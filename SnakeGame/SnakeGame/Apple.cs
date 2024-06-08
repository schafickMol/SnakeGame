using System;

namespace SnakeGame2
{
    class Apple
    {
        public (int X, int Y) Position { get; private set; }

        public void GenerateNewApple()
        {
            var random = new Random();
            Position = (random.Next(1, 29), random.Next(1, 19));
        }
    }
}