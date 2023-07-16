namespace Plague_Pilgrim
{
    abstract class Menu
    {
        #region rMembers

        protected List<UIObject> mMenuObjects;

        #endregion rMembers







        #region rInitialiastion

        /// <summary>
        /// Menu constructor
        /// </summary>
        public Menu()
        {
            mMenuObjects = new List<UIObject>();
        }


         /// <summary>
        /// Load menu objects
        /// </summary>
        public abstract void LoadContent();


        #endregion rInitialisation







        #region rUpdate

        /// <summary>
        /// Update menu
        /// </summary>
        public virtual void Update()
        {
            foreach (UIObject obj in mMenuObjects)
            {
                obj.Update();
            }
        }

        #endregion rUpdate







        #region rDraw

        /// <summary>
        /// Draw menu
        /// </summary>
        /// <param name="info">Info monogame needs to to draw</param>
        public virtual void Draw(DrawInfo info)
        {
             foreach (UIObject obj in mMenuObjects)
            {
                obj.Draw(info);
            }
        }

        #endregion rDraw






        #region  rUtilty

   
        #endregion rUtility
    }
}
