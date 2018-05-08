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
using ProjectHastings.Entities.Player;
using ProjectHastings.Behaviours.States;
namespace ProjectHastings.Behaviours
{
    public class EnemyMind
    {

        private IAnimation SpriteSheet;
        public IStateMachine<IEntity> StateMachine;
        public IAnimationMachine<IEntity> AnimationMachine;

        private IMoveBehaviour move;
        private IEntity body;

        bool turning = true;

        public EnemyMind(IEntity Ent)
        {
            body = Ent;

            //Initialise the spriteSheet animation
            SpriteSheet = new SpriteSheetAnimation(body.Texture);

            //Create a new instance of State Machine
            StateMachine = new StateMachine<IEntity>(body);
            AnimationMachine = new AnimationMachine<IEntity>(body);


            //Add Animation States
            AnimationMachine.AddState(new AnimationState(body, SpriteSheet, 12, 1), "left");
            AnimationMachine.AddState(new AnimationState(body, SpriteSheet, 12, 0), "right");

            //Add the states to the State Machine
            StateMachine.AddState(new Patrol<IEntity>(100, body.Position, AnimationMachine), "Patrol");
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
        bool turn()
        {
            if (turning)
            { return true; }
            else return false;
        }

        /// <summary>
        /// Attack Method
        /// </summary>
        /// <param name="collision"></param>
        public void Collision(IEntity collision)
        {

            //If the enemy collides with the player, take 1 damage
            if (collision.Tag == "Player")
            {
             //   ((Player)collision).healthScript.TakeDamage(1);
            }
            if ( collision.Tag == "Crate")
            {
                turning = true;
            }
            
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


