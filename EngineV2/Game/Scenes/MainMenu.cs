using System.Collections.Generic;
using Engine.BackGround;
using Engine.Buttons;
using Engine.Interfaces;
using Engine.Managers;
using Engine.Service_Locator;
using Engine.Input_Managment;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ProjectHastings.Buttons;

namespace ProjectHastings.Scenes
{
    class MainMenu : IScene
    {
        IButton StartBut, ExitBut;
        IBackGrounds back;
        MouseState mouseState;

        ISoundManager sound = Locator.Instance.getProvider<SoundManager>() as ISoundManager;
        IInputManager input = Locator.Instance.getProvider<InputManager>() as IInputManager;
        ButtonManager buttons = Locator.Instance.getProvider<ButtonManager>() as ButtonManager;

        public MainMenu(int ScreenWidth, int ScreenHeight)
        {
            
            back = new BackGrounds(ScreenWidth, ScreenHeight);
            StartBut = new StartButton();
            ExitBut = new ExitButton();
            input.AddMouseListener(OnNewMouseInput);

        }

        public void LoadContent(ContentManager Content)
        {
            sound.Initialize("MainMenuMusic" ,Content.Load<SoundEffect>("MainMenuMusic"));
            sound.CreateInstance();


            back.Initialize("Menu" ,Content.Load<Texture2D>("MainMenuBackground"));

            StartBut.Initialize(Content.Load<Texture2D>("Start Button"), new Vector2(25, 400));
            ExitBut.Initialize(Content.Load<Texture2D>("Exit Button"), new Vector2(25, 500));

            buttons.AddButton("StartButton", StartBut);
            buttons.AddButton("ExitButton", ExitBut);


        }

        public virtual void OnNewMouseInput(object source, MouseEventData data)
        {
            mouseState = data._newMouse;

            if (mouseState.LeftButton == ButtonState.Pressed && buttons.Buttons["StartButton"].HitBox.Contains(mouseState.Position))
            {
                buttons.Buttons["StartButton"].click();
            }
            if (mouseState.LeftButton == ButtonState.Pressed && buttons.Buttons["ExitButton"].HitBox.Contains(mouseState.Position))
            {
                buttons.Buttons["ExitButton"].click();
            }
        }

        public void update(GameTime gameTime)
        {

            input.Update();
            buttons.Update();
  
        }

        public void Draw(SpriteBatch spriteBatch)
        {
                back.Draw(spriteBatch);
                buttons.Draw(spriteBatch);
        }
    }
}
