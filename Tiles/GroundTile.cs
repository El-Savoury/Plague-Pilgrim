namespace Plague_Pilgrim
{
    /// <summary>
    /// Tile representing the ground
    /// </summary>
    internal class GroundTile : SolidTile
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public GroundTile(Vector2 pos) : base(pos)
        {
        }

        /// <summary>
        /// Load texture
        /// </summary>
        public override void LoadContent()
        {
            mTexture = Main.GetContentManager().Load<Texture2D>("Tiles/ground");
        }
    }
}
