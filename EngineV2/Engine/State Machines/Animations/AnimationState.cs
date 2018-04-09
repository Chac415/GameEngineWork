﻿using System;
using Engine.Animations;
using Engine.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Engine.State_Machines.Animations
{

    public class AnimationState : IAnimationState

    {
        //Int of Current Frame in Animation
        public int CurrentFrame { get; private set; }
        public int TotalFrames { get; set; }
        //In for the FrameRate of the Animation
        public float FrameRate { get; private set; }
        //float for the Speed of the Animation - <0 plays backwards >0 plays forwards
        public float AnimationSpeed { get; private set; }


        private Rectangle sourceRectangle, destinationRectangle;


        private float timeSinceLastFrame = 0f;
        private float millisecondsPerFrame = 100;

        //Animation Variables
        public IAnimation SpriteSheet { get; private set; }
        public IEntity Entity;
        public int TargetRow { get; private set; }
        public int Column { get; private set; }

        private int Height;
        private int Width;

        /// <summary>
        /// Initialise a SpriteSheet Animation state
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="spriteSheet"></param>
        /// <param name="fps"></param>
        /// <param name="targetRow"></param>
        public AnimationState(IEntity entity, IAnimation spriteSheet, int fps, int targetRow) : this(entity, spriteSheet, fps, targetRow, 1, 0)
        {}

        /// <summary>
        /// Initialise a SpriteSheet Animation state and animation speed 
        /// </summary>
        /// <param name="targetRow"></param>
        /// <param name="animationSpeed"></param>
        /// <param name="entity"></param>
        /// <param name="spriteSheet"></param>
        /// <param name="fps"></param>
        public AnimationState(IEntity entity, IAnimation spriteSheet, int fps, int targetRow, float animationSpeed) : this(entity, spriteSheet, fps, targetRow, animationSpeed, 0)
        {}

        /// <summary>
        /// Initialise a SpriteSheet Animation state and Starting frame
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="spriteSheet"></param>
        /// <param name="fps"></param>
        /// <param name="targetRow"></param>
        /// <param name="animationSpeed"></param>
        /// <param name="currentFrame"></param>
        public AnimationState(IEntity entity, IAnimation spriteSheet,  int fps, int targetRow, float animationSpeed, int currentFrame)
        {
            Entity = entity;

            //Perform a null check on the animation
            if (spriteSheet != null)
                //Set the SpriteSheet variable to animation
                SpriteSheet = spriteSheet;
            else { throw new Exception("SpritesheetAnimation is Null"); }

            //Initialise Rows and ColumnsVariables
            TargetRow = targetRow;
            //Set the Animation speed of the animation
            FrameRate = animationSpeed / fps;
            //Store the AnimationSpeed Variable
            AnimationSpeed = animationSpeed;
            //Set the Initialise currentframe variable
            CurrentFrame = currentFrame;
        }

        public void Animate(GameTime gameTime)
        {
            //Set _timeSinceLastFrame += milliseconds generated by gametime.
            timeSinceLastFrame += (float)gameTime.ElapsedGameTime.TotalSeconds;

            //Create an If the check to see if _timeSinceLastFrame > _millisecondsPerFrame.
            if (timeSinceLastFrame > FrameRate)
            {
                //Reset the Time since the last frame
                timeSinceLastFrame = 0;

                //Increment the current frame
                CurrentFrame++;

            }
        }

        public void DrawAnimation(SpriteBatch spriteBatch)
        {

                ////Assign _width variable with the correct width of one of the images with in the spritesheet. 
                ////Do this by getting the texture width and divide it by the amount of columns the spritesheet has.
                Width = SpriteSheet.GetSpriteSheet().Width / SpriteSheet.GetColumns() ;
                ////Assign _height variable with the correct height of one of the images with in the spritesheet. 
                ////Do this by getting the texture height and divide it by the amount of rows the spritesheet has.
                Height = (SpriteSheet.GetSpriteSheet().Height / SpriteSheet.GetRows());
                //Assign _column variable the columns that are going to be shown in the animation.
                var column = CurrentFrame % SpriteSheet.GetColumns();

                //Create a new rectangle and assign it to the sourceRectangle variable.
                //The rectangle will hold all the information it need to create a rectangle around the image on the spritesheet.
                sourceRectangle = new Rectangle(Width * column, Height * TargetRow, Width, Height);
               
                //Call the Draw method from the spritebatch method and assign it the correct parameters need to draw the animations.
                spriteBatch.Draw(Entity.Texture, Entity.Position, sourceRectangle, Color.White);
            
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
