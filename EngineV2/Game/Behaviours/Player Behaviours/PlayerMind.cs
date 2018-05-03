using Engine.Animations;
using Engine.Input_Managment;
using Engine.Interfaces;
using Engine.Managers;
using Engine.Physics;
using Engine.Service_Locator;
using Engine.State_Machines;
using Engine.State_Machines.Test_States;
using Engine.State_Machines.Animations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using ProjectHastings.Entities.Player;

namespace ProjectHastings.Behaviours.Player_Behaviours
{
    class PlayerMind : IBehaviour
    {
        private IEntity body;

        private KeyboardState keyState;
        private Player player;

        private IAnimation SpriteSheet;
        public StateMachine<IEntity> stateMachine { get; private set; }

        ISoundManager sound = Locator.Instance.getProvider<SoundManager>() as ISoundManager;
        private IInputManager input = Locator.Instance.getProvider<InputManager>() as IInputManager;

        private float speed = 2.5f;

        public PlayerMind(IEntity body)
        {
            this.body = body;
            //Initialise the spriteSheet animation
            SpriteSheet = new SpriteSheetAnimation(body.Texture);
            stateMachine = new StateMachine<IEntity>(body);
            stateMachine.AddState(new AnimationState(body, SpriteSheet, 12, 2, 2f), "left");
            stateMachine.AddState(new AnimationState(body, SpriteSheet, 12, 1, 2f), "right");
            stateMachine.AddMethodTransition(left, "right", "left");
            stateMachine.AddMethodTransition(right, "left", "right");
        }

        public void Initialise(IEntity ent)
        {
            body = ent;
            input.AddKeyListener(OnNewKeyInput);
        }

        public virtual void OnNewKeyInput(object source, KeyEventData data)
        {
            keyState = data._newKey;

            if (keyState.IsKeyDown(Keys.D) || keyState.IsKeyDown(Keys.Right))
            {

                speed = 2.5f;
                body.Position += new Vector2(speed, 0);
                stateMachine.ChangeState("right");
            }

            if (keyState.IsKeyDown(Keys.A) || keyState.IsKeyDown(Keys.Left))
            {

                speed = -2.5f;
                body.Position += new Vector2(speed, 0);
            }

            if (/*Player.canClimb &&*/ keyState.IsKeyDown(Keys.W) || /*Player.canClimb &&*/ keyState.IsKeyDown(Keys.Up))
            {
                speed = -2.5f;
                body.Position += new Vector2(0,speed);
                sound.Playsnd("Ladder", 0.3f, true);

            }
            if (/*Player.canClimb &&*/ keyState.IsKeyDown(Keys.S) || /*Player.canClimb &&*/ keyState.IsKeyDown(Keys.Down))
            {
                speed = 2.5f;
                body.Position += new Vector2(0, speed);
                sound.Playsnd("Ladder", 0.3f, true);
            }
        }

        bool left()
        {
            if (body.Position.X <= 0)
            {
                body.Position = new Vector2(1, body.Position.Y);
                return true;
            }

            return false;
        }

        bool right()
        {
            if (body.Position.X + 25 >= 850)
            {
                body.Position = new Vector2(824, body.Position.Y);
                return true;
            }

            return false;
        }
    }
}
