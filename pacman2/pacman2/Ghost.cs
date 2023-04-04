using System;
using System.Collections.Generic;
using System.Threading;

namespace pacman2
{
    class Ghost
    {
        public Map map = new Map();
        
        char starePolicko = '.';

        public int GhostX;
        public int GhostY;

        public Ghost(int x, int y, Map mapp)
        {
            GhostX = x;
            GhostY = y;
            map = mapp;
            map.MapState[GhostX, GhostY] = '&';
        }

        Directions Dir = Directions.Up;
        Random OdbockaPrav = new Random();
        int Prav;
        
        List<Turn> Odbocky = new List<Turn>();
        bool Straight;


        public bool Kaput()
        {
            if(GhostX == map.PacmanRow && GhostY == map.PacmanCol)
            {
                return true;
                //:(
            }
            else
            {
                return false;
            }
        }
        public void Turn()
        {
            
            
            Straight = false;
            Odbocky.Clear();

            if (map.MapState[(GhostX - 1)%map.MapWidth, GhostY] != 'X')
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
            if (map.MapState[(GhostX + 1)%map.MapWidth, GhostY] != 'X')
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
            if (map.MapState[GhostX, (GhostY - 1)%10] != 'X')
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
                    Turn turn = new Turn(Directions.Down);
                    Odbocky.Add(turn);
                }
            }
            if (map.MapState[GhostX, (GhostY + 1)%10] != 'X')
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

            Move();
        }
        private void Move()
        {
            Prav = OdbockaPrav.Next(0, 200);

            if (Straight == true)
            {
                if (Odbocky.Count == 0)
                {
                    if (Prav < 190)
                    {
                        MoveDir(Dir);
                    }
                    else
                    {
                        Dir = (Directions)((int)Directions.Up * -1);
                        MoveDir(Dir);
                    }

                }
                else if (Odbocky.Count == 1)
                {
                    if (Prav < 140)
                    {
                        MoveDir(Dir);
                    }
                    else if (Prav > 139 && Prav < 190)
                    {
                        Dir = Odbocky[0].dir;
                        MoveDir(Dir);
                    }
                    else
                    {

                        Dir = (Directions)((int)Directions.Up * -1);
                        MoveDir(Dir);
                    }
                }
                else if (Odbocky.Count == 2)
                {
                    if (Prav < 70)
                    {
                        Dir = Odbocky[0].dir;
                        MoveDir(Dir);
                    }
                    else if (Prav > 69 && Prav < 140)
                    {
                        Dir = Odbocky[1].dir;
                        MoveDir(Dir);
                    }
                    else if (Prav > 140 && Prav < 190)
                    {
                        MoveDir(Dir);
                    }
                    else
                    {
                        Dir = (Directions)((int)Directions.Up * -1);
                        MoveDir(Dir);

                    }

                }
            }
            else
            {
                if (Odbocky.Count == 2)
                {
                    if (Prav > 120)
                    {
                        Dir = Odbocky[1].dir;
                        MoveDir(Dir);
                    }
                    else if (Prav < 80)
                    {
                        Dir = Odbocky[0].dir;
                        MoveDir(Dir);
                    }
                    else
                    {
                        Dir = (Directions)((int)Directions.Up * -1);
                        MoveDir(Dir);
                    }
                }
                else if (Odbocky.Count == 1)
                {
                    if (Prav < 160)
                    {
                        Dir = Odbocky[0].dir;
                        MoveDir(Dir);
                    }
                    else
                    {
                        Dir = (Directions)((int)Directions.Up * -1);
                        MoveDir(Dir);

                    }
                }
                else
                {
                    Dir = (Directions)((int)Directions.Up * -1);
                    MoveDir(Dir);


                }
            }
        }
        public void GhostLeft()
        {
            if (map.MapState[(GhostX - 1) % map.MapWidth, GhostY] != 'X')
            {

                map.MapState[GhostX, GhostY] = starePolicko;
                GhostX--;
                starePolicko = map.MapState[GhostX, GhostY];
                map.MapState[GhostX, GhostY] = '&';
            }        
        }
        public void GhostRight()
        {
            if (map.MapState[(GhostX + 1) % map.MapWidth, GhostY] != 'X')
            {
                map.MapState[GhostX, GhostY] = starePolicko;
                GhostX++;
                starePolicko = map.MapState[GhostX, GhostY];
                map.MapState[GhostX, GhostY] = '&';
            }
        }
        public void GhostDown()
        {
            if (map.MapState[GhostX, (GhostY + 1) % map.MapHeight] != 'X')
            {
                map.MapState[GhostX, GhostY] = starePolicko;
                GhostY++;
                starePolicko = map.MapState[GhostX, GhostY];
                map.MapState[GhostX, GhostY] = '&';
            }
        }
        public void GhostUp()
        {
            if (map.MapState[GhostX, (GhostY - 1) % map.MapHeight] != 'X')
            {
                map.MapState[GhostX, GhostY] = starePolicko;
                GhostY--;
                starePolicko = map.MapState[GhostX, GhostY];
                map.MapState[GhostX, GhostY] = '&';
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
}
