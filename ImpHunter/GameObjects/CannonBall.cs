using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace ImpHunter
{
    class CannonBall : PhysicsObject
    {
        Vector2 gravity = new Vector2(0, 4f);
        float Bounciness = -0.50f;
        float friction = 0.99f;

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

        public void CheckBounce(SpriteGameObject other)
        {
            if (!this.CollidesWith(other)) return;

            CollisionResult side = this.CollisionSide(other);

            switch (side)
            {
                case CollisionResult.LEFT:
                    position.X = other.Position.X + other.Width + this.Center.X;
                    // decrease velocity by 70%
                    velocity.X = velocity.X * Bounciness;
                    break;
                case CollisionResult.RIGHT:
                    position.X = other.Position.X - this.Center.X;
                    // decrease velocity by 70%
                    velocity.X = velocity.X * Bounciness;
                    break;
                case CollisionResult.TOP:
                    position.Y = other.Position.Y - this.Center.Y;
                    // decrease velocity by 70%
                    velocity.Y = velocity.Y * Bounciness;
                    break;
                case CollisionResult.BOTTOM:
                    position.Y = other.Position.Y - this.Center.Y + 1;
                    // decrease velocity by 70%
                    velocity.Y = velocity.Y * Bounciness;
                    // add friction
                    velocity = velocity * friction;
                    break;
            }
        }
    }
}
