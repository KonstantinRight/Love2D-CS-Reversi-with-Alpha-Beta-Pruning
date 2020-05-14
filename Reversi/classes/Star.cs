using Love;
using System;

namespace Reversi.classes
{
    class Star
    {
        public int x, y;
        public float timer;
        bool visible = true;

        public Star(int x, int y)
        {
            this.x = x;
            this.y = y;
            timer = Timer.GetTime();
        }

        public void Draw()
        {
            if (visible)
            {
                Graphics.SetColor(1, 1, 1, 1);
                Graphics.Rectangle(DrawMode.Fill, x, y, 1, 1);
            }
        }

        public void Update(float dt)
        {
            if (!visible)
            {
                if (Timer.GetTime() > timer)
                {
                    visible = true;
                }
            }
        }

        public void Hide()
        {
            visible = false;
            timer = (float)(Timer.GetTime() + 0.1);
        }
    }
}
