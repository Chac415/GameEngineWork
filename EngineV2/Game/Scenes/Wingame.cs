﻿using Engine.BackGround;
using Engine.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ProjectHastings.Buttons;

namespace ProjectHastings.Scenes
{
    class WinGame : IScene
    {

        IButton ExitBut;
        IBackGrounds back;
        MouseState mouseinput;
        Point mousePosition;


        public WinGame(int ScreenWidth, int ScreenHeight)
        {

            back = new BackGrounds(ScreenWidth, ScreenHeight);
            ExitBut = new ExitButton();
        }


        public void LoadContent(ContentManager Content)
        {
            back.Initialize("WinGameBackground" ,Content.Load<Texture2D>("WinGameBackground"));
            ExitBut.Initialize(Content.Load<Texture2D>("Exit Button"), new Vector2(355, 300));

        }

        public void update(GameTime gameTime)
        {
            ExitBut.update();
            mouseinput = Mouse.GetState();
            mousePosition = new Point(mouseinput.X, mouseinput.Y);


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
