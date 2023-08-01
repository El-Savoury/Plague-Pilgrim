namespace Plague_Pilgrim
{
    /// <summary>
    /// Tile representing the ground
    /// </summary>
    internal class GroundTile : Tile
    {

        #region rInitialisation

        /// <summary>
        /// Constructor
        /// </summary>
        public GroundTile(Vector2 pos, TileManager manager) :base(pos, manager)
        {
        }

        public override void LoadContent()
        {
            mTexture = Main.GetContentManager().Load<Texture2D>("Tiles/ground");
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

    }
}
