namespace Plague_Pilgrim
{
    /// <summary>
    /// Comparison class to sort TileCollision results by their closeness to the entitys
    /// </summary>
    class EntityCollisionSorter : IComparer<EntityCollision>
    {
        /// <summary>
        /// Compare the t value of two collisons to see which occurs first,
        /// the collision with the smaller t value is first
        /// </summary>
        /// <returns>Negative int value if collsion a occurs before b or positive int if vice versa</returns>
        public int Compare(EntityCollision a, EntityCollision b)
        {
            return a.GetResults().t.Value.CompareTo(b.GetResults().t.Value);
        }
    }





    /// <summary>
    /// Represents a collision with an entity
    /// </summary>
    class EntityCollision
    {
        public static EntityCollisionSorter COLLISION_SORTER = new EntityCollisionSorter();
        protected CollisionResults mResult;

        /// <summary>
        /// Constructor
        /// </summary>
        public EntityCollision(CollisionResults result)
        {
            mResult = result;
        }

        /// <summary>
        /// Get results of an entity collision
        /// </summary>
        public CollisionResults GetResults()
        {
            return mResult;
        }

        /// <summary>
        /// Make entity react to a collision
        /// </summary>
        /// <param name="entity"></param>
        public virtual void PostCollisionReact(MovingEntity entity)
        {
        }
    }





    /// <summary>
    /// Represents a collision between tile and a solid hitbox
    /// </summary>
    class SolidEntityCollision : EntityCollision
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public SolidEntityCollision(CollisionResults result) : base(result)
        {
        }


        /// <summary>
        /// Make entity react to a collision
        /// </summary>
        /// <param name="entity"></param>
        public override void PostCollisionReact(MovingEntity entity)
        {
            entity.ReactToCollision(mResult.normal);
        }
    }






    /// <summary>
	/// Represents collision between tile and an entity
    class TileEntityCollision : SolidEntityCollision
    {
        Point mTileCoord;

        public TileEntityCollision(CollisionResults result, Point tileCoord) : base(result)
        {
            mTileCoord = tileCoord;
        }

        public override void PostCollisionReact(MovingEntity entity)
        {
            TileManager.GetTile(mTileCoord).OnTouch(entity, mResult);

            base.PostCollisionReact(entity);
        }
    }






    /// <summary>
    /// Represents a collision between an entity and another entity
    /// </summary>
    class EntityEntityCollision : SolidEntityCollision
    {
        // Represents the entity (treated as a stationary object) that collided with the entity calling UpdateCollisionEntity
        Entity mEntity;


        /// <summary>
        /// Constructor
        /// </summary>
        public EntityEntityCollision(CollisionResults result, Entity entity) : base(result)
        {
            mEntity = entity;
        }

        public override void PostCollisionReact(MovingEntity entity)
        {
            // Both entities react
            entity.OnCollideEntity(mEntity);
            mEntity.OnCollideEntity(entity);

            base.PostCollisionReact(entity);
        }
    }
}
