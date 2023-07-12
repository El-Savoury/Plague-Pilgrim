namespace Plague_Pilgrim
{
    /// <summary>
    /// A role assigned to players party members determining how many points they start with
    /// </summary>
    internal class Role
    {
        #region rMembers

        StreamReader reader;
        string mTitle;
        int mPoints;
        string mDescription;
  
        #endregion rMembers





        #region rInitialisation

        /// <summary>
        /// Constructor
        /// </summary>
        public Role(string title, string DescFilePath, int points)
        {
            mTitle = title;
            mPoints = points;
            reader = new StreamReader(DescFilePath);
            mDescription = reader.ReadToEnd();
        }

        #endregion rInitialisation






        #region rUtility


        /// <summary>
        /// Gets the title/name of a certain role
        /// </summary>
        /// <returns>Name of role</returns>
        public string GetTitle()
        {
            return mTitle;
        }


        /// <summary>
        /// Gets the points associated with a certain role
        /// </summary>
        /// <returns>Number of points</returns>
        public int GetPoints()
        {
            return mPoints;
        }


        /// <summary>
        /// Gets the description of a ceratain role
        /// </summary>
        /// <returns>Role description</returns>
        public string GetDescription()
        {
            return mDescription;
        }

        #endregion rUtility

    }
}