using Engine.BackGround;
using Engine.Interfaces;
using Engine.Managers;
using Engine.Service_Locator;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Engine.Input_Managment;
using ProjectHastings.Buttons;

namespace ProjectHastings.Scenes
{
    class GameOver : IScene
    {
        IButton StartBut, ExitBut;
        IBackGrounds back;
        MouseState mouseState;
        ContentManager Content;
        ISceneManager scn;

        ISoundManager sound = Locator.Instance.getProvider<SoundManager>() as ISoundManager;
        IInputManager input = Locator.Instance.getProvider<InputManager>() as IInputManager;

        public GameOver(int ScreenWidth, int ScreenHeight, ContentManager content, ISceneManager scene)
        {
            Content = content;
            scn = scene;
            back = new BackGrounds(ScreenWidth, ScreenHeight);
            StartBut = new StartButton();
            ExitBut = new ExitButton();
            input.AddMouseListener(OnNewMouseInput);
        }


        public void LoadContent()
        {
            sound.Initialize("MyHeartWillGoOn" ,Content.Load<SoundEffect>("MyHeartWillGoOn"));
            sound.CreateInstance();
            sound.Playsnd("MyHeartWillGoOn", 1.0f, true);
            ExitBut.Initialize(Content.Load<Texture2D>("Exit Button"), new Vector2(350, 300));

            back.Initialize("LoseGameBackground" ,Content.Load<Texture2D>("LoseGameBackground"));

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
