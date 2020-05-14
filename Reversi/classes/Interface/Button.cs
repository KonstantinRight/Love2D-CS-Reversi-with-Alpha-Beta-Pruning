using Love;
using Reversi.classes;
using System;

namespace _2048.classes
{
    public abstract class Button : InterfaceElement
    {
        private int x, y, w, h;
        private bool highlighted = false;
        //private bool hidden = false;
        protected string text = "";
        protected Font font;
        public Color color = Style.ButtonColor;

        public Button(int x, int y, int w, int h)
        {
            this.x = x; this.y = y; this.w = w; this.h = h;
            this.font = Style.ButtonFont;
        }

        public abstract void clickEvent();

        public override void Update(float dt)
        {
            var pos = Mouse.GetPosition();
            if (pos.X >= this.x && pos.X <= this.x + this.w && pos.Y >= this.y && pos.Y <= this.y + this.h)
            {
                highlighted = true;
            }
            else
            {
                highlighted = false;
            }
        }

        public override void MousePressed(float x, float y)
        {
            if (x >= this.x && x <= this.x + this.w && y >= this.y && y <= this.y + this.h)
            {
                this.clickEvent();
            }
        }

        public override void Draw()
        {
            if (highlighted)
            {
                Graphics.SetColor(1, 1, 1, 1);
                float border = (float)(w * 0.0125);
                Graphics.Rectangle(DrawMode.Fill, this.x - border, this.y - border, this.w + border * 2, this.h + border * 2, 10, 10);
            }
            Graphics.SetColor(color);
            Graphics.Rectangle(DrawMode.Fill, this.x, this.y, this.w, this.h, 10, 10);
            Graphics.SetColor(0, 0, 0, 1);
            Graphics.SetFont(this.font);
            Graphics.Printf(this.text, this.x, this.y + (float)(font.GetHeight() / 2.5), this.w, AlignMode.Center);
        }

        /*public void resize()
        {
            var cellParams = Params.Inst.getParams();
            var cx = cellParams[0];
            var side = cellParams[2];
            var gap = side / 8;
            this.x = cx - 3 * (side + gap);
        }*/
    }
}
