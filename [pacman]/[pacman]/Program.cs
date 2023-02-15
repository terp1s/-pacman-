using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace _pacman_
{
    public enum Directions { Up = 1, Down = -1, Left = 2, Right = -2};

    class Map
    {

        public char[,] MapState;
        int MapHeight;
        int MapWidth;
        public int Bodiky { get;private set; }

        int PacmanRow;
        int PacmanCol;

        public void PacmanLeft()
        {
            if (PacmanRow == 0 && MapState[MapWidth-1, PacmanCol] != 'X')
            {
               
                MapState[PacmanRow, PacmanCol] = ' ';
                PacmanRow = MapWidth-1;
                if (MapState[PacmanRow, PacmanCol] == '.')
                {
                    Bodiky -= 1;
                }
                MapState[PacmanRow, PacmanCol] = '<';
            }
            else if (MapState[PacmanRow - 1, PacmanCol] == 'X' || (PacmanRow == 0 && MapState[MapWidth -1, PacmanCol] == 'X'))
            {
                MapState[PacmanRow, PacmanCol] = '<';
            }
            else
            {
               
                MapState[PacmanRow, PacmanCol] = ' ';
                PacmanRow = PacmanRow - 1;
                if (MapState[PacmanRow, PacmanCol] == '.')
                {
                    Bodiky -= 1;
                }
                MapState[PacmanRow, PacmanCol] = '<';
            }
        }
        public void PacmanRight()
        {
            if (PacmanRow == MapWidth-1 && MapState[0, PacmanCol] != 'X')
            {
                
                MapState[PacmanRow, PacmanCol] = ' ';
                PacmanRow = 0;
                if (MapState[PacmanRow, PacmanCol] == '.')
                {
                    Bodiky -= 1;
                }
                MapState[PacmanRow, PacmanCol] = '>';
            }
            else if (MapState[PacmanRow + 1, PacmanCol] == 'X' || (PacmanRow == MapWidth - 1 && MapState[0, PacmanCol] == 'X'))
            {
                MapState[PacmanRow, PacmanCol] = '>';
            }
            else
            {
                
                MapState[PacmanRow, PacmanCol] = ' ';
                PacmanRow = PacmanRow + 1;
                if (MapState[PacmanRow, PacmanCol] == '.')
                {
                    Bodiky -= 1;
                }
                MapState[PacmanRow, PacmanCol] = '>';
            }
        }
        public void PacmanDown()
        {
            if (PacmanCol == MapHeight-1 && MapState[PacmanRow, 0] != 'X')
            {
                
                MapState[PacmanRow, PacmanCol] = ' ';
                PacmanCol = 0;
                if (MapState[PacmanRow, PacmanCol] == '.')
                {
                    Bodiky -= 1;
                }
                MapState[PacmanRow, PacmanCol] = 'v';
            }
            else if (MapState[PacmanRow, PacmanCol + 1] == 'X' || (PacmanCol == MapHeight-1 && MapState[PacmanRow, 0] == 'X'))
            {
                MapState[PacmanRow, PacmanCol] = 'v';
            }
            else
            {
                
                MapState[PacmanRow, PacmanCol] = ' ';
                PacmanCol = PacmanCol + 1;
                if (MapState[PacmanRow, PacmanCol] == '.')
                {
                    Bodiky -= 1;
                }
                MapState[PacmanRow, PacmanCol] = 'v';
            }
        }
        public void PacmanUp()
        {
            if (PacmanCol == 0 && MapState[PacmanRow, MapHeight-1] != 'X')
            {
               
                MapState[PacmanRow, PacmanCol] = ' ';
                PacmanCol = MapHeight-1;
                if (MapState[PacmanRow, PacmanCol] == '.')
                {
                    Bodiky -= 1;
                }
                MapState[PacmanRow, PacmanCol] = '^';
            }
            else if (MapState[PacmanRow, PacmanCol - 1] == 'X' || (PacmanCol == 0 && MapState[PacmanRow, MapHeight-1] == 'X'))
            {
                MapState[PacmanRow, PacmanCol] = '^';
            }
            else
            {
                
                MapState[PacmanRow, PacmanCol] = ' ';
                PacmanCol = PacmanCol - 1;
                if (MapState[PacmanRow, PacmanCol] == '.')
                {
                    Bodiky -= 1;
                }
                MapState[PacmanRow, PacmanCol] = '^';
            }
        }

        public string PrintMap()
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < MapHeight; i++)
            {
                for (int j = 0; j < MapWidth; j++)
                {
                    sb.Append(MapState[j, i]);
                }
                sb.AppendLine();
            }

            return sb.ToString();
        }


        public void Block( int x, int y, int x2, int y2)
        {
            for(int i = Math.Min(x, x2); i <= Math.Max(x, x2); i++)
            {
                for (int j = Math.Min(y, y2); j <= Math.Max(y, y2); j++)
                {
                    MapState[i, j] = 'X';

                    Bodiky =- 1;
                }
            }
        }

        public static Map FirstRound()
        {
            Map t = new Map();
            t.MapState = new char[10, 15];
            t.MapHeight = 15;
            t.MapWidth = 10;
            for (int i = 0; i < t.MapHeight; i++)
            {
                for (int j = 0; j < t.MapWidth; j++)
                {
                    t.MapState[j, i] = '.';
                }
            }

            t.Bodiky = (t.MapHeight * t.MapWidth) - 1;

            t.MapState[0, 0] = '>';

            return t;
        }

    }

    class Turn
    {
        Directions dir;

        public Turn(Directions dir2)
        {
            dir = dir2;
        }
    }

    class Ghost
    {
        Map map;

        int GhostX;
        int GhostY;

        Directions Dir;
        Random OdbockaPrav = new Random();
        int Prav;
        int Cesty;
        List<Turn> Odbocky;
        bool Straight;

        private Timer GTimer = new Timer(200);
        
        private void Turn()
        {
            Cesty = 0;
            Straight = false;
            Odbocky.Clear();

            if(map.MapState[GhostX -1, GhostY] != 'X')
            {
                if(Dir == Directions.Left)
                {
                    Straight = true;
                }
                else if (Dir == Directions.Right)
                {

                }
                else
                {
                    Turn turn = new Turn(Directions.Left);
                    Cesty += 1;
                    Odbocky.Add(turn);
                }
            }
            else if (map.MapState[GhostX + 1, GhostY] != 'X')
            {
                if (Dir == Directions.Right)
                {
                    Straight = true;
                }
                else if(Dir == Directions.Left)
                {

                }
                else
                {
                    Turn turn = new Turn(Directions.Right);
                    Cesty += 1;
                    Odbocky.Add(turn);
                }
            }
            else if (map.MapState[GhostX, GhostY -1] != 'X')
            {
                if (Dir == Directions.Down)
                {
                    Straight = true;
                }
                else if(Dir == Directions.Up)
                {

                }
                else
                {
                    Turn turn = new Turn(Directions.Down);
                    Cesty += 1;
                    Odbocky.Add(turn);
                }
            }
            else if (map.MapState[GhostX, GhostY+1] != 'X')
            {
                if (Dir == Directions.Up)
                {
                    Straight = true;
                }
                else if(Dir == Directions.Down)
                {

                }
                else
                {
                    Turn turn = new Turn(Directions.Up);
                    Cesty += 1;
                    Odbocky.Add(turn);
                }
                
            }
        }

        private void Move()
        {
            if(Straight == true)
            {
                if(Cesty != 0)
                {
                    Prav = OdbockaPrav.Next(0, 100);

                    if (Prav <= 5)
                    {

                    }
                    else
                    {
                        MoveDir(Dir);
                    }
                }
                else
                {

                }
            }
        }

        public void PacmanLeft()
        {
            if (PacmanRow == 0 && MapState[MapWidth-1, PacmanCol] != 'X')
            {
               
                MapState[PacmanRow, PacmanCol] = ' ';
                PacmanRow = MapWidth-1;
                if (MapState[PacmanRow, PacmanCol] == '.')
                {
                    Bodiky -= 1;
                }
                MapState[PacmanRow, PacmanCol] = '<';
            }
            else if (MapState[PacmanRow - 1, PacmanCol] == 'X' || (PacmanRow == 0 && MapState[MapWidth -1, PacmanCol] == 'X'))
            {
                MapState[PacmanRow, PacmanCol] = '<';
            }
            else
            {
               
                MapState[PacmanRow, PacmanCol] = ' ';
                PacmanRow = PacmanRow - 1;
                if (MapState[PacmanRow, PacmanCol] == '.')
                {
                    Bodiky -= 1;
                }
                MapState[PacmanRow, PacmanCol] = '<';
            }
        }
        public void PacmanRight()
        {
            if (PacmanRow == MapWidth-1 && MapState[0, PacmanCol] != 'X')
            {
                
                MapState[PacmanRow, PacmanCol] = ' ';
                PacmanRow = 0;
                if (MapState[PacmanRow, PacmanCol] == '.')
                {
                    Bodiky -= 1;
                }
                MapState[PacmanRow, PacmanCol] = '>';
            }
            else if (MapState[PacmanRow + 1, PacmanCol] == 'X' || (PacmanRow == MapWidth - 1 && MapState[0, PacmanCol] == 'X'))
            {
                MapState[PacmanRow, PacmanCol] = '>';
            }
            else
            {
                
                MapState[PacmanRow, PacmanCol] = ' ';
                PacmanRow = PacmanRow + 1;
                if (MapState[PacmanRow, PacmanCol] == '.')
                {
                    Bodiky -= 1;
                }
                MapState[PacmanRow, PacmanCol] = '>';
            }
        }
        public void PacmanDown()
        {
            if (PacmanCol == MapHeight-1 && MapState[PacmanRow, 0] != 'X')
            {
                
                MapState[PacmanRow, PacmanCol] = ' ';
                PacmanCol = 0;
                if (MapState[PacmanRow, PacmanCol] == '.')
                {
                    Bodiky -= 1;
                }
                MapState[PacmanRow, PacmanCol] = 'v';
            }
            else if (MapState[PacmanRow, PacmanCol + 1] == 'X' || (PacmanCol == MapHeight-1 && MapState[PacmanRow, 0] == 'X'))
            {
                MapState[PacmanRow, PacmanCol] = 'v';
            }
            else
            {
                
                MapState[PacmanRow, PacmanCol] = ' ';
                PacmanCol = PacmanCol + 1;
                if (MapState[PacmanRow, PacmanCol] == '.')
                {
                    Bodiky -= 1;
                }
                MapState[PacmanRow, PacmanCol] = 'v';
            }
        }
        public void PacmanUp()
        {
            if (PacmanCol == 0 && MapState[PacmanRow, MapHeight-1] != 'X')
            {
               
                MapState[PacmanRow, PacmanCol] = ' ';
                PacmanCol = MapHeight-1;
                if (MapState[PacmanRow, PacmanCol] == '.')
                {
                    Bodiky -= 1;
                }
                MapState[PacmanRow, PacmanCol] = '^';
            }
            else if (MapState[PacmanRow, PacmanCol - 1] == 'X' || (PacmanCol == 0 && MapState[PacmanRow, MapHeight-1] == 'X'))
            {
                MapState[PacmanRow, PacmanCol] = '^';
            }
            else
            {
                
                MapState[PacmanRow, PacmanCol] = ' ';
                PacmanCol = PacmanCol - 1;
                if (MapState[PacmanRow, PacmanCol] == '.')
                {
                    Bodiky -= 1;
                }
                MapState[PacmanRow, PacmanCol] = '^';
            }
        }

        public void MoveDir(Directions dir)
        {
            if (dir == Directions.Up)
            {
                GhostUp();
            }
            else if (dir == Directions.Left)
            {
                GhostLeft();
            }
            else if (dir == Directions.Down)
            {
                GhostDown();
            }
            else if (dir == Directions.Right)
            {
                GhostRight();
            }
        }

    }

    class Game
    {
        public Map gameMap { get; private set; }

        public bool Finished { get; private set; } = false;

        public void Finish()
        {
            if (gameMap.Bodiky == 0)
            {
                Finished = true;
            }
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
            Console.SetCursorPosition(0,0);
            return gameMap.PrintMap();
        }

        public Game()
        {
            gameMap = Map.FirstRound();
            gameMap.Block(5, 6, 9, 10);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();

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
                //Console.Clear();
                Console.WriteLine(game.PrintMap());
                Console.WriteLine(game.gameMap.Bodiky.ToString("000"));

                game.Finish();
            }
        }
    }
}
