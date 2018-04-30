using Engine.BackGround;
using Engine.Interfaces;
using Engine.Managers;
using Engine.Service_Locator;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Engine.Buttons;
using Engine.Input_Managment;

namespace ProjectHastings.Scenes
{
    class GameOver : IScene
    {
        IBackGrounds back;
        MouseState mouseState;

        ISoundManager sound = Locator.Instance.getProvider<SoundManager>() as ISoundManager;
        IInputManager input = Locator.Instance.getProvider<InputManager>() as IInputManager;
        ButtonManager buttons = Locator.Instance.getProvider<ButtonManager>() as ButtonManager;

        public GameOver(int ScreenWidth, int ScreenHeight)
        {
            back = new BackGrounds(ScreenWidth, ScreenHeight);
            input.AddMouseListener(OnNewMouseInput);
        }


        public void LoadContent(ContentManager Content)
        {
            sound.Initialize("MyHeartWillGoOn" ,Content.Load<SoundEffect>("MyHeartWillGoOn"));
            sound.CreateInstance();


            back.Initialize("LoseGameBackground" ,Content.Load<Texture2D>("LoseGameBackground"));

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
