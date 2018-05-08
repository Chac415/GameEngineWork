using System;
using System.Collections.Generic;
using Engine.State_Machines.Animations;
using Engine.State_Machines.State_Transitions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Engine.State_Machines
{
    /// <summary>
    /// State Machines Handle used to store and manage different State classes
    /// Author : Nathan Robertson
    /// Date 07/04/18 : Version 0.4
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class StateMachine<T> : IStateMachine<T>
    {
        #region Variables

        //Dictionary to hold the different State classes
        private readonly IDictionary<string, IState<T>> StateBehaviour;
        //Dictionary to hold the different Transition types
        private readonly IDictionary<string, ITransitionHandler> Transitions;

        //String for ActiveState
        private string ActiveState;
        ////String for ActiveAnimation
        //private string ActiveAnimation;

        //To Store the entity which this state machine belongs to
        private readonly T Holder;

        #endregion

        #region StateManagement

        /// <summary>
        /// Construtor for the State Machine, Requires the Entity this state machine belongs to
        /// </summary>
        /// <param name="entity"></param>
        public StateMachine(T entity)  //IAnimation animation)
        {
            //Initialise Variables
            Holder = entity;
            ActiveState = null;

            StateBehaviour = new Dictionary<string, IState<T>>();
            Transitions = new Dictionary<string, ITransitionHandler>();
        }

        /// <summary>
        /// Add States to the States Dictionary
        /// </summary>
        /// <param name="state"></param>
        public void AddState(IState<T> state, string stateID)
        {
            //If the Dictionary is empty
            if (StateBehaviour.Count == 0)
            {
                //The Active State is the State being passed
                ActiveState = stateID;
                //Call the States Enter Method
                state.Enter(Holder);
            }

            //If the Dictionary doesnt hold a copy of this State
            if (!HoldingState(stateID))
            {
                //Add this State to the dictionary
                StateBehaviour.Add(stateID, state);
            }

        }


        /// <summary>
        /// Looks to see whether or not the States Dictionary holds a State
        /// </summary>
        /// <param name="State"></param>
        /// <returns></returns>
        private bool HoldingState(string State)
        {
            //Returns the state in the dictionary based on the Key passed
            return StateBehaviour.ContainsKey(State);
        }

        /// <summary>
        /// When called, Changes the Current State, calls the exit method and the enter method
        /// </summary>
        /// <param name="changeto"></param>
        public void ChangeState(string changeto)
        {
            //If the type isnt null
            if (changeto != null && changeto != ActiveState)
            {
                //Caal the exit behaviour of the current state
                StateBehaviour[ActiveState].Exit(Holder);

                //Look to see if the dicitonary is holding the target State
                if (HoldingState(changeto) && StateBehaviour.Count != 0)  
                    //Change the current to state to the Type being passed into the method
                    ActiveState = changeto;

                //Call The Update method of the new currentState from the dictionary
                StateBehaviour[ActiveState].Enter(Holder);
            }
        }

        #endregion

        #region Transitions

        /// <summary>
        /// Adds transitions between state that 
        /// </summary>
        /// <param name="MethodVal"></param>
        /// <param name="stateFrom"></param>
        /// <param name="targetState"></param>
        public void AddMethodTransition(Func<bool> MethodVal, string stateFrom, string targetState)
        {
            //Call the Method Tranisiton method with a default BOOl state of required result
            AddMethodTransition(MethodVal, stateFrom, targetState, true);
        }

        /// <summary>
        /// Store Method transition types in the Transitions dictionary
        /// </summary>
        /// <param name="MethodVal"></param>
        /// <param name="stateFrom"></param>
        /// <param name="targetState"></param>
        /// <param name="ReqResult"></param>
        public void AddMethodTransition(Func<bool> MethodVal, string stateFrom, string targetState, bool ReqResult)
        {
            //Check to see whether or not the dictionary holds both states and both states aren't the same
            if (IsValidTransition(stateFrom, targetState))
            {
                //Look to see whether or not the base transition state is currently held in the dictionary
                checkHandlerExists(stateFrom);
                //Store the method transition in the transition dictionary
                Transitions[stateFrom].StoreMethodTransition(targetState, MethodVal, ReqResult);
            }
        }


        /// <summary>
        /// returns a true statement id both of the States are held in the dictionary of states
        /// </summary>
        /// <param name="baseState"></param>
        /// <param name="targetState"></param>
        /// <returns></returns>
        private bool IsValidTransition(string baseState, string targetState)
        {
            if (StateBehaviour.Count!= 0)
            {
                //Check to see if base state to target state is a valid transion for both the state behaviour and animation
                if (StateBehaviour.ContainsKey(baseState) && StateBehaviour.ContainsKey(targetState) && baseState != targetState)
                {
                    //The Transition is Valid
                    return true;
                }
            }
            //The transition is invalid
            return false;

        }

        /// <summary>
        /// Check to see whether or not the transitions dictionary holds the transition is currently stored
        /// </summary>
        /// <param name="state"></param>
        public void checkHandlerExists(string state)
        {
            //If the Transitions dictionary doesnt hold the transition with a key of state
            if (!Transitions.ContainsKey(state))
                //Add the new transition to the state
                Transitions.Add(state, new TransitionHandler());
        }
        #endregion

        #region Update      

        /// <summary>
        /// Look to see whether or not the transition requirements have been met for the Method Transitions
        /// </summary>
        public void CheckMethodTransition()
        {
            //If the States Dictionary holds the ActiveState and the ANimationes Doesn't
            if (StateBehaviour.Keys.Contains(ActiveState) && Transitions.Count != 0)
                //Only change the State
                ChangeState(Transitions[ActiveState].CheckMethodTransition());
        }

        /// <summary>
        /// Call the update method for the state behvaiour that ios currently active
        /// </summary>
        public void UpdateBehaviour()
        {
            //Look to see whether or not the state behaviour should be changed
            CheckMethodTransition();
            //Update the State Behaviour
            StateBehaviour[ActiveState].Update(Holder);
        }

        #endregion

    }
}
