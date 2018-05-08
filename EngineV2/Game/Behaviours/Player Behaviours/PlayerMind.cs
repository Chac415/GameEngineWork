using Engine.Animations;
using Engine.Input_Managment;
using Engine.Interfaces;
using Engine.Managers;
using Engine.Physics;
using Engine.Service_Locator;
using Engine.State_Machines;
using Engine.State_Machines.Animations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ProjectHastings.Entities.Player;

namespace ProjectHastings.Behaviours.Player_Behaviours
{
    class PlayerMind
    {
        private IEntity body;
        private KeyboardState keyState;

        private IAnimationMachine<IEntity> Animations;
        private IAnimation SpriteSheet;
        private IStateMachine<IEntity> StateMachine;

        ISoundManager sound = Locator.Instance.getProvider<SoundManager>() as ISoundManager;
        private IInputManager input = Locator.Instance.getProvider<InputManager>() as IInputManager;

        private float speed = 2.5f;

        private bool canClimb = false;

        public PlayerMind(IEntity ent)
        {

            body = ent;
            input.AddKeyListener(OnNewKeyInput);
            //Add states to player for things liek climbing in order to counter the limitations of gravity
            StateMachine = new StateMachine<IEntity>(body);

            SpriteSheet = new SpriteSheetAnimation(body.Texture);
            Animations = new AnimationMachine<IEntity>(body);

            Animations.AddState(new AnimationState(body, SpriteSheet, 0, 2), "idle");
            Animations.AddState(new AnimationState(body, SpriteSheet, 12, 0), "Walking_Left");
            Animations.AddState(new AnimationState(body, SpriteSheet, 12, 1), "Walking_Right");
            Animations.AddState(new AnimationState(body, SpriteSheet, 12, 2), "Climbing");

        }

        public void collisionResults(IEntity Collision)
        {
            if (Collision.Tag == "Ladder")
            {
                canClimb = true;
                //((IPhysics)body).GravityBool = false;
            }
            if (Collision.Tag != "Ladder")
            {
                canClimb = false;
                //((IPhysics)body).GravityBool = true;

            }
        }

        public virtual void OnNewKeyInput(object source, KeyEventData data)
        {
            keyState = data._newKey;

            Animations.ChangeActiveAnimation("idle");

            if (keyState.IsKeyDown(Keys.D) || keyState.IsKeyDown(Keys.Right))
            {
                Animations.ChangeActiveAnimation("Walking_Right");
                speed = 2.5f;
                
                body.Position += new Vector2(speed, 0);
            }

            if (keyState.IsKeyDown(Keys.A) || keyState.IsKeyDown(Keys.Left))
            {
                Animations.ChangeActiveAnimation("Walking_Left");
                speed = -2.5f;
                body.Position += new Vector2(speed, 0);
            }

            if (canClimb && keyState.IsKeyDown(Keys.W) || keyState.IsKeyDown(Keys.Up))
            {
                speed = -5f;
                body.Position += new Vector2(0,speed);
                sound.Playsnd("Ladder", 0.3f, true);

            }
            if (canClimb && keyState.IsKeyDown(Keys.S) || keyState.IsKeyDown(Keys.Down))
            {
                speed = 5f;
                body.Position += new Vector2(0, speed);
                sound.Playsnd("Ladder", 0.3f, true);
            }
        }


        public void Update(GameTime game)
        {
            Animations.UpdateAnimation(game);
        }
        public void Animate(SpriteBatch sprite)
        {
            Animations.DrawAnimation(sprite);
        }

    }
}
