using System;
using System.Collections.Generic;
using System.Text;

namespace pacman2
{
    class Map
    {

        public char[,] MapState;
        public int MapHeight;
        public int MapWidth;
        public int Bodiky { get; set; }

        public int PacmanRow = 5;
        public int PacmanCol = 5;

        public List<Ghost> Duchove = new List<Ghost>();
        public bool Kaput()
        {
            foreach(Ghost x in Duchove)
            {
                if(x.Kaput() == true)
                {
                    return true;

                }
            }
            return false;
        }

        
        public void PacmanLeft()
        {
            if (MapState[Math.Abs(PacmanRow - 1)%MapWidth, PacmanCol] != 'X')
            {
                MapState[PacmanRow, PacmanCol] = ' ';

                if (PacmanRow == 0)
                {
                    PacmanRow = MapWidth;
                }
                PacmanRow--;

                if (MapState[PacmanRow, PacmanCol] == '.')
                {
                    Bodiky -= 1;
                }
                MapState[PacmanRow, PacmanCol] = '>';
            }
            
        }
        public void PacmanRight()
        {
            if (MapState[(PacmanRow + 1)%MapWidth, PacmanCol] != 'X')
            {
                if (PacmanRow == MapWidth)
                {
                    PacmanRow = PacmanRow - MapWidth;
                }

                MapState[PacmanRow, PacmanCol] = ' ';

                PacmanRow++;

                if (MapState[PacmanRow%MapWidth, PacmanCol] == '.')
                {
                    Bodiky -= 1;
                }
                MapState[PacmanRow % MapWidth, PacmanCol] = '<';
            }
            
        }
        public void PacmanDown()
        {
            if (MapState[PacmanRow, (PacmanCol + 1)%MapHeight] != 'X')
            {
                

                if (PacmanCol == MapHeight)
                {
                    PacmanCol = PacmanCol - MapHeight;
                }

                MapState[PacmanRow, PacmanCol] = ' ';

                PacmanCol++;

                if (MapState[PacmanRow, PacmanCol] == '.')
                {
                    Bodiky -= 1;
                }
                MapState[PacmanRow, PacmanCol] = '^';
            }
        }
        public void PacmanUp()
        {
            if (MapState[PacmanRow, Math.Abs(PacmanCol - 1)%MapHeight] != 'X')
            {
                MapState[PacmanRow, PacmanCol] = ' ';

                if (PacmanCol == 0)
                {
                    PacmanCol = MapHeight;
                }

                PacmanCol--;

                if (MapState[PacmanRow, PacmanCol] == '.')
                {
                    Bodiky -= 1;
                }
                MapState[PacmanRow, PacmanCol] = 'V';
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
            Map t = new Map
            {
                MapState = new char[x, y],
                MapHeight = y,
                MapWidth = x
            };

            for (int i = 0; i < t.MapHeight; i++)
            {
                for (int j = 0; j < t.MapWidth; j++)
                {
                    t.MapState[j, i] = '.';
                }
            }

            t.Bodiky = (t.MapHeight * t.MapWidth)-1;

            t.MapState[5, 5] = '>';

            return t;
        }

    }
}
