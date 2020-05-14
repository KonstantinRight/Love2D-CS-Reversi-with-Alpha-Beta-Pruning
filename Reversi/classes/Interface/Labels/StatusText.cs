using _2048.classes.Interface;

namespace Reversi.classes.Interface.Labels
{
    public class StatusText : Text
    {
        public StatusText(int x, int y, int w, int h) : base(x, y, w, h)
        {
            text = "Reversi";
        }
    }
}
