namespace Plague_Pilgrim
{
    /// <summary>
    /// Tile representing a rock
    /// </summary>
    internal class RockTile : SolidTile
    {

        #region rInitialisation

        /// <summary>
        /// Constructor
        /// </summary>
        public RockTile(Vector2 pos) : base(pos)
        {
        }

        public override void LoadContent()
        {
            mTexture = Main.GetContentManager().Load<Texture2D>("Tiles/rock");
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
