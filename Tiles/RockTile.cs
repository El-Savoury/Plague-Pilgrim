namespace Plague_Pilgrim
{
    /// <summary>
    /// Tile representing a rock
    /// </summary>
    internal class RockTile : InteractableTile
    {

        #region rInitialisation

        /// <summary>
        /// Constructor
        /// </summary>
        public RockTile(Vector2 pos) : base(pos)
        {
        }


        /// <summary>
        /// Load texture
        /// </summary>
        public override void LoadContent()
        {
            mTexture = Main.GetContentManager().Load<Texture2D>("Tiles/rock");
        }

        #endregion rInitialisation






        #region rUpdate


        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public override void OnEntityIntersect(MovingEntity entity)
        {
            // TODO: Stop ship and make it flash
        }

        #endregion rUpdate

    }
}
