namespace Plague_Pilgrim
{
    /// <summary>
    /// Manages UI elements
    /// </summary>
    internal class UIManager
    {
        #region rMembers

        List<UIObject> mUIObjects;

        #endregion rMembers







        #region rInitialisation

        /// <summary>
        /// Constructor
        /// </summary>
        public UIManager()
        {
            mUIObjects = new List<UIObject>();
            mUIObjects[0].SetActive(true);
        }


        /// <summary>
        /// Load content needed to create UI objects
        /// </summary>
        public void LoadContent()
        {

        }

        #endregion rInitialisation







        #region rUpdate

        /// <summary>
        /// Update all UI objects
        /// </summary>
        public void Update()
        {
            foreach (UIObject obj in mUIObjects)
            {
                obj.Update();
            }
        }

        #endregion rUpdate







        #region rDraw

        /// <summary>
        /// Draw all UI objects
        /// </summary>
        /// <param name="info">Info needed by Monogame to draw</param>
        public void Draw(DrawInfo info)
        {
            foreach (UIObject obj in mUIObjects)
            {
                obj.Draw(info);
            }
        }

        #endregion rDraw






        #region rUtility

        private void NextObject()
        {

        }

        #endregion rUtility
    }
}
