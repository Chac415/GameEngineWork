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
using ProjectHastings.Behaviours.States;
using ProjectHastings.Entities.Player;

namespace ProjectHastings.Behaviours.Player_Behaviours
{
    public class PlayerMind
    {
        private IEntity body;
        private KeyboardState keyState;

        private IAnimationMachine<IEntity> Animations;
        private IAnimation SpriteSheet;
        private IStateMachine<IEntity> StateMachine;
        string IdleState;

        int direction = 1;
        private float timer;
        public float Timer { get { return timer; } set { timer = value; } }

        ISoundManager sound = Locator.Instance.getProvider<SoundManager>() as ISoundManager;
        private IInputManager input = Locator.Instance.getProvider<InputManager>() as IInputManager;

        public float speed = 2.5f;

        private bool canClimb = false;

        public PlayerMind(IEntity ent)
        {
            IdleState = "Idle_Right";
            body = ent;
            input.AddKeyListener(OnNewKeyInput);
            //Add states to player for things liek climbing in order to counter the limitations of gravity
            StateMachine = new StateMachine<IEntity>(body);

            SpriteSheet = new SpriteSheetAnimation(body.Texture);
            Animations = new AnimationMachine<IEntity>(body);

            StateMachine.AddState(new EmptyState<IEntity>(), "BaseState");
            StateMachine.AddState(new DamagedPlayerState<IEntity>(((Player)body), StateMachine, Animations), "Damaged");

            Animations.AddState(new AnimationState(body, SpriteSheet, 0, 0, 0, 1), "Idle_Left");
            Animations.AddState(new AnimationState(body, SpriteSheet, 0, 1, 0, 1), "Idle_Right");
            Animations.AddState(new AnimationState(body, SpriteSheet, 12, 0), "Walking_Left");
            Animations.AddState(new AnimationState(body, SpriteSheet, 12, 1), "Walking_Right");
            Animations.AddState(new AnimationState(body, SpriteSheet, 12, 2), "Climbing");

        }

        public void collisionResults(IEntity Collision)
        {
            if (Collision.Tag == "Ladder")
            {
                canClimb = true;
            }
            if (Collision.Tag == "Enemy")
            {
                StateMachine.ChangeState("Damaged");
            }
            if (Collision.Tag != "Ladder")
            {
                canClimb = false;
            }
        }

        public virtual void OnNewKeyInput(object source, KeyEventData data)
        {
            keyState = data._newKey;

                Animations.ChangeActiveAnimation(IdleState);


            if (keyState.IsKeyDown(Keys.D) || keyState.IsKeyDown(Keys.Right))
            {
                Animations.ChangeActiveAnimation("Walking_Right");
                speed = 2.5f;
                IdleState = "Idle_Right";
                body.Position += new Vector2(speed, 0);
            }

            if (keyState.IsKeyDown(Keys.A) || keyState.IsKeyDown(Keys.Left))
            {
                Animations.ChangeActiveAnimation("Walking_Left");
                speed = -2.5f;
                IdleState = "Idle_Left";
                body.Position += new Vector2(speed, 0);
            }

            if (canClimb && keyState.IsKeyDown(Keys.W) || keyState.IsKeyDown(Keys.Up))
            {
                speed = -5f;
                body.Position += new Vector2(0, speed);
                Animations.ChangeActiveAnimation("Climbing");
                sound.Playsnd("Ladder", 0.3f, true);

            }
            if (canClimb && keyState.IsKeyDown(Keys.S) || keyState.IsKeyDown(Keys.Down))
            {
                speed = 5f;
                body.Position += new Vector2(0, speed);
                Animations.ChangeActiveAnimation("Climbing");
                sound.Playsnd("Ladder", 0.3f, true);
            }
        }


        public void Update(GameTime game)
        {
            timer += game.ElapsedGameTime.Milliseconds;
            Animations.UpdateAnimation(game);
            StateMachine.UpdateBehaviour();
        }
        public void Animate(SpriteBatch sprite)
        {
            Animations.DrawAnimation(sprite);
        }

    }
}
