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


        //public static void ResolveEntityTouching()
        //{
        //    for (int i = 0; i < mEntities.Count -1; i++)
        //    {
        //        Entity iEntity = mEntities[i];
        //        if (!iEntity.IsEnabled()) continue;

        //        Rect2f iRect = iEntity.ColliderBounds();

        //        for (int j = i + 1; )

        //    }


        #endregion rCollision
    }
}
