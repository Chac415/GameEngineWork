using System;
using Engine.State_Machines.Animations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Engine.State_Machines
{
    /// <summary>
    /// Interface for State Machine. Used for Adding States an calling the Update
    /// </summary>
    public interface IStateMachine<T>
    {
        void AddState(IState<T> state, string id); //Add States to the state Machine Dictionary

        void AddMethodTransition(Func<bool> methodVal, string stateFrom, string targetState);
        void AddMethodTransition(Func<bool> methodVal, string stateFrom, string targetState, bool successVal);

        void ChangeState(string id);
        void UpdateBehaviour();  //Update State Behaviour
    }
}
