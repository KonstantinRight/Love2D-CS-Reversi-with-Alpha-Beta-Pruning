using _2048.classes;
using Reversi.classes.Game;
using Reversi.classes.Interface.Labels;
using System;
using System.Collections.Generic;

namespace Reversi.classes.Interface.Buttons
{
    class ButtonStart : Button
    {
        public ButtonStart(int x, int y, int w, int h) : base(x, y, w, h)
        {
            text = "Start";
        }
        public override void clickEvent()
        {
            if (text == "Start")
            {
                text = "Reset";
                GameScene.OnTurn();
                RadioButtonMode mode = (RadioButtonMode)GameScene.Interface.Find(item => item.GetType() == typeof(RadioButtonMode));
                if (mode.active == 1)
                {
                    int r = Program.rnd.Next(1, 3);
                    if (r == 1)
                    {
                        GameScene.Bot = true;
                    }
                }
                var ToHide = GameScene.Interface.FindAll(item =>
                item.GetType() == typeof(RadioButtonMode) ||
                item.GetType() == typeof(RadioButtonDifficulty) ||
                item.GetType() == typeof(DifficultyText) ||
                item.GetType() == typeof(ModeText));
                foreach(InterfaceElement item in ToHide)
                {
                    item.Hidden = true;
                }
            }
            else if (text == "Reset")
            {
                text = "Start";
                GameScene.Reset();
                var ToUnhide = GameScene.Interface.FindAll(item =>
                item.GetType() == typeof(RadioButtonMode) ||
                item.GetType() == typeof(RadioButtonDifficulty) ||
                item.GetType() == typeof(DifficultyText) ||
                item.GetType() == typeof(ModeText));
                foreach (InterfaceElement item in ToUnhide)
                {
                    item.Hidden = false;
                }
            }
        }
    }
}