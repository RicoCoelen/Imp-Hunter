using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace ImpHunter
{
    class Swarm : Imp
    {
        private const float SPRING_CONSTANT = 0.2f;
        GameObject targetObject;

        public Swarm(SpriteGameObject target) : base(target)
        {
            // get target object to follow
            targetObject = target;
            // scale the imp down
            scale = 0.5f;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (isDead == false)
            {
                force = force + (this.targetObject.Position - this.Position) * 0.2f;
            }
        }
    }
}
