﻿using System;
using Engine.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Engine.State_Machines.Animations
{

    public class AnimationState<T> : IAnimationState<T>

    {
        //Int of Current Frame in Animation
        public int CurrentFrame { get; private set; }
        public int TotalFrames { get; set; }
        //In for the FrameRate of the Animation
        public float FrameRate { get; private set; }
        //float for the Speed of the Animation - <0 plays backwards >0 plays forwards
        public float AnimationSpeed { get; private set; }


        private Rectangle sourceRectangle, destinationRectangle;

        private readonly GameTime gameTime;

        private float timeSinceLastFrame = 0;
        private float millisecondsPerFrame = 200;

        //Animation Variables
        public Texture2D SpriteSheet { get; private set; }
        public IEntity Entity;
        public int TargetRow { get; private set; }
        public int Column { get; private set; }

        /// <summary>
        /// Initialise a SpriteSheet Animation state
        /// </summary>
        /// <param name="spriteSheet"></param>
        /// <param name="fps"></param>
        /// <param name="targetRow"></param>
        /// <param name="column"></param>
        public AnimationState(IEntity entity, Texture2D spriteSheet, int fps, int targetRow, int column)
        {
            gameTime = new GameTime();
            Entity = entity;
            //Perform a null check on the animation
            if (spriteSheet != null)
                //Set the SpriteSheet variable to animation
                SpriteSheet = spriteSheet;
            else { throw new Exception("SpritesheetAnimation is Null"); }

            //Initialise Rows and ColumnsVariables
            TargetRow = targetRow;
            Column = column;

            //Set the Animation speed of the animation
            FrameRate = 1f / fps;

            //Initialise the current Frame
            CurrentFrame = 0;
        }

        /// <summary>
        /// Initialise a SpriteSheet Animation state and animation speed 
        /// </summary>
        /// <param name="targetRow"></param>
        /// <param name="column"></param>
        /// <param name="animationSpeed"></param>
        /// <param name="spriteSheet"></param>
        /// <param name="fps"></param>
        public AnimationState(IEntity entity, Texture2D spriteSheet, int fps, int targetRow, int column, float animationSpeed)
        {
            gameTime = new GameTime();
            Entity = entity;

            //Perform a null check on the animation
            if (spriteSheet != null)
                //Set the SpriteSheet variable to animation
                SpriteSheet = spriteSheet;
            else { throw new Exception("SpritesheetAnimation is Null"); }

            //Initialise Rows and ColumnsVariables
            TargetRow = targetRow;
            Column = column;

            //Set the Animation speed of the animation
            FrameRate = animationSpeed / fps;
            //Store the AnimationSpeed Variable
            AnimationSpeed = animationSpeed;
            //Set the Initialise currentframe variable
            CurrentFrame = 0;
        }

        /// <summary>
        /// Initialise a SpriteSheet Animation state and Starting frame
        /// </summary>
        /// <param name="column"></param>
        /// <param name="animationSpeed"></param>
        /// <param name="spriteSheet"></param>
        /// <param name="fps"></param>
        /// <param name="currentFrame"></param>
        /// <param name="targetRow"></param>
        public AnimationState(IEntity entity, Texture2D spriteSheet, int fps, int targetRow, int column, float animationSpeed, int currentFrame)
        {
            gameTime = new GameTime();
            Entity = entity;

            //Perform a null check on the animation
            if (spriteSheet != null)
                //Set the SpriteSheet variable to animation
                SpriteSheet = spriteSheet;
            else { throw new Exception("SpritesheetAnimation is Null"); }

            //Initialise Rows and ColumnsVariables
            TargetRow = targetRow;
            Column = column;

            //Set the Animation speed of the animation
            FrameRate = animationSpeed / fps;
            //Store the AnimationSpeed Variable
            AnimationSpeed = animationSpeed;
            //Set the Initialise currentframe variable
            CurrentFrame = currentFrame;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public void Animate()
        {
            //Set _timeSinceLastFrame += milliseconds generated by gametime.
            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;

            //Create an If the check to see if _timeSinceLastFrame > _millisecondsPerFrame.
            if (timeSinceLastFrame > FrameRate)
            {
                //Reset the Time since the last frame
                timeSinceLastFrame -= FrameRate;

                //Increment the current frame
                CurrentFrame++;

                //Create an If Statment to check if the current frames is == to the total frames (_currentFrame == _totalFrames) 
                //if so set current frames to 0.
                if (CurrentFrame == TotalFrames)
                {
                    CurrentFrame = 0;
                }
            }
        }

        public void DrawAnimation(SpriteBatch spriteBatch)
        {

            if (Column != 0 && TargetRow != 0)
            {

                //Assign _width variable with the correct width of one of the images with in the spritesheet. 
                //Do this by getting the texture width and divide it by the amount of columns the spritesheet has.
                int width = SpriteSheet.Width / Column;
                //Assign _height variable with the correct height of one of the images with in the spritesheet. 
                //Do this by getting the texture height and divide it by the amount of rows the spritesheet has.
                int height = SpriteSheet.Width / TargetRow;
                //Assign _column variable the columns that are going to be shown in the animation.
                Column = CurrentFrame % Column;
                
                //Create a new rectangle and assign it to the sourceRectangle variable.
                //The rectangle will hold all the information it need to create a rectangle around the image on the spritesheet.
                sourceRectangle = new Rectangle(width * Column, height * TargetRow, width, height);
                //Create a new rectangle and assign it to the destinationRectangle variable.
                //The rectangle will hold all the information it needs for where the animations is going to be created.
                destinationRectangle = new Rectangle((int)Entity.Position.X, (int)Entity.Position.Y, width, height);
                //Call the Draw method from the spritebatch method and assign it the correct parameters need to draw the animations.
                spriteBatch.Draw(Entity.Texture, destinationRectangle, sourceRectangle, Color.White);
            }
        }

        /// <summary>
        /// Reverts the Current Frame Variable to 0
        /// </summary>
        public void ResetAnimation()
        {
            CurrentFrame = 0;
        }
    }
}
