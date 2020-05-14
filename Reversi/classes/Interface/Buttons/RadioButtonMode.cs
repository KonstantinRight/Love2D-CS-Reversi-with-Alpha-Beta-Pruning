using _2048.classes;

namespace Reversi.classes.Interface.Buttons
{
    class RadioButtonMode : RadioButton
    {
        public RadioButtonMode(int x, int y, int w, int h) : base(x, y, w, h)
        {
            text = new string[] { "Player vs Player", "Player vs AI", "AI vs AI" };
            active = 1;
        }

        protected override void OnClick()
        {
        }
    }
}
