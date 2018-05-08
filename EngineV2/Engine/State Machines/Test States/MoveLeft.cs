using System;
using Engine.Physics;
using Microsoft.Xna.Framework;
using Engine.Interfaces;

namespace Engine.State_Machines.Test_States
{
    public class MoveLeft<T> : IState<T> where T: IEntity
    {
        public bool success { get; }

        float moveRange;

        public MoveLeft(float range)
        {
            moveRange = range;
        }

        public void Enter(T entity)
        {
            ((IPhysics)entity).ApplyForce(new Vector2(1,0));
        }

        public void Update(T entity)
        {
            ((IPhysics)entity).ApplyForce(new Vector2(-1, 0));
        }

        public void Exit(T entity)
        {
            Console.WriteLine("Leaving the MoveLeftState");
        }
    }
}
