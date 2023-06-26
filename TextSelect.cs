namespace Plague_Pilgrim
{
    enum Roles
    {
        Brother,
        Sister,
        Friar,
        Monsignor
    }

    internal class TextSelect
    {
        #region rConstants

        #endregion rConstants





        #region rMembers

        Vector2 mPosition;
        string mCurrentText = string.Empty;
        bool mIsActive = true;
        Color mColour;

        Dictionary<Roles, String> mRoles = new Dictionary<Roles, string>();

        #endregion rMembers





        #region rLoadContent

        public void LoadContent(ContentManager content)
        {
            mRoles.Add(Roles.Brother, "Brother");
            mRoles.Add(Roles.Sister, "Sister");
            mRoles.Add(Roles.Friar, "Friar");
            mRoles.Add(Roles.Monsignor, "Monsignor");
        }

        #endregion rLoadContent





        #region rInitialisation

        /// <summary>
        /// Constructor
        /// </summary>
        public TextSelect(Vector2 pos)
        {
            mPosition = pos;
            mCurrentText = mRoles[Roles.Brother];
           
        }

        #endregion rInitialisation





        #region rUpdate

        public void Update()
        {
            mColour = mIsActive ? Color.White : Color.Gray;
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




        #endregion rUtility
    }
}
