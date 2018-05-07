using System.Collections.Generic;
using Engine.Interfaces;
using Microsoft.Xna.Framework;


namespace Engine.Collision_Manager
{
    /// <summary>
    /// Collision Manager which controls collision detection, and what objects can collide with one and other
    /// </summary>
    public class CollisionManager : ICollisionManager, IProvider

    {
    IQuad Quad; //Create Variable for the quad Tree class
    ISAT SAT; //Create Varaible for the SAT class


    List<IEntity> CollidableObjects { get; set; } //List of IAsset of all Objects that have colliders
    List<IEntity> WillCollide { get; set; } //List of IAsset for each of the entities that can colide with each other



    /// <summary>
    /// Contructor
    /// </summary>
    /// <param name="quad"></param>
    /// <param name="sat"></param>
    public CollisionManager(IQuad quad)
    {
        CollidableObjects = new List<IEntity>(); //Initialise CollidableObjects List
        WillCollide = new List<IEntity>(); //Initialise WillCollide List
        Quad = quad;
        SAT = new SAT_CLass(); //Initialise new SAT class
    }

    /// <summary>
    /// The Update method to be called on this class,
    /// Updates QuadTrees and returns a list of objects which can collide with each other
    /// Calls the collision check type from the SAT class
    /// </summary>
    public void Update()
    {
        QuadColi();
        //HierarchyColi();
    }

    /// <summary>
    /// Method to store entities in a list of entities which have collisions
    /// </summary>
    /// <param name="asset"></param>
    public void hasCollisions(IEntity asset)
    {
        CollidableObjects.Add(asset); //Add Entity to the collidable objects list
    }

    /// <summary>
    /// Returns 
    /// </summary>
    /// <param name="asset"></param>
    private void canCollide(IEntity asset)
    {
        WillCollide.Add(asset); //Adds entities to the will collide list, a list of entities 
    }

    public void QuadColi()
    {
        Quad.Clear(); //Clear the list of Quads with the clear() method in the Quad class
        for (int i = 0; i < CollidableObjects.Count; i++) //For every entity in the collidable objects list
        {
            Quad.Insert(CollidableObjects[i]); //Store the entities inside the quadtrees
        }

        /// List<IAsset> canCollide = new List<IAsset>();
        for (int i = 0; i < CollidableObjects.Count; i++) //For every entity in the collidable objects list
        {
            WillCollide.Clear();
            WillCollide = Quad.Retrieve(WillCollide, CollidableObjects[i]); //Find out what objects can collide with each other

            if (WillCollide.Count >= 1) // If there are more than 1 entity in a tree, start collision calculation
            {

                for (int u = 0; u < WillCollide.Count; u++)
                {

                        if (CollidableObjects[i] is ICollidable) //&& WillCollide[u] == typeof(ICollidable))
                        {
                            SAT.PolygonVsPolygon(CollidableObjects[i], WillCollide[u]);
                        }


                        if (SAT.Intersect)
                        {
                            CollidableObjects[i].Position += SAT.MTV;
                        }
                }

            }
        }
    }

    public void HierarchyColi()
    {

        for (int i = 0; i < CollidableObjects.Count; i++)
        {
            for (int x = 0; x < CollidableObjects.Count; x++)
            {

                if (x != i)
                {
                    if (CollidableObjects[i].Hitbox.Intersects(CollidableObjects[x].Hitbox))
                    {
                        SAT.PolygonVsPolygon(CollidableObjects[i], CollidableObjects[x]);


                        if (SAT.Intersect)
                        {
                            CollidableObjects[i].Position += SAT.MTV;
                        }
                    }
                }
            }
        }
    }

    }
}

