namespace Plague_Pilgrim
{
   internal class TextBox : NineSliceBox
   {
       #region rMembers

       protected bool mActive = true; // Inactive objects are visible but don't take input from player
       protected string mCurrentText = string.Empty;
       protected int mPadding;

       #endregion rMembers







       #region rInitialistion

       /// <summary>
       /// Text box constructor
       /// </summary>
       /// <param name="pos">Top left corner position</param>
       /// <param name="size">Width and height of box</param>
       public TextBox(Vector2 pos, string text, int padding, int width = 0, int height = 0) : base(pos, width, height)
       {
           mCurrentText = text;
           mPadding = padding;
           mSize = GetMinSize(new Vector2(width, height));
           mSizeInSlices = GetSizeInSlices();
          
           mColour = mActive ? Color.White : Color.Gray;
       }

       #endregion rInitialisation








       #region rUpdate

       /// <summary>
       /// Update text box
       /// </summary>
       public override void Update()
       {
           mColour = mActive ? Color.White : Color.Gray;
       }

       #endregion rUpdate








       #region rDraw

       /// <summary>
       /// Draw text box
       /// </summary>
       /// <param name="info">Info monogame needs to to draw</param>
       public override void Draw(DrawInfo info)
       {
            base.Draw(info);
            DrawText(info);
       }


       /// <summary>
       /// Draw text
       /// </summary>
       public virtual void DrawText(DrawInfo info)
       {
           Vector2 pos = new Vector2(mPosition.X + mPadding/2, mPosition.Y + mPadding/2);

           Draw2D.DrawString(info, FontManager.GetFont("monogram"), mCurrentText, pos, mColour);
       }

       #endregion rDraw






       #region  rUtility

       /// <summary>
       /// Get the number of patches required to display text
       /// </summary>
       /// <returns>Size of string in patches</returns>
       public Vector2 GetTextSize()
       {
           return FontManager.GetFont("monogram").MeasureString(mCurrentText);
       }


       /// <summary>
       /// Returns size of text
       /// </summary>
       /// <returns></returns>
       public virtual Vector2 GetMinSize(Vector2 size)
       {
           return new Vector2(Math.Max(GetTextSize().X + mPadding, size.X),
                              Math.Max(GetTextSize().Y + mPadding, size.Y));
       }


       /// <summary>
       /// Activate/Deactivate this element. Deactivated elements are visible but do not take input
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

       #endregion rUtility
   }
}
