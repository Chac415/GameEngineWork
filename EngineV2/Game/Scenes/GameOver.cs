﻿using Engine.BackGround;
using Engine.Interfaces;
using Engine.Managers;
using Engine.Service_Locator;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ProjectHastings.Buttons;

namespace ProjectHastings.Scenes
{
    class GameOver : IScene
    {

        IButton ExitBut;
        IBackGrounds back;
        MouseState mouseinput;
        Point mousePosition;

        ISoundManager sound = Locator.Instance.getProvider<SoundManager>() as ISoundManager;

        public GameOver(int ScreenWidth, int ScreenHeight)
        {

            back = new BackGrounds(ScreenWidth, ScreenHeight);
            ExitBut = new ExitButton();
        }


        public void LoadContent(ContentManager Content)
        {
            sound.Initialize("MyHeartWillGoOn" ,Content.Load<SoundEffect>("MyHeartWillGoOn"));
            sound.CreateInstance();


            back.Initialize("LoseGameBackground" ,Content.Load<Texture2D>("LoseGameBackground"));
            ExitBut.Initialize(Content.Load<Texture2D>("Exit Button"), new Vector2(355, 300));

        }

        public void update(GameTime gameTime)
        {
            ExitBut.update();
            mouseinput = Mouse.GetState();
            mousePosition = new Point(mouseinput.X, mouseinput.Y);

            sound.Playsnd("MyHeartWillGoOn", 1.0f);

            if (ExitBut.HitBox.Contains(mousePosition) && mouseinput.LeftButton == ButtonState.Pressed)
            {
                ExitBut.click();
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            back.Draw(spriteBatch);
            ExitBut.Draw(spriteBatch);
        }
    }
}
