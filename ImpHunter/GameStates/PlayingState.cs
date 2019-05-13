using Microsoft.Xna.Framework;
using System;

namespace ImpHunter {
    class PlayingState : GameObjectList{

        // game objects list 
        GameObjectList cannonBalls = new GameObjectList();
        GameObjectList smallImps = new GameObjectList();

        // main actors
        Cannon cannon;
        Crosshair crosshair;
        Fortress fortress;
        BossImp bossImp;

        // imp timer
        private const float delay = 3;
        private float remainingDelay = delay;

        // shoot cooldown
        private const int SHOOT_COOLDOWN = 20;
        private int shootTimer = SHOOT_COOLDOWN;

        /// <summary>
        /// PlayingState constructor which adds the different gameobjects and lists in the correct order of drawing.
        /// </summary>
        public PlayingState() {
            Add(new SpriteGameObject("spr_background"));
            // draw cannonballs before cannon
            Add(cannonBalls);

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

            var timer = (float)gameTime.ElapsedGameTime.TotalSeconds;

            remainingDelay -= timer;

            if (remainingDelay <= 0)
            {
                smallImps.Add(new Imp(cannon));
                remainingDelay = delay;
            }

            // check bounce or collision with towers
            foreach (SpriteGameObject tower in fortress.Towers.Children)
            {
                // check if sides collide
                cannon.CheckBounce(tower);
                
                // check if cannonballs colide with towers
                foreach (CannonBall cannonball in cannonBalls.Children)
                {
                    cannonball.CheckBounce(tower);
                    cannonball.CheckBounce(fortress.Wall);
                }
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

                // make the direction of the cannonball 
                Vector2 TempVelocity = new Vector2(cannon.Position.X - crosshair.Position.X, cannon.Position.Y - crosshair.Position.Y) * -1;
                // create new cannon ball with
                PhysicsObject TempBall = new CannonBall(cannon.Position, TempVelocity);
                // add the cannonball
                cannonBalls.Add(TempBall);
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
