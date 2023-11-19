namespace Plague_Pilgrim
{
    /// <summary>
    /// Represents a tile in the game world
    /// </summary>
    abstract class Tile
    {
        #region rConstants

        public static int TILE_SIZE = 24;

        #endregion rConstants





        #region rMembers

        protected Vector2 mPosition;
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
        public virtual void Update(GameTime gameTime)
        {
        }

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







        #region rCollision

        /// <summary>
        /// Resolve collision with an entity
        /// </summary>
        /// <param name="entity">Entity that is colliding with tile</param>
        /// <param name="gameTime">Frame time</param>
        public abstract CollisionResults Collide(MovingEntity entity, GameTime gameTime);


        /// <summary>
        /// Called when an entity touches tile
        /// </summary>
        /// <param name="entity">Entity that is touching</param>
        public virtual void OnEntityCollision(MovingEntity entity) { }


        /// <summary>
        /// Called when an entity intersects tile
        /// </summary>
        /// <param name="entity">Entity that is intersecting</param>
        public virtual void OnEntityIntersect(Entity entity) { }


        /// <summary>
        /// Get the bounds of this tile
        /// </summary>
        /// <returns>Collision rectangle</returns>
        public Rect2f GetBounds()
        {
            return CalculateBounds();
        }


        /// <summary>
        /// Calculate the bounds of this tile
        /// </summary>
        /// <returns>Collision rectangle</returns>
        protected virtual Rect2f CalculateBounds()
        {
            return new Rect2f(mPosition, mPosition + new Vector2(TILE_SIZE, TILE_SIZE));
        }
        
        #endregion rCollision







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
        public virtual bool IsEnabled()
        {
            return mEnabled;
        }


        /// <summary>
        /// Get the center of the tile
        /// </summary>
        public virtual Vector2 GetCentre()
        {
            return mPosition + new Vector2(TILE_SIZE, TILE_SIZE) * 0.5f;
        }


        /// <summary>
        /// Get tile width and height
        /// </summary>
        /// <returns>Tile size in pixels</returns>
        public virtual int GetSize()
        {
            return TILE_SIZE;
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