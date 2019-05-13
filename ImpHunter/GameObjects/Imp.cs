using Microsoft.Xna.Framework;

namespace ImpHunter
{
    class Imp : PhysicsObject
    {
        public bool isDead = false;
        GameObject targetObject;

        public Imp(GameObject target) : base("spr_imp_flying") 
        {
            float spawnPostion = GameEnvironment.Random.Next(100,500);
            // spawn above randomly but above the screen
            Position = new Vector2(spawnPostion, -100);
            // center the sprite
            Origin = Center;
            // set the target
            targetObject = target;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            // check if not null and dead
            if (targetObject != null && !isDead)
            {
                // move the imp using math
                Steer(targetObject.Position, 250, 50, 50);
            }
        }
    }
}
