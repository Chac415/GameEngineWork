using System;
using Engine.Physics;
using Microsoft.Xna.Framework;
using Engine.Interfaces;
using Engine.State_Machines;

namespace ProjectHastings.Behaviours.States
{
    public class Patrol<T> : IState<T> where T : IEntity
    {
        public bool success { get; }

        float moveRange;
        Vector2 PatrolPoint;
        IAnimationMachine<T> Animator;
        float MoveSpeed = 1;
        public Patrol(float range, Vector2 startingpoint, IAnimationMachine<T> animator)
        {
            moveRange = range;
            PatrolPoint = startingpoint;
            Animator = animator;
        }

        public void Enter(T entity)
        {
            if (MoveSpeed <= 0)
            {
                Animator.ChangeActiveAnimation("left");
                MoveSpeed = -1.4f;
            }
            else if (MoveSpeed >= 0)
            {
                Animator.ChangeActiveAnimation("right");
                MoveSpeed = 1.4f;
            }
        }

        public void Update(T entity)
        {
            if (entity.Position.X <= PatrolPoint.X - moveRange)
            {
                Animator.ChangeActiveAnimation("left");
                MoveSpeed = -1.4f;
            }
            else if (entity.Position.X >= PatrolPoint.X + moveRange)
            {
                Animator.ChangeActiveAnimation("right");
                MoveSpeed = 1.4f;
            }

         ((IPhysics)entity).ApplyForce(new Vector2(MoveSpeed, 0));

        }

        public void Exit(T entity)
        {
            Console.WriteLine("Leaving the MoveLeftState");
        }
    }
}
