namespace Plague_Pilgrim
{
    /// <summary>
    /// A non-solid tile you can interact with 
    /// </summary>
    internal class InteractableTile : EmptyTile
    {
        /// <summary>
		/// Tile with start position
		/// </summary>
		/// <param name="position">Start position</param>
		public InteractableTile(Vector2 position) : base(position)
        {
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public override void OnEntityIntersect(Entity entity)
        {
          
        }


        /// <summary>
        /// Get normal bounds
        /// </summary>
        /// <returns>Square bounds</returns>
        protected override Rect2f CalculateBounds()
        {
            return new Rect2f(mPosition, mPosition + new Vector2(TILE_SIZE, TILE_SIZE));
        }
    }
}
