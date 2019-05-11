using Microsoft.Xna.Framework;
using System;

namespace ImpHunter {
    class PlayingState : GameObjectList{
        Cannon cannon;
        Crosshair crosshair;
        Fortress fortress;

        private const int SHOOT_COOLDOWN = 20;
        private int shootTimer = SHOOT_COOLDOWN;

        /// <summary>
        /// PlayingState constructor which adds the different gameobjects and lists in the correct order of drawing.
        /// </summary>
        public PlayingState() {
            Add(new SpriteGameObject("spr_background"));

            Add(cannon = new Cannon());
            cannon.Position = new Vector2(GameEnvironment.Screen.X / 2, 490);

            Add(fortress = new Fortress());

            // Always draw the crosshair last.
            Add(crosshair = new Crosshair());
        }
        
        /// <summary>
        /// Updates the PlayingState.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime) {
            base.Update(gameTime);

            // check bounce or collision with towers
            foreach(SpriteGameObject tower in fortress.Towers.Children)
            {
                cannon.CheckBounce(tower);
            }
        }

        /// <summary>
        /// Allows the player to shoot after a cooldown.
        /// </summary>
        /// <param name="inputHelper"></param>
        public override void HandleInput(InputHelper inputHelper) {
            base.HandleInput(inputHelper);

            shootTimer++;

            if (inputHelper.MouseLeftButtonPressed() && shootTimer > SHOOT_COOLDOWN) {
                crosshair.Expand(SHOOT_COOLDOWN);
                shootTimer = 0;

                // make a cannonball
                Vector2 TempVelocity = new Vector2(cannon.Position.X - crosshair.Position.X, cannon.Position.Y - crosshair.Position.Y) * -1;
                PhysicsObject TempBall = new CannonBall(new Vector2(cannon.Position.X, cannon.Position.Y), TempVelocity);
                // put at the end of the cannon loop
                //TempBall.Position = new Vector2(cannon.Position.X, cannon.Position.Y - cannon.Barrel.Height);
                // create our angle from the passed in angle
                //Vector2 angleVector = new Vector2((float)Math.Cos(cannon.Barrel.Angle), -(float)Math.Sin(cannon.Barrel.Angle));
                // multiply the angle vector by the bullet to get its angular velocity (velocity on some angle*)
                //TempBall.Acceleration = angleVector;
                // add it to loop
                Add(TempBall);
            }

            if (inputHelper.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Left))
            {
                cannon.Acceleration = new Vector2(-2,0);
            }

            else if (inputHelper.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Right))
            {
                cannon.Acceleration = new Vector2(2, 0);
            }
        }
    }
}
