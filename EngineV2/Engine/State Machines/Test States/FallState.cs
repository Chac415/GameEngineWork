using System;
using Engine.Interfaces;
using Microsoft.Xna.Framework;

namespace Engine.State_Machines.Test_States
{
    class FallState<T> : IState<T> where T : IEntity
    {

        public bool success { get; }

        public void Enter(T entity) //IAnimation animation
        {
            entity.GravityBool = true;
            Console.WriteLine("Successfully entered Fall State");

        }

        public void Update(T entity)//IAnimation animation
        {
            entity.ApplyForce(new Vector2(0, -5));
        }

        public void Exit(T entity) //IAnimation animation
        {    }
    }
}
