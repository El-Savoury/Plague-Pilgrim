namespace Plague_Pilgrim
{
    /// <summary>
    /// Objects can submit potential collisions to entity manager for review
    /// </summary>
    abstract class ColliderSubmission
    {
        protected HashSet<MovingEntity> mCollidedEntities = new HashSet<MovingEntity>(); // Hashsets cannot contain duplicate elements

        /// <summary>
        /// Can we collide with entity?
        /// </summary>
        public abstract bool CanCollideWith(MovingEntity entity);


        /// <summary>
		/// Do collision check and get entity collision result. Can return null 
		/// </summary>
		public EntityCollision GetEntityCollision(GameTime gameTime, MovingEntity entity)
        {
            EntityCollision entityCollision = GetEntityCollisionInternal(gameTime, entity);

            if (entityCollision != null)
            {
                mCollidedEntities.Add(entity);
            }

            return entityCollision;
        }


        /// <summary>
		/// Do collision check and get entity collision result. Can return null
		/// </summary>
		protected abstract EntityCollision GetEntityCollisionInternal(GameTime gameTime, MovingEntity entity);
    }





    class EntityColliderSubmission : ColliderSubmission
    {
        #region rMembers

        Entity mEntity;

        #endregion rMembers







        #region rInitialisation

        /// <summary>
        /// Init EntityColliderSubmission with an entity
        /// </summary>
        /// <param name="entity"></param>
        public EntityColliderSubmission(Entity entity)
        {
            mEntity = entity;
        }

        #endregion rInitialisation








        #region rCollision

        /// <summary>
        /// Entity can collide with any entity except itself
        /// </summary>
        /// <param name="entity">Entity to check against</param>
        /// <returns>True if other entity is not itself</returns>
        public override bool CanCollideWith(MovingEntity entity)
        {
            return !object.ReferenceEquals(mEntity, entity);
        }


        /// <summary>
        /// Check for collison with other entity
        /// </summary>
        private CollisionResults CollideWith(GameTime gameTime, MovingEntity entity)
        {
            return Collision2D.MovingRectVsRect(entity.ColliderBounds(), entity.VelocityToDisplacement(gameTime), mEntity.ColliderBounds());
        }


        /// <summary>
        /// Get reults of collision with other entity
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected override EntityCollision GetEntityCollisionInternal(GameTime gameTime, MovingEntity entity)
        {
            CollisionResults results = CollideWith(gameTime, entity);

            // Collison
            if (results.t.HasValue)
            {
                return new EntityEntityCollision(results, mEntity);
            }

            // No Collision
            return null;
        }

        #endregion rCollision
    }
}
