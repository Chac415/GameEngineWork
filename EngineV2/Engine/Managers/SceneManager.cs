using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Engine.Interfaces;
using Engine.Render;

namespace Engine.Managers
{
    public class SceneManager : DrawableGameComponent, ISceneManager
    {

        string ActiveScene;
        public IDictionary<string, IScene> AllScenes;
        private IRenderable render;
        private SpriteBatch sprt;

        public SceneManager(Game game) : base(game)
        {
            render = new Renderable();
            AllScenes = new Dictionary<string, IScene>();
            sprt = new SpriteBatch(GraphicsDevice);
        }

        public void AddScene(string name, IScene scenes)
        {
            if (AllScenes.Count == 0)  
            {
               ActiveScene = name;
               AllScenes.Add(name, scenes);
            }
            else
            {
                AllScenes.Add(name, scenes);
            }
        }

        public void ChangeScene (string name)
        {
            if (ActiveScene != name)
            {
                ActiveScene = name;
            }
        }

        public void RemoveScene(string name)
        {
            AllScenes.Remove(name);
        }
        public override void Update(GameTime gameTime) 
        {
            if(Keyboard.GetState().IsKeyDown(Keys.Escape))
            Game.Exit();

            AllScenes[ActiveScene].update(gameTime);

            render.Draw(AllScenes[ActiveScene], sprt);

            base.Update(gameTime);

        }



    }
}
