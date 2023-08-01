namespace Plague_Pilgrim
{
    /// <summary>
    /// Represents a tile in the game world
    /// </summary>
    abstract class Tile
    {
        #region rMembers

        private Vector2 mPosition;
        public Texture2D mTexture;
        protected bool mEnabled = true;
        public Point mTileMapIndex;

        #endregion rMembers





        #region rInitialisation

        /// <summary>
        /// Constructor
        /// </summary>
        public Tile(Vector2 pos, TileManager manager)
        {
            mPosition = pos;
            mTileMapIndex = manager.GetTileMapCoord(pos);
        }


        /// <summary>
        /// Load content for this tile
        /// </summary>
        public virtual void LoadContent() { }

        #endregion rInitialisation






        #region rUpdate

        /// <summary>
        /// Update tile
        /// </summary>
        /// <param name="gameTime">Frame time</param>
        public abstract void Update(GameTime gameTime);

        #endregion rUpdate






        #region rDraw

        /// <summary>
        /// Draw tile
        /// </summary>
        /// <param name="drawInfo">Info needed by Monogame to draw</param>
        public virtual void Draw(DrawInfo info)
        {
            Draw2D.DrawTexture(info, mTexture, mPosition);
        }

        #endregion rDraw





        #region rUtility

        /// <summary>
        /// Is this tile enabled?
        /// </summary>
        public bool GetEnabled()
        {
            return mEnabled;
        }


        /// <summary>
        /// Is this tile solid?
        /// </summary>
        /// <returns>True if tile is solid</returns>
        public virtual bool IsSolid()
        {
            return false;
        }

        #endregion rUtility
    }
}
