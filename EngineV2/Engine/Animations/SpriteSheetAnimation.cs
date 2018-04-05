﻿using Engine.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Engine.Animations
{
    public class SpriteSheetAnimation
    {

        #region Variables
        //Create Intergers called _width and _height that will store the height and width of the animation
        public static int _width, _height;
        //Create Intergers called CurrentFrame and totalFrames that will store.
        private int _currentFrame, _totalFrames;
        //Create an Interger called _timeSinceLatFrame that will store how long it has been since last frame, set it to 0.
        private int _timeSinceLastFrame = 0;
        //Create an Interger called _millisecondsPerFrame that stores how many milliseconds there will be between each frame, set it to 200.
        private int _millisecondsPerFrame = 200;
        //Create Intergers called _rows, _columns that will store how many rows and columns are in the spritesheet.
        private int _rows, _columns;
        //Create Intergers called _row, _column that will store which row and column is needed for a specific animation.
        private int _row, _column;
        //Create a variable of type IEntity called entity that will store object of type IEntity.
        public IEntity entity;
        //Create a variable of type Texture2D called SpriteSheet which holds the entity texture/spritesheet
        public Texture2D SpriteSheet;
        //Create a variable of type Rectangle called sourceRectangle which will store information on what part of the spritesheet to look at for the animation.
        // Create a variable of type Rectangle called destinationRectangle which will store information on where sourceRectangle will be created.
        private Rectangle sourceRectangle, destinationRectangle;

        #endregion


        /// <summary>
        /// Create a method called Initialize that gets passed variables of types IEntity, INTS and then stores then with in other variables.
        /// </summary>
        public void Initialize(IEntity ent, int rows, int columns)
        {
            entity = ent;
            _columns = columns;
            _rows = rows;
            SpriteSheet = ent.Texture;
            //Assign _width variable with the correct width of one of the images with in the spritesheet. 
            //Do this by getting the texture width and divide it by the amount of columns the spritesheet has.
            _width = SpriteSheet.Width / _columns;
            //Assign _height variable with the correct height of one of the images with in the spritesheet. 
            //Do this by getting the texture height and divide it by the amount of rows the spritesheet has.
            _height = SpriteSheet.Height / _rows;

        }


        /// <summary>
        /// Create a method called Update that gets passed a variable of type GameTime.
        /// This method will update the animation frames. 
        /// </summary>
        public void Update(GameTime gameTime)
        {
            //Set _timeSinceLastFrame += milliseconds generated by gametime.
            _timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;

            //Create an If the check to see if _timeSinceLastFrame > _millisecondsPerFrame.
            if (_timeSinceLastFrame >= _millisecondsPerFrame)
            {
                //Reset the Time since last frame
                _timeSinceLastFrame = 0;


                _currentFrame++;

                //Create an If Statment to check if the current frames is == to the total frames (_currentFrame == _totalFrames) 
                //if so set current frames to 0.
                if (_currentFrame == _totalFrames)
                {
                    _currentFrame = 0;
                }
            }
        }

        /// <summary>
        /// Create a method called Draw that gets passed a variable of type SpriteBatch.
        /// This method will draw the animation frames. 
        /// </summary>
        public void Draw(SpriteBatch spriteBatch)
        {
            //Assign _row variable the row that wants to be shown in the animation.
            _row = entity.getRows();
            //Assign _column variable the columns that are going to be shown in the animation.
            _column = _currentFrame % _columns;

            //Create a new rectangle and assign it to the sourceRectangle variable.
            //The rectangle will hold all the information it need to create a rectangle around the image on the spritesheet.
            sourceRectangle = new Rectangle(_width * _column, _height * _row, _width, _height);
            //Create a new rectangle and assign it to the destinationRectangle variable.
            //The rectangle will hold all the information it needs for where the animations is going to be created.
            destinationRectangle = new Rectangle((int)entity.Position.X, (int)entity.Position.Y, _width, _height);
            //Call the Draw method from the spritebatch method and assign it the correct parameters need to draw the animations.
            spriteBatch.Draw(entity.Texture, destinationRectangle, sourceRectangle, Color.White);

        }
    }
}