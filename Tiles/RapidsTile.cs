namespace Plague_Pilgrim
{
    enum Direction
    {
        Left,
        Right,
        Down,
    }

    /// <summary>
    /// A tile that speeds up the player in one of three cardinal directions
    /// </summary>
    internal class RapidsTile : InteractableTile
    {
        #region rConstants

        private const int VELOCITY = 8;

        #endregion rConstants





        #region rMembers

        private Direction mDirection;

        #endregion rMembers




        #region rInitialisation

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pos">World position</param>
        public RapidsTile(Vector2 pos) : base(pos)
        {
            /// TODO: USE WORLD SEED
            mDirection = (Direction)RandomManager.Next(0, 3);
        }

        /// <summary>
        /// Load Texture
        /// </summary>
        public override void LoadContent()
        {
            switch (mDirection)
            {
                case Direction.Left:
                    mTexture = Main.GetContentManager().Load<Texture2D>("Tiles/rapidsLeft");
                    break;

                case Direction.Right:
                    mTexture = Main.GetContentManager().Load<Texture2D>("Tiles/rapidsRight");
                    break;

                case Direction.Down:
                    mTexture = Main.GetContentManager().Load<Texture2D>("Tiles/rapidsDown");
                    break;

                default:
                    break;
            }
        }

        #endregion rInitialisation




        #region rUpdate

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity">Moving entity<</param>
        public override void OnEntityIntersect(MovingEntity entity)
        {
            switch (mDirection)
            {
                case Direction.Left:
                    entity.IncreaseVelocity(-VELOCITY, 0);

                    break;

                case Direction.Right:
                    entity.IncreaseVelocity(VELOCITY, 0);
                    break;

                case Direction.Down:
                    entity.IncreaseVelocity(0, VELOCITY);
                    break;

                default:
                    break;
            }
        }

        #endregion rUpdate
    }
}
