using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Timers;

namespace pacman2
{
    public enum Directions { Up = 1, Down = -1, Left = 2, Right = -2 };

    class Map
    {

        public char[,] MapState;
        public int MapHeight;
        public int MapWidth;
        public int Bodiky { get; set; }

        int PacmanRow = 5;
        int PacmanCol = 5;

        public void PacmanLeft()
        {
            if (PacmanRow == 0 && MapState[MapWidth - 1, PacmanCol] != 'X')
            {

                MapState[PacmanRow, PacmanCol] = ' ';
                PacmanRow = MapWidth - 1;
                if (MapState[PacmanRow, PacmanCol] == '.')
                {
                    Bodiky -= 1;
                }
                MapState[PacmanRow, PacmanCol] = '<';
            }
            else if (MapState[PacmanRow - 1, PacmanCol] == 'X' || (PacmanRow == 0 && MapState[MapWidth - 1, PacmanCol] == 'X'))
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
            if (PacmanRow == MapWidth - 1 && MapState[0, PacmanCol] != 'X')
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
            if (PacmanCol == MapHeight - 1 && MapState[PacmanRow, 0] != 'X')
            {

                MapState[PacmanRow, PacmanCol] = ' ';
                PacmanCol = 0;
                if (MapState[PacmanRow, PacmanCol] == '.')
                {
                    Bodiky -= 1;
                }
                MapState[PacmanRow, PacmanCol] = 'v';
            }
            else if (MapState[PacmanRow, PacmanCol + 1] == 'X' || (PacmanCol == MapHeight - 1 && MapState[PacmanRow, 0] == 'X'))
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
            if (PacmanCol == 0 && MapState[PacmanRow, MapHeight - 1] != 'X')
            {

                MapState[PacmanRow, PacmanCol] = ' ';
                PacmanCol = MapHeight - 1;
                if (MapState[PacmanRow, PacmanCol] == '.')
                {
                    Bodiky -= 1;
                }
                MapState[PacmanRow, PacmanCol] = '^';
            }
            else if (MapState[PacmanRow, PacmanCol - 1] == 'X' || (PacmanCol == 0 && MapState[PacmanRow, MapHeight - 1] == 'X'))
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


        public void Block(int x, int y, int x2, int y2)
        {
            for (int i = Math.Min(x, x2); i <= Math.Max(x, x2); i++)
            {
                for (int j = Math.Min(y, y2); j <= Math.Max(y, y2); j++)
                {
                    MapState[i, j] = 'X';

                    Bodiky -= 1;
                }
            }
        }

        public static Map FirstRound(int x, int y)
        {
            Map t = new Map();
            t.MapState = new char[x, y];
            t.MapHeight = y;
            t.MapWidth = x;
            for (int i = 0; i < t.MapHeight; i++)
            {
                for (int j = 0; j < t.MapWidth; j++)
                {
                    t.MapState[j, i] = '.';
                }
            }

            t.Bodiky = (t.MapHeight * t.MapWidth) - 1;

            t.MapState[5, 5] = '>';

            return t;
        }

    }

    class Turn
    {
        public Directions dir;

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
        
        List<Turn> Odbocky;
        bool Straight;

        private System.Timers.Timer GTimer = new System.Timers.Timer(200);

        private void Turn()
        {
            
            Straight = false;
            Odbocky.Clear();

            if (map.MapState[GhostX - 1, GhostY] != 'X')
            {
                if (Dir == Directions.Left)
                {
                    Straight = true;
                }
                else if (Dir == Directions.Right)
                {

                }
                else
                {
                    Turn turn = new Turn(Directions.Left);
                 
                    Odbocky.Add(turn);
                }
            }
            else if (map.MapState[GhostX + 1, GhostY] != 'X')
            {
                if (Dir == Directions.Right)
                {
                    Straight = true;
                }
                else if (Dir == Directions.Left)
                {

                }
                else
                {
                    Turn turn = new Turn(Directions.Right);
                    Odbocky.Add(turn);
                }
            }
            else if (map.MapState[GhostX, GhostY - 1] != 'X')
            {
                if (Dir == Directions.Down)
                {
                    Straight = true;
                }
                else if (Dir == Directions.Up)
                {

                }
                else
                {
                    Turn turn = new Turn(Directions.Down);
                    Odbocky.Add(turn);
                }
            }
            else if (map.MapState[GhostX, GhostY + 1] != 'X')
            {
                if (Dir == Directions.Up)
                {
                    Straight = true;
                }
                else if (Dir == Directions.Down)
                {

                }
                else
                {
                    Turn turn = new Turn(Directions.Up);
                    Odbocky.Add(turn);
                }

            }
        }

