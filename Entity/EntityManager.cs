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

        private static List<Entity> mRegisteredEntities = new List<Entity>();

        #endregion rMembers








        #region rUpdate

        /// <summary>
        /// Update all entities
        /// </summary>
        /// <param name="gameTime">Frame Time</param>
        public static void Update(GameTime gameTime)
        {
            foreach (Entity entity in mRegisteredEntities)
            {
                if (entity.IsEnabled())
                {
                    entity.Update(gameTime);
                }
            }

            ResolveEntityCollision();
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
        /// Resolve all entity vs entity collisions
        /// </summary>
        private static void ResolveEntityCollision()
        {
            for (int i = 0; i < mRegisteredEntities.Count - 1; i++)
            {
                Entity iEntity = mRegisteredEntities[i];
                if (!iEntity.IsEnabled()) continue;

                Rect2f iBounds = iEntity.ColliderBounds();

                for (int j = i + 1; j < mRegisteredEntities.Count; j++)
                {
                    Entity jEntity = mRegisteredEntities[j];
                    if (!jEntity.IsEnabled()) continue;

                    Rect2f jBounds = jEntity.ColliderBounds();

                    if (Collision2D.RectVsRect(iBounds, jBounds))
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
        /// Clear all entities.
        /// </summary>
        public static void ClearEntities()
        {
            mRegisteredEntities.Clear();
        }

        #endregion rEntityRegistry

    }
}
