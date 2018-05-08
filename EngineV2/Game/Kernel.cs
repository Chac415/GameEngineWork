using Engine.Interfaces;
using Engine.Managers;
using Microsoft.Xna.Framework;
using ProjectHastings.Scenes;

namespace ProjectHastings
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Kernel : Game
    {
        #region Instance Variables
        //Constants
        GraphicsDeviceManager graphics;

        ISceneManager scn;

        IScene TestScene;
        IScene mainmenu;
        IScene Wingame;
        IScene LoseScreen;
        IScene Level1;
        IScene Level2;



        //Screen Size
        int screenWidth = 900;
        int screenHeight = 600;

        #endregion


        public Kernel()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferHeight = screenHeight;
            graphics.PreferredBackBufferWidth = screenWidth;
            this.IsMouseVisible = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            scn = new SceneManager(this);
            mainmenu = new MainMenu(screenWidth, screenHeight, Content, scn);
            TestScene = new TestLevel(screenWidth, screenHeight, Content, scn);
            Level1 = new Level1(screenWidth, screenHeight, Content, scn);
            Level2 = new Level2(screenWidth, screenHeight, Content, scn);
            Wingame = new WinGame(screenWidth, screenHeight, Content, scn);
            LoseScreen = new GameOver(screenWidth, screenHeight, Content, scn);

            scn.AddScene("Mainmenu", mainmenu);
            scn.AddScene("TestLevel", TestScene);
            scn.AddScene("Level1", Level1);
            scn.AddScene("Level2", Level2);
            scn.AddScene("WinScreen", Wingame);
            scn.AddScene("LoseScreen", LoseScreen);

            Components.Add((GameComponent)scn);

            base.Initialize();
        }

    }
}
