using _2048.classes;
using Reversi.classes.Game;

namespace Reversi.classes.Interface.Buttons
{
    class RadioButtonAnimation : RadioButton
    {
        public RadioButtonAnimation(int x, int y, int w, int h) : base(x, y, w, h)
        {
            text = new string[] { "On", "Off" };
        }

        protected override void OnClick()
        {
            if (active == 0)
                GameScene.TransitGapTime = (float)0.25;
            else
                GameScene.TransitGapTime = 0;
        }
    }
}
