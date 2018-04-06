using System;
using Engine.State_Machines.Animations;
using Microsoft.Xna.Framework.Graphics;

namespace Engine.State_Machines
{
    /// <summary>
    /// Interface for State Machine. Used for Adding States an calling the Update
    /// </summary>
    public interface IStateMachine<T>
    {
        void AddState(IState<T> state, string id); //Add States to the state Machine Dictionary
        void AddState(IAnimationState<T> animState, string stateID);
        void AddState(IAnimationState<T> animState, IState<T> state, string stateID);

        void AddMethodTransition(Func<bool> methodVal, string stateFrom, string targetState);
        void AddMethodTransition(Func<bool> methodVal, string stateFrom, string targetState, bool successVal);

        void UpdateBehaviour();  //Update State Behaviour
        void UpdateAnimation(SpriteBatch sprite); //Update Animation State
        void UpdateStates(SpriteBatch sprite); //Update both the Animation and the Behvaiour
        void DrawAnimation(SpriteBatch sprite);
    }
}
