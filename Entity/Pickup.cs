namespace Plague_Pilgrim
{
    internal class Pickup : MovingEntity
    {

        #region rConstants

        #endregion rConstants







        #region rMembers

        Color mColour = Color.Blue;

        #endregion rMembers







        #region rInitialisation

        /// <summary>
        /// Construct player at position.
        /// </summary>
        /// <param name="pos">Starting position</param>
        public Pickup(Vector2 pos) : base(pos)
        {

        }


        /// <summary>
        /// Load player textures and assets.
        /// </summary>
        /// <param name="content">Monogame content manager</param>
        public override void LoadContent()
        {
            mTexture = Main.GetContentManager().Load<Texture2D>("Boats/boat");
        }

        #endregion rInitialisation








        #region rUpdate


        /// <summary>
        /// Update player
        /// </summary>
        /// <param name="gameTime">Frame time</param>
        public override void Update(GameTime gameTime)
        {
            mPosition.Y += 3.0f;

            base.Update(gameTime);
        }



        #endregion rUpdate








        #region rDraw

        /// <summary>
        /// Draw player
        /// </summary>
        /// <param name="info">Info needed to draw</param>
        public override void Draw(DrawInfo info)
        {
            Draw2D.DrawTexture(info, mTexture, mPosition);
        }

        #endregion rDraw






        #region rCollision

        /// <summary>
        /// Deal with entities colliding with us
        /// </summary>
        /// <param name="entity">Entity that is colliding with player</param>
        public override void OnCollideEntity(Entity entity)
        {
            mColour = Color.Green;
        }

        /// <summary>
        /// React to collision 
        /// </summary>
        /// <param name="collisionNormal"></param>
        public override void ReactToCollision(Vector2 collisionNormal)
        {

        }

        #endregion rCollision



        #region mUtility

        #endregion mUtility
    }
}
