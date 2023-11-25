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

        public override void LoadContent()
        {
            mTexture = Main.GetContentManager().Load<Texture2D>("Tiles/ground");
        }
    }
}
