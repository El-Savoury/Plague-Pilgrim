namespace Plague_Pilgrim
{
    static class Inventory
    {
        #region rConstants

        #endregion rConstants






        #region rMembers

        static int mFood;

        #endregion rMembers






        #region rDraw

        public static void Draw(DrawInfo info, int x , int y)
        {
            Microsoft.Xna.Framework.Vector2 pos = new Microsoft.Xna.Framework.Vector2(x,y);
            Draw2D.DrawString(info, FontManager.GetFont("monogram"), "Food = "+ Convert.ToString(mFood), pos, Color.White);
        }
        
        #endregion rDraw






        #region rUtility

        static public void Add(int x)
        {
            mFood += x;
        }

        #endregion rUtility
    }
}