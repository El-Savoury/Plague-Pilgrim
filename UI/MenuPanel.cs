namespace Plague_Pilgrim
{
    /// <summary>
    /// Represents a menu panel with a table of options
    /// </summary>
    internal class MenuPanel : UIObject
    {
        #region rMembers

        UIObject[,] mTable;
        Point mCellTable = new Point(1, 0);

        #endregion rMembers






        #region rInitialisation

        public MenuPanel(Vector2 pos, Vector2 size) : base(pos, size)
        {

        }

        #endregion rInitialisation






        #region rUpdate

        /// <summary>
        /// Update menu panel
        /// </summary>
        public override void Update()
        {
            foreach (UIObject obj in mTable)
            {
                obj.Update();
            }
        }

        #endregion rUpdate






        #region rDraw

        /// <summary>
        /// Draw menu panel
        /// </summary>
        public override void Draw(DrawInfo info)
        {
            foreach (UIObject obj in mTable)
            {
                obj.Draw(info);
            }
        }

        #endregion rDraw








        #region rUtility

        /// <summary>
        /// Set number of columns and rows in table
        /// </summary>
        /// <param name="columns">Vertical</param>
        /// <param name="rows">Horizontal</param>
        private void SetTable(int columns, int rows)
        {
            mCellTable = new Point(columns, rows);
        }


        /// <summary>
        /// Set all cells to the size of largest cell
        /// </summary>
        private void SetMaxCellSize()
        {
            foreach (UIObject obj in mTable)
            {
                mCellSize.X = Math.Max(obj.GetCellSize().X, mCellSize.X);
                mCellSize.Y = Math.Max(obj.GetCellSize().Y, mCellSize.Y);
            }
        }

        #endregion rUtility
    }
}
