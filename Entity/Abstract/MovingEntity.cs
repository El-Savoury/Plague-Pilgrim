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

        public override void Update(GameTime gameTime)
        {
            ApplyVelocity(gameTime);

            base.Update(gameTime);
        }

        #endregion rUpdate






        #region rUtility

        /// <summary>
		/// Move position by velocity
		/// </summary>
		/// <param name="gameTime">Frame time</param>
		protected void ApplyVelocity(GameTime gameTime)
        {
            mPosition += VelocityToDisplacement(gameTime);
        }


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


        #endregion rUtility
    }
}
