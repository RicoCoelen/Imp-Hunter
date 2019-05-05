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

            // add friciton and acceleration to velocity
            cannon.Velocity += cannon.Acceleration;
            cannon.Acceleration = new Vector2(0,0);
            cannon.Velocity = cannon.Velocity * 0.99f;

            if (cannon.Velocity.X < 1 && cannon.Velocity.X > -1 && cannon.Acceleration.X < 0)
            {
                cannon.Velocity = new Vector2(0,0);
            }

            Console.WriteLine(cannon.Velocity);

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
