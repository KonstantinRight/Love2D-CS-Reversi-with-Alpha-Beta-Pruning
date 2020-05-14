namespace Reversi.classes
{
    public abstract class InterfaceElement
    {
        public bool Hidden = false;
        public abstract void Update(float dt);
        public abstract void Draw();
        public abstract void MousePressed(float x, float y);
    }
}
