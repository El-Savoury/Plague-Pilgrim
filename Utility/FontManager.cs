namespace Plague_Pilgrim
{
    internal static class FontManager
    {
        #region rMembers

        static Dictionary<string, SpriteFont> mFonts = new Dictionary<string, SpriteFont>();

        #endregion rMembers






        #region rUtility

        /// <summary>
        /// Loads all fonts
        /// </summary>
        /// <param name="content">Monogame content manager</param>
        public static void LoadAllFonts(ContentManager content)
        {
            mFonts.Add("monogram", content.Load<SpriteFont>("Fonts/monogram"));
        }
        

        /// <summary>
        /// Get a specific font.
        /// </summary>
        /// <param name="key">Font name</param>
        /// <returns>Spritefont reference</returns>
        public static SpriteFont GetFont(string key)
        {
            return mFonts.GetValueOrDefault(key);
        }

        #endregion rUtility
    }
}
