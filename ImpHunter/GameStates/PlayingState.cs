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
        }
    }
}
