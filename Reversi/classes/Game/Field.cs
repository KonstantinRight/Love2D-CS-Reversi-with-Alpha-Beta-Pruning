using Love;
using Reversi.classes.misc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi.classes.Game
{
    class Field
    {
        private float x, y, w, h;
        public Cell[,] Cells = new Cell[8, 8];

        public Field(int x, int y, int s)
        {
            this.x = x + s * (-4); this.y = y + s * (-4);
            this.w = Math.Abs(this.x - (x + s * (-4 + Cells.GetLength(0))) ); this.h = Math.Abs(this.y - (y + s * (-4 + Cells.GetLength(1))));
            for (int i = 0; i < Cells.GetLength(0); i++)
            {
                for (int j = 0; j < Cells.GetLength(1); j++)
                {
                    Cells[i, j] = new Cell(x - s * (-3 + i), y - s * (-3 + j), s);
                }
            }
        }

        private Field()
        {

        }

        public void Draw()
        {
            for (int i = 0; i < Cells.GetLength(0); i++)
            {
                for (int j = 0; j < Cells.GetLength(1); j++)
                {
                    Cells[i, j].Draw();
                }
            }
        }

        public void Update(float dt)
        {
            for (int i = 0; i < Cells.GetLength(0); i++)
            {
                for (int j = 0; j < Cells.GetLength(1); j++)
                {
                    Cells[i, j].Update(dt);
                }
            }
        }

        public int[] MousePressed(float x, float y, char turn)
        {
            var res = new List<Cell>();
            if (x >= this.x && x <= this.x + this.w && y >= this.y && y <= this.y + this.h)
            {
                for (int i = 0; i < Cells.GetLength(0); i++)
                {
                    for (int j = 0; j < Cells.GetLength(1); j++)
                    {
                        if (Cells[i, j].MousePressed(x, y, turn))
                        {
                            return new int[] { i, j };
                        }
                    }
                }
            }
            return null;
        }

        public int Count(char side)
        {
            int res = 0;
            for (int i = 0; i < Cells.GetLength(0); i++)
            {
                for (int j = 0; j < Cells.GetLength(1); j++)
                {
                    if (Cells[i, j].state == side)
                    {
                        res++;
                    }
                }
            }

            return res;
        }

        public void ClearPossible()
        {
            for (int i = 0; i < Cells.GetLength(0); i++)
            {
                for (int j = 0; j < Cells.GetLength(1); j++)
                {
                    if (Cells[i, j].state == 'g')
                    {
                        Cells[i, j].state = 'e';
                    }
                }
            }
        }

        public Field Copy()
        {
            var copy = new Field();
            for (int i = 0; i < Cells.GetLength(0); i++)
            {
                for (int j = 0; j < Cells.GetLength(1); j++)
                {
                    copy.Cells[i, j] = new Cell(Cells[i,j].x, Cells[i, j].y, Cells[i, j].s);
                    copy.Cells[i, j].state = Cells[i, j].state;
                }
            }
            return copy;
        }

        public void OnTurn(char turn, int[] Chosen = null)
        {
            if (Chosen != null)
            {
                var ToPaint = FindPainted(Chosen, turn);
                foreach (int[] item in ToPaint)
                {
                    Cells[item[0], item[1]].state = turn;
                }
            }
        }

        public bool IsTerminal()
        {
            if (FindChecker('w').Count + FindChecker('b').Count == 0)
            {
                return true;
            }
            return false;
        }

        public List<int[]> FindPainted(int[] Chosen, char side)
        {
            List<int[]> res = new List<int[]>();
            Cells[Chosen[0], Chosen[1]].state = side;
            var AvailableToPaintBetween = FindAvailable(Chosen, side, side);
            foreach (int[] item in AvailableToPaintBetween)
            {
                for (int i = 0; i < Cells.GetLength(0); i++)
                {
                    for (int j = 0; j < Cells.GetLength(1); j++)
                    {
                        if (Misc.IsElementBetweenTwo(new int[] { i, j }, Chosen, item))
                        {
                            res.Add(new int[] {i, j});
                            //Cells[i, j].state = turn;
                        }
                    }
                }
            }
            return res;
        }

        public List<int[]> FindChecker(char side)
        {
            List<int[]> res = new List<int[]>();
            for (int i = 0; i < Cells.GetLength(0); i++)
            {
                for (int j = 0; j < Cells.GetLength(1); j++)
                {
                    if (Cells[i, j].state == side)
                    {
                        res.AddRange(FindAvailable(new int[] { i, j }, side));
                    }
                }
            }

            return res;
        }

        private List<int[]> FindAvailable(int[] pos, char side, char ToFind = 'e')
        {
            List<int[]> res = new List<int[]>();
            int x = pos[0]; int y = pos[1];

            char opposite = 'b';
            if (side == 'b')
            {
                opposite = 'w';
            }
            //else if (side == 'w')
            //{
            //    opposite = 'b';
            //}
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    var curx = x + i; var cury = y + j;
                    if (curx >= 0 && cury >= 0 && curx < Cells.GetLength(0) && cury < Cells.GetLength(1))
                        if (Cells[curx, cury].state == opposite)
                        {
                            if (i == -1 && j == -1)
                                res.AddRange(CheckUpLeft(new int[] { curx, cury }, side, ToFind));
                            if (i == 0 && j == -1)
                                res.AddRange(CheckUp(new int[] { pos[0], cury }, side, ToFind));
                            if (i == 1 && j == -1)
                                res.AddRange(CheckUpRight(new int[] { curx, cury }, side, ToFind));
                            if (i == 1 && j == 0)
                                res.AddRange(CheckRight(new int[] { curx, pos[1] }, side, ToFind));
                            if (i == 1 && j == 1)
                                res.AddRange(CheckDownRight(new int[] { curx, cury }, side, ToFind));
                            if (i == 0 && j == 1)
                                res.AddRange(CheckDown(new int[] { pos[0], cury }, side, ToFind));
                            if (i == -1 && j == 1)
                                res.AddRange(CheckDownLeft(new int[] { curx, cury }, side, ToFind));
                            if (i == -1 && j == 0)
                                res.AddRange(CheckLeft(new int[] { curx, pos[1] }, side, ToFind));
                        }
                }
            }

            return res;
        }

        //Check methods

        public List<int[]> CheckRight(int[] pos, char side, char ToFind)
        {
            List<int[]> res = new List<int[]>();
            int x = pos[0];
            while (x < Cells.GetLength(0) - 1)
            {
                x++;
                if (Cells[x, pos[1]].state == side && ToFind == 'e' || Cells[x, pos[1]].state == 'e' && ToFind != 'e')
                    break;
                if (Cells[x, pos[1]].state == ToFind)
                {
                    res.Add(new int[] { x, pos[1] });
                    break;
                }
            }
            return res;
        }
        public List<int[]> CheckDownRight(int[] pos, char side, char ToFind)
        {
            List<int[]> res = new List<int[]>();
            int x = pos[0];
            int y = pos[1];
            while (y < Cells.GetLength(1) - 1 && x < Cells.GetLength(1) - 1)
            {
                y++;
                x++;
                if (Cells[x, y].state == side && ToFind == 'e' || Cells[x, y].state == 'e' && ToFind != 'e')
                    break;
                if (Cells[x, y].state == ToFind)
                {
                    res.Add(new int[] { x, y });
                    break;
                }
            }
            return res;
        }
        public List<int[]> CheckDown(int[] pos, char side, char ToFind)
        {
            List<int[]> res = new List<int[]>();
            int y = pos[1];
            while (y < Cells.GetLength(1) - 1)
            {
                y++;
                if (Cells[pos[0], y].state == side && ToFind == 'e' || Cells[pos[0], y].state == 'e' && ToFind != 'e')
                    break;
                if (Cells[pos[0], y].state == ToFind)
                {
                    res.Add(new int[] { pos[0], y });
                    break;
                }
            }
            return res;
        }

        public List<int[]> CheckDownLeft(int[] pos, char side, char ToFind)
        {
            List<int[]> res = new List<int[]>();
            int x = pos[0];
            int y = pos[1];
            while (y < Cells.GetLength(1) - 1 && x > 0)
            {
                y++;
                x--;
                if (Cells[x, y].state == side && ToFind == 'e' || Cells[x, y].state == 'e' && ToFind != 'e')
                    break;
                if (Cells[x, y].state == ToFind)
                {
                    res.Add(new int[] { x, y });
                    break;
                }
            }
            return res;
        }
        public List<int[]> CheckLeft(int[] pos, char side, char ToFind)
        {
            List<int[]> res = new List<int[]>();
            int x = pos[0];
            while (x > 0)
            {
                x--;
                if (Cells[x, pos[1]].state == side && ToFind == 'e' || Cells[x, pos[1]].state == 'e' && ToFind != 'e')
                    break;
                if (Cells[x, pos[1]].state == ToFind)
                {
                    res.Add(new int[] { x, pos[1] });
                    break;
                }
            }
            return res;
        }
        public List<int[]> CheckUpLeft(int[] pos, char side, char ToFind)
        {
            List<int[]> res = new List<int[]>();
            int x = pos[0];
            int y = pos[1];
            while (y > 0 && x > 0)
            {
                y--;
                x--;
                if (Cells[x, y].state == side && ToFind == 'e' || Cells[x, y].state == 'e' && ToFind != 'e')
                    break;
                if (Cells[x, y].state == ToFind)
                {
                    res.Add(new int[] { x, y });
                    break;
                }
            }
            return res;
        }
        public List<int[]> CheckUp(int[] pos, char side, char ToFind)
        {
            List<int[]> res = new List<int[]>();
            int y = pos[1];
            while (y > 0)
            {
                y--;
                if (Cells[pos[0], y].state == side && ToFind == 'e' || Cells[pos[0], y].state == 'e' && ToFind != 'e')
                    break;
                if (Cells[pos[0], y].state == ToFind)
                {
                    res.Add(new int[] { pos[0], y });
                    break;
                }
            }
            return res;
        }
        public List<int[]> CheckUpRight(int[] pos, char side, char ToFind)
        {
            List<int[]> res = new List<int[]>();
            int x = pos[0];
            int y = pos[1];
            while (y > 0 && x < Cells.GetLength(0) - 1)
            {
                y--;
                x++;
                if (Cells[x, y].state == side && ToFind == 'e' || Cells[x, y].state == 'e' && ToFind != 'e')
                    break;
                if (Cells[x, y].state == ToFind)
                {
                    res.Add(new int[] { x, y });
                    break;
                }
            }
            return res;
        }
    }
}
