using System.Reflection.PortableExecutable;

namespace Plague_Pilgrim
{
    /// <summary>
    /// Text box that lets the player select an option
    /// </summary>
    class TextSelect : UIElement
    {
        #region rMembers

        string[] mText;

        #endregion rMembers







        #region rInitialisation

        /// <summary>
        /// Constructor
        /// </summary>
        public TextSelect(Vector2 pos, Vector2 size, string[] text) : base(pos, size)
        {
            mText = text;
        }

        #endregion rInitialisation





        #region rUpdate

        /// <summary>
        /// Update text select box
        /// </summary>
        public override void Update()
        {
            if (mActive)
            {
                ChangeSelection();
            }
        }

        #endregion rUpdate






        #region rDraw

        /// <summary>
        /// Draw text select box
        /// </summary>
        /// <param name="info">Info needed by monogame to draw</param>
        public override void Draw(DrawInfo info)
        {
            Draw2D.DrawString(info, FontManager.GetFont("monogram"), mCurrentText, mPosition, mColour);
        }

        #endregion rDraw






        #region rUtility


        private void ToggleActive()
        {
            mActive = !mActive;
        }


        /// <summary>
        /// Changes the displayed text when player presses left or right
        /// </summary>
        private void ChangeSelection()
        {
            int index = Array.IndexOf(mText, mCurrentText);

            if (InputManager.KeyPressed(Controls.Right))
            {
                index++;
            }
            else if (InputManager.KeyPressed(Controls.Left))
            {
                index--;
            }

            if (index > mText.Length - 1)
            {
                index = 0;
            }
            else if (index < 0)
            {
                index = mText.Length - 1;
            }

            mCurrentText = mText[index];
        }


        #endregion rUtility
    }
}
