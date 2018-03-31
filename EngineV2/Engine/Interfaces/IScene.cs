﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace Engine.Interfaces
{
    public interface IScene
    {
        void LoadContent(ContentManager Content);
        void update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
    }
}