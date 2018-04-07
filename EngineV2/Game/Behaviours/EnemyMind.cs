using Engine.Interfaces;
using Engine.Physics;
using Engine.State_Machines;
using Microsoft.Xna.Framework;

namespace ProjectHastings.Behaviours
{
    public class EnemyMind
    {
        private IMoveBehaviour move;
        private IPhysics body;

        public EnemyMind(IPhysics Ent, IStateMachine<IPhysics> stateMachine)
        {
            var stateMachine1 = stateMachine;
            body = Ent;
            //move = new xMoveBehaviour(body);
            stateMachine1.AddMethodTransition(right, "left", "right");
            stateMachine1.AddMethodTransition(left, "right", "left");
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


