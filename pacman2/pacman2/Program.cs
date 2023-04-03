using System;
using System.Linq;
using System.Threading;
using System.Timers;

namespace pacman2
{
    class Program
    {
        //ma to nejake ty brouky (haha) ale myslenka tam je

        static void Main(string[] args)
        {
            Game game = new Game(28,31);
            Ghost ghost = new Ghost(8, 7, game.gameMap);
            Ghost blinky = new Ghost(2, 2, game.gameMap);

            Console.WriteLine(game.PrintMap());
            

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
                blinky.Turn();
                Console.WriteLine(game.PrintMap());
                Console.WriteLine(game.gameMap.Bodiky.ToString("000"));

                
                game.Finish();
            }
        }
    }
}
