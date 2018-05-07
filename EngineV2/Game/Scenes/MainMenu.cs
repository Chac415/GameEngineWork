using System.Collections.Generic;
using System.Linq;
using Engine.BackGround;
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
        ContentManager Content;
        ISceneManager scn;

        ISoundManager sound = Locator.Instance.getProvider<SoundManager>() as ISoundManager;
        IInputManager input = Locator.Instance.getProvider<InputManager>() as IInputManager;
        public IDictionary<string, IButton> buttons = new Dictionary<string, IButton>();

        public MainMenu(int ScreenWidth, int ScreenHeight, ContentManager content, ISceneManager scene)
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
            sound.Initialize("MainMenuMusic" ,Content.Load<SoundEffect>("MainMenuMusic"));
            sound.CreateInstance();
            sound.Playsnd("MainMenuMusic", 1.0f, true);

            back.Initialize("Menu" ,Content.Load<Texture2D>("MainMenuBackground"));

            StartBut.Initialize(Content.Load<Texture2D>("Start Button"), new Vector2(25, 400));
            ExitBut.Initialize(Content.Load<Texture2D>("Exit Button"), new Vector2(25, 500));

            buttons.Add("StartButton", StartBut);
            buttons.Add("ExitButton", ExitBut);


        }

        public virtual void OnNewMouseInput(object source, MouseEventData data)
        {
            mouseState = data._newMouse;

            if (mouseState.LeftButton == ButtonState.Pressed && buttons["StartButton"].HitBox.Contains(mouseState.Position))
            {
                sound.Stopsnd("MainMenuMusic");
                scn.ChangeScene("Level1");
            }
            if (mouseState.LeftButton == ButtonState.Pressed && buttons["ExitButton"].HitBox.Contains(mouseState.Position))
            {
                scn.Exit();
            }

        }

        public void update(GameTime gameTime)
        {

            input.Update();

            IList<IButton> butts = buttons.Values.ToList();
            foreach (IButton button in butts)
            {
                button.update();
            }
            
  
        }

        public void Draw(SpriteBatch spriteBatch)
        {
                back.Draw(spriteBatch);
                IList<IButton> butts = buttons.Values.ToList();
                foreach (IButton button in butts)
                {
                button.Draw(spriteBatch);
                }
        }
    }
}
