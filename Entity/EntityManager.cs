namespace Plague_Pilgrim
{
    /// <summary>
    /// Manager that updates and draws all entities
    /// </summary>
    static class EntityManager
    {
        #region rConstants

        #endregion rConstants






        #region rMembers

        static List<Entity> mRegisteredEntities = new List<Entity>();
        static List<EntityCollision> mCollisionBuffer = new List<EntityCollision>(); // List of legitimate collisions
        static List<ColliderSubmission> mAuxiliaryColliders = new List<ColliderSubmission>(); // List of possible collisions

        static List<Entity> mQueuedRegisters = new List<Entity>();
        static List<Entity> mQueuedDeletes = new List<Entity>();

        #endregion rMembers








        #region rUpdate

        /// <summary>
        /// Update all entities
        /// </summary>
        /// <param name="gameTime">Frame Time</param>
        public static void Update(GameTime gameTime)
        {
            mAuxiliaryColliders.Clear();

            foreach (Entity entity in mRegisteredEntities)
            {
                if (entity.IsEnabled())
                {
                    entity.Update(gameTime);
                }
            }

            ResolveEntityTouching();
            FlushQueues();
        }


        /// <summary>
		/// Flush add/delete queues.
		/// </summary>
		private static void FlushQueues()
        {
            foreach (Entity entity in mQueuedRegisters)
            {
                RegisterEntity(entity);
            }

            foreach (Entity entity in mQueuedDeletes)
            {
                DeleteEntity(entity);
            }

            mQueuedDeletes.Clear();
            mQueuedRegisters.Clear();
        }

        #endregion rUpdate








        #region rDraw

        /// <summary>
        /// Draw all entities
        /// </summary>
        /// <param name="info">Info needed to draw</param>
        public static void Draw(DrawInfo info)
        {
            foreach (Entity entity in mRegisteredEntities)
            {
                if (entity.IsEnabled())
                {
                    entity.Draw(info);
                }
            }
        }

        #endregion rDraw






        #region rCollision

        /// <summary>
        /// Add collider to list of enabled objects for checking
        /// </summary>
        public static void AddColliderSubmission(ColliderSubmission submission)
        {
            mAuxiliaryColliders.Add(submission);
        }


        /// <summary>
        /// Get the collision this entity will hit next. Returns null if no more collisions.
        /// </summary>
        public static EntityCollision GetNextCollision(GameTime gameTime, MovingEntity entity)
        {
            mCollisionBuffer.Clear();

            TileManager.GatherCollisions(gameTime, entity, ref mCollisionBuffer);

            // Filter out unnecessary collison checks and only store collisons that will legitimately occur
            foreach (ColliderSubmission submission in mAuxiliaryColliders)
            {
                if (submission.CanCollideWith(entity))
                {
                    EntityCollision collision = submission.GetEntityCollision(gameTime, entity);

                    if (collision != null)
                    {
                        mCollisionBuffer.Add(collision);
                    }
                }
            }

            // Sort collisons into the order they will occur based on proximity to player
            if (mCollisionBuffer.Count > 0)
            {
                return Utility.GetMin(ref mCollisionBuffer, EntityCollision.COLLISION_SORTER);
            }

            return null;
        }


        /// <summary>
        /// Resolve all Entity v Entity collisions
        /// </summary>
        public static void ResolveEntityTouching()
        {
            for (int i = 0; i < mRegisteredEntities.Count - 1; i++)
            {
                Entity iEntity = mRegisteredEntities[i];
                if (!iEntity.IsEnabled()) continue;

                Rect2f iRect = iEntity.ColliderBounds();

                for (int j = i + 1; j < mRegisteredEntities.Count; j++)
                {
                    Entity jEntity = mRegisteredEntities[j];
                    if (!jEntity.IsEnabled()) continue;

                    Rect2f jRect = jEntity.ColliderBounds();

                    if (Collision2D.RectVsRect(iRect, jRect))
                    {
                        // Both entities react
                        iEntity.OnCollideEntity(jEntity);
                        jEntity.OnCollideEntity(iEntity);
                    }
                }
            }
        }

        #endregion rCollision







        #region rEntityRegistry

        /// <summary>
		/// Register entity to this manager
		/// </summary>
		/// <param name="entity">Entity to be registered</param>
		public static void RegisterEntity(Entity entity)
        {
            mRegisteredEntities.Add(entity);
            entity.LoadContent();
        }


        /// <summary>
		/// Insert entity without reloading it
		/// </summary>
		public static void InsertEntity(Entity entity)
        {
            mRegisteredEntities.Add(entity);
        }


        /// <summary>
		/// Remove entity from registry
		/// </summary>
		/// <param name="entity">Entity to be removed</param>
		public static void DeleteEntity(Entity entity)
        {
            mRegisteredEntities.Remove(entity);
        }


        /// <summary>
        /// Call this when adding entities at runtime
        /// </summary>
        public static void QueueRegisterEntity(Entity entity)
        {
            mQueuedRegisters.Add(entity);
        }


        /// <summary>
        /// Call this when adding entities at runtime
        /// </summary>
        public static void QueueDeleteEntity(Entity entity)
        {
            mQueuedDeletes.Add(entity);
        }

        #endregion rEntityRegistry



        #region rFactory

        #endregion rFactory
    }
}
