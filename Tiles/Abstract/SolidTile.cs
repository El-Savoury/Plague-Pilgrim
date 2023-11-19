namespace Plague_Pilgrim
{
    /// <summary>
    /// A collidable tile with square hitbox
    /// </summary>
    abstract class SolidTile : Tile
    {
        public SolidTile(Vector2 position) : base(position)
        {
        }


        /// <summary>
        /// Resolve collision with an entity
        /// </summary>
        /// <param name="entity">Entity that is colliding with us</param>
        /// <param name="gameTime">Frame time</param>
        public override CollisionResults Collide(MovingEntity entity, GameTime gameTime)
        {
            return CollisionResults.None;
        }


        /// <summary>
        /// Resolve collision with an entity.
        /// </summary>
        /// <param name="entity"></param>
        public override void OnEntityCollision(MovingEntity entity)
        {
            entity.UpdateCollision(GetBounds());
        }


        /// <summary>
		/// Is this tile solid?
		/// </summary>
		/// <returns>True if a tile is solid</returns>
		public override bool IsSolid()
        {
            return true;
        }
    }
}
