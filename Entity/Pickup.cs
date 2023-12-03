namespace Plague_Pilgrim
{
    internal class Pickup : MovingEntity
    {
        #region rConstants

        const float SPEED = 10.0f;

        #endregion rConstants






        #region rMembers

        private Inventory mInventory = new Inventory();

        #endregion rMembers







        #region rInitialisation

        /// <summary>
        /// Construct pickup at position.
        /// </summary>
        /// <param name="pos">Starting position</param>
        public Pickup(Vector2 pos) : base(pos)
        {
            mInventory.AddItem("Food", RandomManager.Next(0, 6));
            mInventory.AddItem("Herbs", RandomManager.Next(0, 6));
            mInventory.AddItem("Vinegar", RandomManager.Next(0, 6));
        }


        /// <summary>
        /// Load player textures and assets.
        /// </summary>
        /// <param name="content">Monogame content manager</param>
        public override void LoadContent()
        {
            mTexture = Main.GetContentManager().Load<Texture2D>("Items/crate");
        }

        #endregion rInitialisation








        #region rUpdate

        /// <summary>
        /// Update player
        /// </summary>
        /// <param name="gameTime">Frame time</param>
        public override void Update(GameTime gameTime)
        {
            mVelocity = new Vector2(0, SPEED);

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
            if (entity is Player)
            {
                foreach (KeyValuePair<string, int> item in mInventory.Contents())
                {
                    InventoryManager.AddItem(InventoryManager.mGlobalInventory, item.Key, item.Value);
                }
            }

            EntityManager.DeleteEntity(this); 
        }


        /// <summary>
        /// React to collision 
        /// </summary>
        /// <param name="collisionNormal"></param>
        public override void ReactToCollision(Vector2 collisionNormal)
        {
        }


        /// <summary>
        /// Decrease players velocity
        /// </summary>
        public override void DecreaseVelocity()
        {
            if (mVelocity.X != 0) { mVelocity.X = Math.Sign(mVelocity.X) * (SPEED * 0.5f); }
            if (mVelocity.Y != 0) { mVelocity.Y = Math.Sign(mVelocity.Y) * (SPEED * 0.5f); }
        }

        #endregion rCollision






        #region rUtility

        #endregion rUtility
    }
}
