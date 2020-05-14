using Love;
using Reversi.classes;
using System;

namespace _2048.classes
{
    public abstract class RadioButton : InterfaceElement
    {
        public string[] text;
        public int active;
        private int x, y, w, h;
        private int gap;
        protected Font font = Style.RadioButtonFont;

        public RadioButton(int x, int y, int w, int h)
        {
            this.x = x; this.y = y; this.h = h; this.w = w;
            this.active = 0;
            gap = this.h / 10;
            this.font = Graphics.NewFont(this.h / 2);
        }

        public override void Draw()
        {
            if (!Hidden)
            {
                Graphics.SetColor(Style.ButtonColor);
                Graphics.Rectangle(DrawMode.Fill, x, y, w, h * 3, 10, 10);
                int left = 25;
                y += gap;
                for (int i = 0; i < text.Length; i++)
                {
                    Graphics.SetColor(0, 0, 0, 1);
                    Graphics.Circle(DrawMode.Fill, this.x + left, this.y + i * (h + gap) + this.font.GetHeight() / 2, this.h / 3 + 2);

                    Graphics.SetColor(1, 1, 1, 1);
                    Graphics.Circle(DrawMode.Fill, this.x + left, this.y + i * (h + gap) + this.font.GetHeight() / 2, this.h / 3);
                    Graphics.SetColor(0, 0, 0, 1);
                    if (i == this.active)
                    {
                        Graphics.Circle(DrawMode.Fill, this.x + left, this.y + i * (h + gap) + this.font.GetHeight() / 2, this.h / 3 - 2);
                    }
                    Graphics.SetFont(this.font);
                    Graphics.Printf(this.text[i], this.x + left * 2, this.y + i * (h + gap), w, AlignMode.Left);
                }
                y -= gap;
            }
        }

        public override void Update(float dt)
        {
        }

        protected abstract void OnClick();

        public override void MousePressed(float x, float y)
        {
            if (!Hidden)
            {
                if (x >= this.x & x <= this.x + w & y >= this.y & y <= this.y + (this.h + gap) * this.text.Length)
                {
                    for (int i = 0; i < this.text.Length; i++)
                    {
                        if (y >= this.y + (this.h + gap) * i & y <= this.y + (this.h + gap) * (i + 1))
                        {
                            this.active = i;
                            OnClick();
                            return;
                        }
                    }
                }
            }
        }

        public void resize()
        {
        }
    }
}
