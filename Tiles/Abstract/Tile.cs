namespace Plague_Pilgrim
{
    /// <summary>
    /// Represents a tile in the game world
    /// </summary>
    abstract class Tile
    {
        #region rConstants

        private int TILE_SIZE = 16;

        #endregion rConstants





        #region rMembers

        private Vector2 mPosition;
        protected Texture2D mTexture;
        protected bool mEnabled = true;
        protected Point mTileMapIndex;

        #endregion rMembers





        #region rInitialisation

        /// <summary>
        /// Constructor
        /// </summary>
        public Tile(Vector2 pos)
        {
            mPosition = pos;
            mTileMapIndex = TileManager.GetTileMapCoord(pos);
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
        /// Get tile texture
        /// </summary>
        /// <returns>Texture reference</returns>
        public virtual Texture2D GetTexture()
        {
            return mTexture;
        }

        #endregion rDraw






        #region rUtility

        /// <summary>
        /// Get the tile map coordiante of this tile
        /// </summary>
        /// <returns>Tile map coords/returns>
        public virtual Point GetMapIndex()
        {
            return mTileMapIndex;
        }


        /// <summary>
        /// Is this tile enabled?
        /// </summary>
        public virtual bool GetEnabled()
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


        /// <summary>
        /// Get tile width and height
        /// </summary>
        /// <returns>Tile size in pixels</returns>
        public virtual int GetSize()
        {
            return TILE_SIZE;
        }

        #endregion rUtility






        #region rCollision

        /// <summary>
        /// Get the bounds of this tile
        /// </summary>
        /// <returns>Collision rectangle</returns>
        public Rect2f GetBounds()
        {
            return CalculateBounds();
        }


        /// <summary>
        /// Calculate the bounds of this tile.
        /// </summary>
        /// <returns>Collision rectangle</returns>
        protected virtual Rect2f CalculateBounds()
        {
            return new Rect2f(mPosition, mPosition + new Vector2(TILE_SIZE, TILE_SIZE));
        }

        #endregion rCollision
    }
}