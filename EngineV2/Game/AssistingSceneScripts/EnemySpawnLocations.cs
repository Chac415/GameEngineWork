using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectHastings.AssistingSceneScripts
{
    class EnemySpawnLocations
    {
        IList<Vector2> spawnPoints;

        public IList<Vector2> SpawnPoints { get { return spawnPoints; } set { spawnPoints = value; } }

        public EnemySpawnLocations()
        {
            spawnPoints = new List<Vector2>();
        }
        

        public void addToSpawnPoints(Vector2 location)
        {
            spawnPoints.Add(location);
        }



        
    }
}
