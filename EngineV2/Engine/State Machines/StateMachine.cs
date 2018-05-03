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
        //Dictionary to hold the AnimationStates
        private readonly IDictionary<string, IAnimationState> StateAnimation;
        //Dictionary to hold the different Transition types
        private readonly IDictionary<string, ITransitionHandler> Transitions;

        //String for ActiveState
        private string ActiveState;
        //String for ActiveAnimation
        private string ActiveAnimation;

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
            //AnimationClass = animation
            StateBehaviour = new Dictionary<string, IState<T>>();
            Transitions = new Dictionary<string, ITransitionHandler>();
            StateAnimation = new Dictionary<string, IAnimationState>();
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
        /// Add Animation States to the Animation State Dictionary
        /// </summary>
        /// <param name="animState"></param>
        /// <param name="stateID"></param>
        public void AddState(IAnimationState animState, string stateID)
        {
            //If the Dictionary is empty
            if (StateAnimation.Count == 0)
            {
                //The Active State is the State being passed
                ActiveAnimation = stateID;
            }

            //If the Dictionary doesnt hold a copy of this State
            if (!HoldingAnimationState(stateID))
            {
                //Add this State to the dictionary
                StateAnimation.Add(stateID, animState);
            }
        }

        /// <summary>
        /// Add Animation States to the Animation State Dictionary
        /// </summary>
        /// <param name="animState"></param>
        /// <param name="state"></param>
        /// <param name="stateID"></param>
        public void AddState(IAnimationState animState, IState<T> state, string stateID)
        {
            //If the State Behaviour Dictionary is empty
            if (StateBehaviour.Count == 0)
            {
                //The Active State is the State being passed
                ActiveState = stateID;
                //Call the AnimStates Animation Method
                state.Enter(Holder);
            }
            //Look to see if the animation dictionary is empty
            if (StateAnimation.Count == 0)
            {
                //If the dictionary is empty, Change the active animation to the state Being passed
                ActiveAnimation = stateID;
            }
            //Check to see whether or not each of the Dictionaries hold a copy of this State
            if (!HoldingState(stateID))
            {
                //Add the state the the States Dictionary
                StateBehaviour.Add(stateID, state);
            }
            if (!HoldingAnimationState(stateID))
            {
                //Add this Animation State to the Animations dictionary
                StateAnimation.Add(stateID, animState);
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
        /// Looks to see whether or not the Animation State Dictionary holds a State
        /// </summary>
        /// <param name="State"></param>
        /// <returns></returns>
        private bool HoldingAnimationState(string State)
        {
            //Returns the state in the dictionary based on the Key passed
            return StateAnimation.ContainsKey(State);
        }


        /// <summary>
        /// When called, Changes the Current State, calls the exit method and the enter method
        /// </summary>
        /// <param name="changeto"></param>
        public void ChangeState(string changeto)
        {
            //If the type isnt null
            if (changeto != null)
            {
                //Caal the exit behaviour of the current state
                StateBehaviour[ActiveState].Exit(Holder);

                //Look to see if the dicitonary is holding the target State
                if (HoldingState(changeto) && StateBehaviour.Count != 0)
                    //Change the current to state to the Type being passed into the method
                    ActiveState = changeto;

                //Null Check on Animation States Dictionary and to see if it is holding the target Animation state
                if (HoldingAnimationState(changeto) && StateAnimation.Count != 0)
                    //Change the current Aniation state to the Type being passed into the method
                    ActiveAnimation = changeto;

                //Call The Update method of the new currentState from the dictionary
                StateBehaviour[ActiveState].Enter(Holder);
            }
        }

        /// <summary>
        /// When called, Changes the Current State, calls the exit method and the enter method
        /// </summary>
        /// <param name="changeto"></param>
        private void ChangeAnimationState(string changeto)
        {
            //If the type isnt null
            if (changeto != null)
            {
                //Change the current Aniation state to the Type being passed into the method
                ActiveAnimation = changeto;
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
            if (StateBehaviour.Count != 0 && StateAnimation.Count != 0)
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
        /// Look to see whether or not the transition requirements have been met for the Method Transitions
        /// </summary>
        public void CheckMethodTransition()
        {
            if (StateBehaviour.Count != 0)
            {
                //If the States Dictionary holds the ActiveState and the Animationes Doesn't
                if (StateBehaviour.Keys.Contains(ActiveState) && Transitions.Count != 0)
                    //Only change the State
                    ChangeState((Transitions[ActiveState].CheckMethodTransition()));
            }
            if (StateAnimation.Count != 0)
            {
                //If only the Animation holds the active state
                if (StateAnimation.Keys.Contains(ActiveAnimation) && StateAnimation[ActiveAnimation] != null && Transitions.Count != 0)
                    //Only change the Animation State
                    ChangeAnimationState(Transitions[ActiveAnimation].CheckMethodTransition());
            }
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

        #region Updates

        /// <summary>
        /// Update Method for State Machine
        /// </summary>
        public void UpdateStates(SpriteBatch sprite, GameTime gameTime)
        {
            //Look to see whether or not the state behaviour should be changed
            CheckMethodTransition();
            //Call the update method on the active state
            if (StateBehaviour.ContainsKey(ActiveState))
                //Update the State Behaviour
                StateBehaviour[ActiveState].Update(Holder);

            if (!StateAnimation.ContainsKey(ActiveAnimation))
            {
                //Update the Animation
                StateAnimation[ActiveAnimation].Animate(gameTime);
            }
        }
        /// <summary>
        /// Calls the Animate Method from the Animation States
        /// </summary>
        /// <param name="gameTime"></param>
        public void UpdateAnimation(GameTime gameTime)
        {
            //Look to see whether or not the state behaviour should be changed
            CheckMethodTransition();
            //Updat the Animation
            if (!StateAnimation.ContainsKey(ActiveAnimation))
                throw new Exception("There is no Animation to draw in the State Machine");

            StateAnimation[ActiveAnimation].Animate(gameTime);

        }
        /// <summary>
        /// Draws the current Animation State
        /// </summary>
        /// <param name="sprite"></param>
        public void DrawAnimation(SpriteBatch sprite)
        {
            if (!StateAnimation.ContainsKey(ActiveAnimation))
                throw new Exception("There is no Animation to draw in the State Machine");
            StateAnimation[ActiveAnimation].DrawAnimation(sprite);
        }
        /// <summary>
        /// Call the update method for the state behvaiour that ios currently active
        /// </summary>
        public void UpdateBehaviour()
        {
            //Look to see whether or not the state behaviour should be changed
            CheckMethodTransition();
            //Update the State Behaviour
           if (StateBehaviour.Count != 0)
                StateBehaviour[ActiveState].Update(Holder);
        }

        #endregion

    }
}
