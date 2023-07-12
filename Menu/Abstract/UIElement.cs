namespace Plague_Pilgrim
{
    /// <summary>
    /// Represents a UI element containing text
    /// </summary>
    abstract class UIElement
    {
        #region rMembers

        protected Vector2 mPosition;
        protected Vector2 mSize;
        protected string mCurrentText;
        protected bool mEnabled; // Disabled entities will not be drawn or updated
        protected bool mActive; // Inactive objects are visible but don't take input from player
        protected Color mColour;

        #endregion rMembers






        #region rInitialisation

        public UIElement(Vector2 pos, Vector2 size)
        {
            mPosition = pos;
            mSize = size;
            mCurrentText = string.Empty;
            mEnabled = true;
            mActive = false;
        }

        #endregion rInitialisation






        #region rUpdate

        /// <summary>
        /// Update entity
        /// </summary>
        public virtual void Update()
        {
            if (mEnabled)
            {
                mColour = mActive ? Color.White : Color.Gray;
            }
        }

        #endregion rUpdate






        #region rDraw

        /// <summary>
        /// Draw entity
        /// </summary>
        public abstract void Draw(DrawInfo info);
       
        #endregion rDraw








        #region rUtility

        /// <summary>
        /// Activate/Deactivate this entity. Deactivated entities are visible but do not take input
        /// </summary>
        public virtual void SetActive(bool active)
        {
            mActive = active;
        }


        /// <summary>
        /// Is this entity avtive?
        /// </summary>
        /// <returns>True if active</returns>
        public virtual bool IsActive()
        {
            return mActive;
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
