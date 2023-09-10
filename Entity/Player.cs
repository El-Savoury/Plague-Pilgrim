namespace Plague_Pilgrim
{
    /// <summary>
    /// Playable entity
    /// </summary>
    class Player : MovingEntity
    {
        #region rConstants

        const float SPEED = 10.0f;
        const int WIDTH = 16;
        const int HEIGHT = 16;

        #endregion rConstants







        #region rMembers

        // Add members here

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
        public override void LoadContent(ContentManager content)
        {
            mTexture = content.Load<Texture2D>("Boats/boat");
        }

        #endregion rInitialisation








        #region rUpdate


        /// <summary>
        /// Update player
        /// </summary>
        /// <param name="gameTime">Frame time</param>
        public override void Update(GameTime gameTime)
        {
            mPosition.Y -= 3.0f * Utility.GetDeltaTime(gameTime);
            SetVelocity(CalcDirection() * SPEED);

            base.Update(gameTime);
        }


        /// <summary>
        /// Calculate player movement based on directional input 
        /// </summary>
        /// <returns>Vector representing distance and direction to move</returns>
        private Vector2 CalcDirection()
        {
            Vector2 inputDir = Vector2.Zero;

            if (InputManager.KeyHeld(Controls.Left)) { inputDir.X -= 1; }

            if (InputManager.KeyHeld(Controls.Right)) { inputDir.X += 1; }

            if (InputManager.KeyHeld(Controls.Up)) { inputDir.Y -= 1; }

            if (InputManager.KeyHeld(Controls.Down)) { inputDir.Y += 1; }

            if (inputDir != Vector2.Zero) { inputDir.Normalize(); }

            return inputDir;
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

            // Draw ray in movement direction for collison debugging
            Draw2D.DrawLine(info, GetCentrePos(), GetCentrePos() + (GetDirection() * 100), Color.White, 2);

        }

        #endregion rDraw






        #region mUtility

        #endregion mUtility


    }
}
