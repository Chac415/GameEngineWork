using System;
using System.Runtime.CompilerServices;
using Engine.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Engine.Animations
{
    public class SpriteSheetAnimation : IAnimation
    {

        //Create Intergers called _rows, _columns that will store how many rows and columns are in the spritesheet.
        public int Rows { get; protected set; }
        public int Columns { get; protected set; }


        //Create a variable of type Texture2D called SpriteSheet which holds the entity texture/spritesheet
        public Texture2D SpriteSheet { get; protected set; }

        /// <summary>
        /// Constructor for the SpriteSheet Animations - Sheet size 3x3
        /// </summary>
        /// <param name="spriteSheet"></param>
        public SpriteSheetAnimation(Texture2D spriteSheet) :this(spriteSheet, 3,3)
        {}
        /// <summary>
        /// Constructor for the spriteSheet Animations - customisable sheet size
        /// </summary>
        /// <param name="spriteSheet"></param>
        /// <param name="rows"></param>
        /// <param name="columns"></param>
        public SpriteSheetAnimation(Texture2D spriteSheet, int rows, int columns)
        {
            SpriteSheet = spriteSheet;
            Rows = rows;
            Columns = columns;
        }

        /// <summary>
        /// Return the SpriteSheet Texture
        /// </summary>
        /// <returns></returns>
        public Texture2D GetSpriteSheet()
        {
            return SpriteSheet;
        }

        /// <summary>
        /// Return the number of rows
        /// </summary>
        /// <returns></returns>
        public int GetRows()
        {
            return Rows;
        }

        /// <summary>
        /// Return the number of columns
        /// </summary>
        /// <returns></returns>
        public int GetColumns()
        {
            return Columns;
        }
    }

    
}