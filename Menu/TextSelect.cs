namespace Plague_Pilgrim
{
    /// <summary>
    /// Text that lets the player select an option
    /// </summary>
    internal class TextSelect
    {
        #region rConstants

        #endregion rConstants





        #region rMembers

        Vector2 mPosition;
        string mCurrentText = string.Empty;
        string[] mText;
        bool mIsActive = true;
        Color mColour;

        #endregion rMembers







        #region rInitialisation

        /// <summary>
        /// Constructor
        /// </summary>
        public TextSelect(Vector2 pos, string[] text)
        {
            mPosition = pos;
            mText = text;
        }

        #endregion rInitialisation





        #region rUpdate

        public void Update()
        {
            mColour = mIsActive ? Color.White : Color.Gray;

            if (mIsActive)
            {
                ChangeSelection();
            }
        }

        #endregion rUpdate






        #region rDraw

        public void Draw(DrawInfo info)
        {
            Draw2D.DrawString(info, FontManager.GetFont("monogram"), mCurrentText, mPosition, mColour); ;
        }

        #endregion rDraw






        #region rUtility


        private void ToggleActive()
        {
            mIsActive = !mIsActive;
        }


        /// <summary>
        /// Sets whether text input is active or not
        /// </summary>
        /// <param name="active">True to activate, false to deactivate</param>
        public void SetActive(bool active)
        {
            mIsActive = active;
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
