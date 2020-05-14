using Love;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi.classes.Game
{
    class Cell
    {
        public int x, y, s;
        public char state = 'e'; //"e", "w", "b", "g"

        public Cell(int x, int y, int s)
        {
            this.x = x;
            this.y = y;
            this.s = s;
        }

        public void Draw(float[] color = null)
        {
            Graphics.SetColor(Style.BorderColor);
            Graphics.Rectangle(DrawMode.Fill, x, y, s, s);

            float border = (float)(s * 0.05);
            Graphics.SetColor(Style.CellColor);
            Graphics.Rectangle(DrawMode.Fill, x + border, y + border, s - border * 2, s - border * 2);

            int cx = x + s / 2; int cy = y + s / 2;
            int cs = (int)(s - border * 2) / 2;
            if (color == null)
            {
                if (state == 'b')
                {
                    Graphics.SetColor(0, 0, 0, 1);
                    Graphics.Circle(DrawMode.Fill, cx, cy, cs);
                }
                if (state == 'w')
                {
                    Graphics.SetColor(1, 1, 1, 1);
                    Graphics.Circle(DrawMode.Fill, cx, cy, cs);
                }
                if (state == 'g')
                {
                    Graphics.SetColor(0, 1, 0, 1);
                    Graphics.Circle(DrawMode.Fill, cx, cy, cs / 2);
                }
            }
            else
            {
                Graphics.SetColor(color[0], color[1], color[2], color[3]);
                Graphics.Circle(DrawMode.Fill, cx, cy, cs);
            }
        }

        public bool MousePressed(float x, float y, char turn)
        {
            var res = false;
            if (state == 'g')
            {
                if (x >= this.x && x <= this.x + this.s && y >= this.y && y <= this.y + this.s)
                {
                    res = true;   
                    //OnClick();
                }
            }
            return res;
        }

        public void Update(float dt)
        {

        }
    }
}
