using System;
using Engine.Animations;
using Engine.Interfaces;
using Engine.Physics;
using Engine.State_Machines;
using Engine.State_Machines.Animations;
using Engine.State_Machines.Test_States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjectHastings.Behaviours
{
    public class EnemyMind
    {

        private IAnimation SpriteSheet;
        public IStateMachine<IEntity> StateMachine;
        public IAnimationMachine<IEntity> AnimationMachine;

        private IMoveBehaviour move;
        private IEntity body;

        public EnemyMind(IEntity Ent)
        {
            body = Ent;

            //Initialise the spriteSheet animation
            SpriteSheet = new SpriteSheetAnimation(body.Texture);

            //Create a new instance of State Machine
            StateMachine = new StateMachine<IEntity>(body);
            AnimationMachine = new AnimationMachine<IEntity>(body);

            //Add the states to the State Machine
            StateMachine.AddState(new MoveLeft<IEntity>(), "left");
            StateMachine.AddState(new MoveRight<IEntity>(), "right");

            //Add state behaviour Transitions
            StateMachine.AddMethodTransition(right, "left", "right");
            StateMachine.AddMethodTransition(left, "right", "left");

            //Add Animation States
            AnimationMachine.AddState(new AnimationState(body, SpriteSheet, 12, 1), "left");
            AnimationMachine.AddState(new AnimationState(body, SpriteSheet, 12, 0), "right");
            //Add Animation Transitions
            AnimationMachine.AddMethodTransition(right, "left", "right");
            AnimationMachine.AddMethodTransition(left, "right", "left");
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

        public void Update(GameTime game)
        {
            StateMachine.UpdateBehaviour();
            AnimationMachine.UpdateAnimation(game);
        }

        public void Animate(SpriteBatch sprite)
        {
            AnimationMachine.DrawAnimation(sprite);
        }
    }
}


