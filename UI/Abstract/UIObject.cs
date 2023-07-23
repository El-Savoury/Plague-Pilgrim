namespace Plague_Pilgrim
{
    /// <summary>
    /// Represents a UI element containing text
    /// </summary>
    abstract class UIObject
    {
        #region rConstants

        const int PATCH_SIZE = 8;

        #endregion rConstants





        #region rMembers

        protected bool mEnabled = true; // Disabled entities will not be drawn or updated
        protected bool mActive = true; // Inactive objects are visible but don't take input from player

        protected Vector2 mPosition;
        protected Vector2 mCellSize = Vector2.Zero;
        protected Point mCellPadding = new Point(2, 0);
        private Vector2 mSizeInPatches = Vector2.Zero;

        protected string mCurrentText = string.Empty;
        protected Color mColour;

        #endregion rMembers






        #region rInitialisation

        public UIObject(Vector2 pos, Vector2 size)
        {
            mPosition = pos;
            mCellSize = GetCellSize();
            mColour = mActive ? Color.White : Color.Gray;
        }

        #endregion rInitialisation






        #region rUpdate

        /// <summary>
        /// Update entity
        /// </summary>
        public virtual void Update()
        {
            mColour = mActive ? Color.White : Color.Gray;
        }

        #endregion rUpdate






        #region rDraw

        /// <summary>
        /// Draw entity
        /// </summary>
        public virtual void Draw(DrawInfo info)
        {
            Draw2D.DrawString(info, FontManager.GetFont("monogram"), mCurrentText, mPosition, mColour);
        }

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


        /// <summary>
        /// Get the size required to display the text within this UIObject
        /// </summary>
        /// <returns>Size of string in pixels</returns>
        public Vector2 GetTextSize()
        {
            return (FontManager.GetFont("monogram").MeasureString(mCurrentText));
        }


        /// <summary>
        /// Returns size of UIObject cell
        /// </summary>
        /// <returns></returns>
        public Vector2 GetCellSize()
        {
            return new Vector2(Math.Max(GetTextSize().X, mCellSize.X),
                               Math.Max(GetTextSize().Y, mCellSize.Y));
        }


        /// <summary>
        /// Get necessary size of background panel in patches
        /// </summary>
        public Vector2 GetPanelSize()
        {
            return new Vector2(mCellSize.X + mCellSize.X, mCellSize.Y + mCellSize.Y);
        }


        public void DrawPanel(DrawInfo info)
        {
            Vector2 patchPos = Vector2.Zero;

            for (int x = 0; x < mSizeInPatches.X; x++)
            {
                for (int y = 0; y < mSizeInPatches.Y; y++)
                {
                    // Determine screen position
                    Vector2 screenPos = patchPos * PATCH_SIZE + mPosition;

                    // Calculate which patch of sprite is needed
                    Vector2 sourcePatch = Vector2.Zero;

                    if (patchPos.X > 0) { sourcePatch.X = 1; }
                    if (patchPos.X == mSizeInPatches.X - 1) { sourcePatch.X = 2; }
                    if (patchPos.Y > 0) { sourcePatch.Y = 1; }
                    if (patchPos.Y == mSizeInPatches.Y - 1) { sourcePatch.Y = 2; }

                    Draw2D.DrawPartialSprite(info, Main.GetContentManager().Load<Texture2D>("UI/border"), screenPos, sourcePatch, new Vector2(PATCH_SIZE, PATCH_SIZE), Color.White);
                }
            }
        }

        #endregion rUtility
    }
}
