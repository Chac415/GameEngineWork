using System;
using Engine.State_Machines.Animations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Engine.State_Machines
{
    /// <summary>
    /// Interface for State Machine. Used for Adding States an calling the Update
    /// </summary>
    public interface IAnimationMachine<T>
    {
        void AddState(IAnimationState animState, string stateID);

        void AddMethodTransition(Func<bool> methodVal, string stateFrom, string targetState);
        void AddMethodTransition(Func<bool> methodVal, string stateFrom, string targetState, bool successVal);
        void ChangeActiveAnimation(string animation);

        void UpdateAnimation(GameTime gameTime); //Update Animation State
        void DrawAnimation(SpriteBatch sprite);
    }
}