using System;
using System.Linq;

namespace task3_csharp
{
    class Program
    {
        static void Main(string[] args)
        {
            String[] moves = args;
            while ((moves.Length < 3) || (moves.Length % 2 == 0) || (moves.Length != moves.ToList().Distinct().ToArray().Length))
            {
                Console.Out.WriteLine("Please enter valid data");
                String ch = Console.In.ReadLine();
                if (ch != null)
                {
                    moves = ch.Split(' ');
                }
            }
            Console.Out.WriteLine("______________________________________________________________________________________________________________________________________");
            Game game = new Game(moves);
            game.Start();
        }
    }
}
