using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace ImpHunter
{
    class BossImp : PhysicsObject
    {
        Vector2[] waypoint;
        int currentWaypoint = 0;

        public BossImp() : base("spr_imp_flying")
        {
            // center the sprite
            origin = Center;
            // put waypoints
            waypoint = new Vector2[]
            {
                new Vector2(200, 400),
                new Vector2(300, 100),
                new Vector2(400, 400),
                new Vector2(300, 300),
                new Vector2(200, 400)
            };
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            // steer the bossimp to correct waypoint
            Steer(waypoint[currentWaypoint], 250, 50, 50);
            // check if close enough to waypoint move to next one
            float dist = Vector2.Distance(waypoint[currentWaypoint], position);
            // if distance is smaller than 10
            if (dist < 50)
            {   
                // check if currentwaypoint is smaller than length
                if (currentWaypoint < waypoint.Length - 1)
                {
                    currentWaypoint++;
                }
                else
                {
                    // otherwise set waypoint back to zero
                    currentWaypoint = 0;
                }
                
            }
        }
    }
}
