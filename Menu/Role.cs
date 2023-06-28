namespace Plague_Pilgrim
{
    internal class Role
    {
        #region rMembers

        StreamReader reader;
        string mTitle;
        string mDescription;
        int mPoints;
        Color mColour = Color.White;

        #endregion rMembers





        #region rInitialisation

        public Role(string title, string DescFilePath, int points)
        {
            mTitle = title;
            mPoints = points;
            reader = new StreamReader(DescFilePath);
            mDescription = reader.ReadToEnd();
        }

        #endregion rInitialisation





        #region rDraw

        public void Draw(DrawInfo info)
        {
            // info.spriteBatch.DrawString(FontManager.GetFont("monogram"), mDescription, new Vector2(100, 100), mColour);
        }

        #endregion rDraw






        #region rUtility

        public string GetTitle()
        {
            return mTitle;
        }

        #endregion rUtility

    }
}