using Love;
using Reversi.classes;
using Reversi.classes.Game;
using System;

namespace Reversi
{
    class Program : Scene
    {
        public static Random rnd = new Random();

        static void Main(string[] args)
        {
            Boot.Init(new BootConfig
            {
                WindowWidth = 1280,
                WindowHeight = 720,
                WindowTitle = "Reversi",
                WindowResizable = false
            });
            Boot.Run(new Program());
        }

        public override void Load()
        {
            base.Load();
            Style.Load();
            GameScene.Load();
        }

        public override void Update(float dt)
        {
            base.Update(dt);
            GameScene.Update(dt);
        }

        public override void Draw()
        {
            base.Draw();
            GameScene.Draw();
        }

        public override void MousePressed(float x, float y, int button, bool isTouch)
        {
            base.MousePressed(x, y, button, isTouch);
            GameScene.MousePressed(x, y, button, isTouch);
        }
    }
}
