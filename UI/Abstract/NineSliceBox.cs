namespace Plague_Pilgrim
{
    /// <summary>
    /// A box made from a texture with nine sections
    /// </summary>
    public abstract class NineSliceBox
    {
        #region rConstants

        protected const int SLICE_SIZE = 8;

        #endregion rConstants





        #region rMembers

        protected bool mEnabled = true; // Disabled entities will not be drawn or updated
        protected Vector2 mPosition;
        protected Vector2 mSize;
        protected Vector2 mSizeInSlices = Vector2.Zero;
        protected Color mColour;

        //protected Point mCellPadding = new Point(2, 0); // Space between multiple cells

        #endregion rMembers






        #region rInitialisation

        public NineSliceBox(Vector2 pos, int width = 0, int height = 0)
        {
            mPosition = pos;
            mSize = new Vector2(width, height);
            mSizeInSlices = GetSizeInSlices();
        }

        #endregion rInitialisation






        #region rUpdate

        /// <summary>
        /// Update box
        /// </summary>
        public abstract void Update();

        #endregion rUpdate






        #region rDraw

        /// <summary>
        /// Draw box for text to be displayed on 
        /// </summary>
        /// <param name="info">Info needed to draw</param>
        public virtual void Draw(DrawInfo info)
        {
            Vector2 slicePos = Vector2.Zero;

            for (slicePos.X = 0; slicePos.X < mSizeInSlices.X; slicePos.X++)
            {
                for (slicePos.Y = 0; slicePos.Y < mSizeInSlices.Y; slicePos.Y++)
                {
                    // Determine slice screen position
                    Vector2 screenPos = mPosition + (slicePos * SLICE_SIZE);

                    // Calculate which slice of sprite is needed
                    Vector2 sourceSlice = Vector2.Zero;

                    if (slicePos.X > 0) { sourceSlice.X = 1; }
                    if (slicePos.X == mSizeInSlices.X - 1) { sourceSlice.X = 2; }
                    if (slicePos.Y > 0) { sourceSlice.Y = 1; }
                    if (slicePos.Y == mSizeInSlices.Y - 1) { sourceSlice.Y = 2; }

                    Draw2D.DrawPartialSprite(info, Main.GetContentManager().Load<Texture2D>("UI/border"), new Vector2(screenPos.X, screenPos.Y), sourceSlice * SLICE_SIZE, new Vector2(SLICE_SIZE, SLICE_SIZE), Color.White);
                }
            }
        }

        #endregion rDraw









        #region rUtility

        /// <summary>
        /// Get size of box in slices
        /// </summary>
        public Vector2 GetSizeInSlices()
        {
            int x = (int)Math.Ceiling(mSize.X / SLICE_SIZE);
            int y = (int)Math.Ceiling(mSize.Y / SLICE_SIZE);

            return new Vector2(x + 2, y); // Add two extra patches to allow text centering
        }


        /// <summary>
        /// Enable/Disable this box. Disabled boxes will not be drawn or updated
        /// </summary>
        public virtual void SetEnabled(bool enabled)
        {
            mEnabled = enabled;
        }


        /// <summary>
        /// Is this box enabled?
        /// </summary>
        /// <returns>True if enabled</returns>
        public bool IsEnabled()
        {
            return mEnabled;
        }

        #endregion rUtility
    }
}
