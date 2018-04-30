using Engine.Interfaces;
using Microsoft.Xna.Framework;

namespace Engine.State_Machines.Test_States
{
    public class MoveRight<T> : IState<T> where T: IEntity
    {
        public bool success { get; }

        public void Enter(T entity)
        {
            entity.ApplyForce(new Vector2(-1, 0));
        }

        public void Update(T entity)
        {
            entity.ApplyForce(new Vector2(1, 0));

        }

        public void Exit(T entity)
        {

        }
    }
}
