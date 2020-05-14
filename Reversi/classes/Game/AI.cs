using Reversi.classes.Game;
using System.Collections.Generic;

namespace Reversi.classes.Game
{
    static class AI
    {
        private static List<int[]> Score1Zone;
        private static List<int[]> Score2Zone;
        private static List<int[]> Score3Zone;
        private static List<int[]> Score4Zone;
        private static List<int[]> Score5Zone;

        private struct Node
        {
            public double Value;
            public int[] Pos;
            public Node(double Value, int[] Pos)
            {
                this.Value = Value;
                this.Pos = Pos;
            }
        }

        public static void Load(int w, int h)
        {

            LoadZones(w, h);
        }

        public static int[] DetermineTurn(Field field, int depth, char side)
        {
            var opposite = 'w';
            if (side == 'w')
            {
                opposite = 'b';
            }

            var res = new Node(double.NegativeInfinity, new int[] { -1, -1 });

            var Possible = field.FindChecker(side);
            var alpha = Double.NegativeInfinity;
            var beta = Double.PositiveInfinity;

            res.Pos = Possible[0];
            foreach (int[] item in Possible)
            {
                var BoardCopy = field.Copy();
                BoardCopy.OnTurn(side, item);
                var node = Minimax(BoardCopy, depth, alpha, beta, opposite, side, item);
                if (node.Value > res.Value)
                {
                    res.Value = node.Value;
                    res.Pos = node.Pos;
                }

                alpha = Math.Max(alpha, res.Value);
                if (alpha >= beta)
                {
                    break;
                }
            }
            return res.Pos;
        }

        private static Node Minimax(Field field, int depth, double alpha, double beta, char side, char maximizing, int[] pos = null)
        {
            var opposite = 'w';
            if (side == 'w')
            {
                opposite = 'b';
            }
            if (depth == 0 || field.IsTerminal())
            {
                return new Node(Evaluate(field, maximizing), pos);
            }
            if (side == maximizing)
            {
                var value = double.NegativeInfinity;
                var Possible = field.FindChecker(side);
                foreach (int[] item in Possible)
                {
                    var BoardCopy = field.Copy();
                    BoardCopy.OnTurn(side, item);
                    var node = Minimax(BoardCopy, depth - 1, alpha, beta, opposite, maximizing);
                    value = Math.Max(value, node.Value);
                    alpha = Math.Max(alpha, value);
                    if (alpha >= beta)
                    {
                        break;
                    }
                }
                return new Node(value, pos);
            }
            else
            {
                var value = double.PositiveInfinity;
                var Possible = field.FindChecker(side);
                foreach (int[] item in Possible)
                {
                    var BoardCopy = field.Copy();
                    BoardCopy.OnTurn(side, item);
                    var node = Minimax(BoardCopy, depth - 1, alpha, beta, opposite, maximizing);
                    value = Math.Min(value, node.Value);
                    beta = Math.Min(beta, value);
                    if (alpha >= beta)
                    {
                        break;
                    }
                }
                return new Node(value, pos);
            }
        }

        private static double Evaluate(Field Board, char side)
        {
            double value = 0;
            if (Board.IsTerminal())
            {
                var opposite = 'w';
                if (side == 'w')
                {
                    opposite = 'b';
                }
                if (Board.Count(side) > Board.Count(opposite))
                {
                    return Double.PositiveInfinity;
                }
                if (Board.Count(side) < Board.Count(opposite))
                {
                    return Double.NegativeInfinity;
                }
                if (Board.Count(side) == Board.Count(opposite))
                {
                    return 1000;
                }
            }

            foreach (int[] item in Score1Zone)
            {
                if (Board.Cells[item[0], item[1]].state == side)
                {
                    value += 1;
                }
            }
            foreach (int[] item in Score2Zone)
            {
                if (Board.Cells[item[0], item[1]].state == side)
                {
                    value += 2*2;
                }
            }
            foreach (int[] item in Score3Zone)
            {
                if (Board.Cells[item[0], item[1]].state == side)
                {
                    value += 3*3;
                }
            }
            foreach (int[] item in Score4Zone)
            {
                if (Board.Cells[item[0], item[1]].state == side)
                {
                    value += 4*4;
                }
            }
            foreach (int[] item in Score5Zone)
            {
                if (Board.Cells[item[0], item[1]].state == side)
                {
                    value += 5*5;
                }
            }
            return value;
        }

