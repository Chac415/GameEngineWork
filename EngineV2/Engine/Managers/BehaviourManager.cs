using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Engine.Interfaces;

namespace Engine.Managers
{

    /// <summary>
    /// Creates and Store minds, Attaching them to their respective entites
    /// Author: Nathan Robertson & Carl Chalmers
    /// Version : 0.5 - Date 07/04/18
    /// </summary>
    public sealed class BehaviourManager: IBehaviourManager, IProvider
    {
        //List to hold behaviours/minds of type IBehaviour
        public static List<IBehaviour> behaviours = new List<IBehaviour>();

        /// <summary>
        /// Update every IBehaviour
        /// </summary>
        public void Update()
        {
            foreach (IBehaviour ent in behaviours)
            {
                ent.update();
            }
        }

        /// <summary>
        /// Factory for the creatiion of minds and attaching them to their body
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ent"></param>
        /// <returns></returns>
        public T createMind<T>(IEntity ent) where T : IBehaviour, new()
        {
            //Create new IBehaviour
            T Mind = new T();
            //Call the IBehaviour initialise method, attaching the entity to the mind
            Mind.Initialise(ent);
            //Add the Ibehaviour to the beahviours list
            behaviours.Add(Mind);
            //return
            return Mind;
        }


        /// <summary>
        /// Used to remove Mind from the Behaviour Managers list
        /// </summary>
        /// <param name="mind"></param>
        public void removeMind(IBehaviour mind)
        {
            behaviours.Remove(mind);
        }
    }
}