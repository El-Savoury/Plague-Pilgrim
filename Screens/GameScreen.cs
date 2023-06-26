namespace Plague_Pilgrim
{
    /// <summary>
    /// Gameplay screen
    /// </summary>
    internal class GameScreen : Screen
    {
        #region rMembers

        TextInput textInput;
        
        Role role;
   
        #endregion rMembers






        #region rInitialisation

        /// <summary>
        /// Game screen constructor
        /// </summary>
        /// <param name="graphics">Graphics device</param>
        public GameScreen(GraphicsDeviceManager graphics) : base(graphics)
        {
        }

        /// <summary>
        /// Load content required for gameplay
        /// </summary>s
        public override void LoadContent()
        {
           textInput = new TextInput(Vector2.Zero);
           role = new Role("Brother", "Content/Descriptions/test_file.txt", 5);
        }

        #endregion rInitialisation






        #region rUpdate

        /// <summary>
        /// Update game screen
        /// </summary>
        /// <param name="gameTime">Frame time</param>
        public override void Update(GameTime gameTime)
        {
            textInput.Update();
        }

        #endregion rUpdate






        #region rDraw

        /// <summary>
        /// Draw game screen to render target
        /// </summary>
        /// <param name="info">Info needed to draw</param>
        /// <returns>Render target with game screen drawn on it</returns>
        public override RenderTarget2D DrawToRenderTarget(DrawInfo info)
        {
            info.device.SetRenderTarget(mScreenTarget);
            info.device.Clear(Color.Black);

            info.spriteBatch.Begin();
            textInput.Draw(info);
            role.Draw(info);
            info.spriteBatch.End();

            return mScreenTarget;
        }

        #endregion rDraw







        #region rUtility

        #endregion rUtility
    }
}
