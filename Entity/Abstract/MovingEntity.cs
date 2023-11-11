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
        protected Vector2 mPrevVelocity;

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
            mPrevVelocity = mVelocity;
            UpdateCollision(gameTime);

            base.Update(gameTime);
        }



        /// <summary>
        /// Handle all collisions
        /// </summary>
        /// <param name="gameTime">Frame time</param>
        private void UpdateCollision(GameTime gameTime)
        {
            // List of all genuine collisions. A collision can be detected but never actually happen
            // E.g. We are going to collide with a wall but a block is in the way
            List<EntityCollision> collisionList = new List<EntityCollision>();

            // Get first collision in list
            EntityCollision currentCollision = EntityManager.GetNextCollision(gameTime, this);

            while (currentCollision != null)
            {
                EntityCollision entityCollision = currentCollision;
                CollisionResults collisionResults = entityCollision.GetResults();

                // Get the amount we have intersected the other entity by
                Vector2 intersect = new Vector2(Math.Abs(mVelocity.X), Math.Abs(mVelocity.Y) * (1.0f - collisionResults.t.Value));

                // Move position back outside of entity we have collided with 
                Vector2 pushVec = collisionResults.normal * intersect;
                mVelocity += pushVec;

                // Check for bugged collisions
                if (collisionList.Count > COLLISION_MAX_COUNT)
                {
                    // Fail-safe, don't move
                    mVelocity = Vector2.Zero;

                    // Clear list of stuck collision
                    collisionList.Clear();
                    break;
                }

                // Get the next collision happening to this entity from the list
                currentCollision = EntityManager.GetNextCollision(gameTime, this);
            }

            ApplyVelocity(gameTime);

            // React to each collision
            foreach (EntityCollision entityCollision in collisionList)
            {
                entityCollision.PostCollisionReact(this);
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
            Vector2 nextPos = GetCentrePos() + mVelocity;

            return Vector2.Normalize(nextPos - GetCentrePos());
        }

        #endregion rUtility
    }
}
