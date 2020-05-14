using _2048.classes;

namespace Reversi.classes.Interface.Buttons
{
    class RadioButtonDifficulty : RadioButton
    {
        public RadioButtonDifficulty(int x, int y, int w, int h) : base(x, y, w, h)
        {
            text = new string[] { "Easy (2)", "Medium (4)", "Hard (6)" };
            active = 1;
        }

        protected override void OnClick()
        {
        }
    }
}
