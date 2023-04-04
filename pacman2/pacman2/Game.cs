using System;
using System.Diagnostics;
using System.Timers;
using System.Windows.Input;
using static System.Net.Mime.MediaTypeNames;

namespace pacman2
{

    class Game
    {
        
        public Map gameMap { get; private set; }

        public bool Finished { get; private set; } = false;

        public void Finish()
        {

            if (gameMap.Bodiky == 0)
            {
                Finished = true;
                Console.Clear();
                Console.WriteLine("\n\n\n\n\nJuhu");
            }
            else if(gameMap.Kaput() == true)
            {
                Finished = true;
                Console.WriteLine("\n\n\n\n\nBudu delat jakoze to nevidim ok?");
            }
            
        }

        public void Play()
        {
            var sw = new Stopwatch();
            sw.Start();
            var lastTime = sw.ElapsedMilliseconds;

            while (!Finished)
            {
                var currentTime = sw.ElapsedMilliseconds;
                var deltaTime = currentTime - lastTime;
                

                if(deltaTime > 200)
                {
                   
                    if (Keyboard.IsKeyDown(Key.W))
                    {
                        MoveItMoveIt(Directions.Up);
                    }
                    else if (Keyboard.IsKeyDown(Key.A))
                    {
                        MoveItMoveIt(Directions.Left);
                    }
                    else if (Keyboard.IsKeyDown(Key.S))
                    {
                        MoveItMoveIt(Directions.Down);
                    }
                    else if (Keyboard.IsKeyDown(Key.D))
                    {
                        MoveItMoveIt(Directions.Right);
                    }
                    else
                    {
                        Console.WriteLine("Invalid input");
                    }

                    foreach (Ghost x in gameMap.Duchove)
                    {
                        x.Turn();
                    }

                    
                    Console.WriteLine(PrintMap());
                    Console.WriteLine(gameMap.Bodiky.ToString("000"));

                    Finish();

                    lastTime = currentTime;
                }
                
            }

            System.Threading.Thread.Sleep(5000);

        }

        public void MoveItMoveIt(Directions dir)
        {
            switch (dir)
            {
                case Directions.Up:
                    gameMap.PacmanUp();
                    break;
                case Directions.Down:
                    gameMap.PacmanDown();
                    break;
                case Directions.Left:
                    gameMap.PacmanLeft();
                    break;
                case Directions.Right:
                    gameMap.PacmanRight();
                    break;
                default:
                    break;
            }

        }

        public string PrintMap()
        {
            Console.SetCursorPosition(0, 0);
            return gameMap.PrintMap();
        }

        public Game(int x, int y)
        {
            gameMap = Map.FirstRound(x,y);

            //steny
            gameMap.Block(0, 0, 27, 0);
            gameMap.Block(0, 30, 27, 30);

            gameMap.Block(0, 1, 0, 8);
            gameMap.Block(27, 1, 27, 8);
            gameMap.Block(16, 27, 25, 28);

            gameMap.Block(13, 1, 14, 4);

            gameMap.Block(0, 16, 0, 29);
            gameMap.Block(27, 16, 27, 29);

            //horni bloky
            gameMap.Block(2, 2, 5, 4);
            gameMap.Block(7, 2, 11, 4);  
            gameMap.Block(16, 2, 20, 4);
            gameMap.Block(22, 2, 25, 4);
            gameMap.Block(2, 6, 5, 7);
            gameMap.Block(22, 6, 25, 7);

            //horni tecka
            gameMap.Block(7, 6, 8, 13);
            gameMap.Block(9, 9, 11, 10);

            gameMap.Block(10, 6, 17, 7);
            gameMap.Block(13, 7, 14, 10);

            gameMap.Block(16, 9, 18, 10);
            gameMap.Block(19, 6, 20, 13);

            //boule
            gameMap.Block(0, 9, 5, 13);
            gameMap.Block(0, 15, 5, 19);
            gameMap.Block(22, 9, 27, 13);
            gameMap.Block(22, 15, 27, 19);

            //dolni cary
            gameMap.Block(7, 15, 8, 19);
            gameMap.Block(19, 15, 20, 19);
            gameMap.Block(7, 21, 11, 22);
            gameMap.Block(16, 21, 20, 22);

            //dolni tecka dolu
            gameMap.Block(10, 18, 17, 19);
            gameMap.Block(13, 20, 14, 22);

            gameMap.Block(10, 24, 17, 25);
            gameMap.Block(13, 26, 14, 28);

            //dolni tecka nahoru
            gameMap.Block(7, 24, 8, 26);
            gameMap.Block(2, 27, 11, 28);

            gameMap.Block(19, 24, 20, 26);
            gameMap.Block(16, 27, 25, 28);

            //lka
            gameMap.Block(2, 21, 5, 22);
            gameMap.Block(4, 23, 5, 25);

            gameMap.Block(22, 21, 25, 22);
            gameMap.Block(22, 23, 23, 25);

            //ty dva trny dole
            gameMap.Block(1, 24, 2, 25);
            gameMap.Block(25, 24, 27, 25);



        }
    }
}
