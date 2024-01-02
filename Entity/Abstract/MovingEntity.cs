using System.ComponentModel.DataAnnotations;

namespace Plague_Pilgrim
{
    /// <summary>
	/// Entity that can move and collide
	/// </summary>
    abstract class MovingEntity : Entity
    {
        #region rConstants

        //Maximum number of collisions before we abort and assume we are stuck in an infinite loop.
        const int COLLISION_MAX_COUNT = 1024;

        #endregion rConstants






        #region rMembers

        protected Vector2 mVelocity;

        #endregion rMembers







        #region rInitialisation

        /// <summary>
        /// Create moving entity
        /// </summary>
        /// <param name="pos">Starting position</param>
        public MovingEntity(Vector2 pos) : base(pos)
        {
        }

        #endregion rInitialisation






        #region rUpdate

        /// <summary>
        /// Update moving entity
        /// </summary>
        /// <param name="gameTime">Frame time</param>
        public override void Update(GameTime gameTime)
        {
            TileManager.ResolveEntityTileCollision(gameTime, this);
            ApplyVelocity(gameTime);

            // Cast position to ints to prevent jerky sub pixel movement
            mPosition.X = (int)Math.Round(mPosition.X);
            mPosition.Y = (int)Math.Round(mPosition.Y);
        }


        /// <summary>
        /// Handle all collisions
        /// </summary>
        /// <param name="rect">Bounds of object to collide with</param>
        public void UpdateCollision(Rect2f rect)
        {
            if (Collision2D.RectVsRect(ColliderBounds(), rect))
            {
                // Calculate overlap by gettng distance between intersected edges
                float overlapX = Math.Min(ColliderBounds().max.X, rect.max.X) - Math.Max(ColliderBounds().min.X, rect.min.X);
                float overlapY = Math.Min(ColliderBounds().max.Y, rect.max.Y) - Math.Max(ColliderBounds().min.Y, rect.min.Y);

                // Resolve collision along axis with smallest overap
                if (overlapX < overlapY)
                {
                    // Resolve X axis
                    if (ColliderBounds().Centre.X < rect.Centre.X) { mPosition.X -= overlapX; }
                    else { mPosition.X += overlapX; }
                }
                else
                {
                    // Resovle Y axis
                    if (ColliderBounds().Centre.Y < rect.Centre.Y) { mPosition.Y -= overlapY; }
                    else { mPosition.Y += overlapY; }
                }
            }
        }


        /// <summary>
        /// Move position by velocity
        /// </summary>
        /// <param name="gameTime">Frame time</param>
        protected void ApplyVelocity(GameTime gameTime)
        {
            mPosition += VelocityToDisplacement(gameTime);
        }


        /// <summary>
        /// Lower the velocity of moving entity
        /// </summary>
        public virtual void DecreaseVelocity()
        {
        }


        /// <summary>
        /// Resolve collision with specific normal
        /// </summary>
        /// <param name="collisionNormal">Specific side of object being hit</param>
        public abstract void ReactToCollision(Vector2 collisionNormal);

        #endregion rUpdate






        #region rUtility


        /// <summary>
        /// Convert velocity into actual position displacement
        /// </summary>
        /// <param name="gameTime">Frame time</param>
        /// <returns></returns>
        public Vector2 VelocityToDisplacement(GameTime gameTime)
        {
            return mVelocity * Utility.GetDeltaTime(gameTime);
        }


        /// <summary>
        /// Get current velocity
        /// </summary>
        public Vector2 GetVelocity()
        {
            return mVelocity;
        }


        /// <summary>
        /// Set velocity
        /// </summary>
        public void SetVelocity(Vector2 vel)
        {
            mVelocity = vel;
        }


        /// <summary>
        /// Get direction entity is moving
        /// </summary>
        /// <returns>Normalised direciton vector</returns>s
        public Vector2 GetDirection()
        {
            Vector2 nextPos = GetCentre() + mVelocity;

            return Vector2.Normalize(nextPos - GetCentre());
        }

        #endregion rUtility
    }
}
