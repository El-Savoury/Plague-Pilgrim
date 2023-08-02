namespace Plague_Pilgrim
{
    /// <summary>
    /// An invisible tile that does nothing
    /// </summary>
    internal class EmptyTile : Tile
    {
        #region rInitialisation

        /// <summary>
        /// Constructor
        /// </summary>
        public EmptyTile(Vector2 pos) : base(pos)
        {
        }

        public override void LoadContent()
        {
            mTexture = Main.GetContentManager().Load<Texture2D>("Tiles/empty");
        }

        #endregion rInitialisation






        #region rUpdate

        /// <summary>
        /// Update tile
        /// </summary>
        /// <param name="gameTime">Frame time</param>
        public override void Update(GameTime gameTime)
        {
        }

        #endregion rUpdate





        #region rUtility

        /// <summary>
        /// Is this tile solid?
        /// </summary>
        /// <returns>True if tile is solid</returns>
        public override bool IsSolid()
        {
            return false;
        }

        #endregion rUtility

    }
}