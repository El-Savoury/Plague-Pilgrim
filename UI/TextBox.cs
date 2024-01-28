//namespace Plague_Pilgrim
//{
//    internal class TextBox : NineSliceBox
//    {
//        #region rMembers

//        protected bool mActive = true; // Inactive objects are visible but don't take input from player
//        protected string mCurrentText = string.Empty;

//        #endregion rMembers







//        #region rInitialiastion

//        /// <summary>
//        /// Text box constructor
//        /// </summary>
//        /// <param name="pos">Top left corner position</param>
//        /// <param name="size">Width and height of box</param>
//        public TextBox(Vector2 pos, Vector2 size) : base(pos, size)
//        {
//            size = GetMinSize(size);

//            mColour = mActive ? Color.White : Color.Gray;


//        }

//        #endregion rInitialisation








//        #region rUpdate

//        /// <summary>
//        /// Update text box
//        /// </summary>
//        public override void Update()
//        {
//            mColour = mActive ? Color.White : Color.Gray;
//        }

//        #endregion rUpdate








//        #region rDraw

//        /// <summary>
//        /// Draw text box
//        /// </summary>
//        /// <param name="info">Info monogame needs to to draw</param>
//        public override void Draw(DrawInfo info)
//        {
//            DrawText(info);

//            base.Draw(info);
//        }


//        /// <summary>
//        /// Draw text
//        /// </summary>
//        public virtual void DrawText(DrawInfo info)
//        {
//            Draw2D.DrawString(info, FontManager.GetFont("monogram"), mCurrentText, mPosition, mColour);
//        }

//        #endregion rDraw






//        #region  rUtilty

//        /// <summary>
//        /// Get the number of patches required to display text
//        /// </summary>
//        /// <returns>Size of string in patches</returns>
//        public Vector2 GetTextSize()
//        {
//            return FontManager.GetFont("monogram").MeasureString(mCurrentText);
//        }


//        /// <summary>
//        /// Returns size of text
//        /// </summary>
//        /// <returns></returns>
//        public virtual Vector2 GetMinSize(Vector2 size)
//        {
//            int padding = 20;

//            return new Vector2(Math.Max(GetTextSize().X + padding, mSize.X),
//                               Math.Max(GetTextSize().Y + padding, mSize.Y));
//        }


//        /// <summary>
//        /// Activate/Deactivate this element. Deactivated elements are visible but do not take input
//        /// </summary>
//        public virtual void SetActive(bool active)
//        {
//            mActive = active;
//        }


//        /// <summary>
//        /// Is this entity avtive?
//        /// </summary>
//        /// <returns>True if active</returns>
//        public virtual bool IsActive()
//        {
//            return mActive;
//        }

//        #endregion rUtility
//    }
//}
