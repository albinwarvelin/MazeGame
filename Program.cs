using System;

namespace MazeGame
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new Game1())
                game.Run();

            int[,] test = new int[10,10];
            for (int y = 0; y < 10; y++)
            {
                for(int x = 0; x < 10; x++)
                {
                    test[y,x] = x + y * 10;
                    Console.WriteLine(test[y,x]);
                }
            }

            foreach(int x in test)
            {
                Console.WriteLine(x);
            }
        }
    }
}
