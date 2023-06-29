namespace Plague_Pilgrim
{
    abstract class TextBox
    {
        #region rMembers

        protected Vector2 mPosition;
        protected Vector2 mSize;
        private bool mActive;

        #endregion rMembers







        #region rInitialiastion

        /// <summary>
        /// Text box constructor
        /// </summary>
        /// <param name="pos">Top left corner position</param>
        /// <param name="size">Width and height of box</param>
        public TextBox(Vector2 pos, Vector2 size)
        {
            mPosition = pos;
            mSize = size;
            mActive = false;
        }


        ///// <summary>
        ///// Load content needed to display textbox
        ///// </summary>
        ///// <param name="content">Monogame content manager</param>
        //public abstract void LoadContent(ContentManager content);


        #endregion rInitialisation







        #region rUpdate

        /// <summary>
        /// Update text box
        /// </summary>
        public virtual void Update(GameTime gameTime)
        {
        }

        #endregion rUpdate







        #region rDraw

        /// <summary>
        /// Draw text box
        /// </summary>
        /// <param name="info">Info monogame needs to to draw</param>
        public abstract void Draw(DrawInfo info);

        #endregion rDraw






        #region  rUtilty

        /// <summary>
        /// Activate/deactivate the functions of the text box. Deactivated text boxes can't be interacted with by player
        /// </summary>
        /// <param name="active"></param>
        public virtual void SetActive(bool active)
        {
            mActive = active;
        }


        /// <summary>
        /// Is the text box active?
        /// </summary>
        /// <returns>True if active</returns>
        public bool IsActive()
        {
            return mActive;
        }

        #endregion rUtility
    }
}
