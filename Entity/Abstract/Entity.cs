namespace Plague_Pilgrim
{
    /// <summary>
    /// Represents an interactive entity in the game world
    /// </summary>
    abstract class Entity
    {
        #region rMembers

        protected Vector2 mPosition;
        protected Texture2D mTexture;
        private bool mEnabled;

        #endregion rMembers





        #region mInitialisation

        /// <summary>
        /// Entity constructor
        /// </summary>
        /// <param name="pos">Starting position</param>
        public Entity(Vector2 pos)
        {
            mPosition = pos;
            mEnabled = true;
        }


        /// <summary>
        /// Load content for entity such as textures
        /// </summary>
        /// <param name="content">Monogame content manager</param>
        public abstract void LoadContent();

        #endregion mInitialsation





        #region rUpdate

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="gameTime">Frame time</param>
        public virtual void Update(GameTime gameTime)
        {

        }


        /// <summary>
        /// Trigger event when interacted with
        /// </summary>
        protected virtual void OnPlayerInteract()
        {

        }


        /// <summary>
        /// React to a collision with another entity
        /// </summary>
        /// <param name="entity">Entity that is colliding with us</param>
        public abstract void OnCollideEntity(Entity entity);
        

        #endregion rUpdate





        #region rDraw

        /// <summary>
        /// Draw entity
        /// </summary>
        public abstract void Draw(DrawInfo info);

        #endregion rDraw





        #region rUtility

        /// <summary>
        /// Get the collider bounds for this entity
        /// </summary>
        public Rect2f ColliderBounds()
        {
            return new Rect2f(mPosition, mPosition + new Vector2(mTexture.Width, mTexture.Height));
        }


        /// <summary>
        /// Get position of entity
        /// </summary>
        public Vector2 GetPosition()
        {
            return mPosition;
        }


        /// <summary>
        /// Set position of entity
        /// </summary>
        public void SetPosition(Vector2 pos)
        {
            mPosition = pos;
        }


        /// <summary>
        /// Get centre position of entity
        /// </summary>
        /// <returns></returns>
        public Vector2 GetCentrePos()
        {
            Rect2f collider = ColliderBounds();

            return (collider.min + collider.max) / 2.0f;

        }


        /// <summary>
        /// Enable/Disable this entity. Disabled entities will not be drawn or updated
        /// </summary>
        public virtual void SetEnabled(bool enabled)
        {
            mEnabled = enabled;
        }


        /// <summary>
        /// Is this entity enabled?
        /// </summary>
        /// <returns>True if enabled</returns>
        public bool IsEnabled()
        {
            return mEnabled;
        }

        #endregion rUtility
    }
}
