using Microsoft.VisualBasic.FileIO;

namespace Plague_Pilgrim
{
    /// <summary>
    /// Playable entity
    /// </summary>
    internal class Player : MovingEntity
    {
        #region rConstants

        const float SPEED = 10.0f;

        #endregion rConstants







        #region rMembers

        Color mColour = Color.White;

        #endregion rMembers







        #region rInitialisation

        /// <summary>
        /// Construct player at position.
        /// </summary>
        /// <param name="pos">Starting position</param>
        public Player(Vector2 pos) : base(pos)
        {

        }


        /// <summary>
        /// Load player textures and assets.
        /// </summary>
        /// <param name="content">Monogame content manager</param>
        public override void LoadContent()
        {
            mTexture = Main.GetContentManager().Load<Texture2D>("Boats/boat");
        }

        #endregion rInitialisation








        #region rUpdate

        /// <summary>
        /// Update player
        /// </summary>
        /// <param name="gameTime">Frame time</param>
        public override void Update(GameTime gameTime)
        {
            mVelocity = new Vector2(0, -SPEED / 2);

            if (GetInputDirection() != Vector2.Zero)
            {
                mVelocity = GetInputDirection() * SPEED;
            }

            //if (InputManager.KeyHeld(Controls.Left)) { mVelocity.X = -SPEED * 2; }
            //if (InputManager.KeyHeld(Controls.Right)) { mVelocity.X = SPEED * 2; }
            //if (InputManager.KeyHeld(Controls.Up)) { mVelocity.Y = -SPEED * 2; }
            //if (InputManager.KeyHeld(Controls.Down)) { mVelocity.Y = SPEED * 2; }

            base.Update(gameTime);
        }

        /// <summary>
        /// Calculate player movement based on directional input 
        /// </summary>
        /// <returns>Vector representing distance and direction to move</returns>
        private Vector2 GetInputDirection()
        {
            Vector2 direction = Vector2.Zero;

            if (InputManager.KeyHeld(Controls.Left)) { direction.X--; }
            if (InputManager.KeyHeld(Controls.Right)) { direction.X++; }
            if (InputManager.KeyHeld(Controls.Up)) { direction.Y--; }
            if (InputManager.KeyHeld(Controls.Down)) { direction.Y++; }

            // TODO: FIND OUT WHY THIS CAUSES COLLISON BUG
            if (direction != Vector2.Zero) { direction.Normalize(); }

            return direction;
        }


        /// <summary>
        /// Decrease players velocity
        /// </summary>
        public override void DecreaseVelocity()
        {
            if (mVelocity.X != 0) { mVelocity.X = Math.Sign(mVelocity.X) * SPEED * 0.8f; }
            if (mVelocity.Y != 0) { mVelocity.Y = Math.Sign(mVelocity.Y) * SPEED * 0.8f; }
        }

        #endregion rUpdate








        #region rDraw

        /// <summary>
        /// Draw player
        /// </summary>
        /// <param name="info">Info needed to draw</param>
        public override void Draw(DrawInfo info)
        {
            Draw2D.DrawTexture(info, mTexture, mPosition);
        }

        #endregion rDraw







        #region rCollision

        /// <summary>
        /// Deal with entities colliding with us
        /// </summary>
        /// <param name="entity">Entity that is colliding with player</param>
        public override void OnCollideEntity(Entity entity)
        {
            mColour = Color.Red;
        }

        /// <summary>
        /// React to collision 
        /// </summary>
        /// <param name="collisionNormal"></param>
        public override void ReactToCollision(Vector2 collisionNormal)
        {

        }

        #endregion rCollision







        #region mUtility

        public void ClamptoCameraView(Camera cam)
        {
            Rect2f view = new Rect2f(cam.GetPos(), 800, 800);

            mPosition.X = Math.Clamp(mPosition.X, view.min.X, view.max.X - mTexture.Width);
            mPosition.Y = Math.Clamp(mPosition.Y, view.min.Y, view.max.Y - mTexture.Height);
        }

        #endregion mUtility


    }
}
