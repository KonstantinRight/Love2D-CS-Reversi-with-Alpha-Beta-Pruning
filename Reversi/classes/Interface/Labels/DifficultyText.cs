using _2048.classes.Interface;

namespace Reversi.classes.Interface.Labels
{
    public class DifficultyText : Text
    {
        public DifficultyText(int x, int y, int w, int h) : base(x, y, w, h)
        {
            text = "Difficulty (Depth)";
            font = Style.DifficultyTextFont;
        }
    }
}
