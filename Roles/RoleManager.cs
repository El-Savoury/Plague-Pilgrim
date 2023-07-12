namespace Plague_Pilgrim
{
    internal class RoleManager
    {
        #region rMembers

        Role[] mRoles;

        #endregion rMembers





        #region rLoadContent

        /// <summary>
        /// Loads a list of roles
        /// </summary>
        public void LoadContent()
        {
            mRoles = new Role[]
              {
                new Role("Brother", "Content/Descriptions/test_file.txt", 5),
                new Role("Sister", "Content/Descriptions/test_file.txt", 5),
                new Role("Friar", "Content/Descriptions/test_file.txt", 5),
                new Role("Monsignor", "Content/Descriptions/test_file.txt", 5)
              };
        }

        #endregion rLoadContent






        #region rUtility

        /// <summary>
        /// Gets an array containing the titles of all roles
        /// </summary>
        /// <returns>Array of role titles</returns>
        public string[] GetTitles()
        {
            List<string> titles = new List<string>();

            foreach (Role role in mRoles)
            {
                titles.Add(role.GetTitle());
            }

            return titles.ToArray();
        }

        #endregion rUtility
    }
}
