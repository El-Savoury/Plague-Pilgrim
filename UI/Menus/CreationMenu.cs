namespace Plague_Pilgrim
{
    internal class CreationMenu : Menu
    {
        #region rMembers

        RoleManager mRoleManager;
        
        #endregion rMembers







        #region rInitialiastion

        /// <summary>
        /// Creation menu constructor
        /// </summary>
        public CreationMenu()
        {
            mRoleManager = new RoleManager();
        }

        public override void LoadContent()
        {
            mRoleManager.LoadContent();

            mMenuObjects.Add(new TextSelect(Vector2.Zero, Vector2.Zero, mRoleManager.GetTitles()));
            mMenuObjects.Add(new TextSelect(new Vector2(0, 50), Vector2.Zero, mRoleManager.GetTitles()));
            mMenuObjects.Add(new TextSelect(new Vector2(0, 100), Vector2.Zero, mRoleManager.GetTitles()));
            mMenuObjects.Add(new TextSelect(new Vector2(0, 150), Vector2.Zero, mRoleManager.GetTitles()));
            mMenuObjects.Add(new TextInput(new Vector2(120, 0), Vector2.Zero));
            mMenuObjects.Add(new TextInput(new Vector2(120, 50), Vector2.Zero));
            mMenuObjects.Add(new TextInput(new Vector2(120, 100), Vector2.Zero));
            mMenuObjects.Add(new TextInput(new Vector2(120, 150), Vector2.Zero));

        }


        #endregion rInitialisation







        #region rUpdate

        // /// <summary>
        // /// Update creation menu
        // /// </summary>
        // public override void Update()
        // {
        // }

        #endregion rUpdate







        #region rDraw

        // /// <summary>
        // /// Draw creation menu
        // /// </summary>
        // /// <param name="info">Info monogame needs to to draw</param>
        // public override void Draw(DrawInfo info)
        // {

        // }

        #endregion rDraw






        #region  rUtilty

   
        #endregion rUtility
    }
}