        private static void LoadZones(int w, int h)
        {
            Score5Zone = new List<int[]>();
            Score4Zone = new List<int[]>();
            Score3Zone = new List<int[]>();
            Score2Zone = new List<int[]>();
            Score1Zone = new List<int[]>();
            //score 5
            Score5Zone.Add(new int[] { 0, 0 });
            Score5Zone.Add(new int[] { 0, h - 1 });
            Score5Zone.Add(new int[] { w - 1, 0 });
            Score5Zone.Add(new int[] { w - 1, h - 1 });

            //score 4
            for (int i = 0; i < w - 4; i++)
            {
                Score4Zone.Add(new int[] { 2 + i, 0 });
                Score4Zone.Add(new int[] { 2 + i, h - 1 });
            }
            for (int i = 0; i < w - 4; i++)
            {
                Score4Zone.Add(new int[] { 0, 2 + i });
                Score4Zone.Add(new int[] { w - 1, 2 + i });
            }

            //score 3
            for (int i = 0; i < w - 4; i++)
            {
                for (int j = 0; j < h - 4; j++)
                {
                    Score3Zone.Add(new int[] { 2 + i, 2 + j });
                }
            }

            //score 2
            for (int i = 0; i < w - 4; i++)
            {
                Score2Zone.Add(new int[] { 2 + i, 1 });
                Score2Zone.Add(new int[] { 2 + i, h - 2 });
            }
            for (int i = 0; i < w - 4; i++)
            {
                Score2Zone.Add(new int[] { 1, 2 + i });
                Score2Zone.Add(new int[] { w - 2, 2 + i });
            }

            //score 1
            Score1Zone.Add(new int[] { 0, 1 });
            Score1Zone.Add(new int[] { 1, 1 });
            Score1Zone.Add(new int[] { 1, 0 });

            Score1Zone.Add(new int[] { w - 2, h - 2 });
            Score1Zone.Add(new int[] { w - 1, h - 2 });
            Score1Zone.Add(new int[] { w - 2, h - 1 });

            Score1Zone.Add(new int[] { 1, h - 2 });
            Score1Zone.Add(new int[] { 1, h - 1 });
            Score1Zone.Add(new int[] { 0, h - 2 });

            Score1Zone.Add(new int[] { w - 2, 1 });
            Score1Zone.Add(new int[] { w - 1, 1 });
            Score1Zone.Add(new int[] { w - 2, 0 });
        }

        public static void Draw()
        {
            int x = Love.Graphics.GetWidth() / 2; int y = Love.Graphics.GetHeight() / 2;
            int w = (int)Math.Round(Love.Graphics.GetWidth() * 0.15); int h = (int)Math.Round(Love.Graphics.GetHeight() * 0.1);
            Love.Graphics.SetColor(Love.Color.FromRGBA(255, 0, 0, 100)); 
            foreach (int[] item in Score1Zone)
            {
                Love.Graphics.Rectangle(Love.DrawMode.Fill, x - h * (-3 + item[0]), y - h * (-3 + item[1]), h, h);
            }
            Love.Graphics.SetColor(Love.Color.FromRGBA(0, 0, 255, 100));
            foreach (int[] item in Score2Zone)
            {
                Love.Graphics.Rectangle(Love.DrawMode.Fill, x - h * (-3 + item[0]), y - h * (-3 + item[1]), h, h);
            }
            Love.Graphics.SetColor(Love.Color.FromRGBA(0, 255, 0, 100));
            foreach (int[] item in Score3Zone)
            {
                Love.Graphics.Rectangle(Love.DrawMode.Fill, x - h * (-3 + item[0]), y - h * (-3 + item[1]), h, h);
            }
            Love.Graphics.SetColor(Love.Color.FromRGBA(0, 255, 255, 100));
            foreach (int[] item in Score4Zone)
            {
                Love.Graphics.Rectangle(Love.DrawMode.Fill, x - h * (-3 + item[0]), y - h * (-3 + item[1]), h, h);
            }
            Love.Graphics.SetColor(Love.Color.FromRGBA(255, 255, 0, 100));
            foreach (int[] item in Score5Zone)
            {
                Love.Graphics.Rectangle(Love.DrawMode.Fill, x - h * (-3 + item[0]), y - h * (-3 + item[1]), h, h);
            }
        }
    }
}
