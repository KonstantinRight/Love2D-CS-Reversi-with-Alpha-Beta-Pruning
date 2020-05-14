using Love;
using Reversi.classes.Interface.Buttons;
using Reversi.classes.Interface.Labels;
using Reversi.classes.misc;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Reversi.classes.Game
{
    static class GameScene
    {
        private const int StarAmount = 1000;
        public static float TransitGapTime = (float)0.25;
        public static List<InterfaceElement> Interface = new List<InterfaceElement>();
        static List<Star> Stars = new List<Star>();
        static float StarTimer;
        static Field Board;
        static char turn = 'w';
        static short end = 0;
        static bool coloring = false;
        static float[] color;
        static float TransitTime;
        static int[] chosen;
        public static bool Bot = false;

        public static void Load()
        {
            /*test.Add(new int[] { 3,2});
            test.Add(new int[] { 2,2});
            test.Add(new int[] { 1,2});
            test.Add(new int[] { 2,4});
            test.Add(new int[] { 5,5});
            test.Add(new int[] { 5,4});
            test.Add(new int[] { 2,5});*/
            
            int x = Graphics.GetWidth() / 2; int y = Graphics.GetHeight() / 2;
            int w = (int)Math.Round(Graphics.GetWidth() * 0.15); int h = (int)Math.Round(Graphics.GetHeight() * 0.1);
            for (int i = 0; i < StarAmount; i++)
            {
                Stars.Add(new Star(Program.rnd.Next(Graphics.GetWidth()) + 1, Program.rnd.Next(Graphics.GetHeight()) + 1));
            }
            Reset();

            int gap = 10;
            Interface.Add(new ButtonStart(x + h * -4 - gap - w, y - h * 4, w, h));
            Interface.Add(new AnimationText(x + h * -4 - gap - w, y - h * 3 + gap, w, h));
            Interface.Add(new RadioButtonAnimation(x + h * -4 - gap - w, y - h * 2 + gap * 2, w, h / 3));
            Interface.Add(new ModeText(x + h * -4 - gap - w, y - h * 1 + gap * 3, w, h));
            Interface.Add(new RadioButtonMode(x + h * -4 - gap - w, y - h * 0 + gap * 4, w, h / 3));
            Interface.Add(new DifficultyText(x + h * -4 - gap - w, y - h * -1 + gap * 5, w, h));
            Interface.Add(new RadioButtonDifficulty(x + h * -4 - gap - w, y - h * -2 + gap * 6, w, h / 3));

            Interface.Add(new StatusText(x + h * -4, y - h * 5, Math.Abs(x - h * (-3) - (x - h * (-3 + Board.Cells.GetLength(0)) ) ), h));

            AI.Load(Board.Cells.GetLength(0), Board.Cells.GetLength(1));
        }

        public static void Reset()
        {
            int x = Graphics.GetWidth() / 2; int y = Graphics.GetHeight() / 2;
            int h = (int)Math.Round(Graphics.GetHeight() * 0.1);
            Board = new Field(x, y, h);
            Board.Cells[4, 4].state = 'w';
            Board.Cells[3, 3].state = 'w';
            Board.Cells[3, 4].state = 'b';
            Board.Cells[4, 3].state = 'b';
            StatusText status = (StatusText)Interface.Find(item => item.GetType() == typeof(StatusText));
            if (status != null)
                status.text = "Reversi";
        }

        public static void Update(float dt)
        {
            foreach(InterfaceElement item in Interface)
            {
                item.Update(dt);
            }
            foreach(Star item in Stars)
            {
                item.Update(dt);
            }

            if (StarTimer < Timer.GetTime())
            {
                int chosen = Program.rnd.Next(0, StarAmount);
                Stars[chosen].Hide();

                StarTimer = (float)(Timer.GetTime() + Program.rnd.Next(1,11)/10);
            }
            Board.Update(dt);
            Transit();
        }

        public static void Draw()
        {
            foreach(Star item in Stars)
            {
                item.Draw();
            }

            Board.Draw();
            if (coloring)
            {
                var ToPaint = Board.FindPainted(chosen, turn);
                foreach (int[] item in ToPaint)
                {
                    Board.Cells[item[0], item[1]].Draw(color);
                }
            }

            foreach (InterfaceElement item in Interface)
            {
                item.Draw();
            }
            //AI.Draw();
        }

        public static void Transit()
        {
            if (coloring)
            {
                if (turn == 'w')
                    color = new float[] { 1-(TransitTime - Timer.GetTime()) / TransitGapTime, 1-(TransitTime - Timer.GetTime()) / TransitGapTime, 1-(TransitTime - Timer.GetTime()) / TransitGapTime, 1};
                else
                    color = new float[] { (TransitTime - Timer.GetTime()) / TransitGapTime, (TransitTime - Timer.GetTime()) / TransitGapTime, (TransitTime - Timer.GetTime()) / TransitGapTime, 1 };
                if (Timer.GetTime() >= TransitTime)
                {
                    coloring = false;
                    OnTurn(chosen);
                }
            }
        }

        public static void OnTurn(int[] Chosen = null)
        {
            RadioButtonMode mode = (RadioButtonMode)GameScene.Interface.Find(item => item.GetType() == typeof(RadioButtonMode));
            if (mode.active == 1)
            {
                Bot = !Bot;
            }
            if (mode.active == 2)
            {
                Bot = true;
            }

            if (Chosen != null)
            {
                var ToPaint = Board.FindPainted(Chosen, turn);
                foreach(int[] item in ToPaint)
                {
                    Board.Cells[item[0], item[1]].state = turn;
                }
            }

            Board.ClearPossible();
            if (turn == 'w')
            {
                turn = 'b';
                StatusText status = (StatusText)Interface.Find(item => item.GetType() == typeof(StatusText));
                status.text = "Turn: Black";
            }
            else
            {
                turn = 'w';
                StatusText status = (StatusText)Interface.Find(item => item.GetType() == typeof(StatusText));
                status.text = "Turn: White";
            }
            var Possible = Board.FindChecker(turn);

            foreach (int[] item in Possible)
            {
                Board.Cells[item[0], item[1]].state = 'g';
            }

            if(Possible.Count == 0)
            {
                end++;
                if (end == 1)
                    OnTurn();
                StatusText status = (StatusText)Interface.Find(item => item.GetType() == typeof(StatusText));
                
                if (end == 2)
                {
                    int black = Board.Count('b');
                    int white = Board.Count('w');
                    if (black > white)
                        status.text = "Black won. White: " + white + ", Black: " + black;
                    if (black < white)
                        status.text = "White won. White: " + white + ", Black: " + black;
                    if (black == white)
                        status.text = "Draw. White: " + white + ", Black: " + black;
                }
                return;
            }
            end = 0;

            if (Bot)
            {
                Board.ClearPossible();
                TransitTime = Timer.GetTime() + TransitGapTime;
                coloring = true;

                RadioButtonDifficulty rb = (RadioButtonDifficulty)GameScene.Interface.Find(item => item.GetType() == typeof(RadioButtonDifficulty));
                chosen = AI.DetermineTurn(Board, (1 + rb.active) * 2, turn);
            }
        }

        public static void KeyPressed()
        {

        }

        public static void MousePressed(float x, float y, int button, bool isTouch)
        {
            if (button == 0)
            {
                foreach (InterfaceElement item in Interface)
                {
                    item.MousePressed(x, y);
                }
                
                var Chosen = Board.MousePressed(x, y, turn);
                if (Chosen != null)
                {

                    TransitTime = Timer.GetTime() + TransitGapTime;
                    coloring = true;

                    chosen = Chosen;
                    //OnTurn(Chosen);
                }
                //if (end == 1)
                //{
                //OnTurn(); //stackoverflow exception if in OnTurn();
                //}
            }
        }
    }
}
