using _2048.classes.Interface;

namespace Reversi.classes.Interface.Labels
{
    public class AnimationText : Text
    {
        public AnimationText(int x, int y, int w, int h) : base(x, y, w, h)
        {
            text = "Animation";
        }
    }
}
