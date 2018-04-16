using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace ProjectHastings
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Kernel : Game
    {
        #region Instance Variables
        //Constants
        GraphicsDeviceManager _Graphics;
        ContentManager _Content;

        //Screen Size
        int screenWidth = 900;
        int screenHeight = 600;

        #endregion


        public Kernel()
        {
            Content.RootDirectory = "Content";

            //graphics.PreferredBackBufferHeight = screenHeight;
            //graphics.PreferredBackBufferWidth = screenWidth;
            this.IsMouseVisible = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize(GraphicsDeviceManager graphics, ContentManager content, Game GameBase)
        {
            // TODO: Add your initialization logic here

            _Graphics = graphics;
            _Content = content;


            GameBase.Components.Add((GameComponent)scn);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }
    }
}
