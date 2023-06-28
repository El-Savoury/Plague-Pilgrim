namespace Plague_Pilgrim
{
    internal class RoleManager
    {
        #region rMembers

        Role[] mRoles;

        #endregion rMembers





        #region rLoadContent

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
