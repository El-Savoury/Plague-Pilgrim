namespace Plague_Pilgrim
{
    /// <summary>
    /// Playable entity
    /// </summary>
    internal class Player : MovingEntity
    {
        #region rConstants

        const int DEFAULT_SPEED = 4;

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
            mVelocity = new Vector2(0, -DEFAULT_SPEED);

            //if (GetInputDirection() != Vector2.Zero)
            //{
            //    mVelocity = GetInputDirection() * DEFAULT_SPEED;
            //}

            // TODO: UN-MAGIC NUMBER THESE TEMP CONTROLS

            if (InputManager.KeyHeld(Controls.Left)) { mVelocity.X -= 6; }
            if (InputManager.KeyHeld(Controls.Right)) { mVelocity.X += 6; }
            if (InputManager.KeyHeld(Controls.Up)) { mVelocity.Y -= 1; }
            if (InputManager.KeyHeld(Controls.Down)) { mVelocity.Y += 10; }

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
             { mVelocity.X = Math.Sign(mVelocity.X) * 2 ; }
              
               // mVelocity.Y = Math.Sign(mVelocity.Y) + (mVelocity.Y / 0.4f); 

              if(mVelocity.Y < 0) 
                {
                    mVelocity.Y = MathF.Round(mVelocity.Y * 0.6f);
                }
               else if(mVelocity.Y > 0) 
               {
                mVelocity.Y = MathF.Round(mVelocity.Y * 0.6f);
                } 

             
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

            Draw2D.DrawString(info, FontManager.GetFont("monogram"), Convert.ToString(mVelocity), new Vector2(mPosition.X, mPosition.Y -20), Color.White);

            Draw2D.DrawString(info, FontManager.GetFont("monogram"), Convert.ToString(mPosition), new Vector2(mPosition.X, mPosition.Y -50), Color.White);


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

        #endregion mUtility
    }
}
