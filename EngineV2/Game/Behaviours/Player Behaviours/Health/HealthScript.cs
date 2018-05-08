using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectHastings.Behaviours.Player_Behaviours.Health
{
    class HealthScript : IDamageable
    {
        int health;

        public HealthScript()
        {
            health = 3;
        }

        public void TakeDamage(int damage)
        {
            health -= damage;
            Console.WriteLine("taking damage");

            //Update UI

            if (health <= 0)
            {
                Console.WriteLine("the player has died, get fucked four eyes");
                //gameOver
            }
        }

    }
}
