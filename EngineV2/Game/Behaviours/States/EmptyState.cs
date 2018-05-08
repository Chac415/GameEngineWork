using Engine.Interfaces;
using Engine.State_Machines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectHastings.Behaviours.States
{
    class EmptyState<T> : IState<T> where T : IEntity
    {
        public bool success
        {
            get;
        }

        public void Enter(T entity)
        {

        }

        public void Exit(T entity)
        {
        }

        public void Update(T entity)
        {
        }
    }
}
