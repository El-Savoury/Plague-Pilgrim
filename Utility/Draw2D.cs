namespace Plague_Pilgrim
{
    /// <summary>
    /// Info needed to draw
    /// </summary>
    struct DrawInfo
    {
        public GameTime gameTime;
        public SpriteBatch spriteBatch;
        public GraphicsDeviceManager graphics;
        public GraphicsDevice device;
    }


    /// <summary>
    /// Simple rendering methods
    /// </summary>
    static class Draw2D
    {
        #region rRender

        /// <summary>
        /// Draw a texture at a position
        /// </summary>
        public static void DrawTexture(DrawInfo info, Texture2D texture2D, Vector2 position)
        {
            info.spriteBatch.Draw(texture2D, position, Color.White);
        }


        /// <summary>
        /// Draw string at position (top left)
        /// </summary>
        public static void DrawString(DrawInfo info, SpriteFont font, string text, Vector2 pos, Color colour)
        {
            info.spriteBatch.DrawString(font, text, pos, colour);
        }


        /// <summary>
        /// Draw string centered at position
        /// </summary>
        public static void DrawStringCentered(DrawInfo info, SpriteFont font, string text, Vector2 pos, Color colour)
        {
            Vector2 size = font.MeasureString(text);

            info.spriteBatch.DrawString(font, text, pos - size / 2, colour); 
        }


        /// <summary>
        /// Draw a simple rectangle
        /// </summary>
        public static void DrawRect(DrawInfo info, Rectangle rect, Color col)
        {
            info.spriteBatch.Draw(Main.GetDummyTexture(), rect, col);
        }

        #endregion rRender
    }
}
