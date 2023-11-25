namespace Plague_Pilgrim
{
    internal class SlowTile : InteractableTile
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public SlowTile(Vector2 pos) : base(pos)
        {
        }

        public override void LoadContent()
        {
            mTexture = Main.GetContentManager().Load<Texture2D>("Tiles/slow");
        }

        public override void OnEntityIntersect(MovingEntity entity)
        {
            entity.DecreaseVelocity();
        }
    }
}
