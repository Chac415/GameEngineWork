using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine.Interfaces;
using Engine.Physics;

namespace Engine.Managers
{
    /// <summary>
    /// Updates Physics Objects
    /// Author: Nathan Robertson 
    /// Date: 20/03/18 Version: 0.2
    /// </summary>
   public sealed class PhysicsManager : IPhysicsManager
    {
        private readonly IList<IPhysics> PhysicsEntites;

        /// <summary>
        /// Ctor for Physics Manager
        /// </summary>
        public PhysicsManager()
        {
            //Initialise the Physics Entities List
            PhysicsEntites = new List<IPhysics>();
        }

        /// <summary>
        /// Add Entities to the List of Physics Entites
        /// </summary>
        /// <param name="physicsObj"></param>
        public void AddToList(IPhysics physicsObj)
        {
            // Add Entities to the List of Physics Entites
            PhysicsEntites.Add(physicsObj);
        }

        /// <summary>
        /// Calls the Update methodds for each of the IPhysics Objects on the Physics Entites list
        /// </summary>
        /// <param name="physicsObjs"></param>
        public void Update()
        {
            //Null check to see whether or not there are any entities to Update
            if (PhysicsEntites.Count != 0)
            {
                //For each entity in PhysicsEntites
                foreach (var physics in PhysicsEntites)
                {
                    //update Entity Physics
                    physics.UpdatePhysics();
                }
            }
        }
    }
}
