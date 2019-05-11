using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace ImpHunter
{
    class CannonBall : PhysicsObject
    {
        Vector2 gravity = new Vector2(0, 2f);
        public CannonBall(Vector2 Position, Vector2 Velocity) : base("spr_cannon_ball")
        {
            this.position = Position;
            this.velocity = Velocity;
            this.origin = this.Center;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            this.velocity += gravity;
        }
    }
}
