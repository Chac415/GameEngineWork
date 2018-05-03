using Engine.Animations;
using Engine.Interfaces;
using Engine.State_Machines.Test_States;
using Engine.Physics;
using Engine.State_Machines;
using Engine.State_Machines.Animations;
using Microsoft.Xna.Framework;

namespace ProjectHastings.Behaviours
{
    public class EnemyMind
    {

        private IEntity body;
        public StateMachine<IEntity> stateMachine { get; protected set; }
        private IAnimation SpriteSheet;


        public EnemyMind(IEntity ent)
        {
            body = ent;
            //Initialise the spriteSheet animation
            SpriteSheet = new SpriteSheetAnimation(body.Texture);
            //Create a new instance of State Machine
            stateMachine = new StateMachine<IEntity>(body);

            //Add the states to the State Machine
            stateMachine.AddState(new AnimationState(body, SpriteSheet, 12, 1, 2f), new MoveLeft<IEntity>(), "left");
            stateMachine.AddState(new AnimationState(body, SpriteSheet, 12, 0, 2f), new MoveRight<IEntity>(), "right");


            stateMachine.AddMethodTransition(right, "left", "right");
            stateMachine.AddMethodTransition(left, "right", "left");
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


