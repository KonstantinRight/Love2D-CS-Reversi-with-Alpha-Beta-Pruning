using Love;
using System;
using System.Collections.Generic;

namespace Reversi.classes
{
    static class Style
    {
        public static Font ButtonFont;
        public static Font RadioButtonFont;
        public static Font TextFont;
        public static Font DifficultyTextFont;
        public static int CommonHeight;
        public static Color CellColor = Color.FromRGBA(200,200,200,200);
        public static Color BorderColor = Color.FromRGBA(30, 30, 30, 200);
        public static Color ButtonColor = Color.FromRGBA(200, 200, 200, 255);
        public static Color TextBackgroundColor = Color.FromRGBA(255, 255, 255, 50);

        public static void Load()
        {
            CommonHeight = (int)Math.Round(Graphics.GetHeight() * 0.1);
            ButtonFont = Graphics.NewFont((int)(CommonHeight * 0.5));
            RadioButtonFont = Graphics.NewFont((int)(CommonHeight * 0.25));
            TextFont = ButtonFont;
            DifficultyTextFont = Graphics.NewFont((int)(CommonHeight * 0.5 / 1.5));
        }
    }
}
