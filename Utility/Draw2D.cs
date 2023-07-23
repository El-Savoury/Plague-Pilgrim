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
        public static void DrawTexture(DrawInfo info, Texture2D texture, Vector2 position)
        {
            info.spriteBatch.Draw(texture, position, Color.White);
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


        /// <summary>
        /// Draw a segment of a sprite
        /// </summary>
        public static void DrawPartialSprite(DrawInfo info, Texture2D texture, Vector2 pos, Vector2 sourceFrame, Vector2 frameSize, Color col)
        {
            info.spriteBatch.Draw(texture, pos, new Rectangle(new Point((int)sourceFrame.X, (int)sourceFrame.Y), new Point((int)frameSize.X, (int)frameSize.Y)), col);
        }

        #endregion rRender
    }
}
