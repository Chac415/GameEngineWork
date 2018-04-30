using Engine.BackGround;
using Engine.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ProjectHastings.Buttons;
using Engine.Buttons;
using Engine.Service_Locator;
using Engine.Managers;
using Engine.Input_Managment;

namespace ProjectHastings.Scenes
{
    class WinGame : IScene
    {

        IBackGrounds back;
        MouseState mouseState;

        ISoundManager sound = Locator.Instance.getProvider<SoundManager>() as ISoundManager;
        IInputManager input = Locator.Instance.getProvider<InputManager>() as IInputManager;
        ButtonManager buttons = Locator.Instance.getProvider<ButtonManager>() as ButtonManager;


        public WinGame(int ScreenWidth, int ScreenHeight)
        {

            back = new BackGrounds(ScreenWidth, ScreenHeight);
            input.AddMouseListener(OnNewMouseInput);
        }


        public void LoadContent(ContentManager Content)
        {
            back.Initialize("WinGameBackground" ,Content.Load<Texture2D>("WinGameBackground"));

        }

        public void update(GameTime gameTime)
        {
            buttons.Update();
            input.Update();
        }

        public virtual void OnNewMouseInput(object source, MouseEventData data)
        {
            mouseState = data._newMouse;

            if (mouseState.LeftButton == ButtonState.Pressed && buttons.Buttons["ExitButton"].HitBox.Contains(mouseState.Position))
            {
                buttons.Buttons["ExitButton"].click();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            back.Draw(spriteBatch);
            buttons.Buttons["ExitButton"].Draw(spriteBatch);
        }
    }
}