        private void Move()
        {
            Prav = 0;
            Prav = OdbockaPrav.Next(0, 200);

            if (Straight == true)
            {
                if (Odbocky.Count != 0)
                {
                    

                    if (Prav <= 10)
                    {
                        if(Prav >5 && Odbocky.Count == 2)
                        {
                            MoveDir(Odbocky[1].dir);
                        }
                        else
                        {
                            MoveDir(Odbocky[0].dir);
                        }
                    }
                    else
                    {
                        MoveDir(Dir);
                    }
                }
                else
                {
                    

                    if (Odbocky.Count == 2 && Prav > 100)
                    {
                        MoveDir(Odbocky[1].dir);
                    }
                    else
                    {
                        MoveDir(Odbocky[0].dir);
                    }
                }
            }
        }


        public void GhostLeft()
        {
            if (GhostX == 0 && map.MapState[map.MapWidth - 1, GhostY] != 'X')
            {

                map.MapState[GhostX, GhostY] = ' ';
                GhostX = map.MapWidth - 1;
                if (map.MapState[GhostX, GhostY] == '.')
                {
                    map.Bodiky -= 1;
                }
                map.MapState[GhostX, GhostY] = '<';
            }
            else if (map.MapState[GhostX - 1, GhostY] == 'X' || (GhostX == 0 && map.MapState[map.MapWidth - 1, GhostY] == 'X'))
            {
                map.MapState[GhostX, GhostY] = '<';
            }
            else
            {

                map.MapState[GhostX, GhostY] = ' ';
                GhostX = GhostX - 1;
                if (map.MapState[GhostX, GhostY] == '.')
                {
                    map.Bodiky -= 1;
                }
                map.MapState[GhostX, GhostY] = '<';
            }
        }
        public void GhostRight()
        {
            if (GhostX == map.MapWidth - 1 && map.MapState[0, GhostY] != 'X')
            {

                map.MapState[GhostX, GhostY] = ' ';
                GhostX = 0;
                if (map.MapState[GhostX, GhostY] == '.')
                {
                    map.Bodiky -= 1;
                }
                map.MapState[GhostX, GhostY] = '>';
            }
            else if (map.MapState[GhostX + 1, GhostY] == 'X' || (GhostX == map.MapWidth - 1 && map.MapState[0, GhostY] == 'X'))
            {
                map.MapState[GhostX, GhostY] = '>';
            }
            else
            {

                map.MapState[GhostX, GhostY] = ' ';
                GhostX = GhostX + 1;
                if (map.MapState[GhostX, GhostY] == '.')
                {
                    map.Bodiky -= 1;
                }
                map.MapState[GhostX, GhostY] = '>';
            }
        }
        public void GhostDown()
        {
            if (GhostY == map.MapHeight - 1 && map.MapState[GhostX, 0] != 'X')
            {

                map.MapState[GhostX, GhostY] = ' ';
                GhostY = 0;
                if (map.MapState[GhostX, GhostY] == '.')
                {
                    map.Bodiky -= 1;
                }
                map.MapState[GhostX, GhostY] = 'v';
            }
            else if (map.MapState[GhostX, GhostY + 1] == 'X' || (GhostY == map.MapHeight - 1 && map.MapState[GhostX, 0] == 'X'))
            {
                map.MapState[GhostX, GhostY] = 'v';
            }
            else
            {

                map.MapState[GhostX, GhostY] = ' ';
                GhostY = GhostY + 1;
                if (map.MapState[GhostX, GhostY] == '.')
                {
                    map.Bodiky -= 1;
                }
                map.MapState[GhostX, GhostY] = 'v';
            }
        }
        public void GhostUp()
        {
            if (GhostY == 0 && map.MapState[GhostX, map.MapHeight - 1] != 'X')
            {

                map.MapState[GhostX, GhostY] = ' ';
                GhostY = map.MapHeight - 1;
                if (map.MapState[GhostX, GhostY] == '.')
                {
                    map.Bodiky -= 1;
                }
                map.MapState[GhostX, GhostY] = '^';
            }
            else if (map.MapState[GhostX, GhostY - 1] == 'X' || (GhostY == 0 && map.MapState[GhostX, map.MapHeight - 1] == 'X'))
            {
                map.MapState[GhostX, GhostY] = '^';
            }
            else
            {

                map.MapState[GhostX, GhostY] = ' ';
                GhostY = GhostY - 1;
                if (map.MapState[GhostX, GhostY] == '.')
                {
                    map.Bodiky -= 1;
                }
                map.MapState[GhostX, GhostY] = '^';
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
            Console.SetCursorPosition(0, 0);
            return gameMap.PrintMap();
        }

        public Game(int x, int y)
        {
            gameMap = Map.FirstRound(x,y);
            gameMap.Block(0, 0, 9, 0);
            gameMap.Block(0, 1, 0, 9);
            gameMap.Block(2, 2, 2, 8);
            gameMap.Block(4, 1, 6, 4);
            gameMap.Block(2, 8, 8, 8);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game(10,10);

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
                
                Console.WriteLine(game.PrintMap());
                Console.WriteLine(game.gameMap.Bodiky.ToString("000"));

                game.Finish();
            }
        }
    }
}
