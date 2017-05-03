﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using EngineV2.Buttons;
using EngineV2.BackGround;
using EngineV2.Managers;

namespace EngineV2.Scenes
{
    class GameOver : IScene
    {

        IButton ExitBut;
        IBackGrounds back;
        MouseState mouseinput;


        public GameOver()
        {

            back = new BackGrounds(900, 600);
            ExitBut = new ExitButton();
        }


        public void LoadContent(ContentManager Content)
        {
            back.Initialize(Content.Load<Texture2D>("LoseGame"));
            ExitBut.Initialize(Content.Load<Texture2D>("Exit Button"), new Vector2(355, 300));

        }

        public void update(GameTime gameTime)
        {
            ExitBut.update();
            mouseinput = Mouse.GetState();


            if (mouseinput.X > ExitBut.getHitbox().X && mouseinput.Y > ExitBut.getHitbox().Y && mouseinput.LeftButton == ButtonState.Pressed)
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
