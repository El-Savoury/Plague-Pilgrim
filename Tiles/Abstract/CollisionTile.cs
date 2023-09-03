namespace Plague_Pilgrim
{
    /// <summary>
    /// A collidable tile with square hitbox
    /// </summary>
    abstract class CollisionTile : Tile
    {
        public CollisionTile(Vector2 position) : base(position)
        {
        }


        /// <summary>
        /// Resolve collision with an entity
        /// </summary>
        /// <param name="entity">Entity that is colliding with us</param>
        /// <param name="gameTime">Frame time</param>
        public virtual bool Collide(MovingEntity entity, GameTime gameTime)
        {
            return Collision.RectVsRect(entity.ColliderBounds(), GetBounds());
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
