﻿using System;
using System.Collections.Generic;
using Engine.Interfaces;
using Microsoft.Xna.Framework;

namespace Engine.Collision_Management
{
    public class SAT_CLass : ISAT
    {
        public bool Intersect { get; set; }
        public Vector2 MTV { get; set; }
        public Vector2 ClosingVelo;
        public Vector2 CNormal;

        /// <summary>
        /// Test for collisions between Convex Polygons
        /// </summary>
        /// <param name="_ent1"></param>
        /// <param name="_ent2"></param>
        /// <param name="velocity"></param>
        public void PolygonVsPolygon(IEntity _ent1, IEntity _ent2)
        {

            //Initialise booleans
            Intersect = true;


            //Iniitialise edges lists
            int ent1Edges = _ent1.Edges().Count;
            int ent2Edges = _ent2.Edges().Count;

            //Variabls for MTV
            float minInterDis = float.PositiveInfinity;
            Vector2 edgeNormal = new Vector2();
            Vector2 edgeNumber;

            //Get the edges we are testing against
            for (int i = 0; i < ent1Edges + ent2Edges; i++)
            {

                //============================================================  Generate Edges ================================================================\\

                if (i < ent1Edges)
                {
                    edgeNumber = _ent1.Edges()[i];
                }
                else
                {
                    edgeNumber = _ent2.Edges()[i - ent1Edges];
                }

                edgeNumber.Normalize();   //Convert Axies to a unit Vector 


                //=============================================== PROJECT EVERY POINT ON EVERY AXIES FOR BOTH OBJECTS ===========================================\\


                float Ent1Min = 0, Ent2Min = 0, Ent1Max = 0, Ent2Max = 0;       //Initialise min/Max variables for each obj

                ProjectPolygon(edgeNumber, _ent1, ref Ent1Min, ref Ent1Max);    //Get the distance of object 1's min and max points on the axies
                ProjectPolygon(edgeNumber, _ent2, ref Ent2Min, ref Ent2Max);    //Get the distance of object 2's min and max points on the axies

                //====================================================== DETERMINE IF COLLISIONS ARE OCCURING ====================================================\\

                //Determine if there is an overlap
                float interdis = Ent1Min - Ent2Max;
                if (interdis > 0)
                {
                    //if the value is greater than zero, there is no collision
                    Intersect = false;
                    break;
                }
                else if (interdis < 0)
                {
                    Intersect = true;
                }

                interdis = Math.Abs(interdis);
                if (interdis < minInterDis)
                {
                    minInterDis = interdis;
                    edgeNormal = edgeNumber;
                }
                //Set the MTV variable if collision             

                
                //CNormal = edgeNormal;
                // ClosingVelocity(edgeNormal, _ent1.Velocity(), _ent2.Velocity());



            }
            if (Intersect)
            MTV = edgeNormal * minInterDis;

        }

        // Calculate the projection of a polygon on an axis and returns it as a [min, max] interval
        public void ProjectPolygon(Vector2 axis, IEntity Entity, ref float min, ref float max)
        {
            // To project a point on an axis use the dot product
            List<Vector2> points = Entity.Point();


            float projection = Vector2.Dot(axis, points[0]);
            min = projection;
            max = projection;
            for (int i = 0; i < points.Count; i++)
            {
                projection = Vector2.Dot(axis, points[i]);
                if (projection < min)
                {
                    min = projection;
                }
                else if (projection > max)
                {
                    max = projection;
                }
            }
        }

        public Vector2 ClosingVelocity(Vector2 CNormal, Vector2 Velocity1, Vector2 Velocity2)
        {


            float dotProduct = Vector2.Dot(CNormal, (Velocity1 - Velocity2));
            ClosingVelo = dotProduct * CNormal;

            return ClosingVelo;
        }
    }


}

