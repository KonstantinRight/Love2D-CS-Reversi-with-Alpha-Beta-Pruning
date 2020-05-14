using Love;
using Reversi.classes;

namespace _2048.classes.Interface
{
    public abstract class Text : InterfaceElement
    {
        private int x, y, w, h;
        public string text;
        protected Font font;
        public float[] color;
        public Text(int x, int y, int w, int h)
        {
            this.x = x; this.y = y; this.w = w; this.h = h;
            this.font = Style.TextFont;
        }

        public override void Draw()
        {
            if (!Hidden)
            {
                Graphics.SetColor(Style.TextBackgroundColor);
                Graphics.Rectangle(DrawMode.Fill, this.x, this.y, this.w, this.h, 10, 10);
                Graphics.SetColor(Color.White);
                Graphics.SetFont(this.font);
                Graphics.Printf(this.text, this.x, this.y + this.font.GetHeight() / 2, this.w, AlignMode.Center);
            }
        }

        public override void Update(float dt)
        {
        }

        public override void MousePressed(float x, float y)
        {
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
