using Engine.BackGround;
using Engine.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ProjectHastings.Buttons;
using Engine.Service_Locator;
using Engine.Managers;
using Engine.Input_Managment;

namespace ProjectHastings.Scenes
{
    class WinGame : IScene
    {
        IButton StartBut, ExitBut;
        IBackGrounds back;
        MouseState mouseState;
        ContentManager Content;
        ISceneManager scn;

        ISoundManager sound = Locator.Instance.getProvider<SoundManager>() as ISoundManager;
        IInputManager input = Locator.Instance.getProvider<InputManager>() as IInputManager;


        public WinGame(int ScreenWidth, int ScreenHeight, ContentManager content, ISceneManager scene)
        {

            back = new BackGrounds(ScreenWidth, ScreenHeight);
            StartBut = new StartButton();
            ExitBut = new ExitButton();
            Content = content;
            scn = scene;
            input.AddMouseListener(OnNewMouseInput);
        }


        public void LoadContent()
        {
            ExitBut.Initialize(Content.Load<Texture2D>("Exit Button"), new Vector2(350, 300));
            back.Initialize("WinBackground" ,Content.Load<Texture2D>("WinGameBackground"));
        }

        public void update(GameTime gameTime)
        {
            ExitBut.update();
            input.Update();
        }

        public virtual void OnNewMouseInput(object source, MouseEventData data)
        {
            mouseState = data._newMouse;

            if (mouseState.LeftButton == ButtonState.Pressed && ExitBut.HitBox.Contains(mouseState.Position))
            {
                scn.Exit();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            back.Draw(spriteBatch);
            ExitBut.Draw(spriteBatch);
        }
    }
}
