using System;
using System.Linq;
using System.Threading;
using System.Timers;

namespace pacman2
{
    public enum Directions { Up = 1, Down = -1, Left = 2, Right = -2 };

    class Turn
    {
        public Directions dir;

        public Turn(Directions dir2)
        {
            dir = dir2;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game(10,10);
            Ghost ghost = new Ghost(8,7, game.gameMap);
            

            Console.WriteLine(game.PrintMap());
            Console.ReadKey();
            

            while (!game.Finished)
            {
                
                var tt = Console.ReadKey();
                Console.WriteLine();
                var t = tt.KeyChar.ToString();
                if (t == "w")
                {
                    game.MoveItMoveIt(Directions.Up);
                }
                else if (t == "a")
                {
                    game.MoveItMoveIt(Directions.Left);
                }
                else if (t == "s")
                {
                    game.MoveItMoveIt(Directions.Down);
                }
                else if (t == "d")
                {
                    game.MoveItMoveIt(Directions.Right);
                }
                else
                {
                    Console.WriteLine("Invalid input");
                }

                ghost.Turn();
                Console.WriteLine(game.PrintMap());
                Console.WriteLine(game.gameMap.Bodiky.ToString("000"));

                game.Finish();
            }
        }
    }
}
