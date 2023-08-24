namespace Plague_Pilgrim
{
    /// <summary>
    /// New game menu screen
    /// </summary>
    internal class NewGameScreen : Screen
    {
        #region rMembers

        RoleManager mRoleManager;
        List<UIObject> mNameInputs = new List<UIObject>();
        List<UIObject> mRoleSelects = new List<UIObject>();

        #endregion rMembers






        #region rInitialisation

        /// <summary>
        /// Game screen constructor
        /// </summary>
        /// <param name="graphics">Graphics device</param>
        public NewGameScreen(GraphicsDeviceManager graphics) : base(graphics)
        {
            mRoleManager = new RoleManager();
        }

        /// <summary>
        /// Load content required for gameplay
        /// </summary>
        public override void LoadContent()
        {
            mRoleManager.LoadContent();

            mRoleSelects.Add(new TextSelect(Vector2.Zero, Vector2.Zero, mRoleManager.GetTitles()));
            mRoleSelects.Add(new TextSelect(new Vector2(0, 50), Vector2.Zero, mRoleManager.GetTitles()));
            mRoleSelects.Add(new TextSelect(new Vector2(0, 100), Vector2.Zero, mRoleManager.GetTitles()));
            mRoleSelects.Add(new TextSelect(new Vector2(0, 150), Vector2.Zero, mRoleManager.GetTitles()));

            mNameInputs.Add(new TextInput(new Vector2(120, 0), Vector2.Zero));
            mNameInputs.Add(new TextInput(new Vector2(120, 50), Vector2.Zero));
            mNameInputs.Add(new TextInput(new Vector2(120, 100), Vector2.Zero));
            mNameInputs.Add(new TextInput(new Vector2(120, 150), Vector2.Zero));
        }

        #endregion rInitialisation






        #region rUpdate

        /// <summary>
        /// Update game screen
        /// </summary>
        /// <param name="gameTime">Frame time</param>
        public override void Update(GameTime gameTime)
        {
            foreach (UIObject role in mRoleSelects)
            {
                role.Update();
            }

            foreach (UIObject name in mNameInputs)
            {
                name.Update();
            }
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

            foreach (UIObject role in mRoleSelects)
            {
                role.DrawPanel(info);
                role.Draw(info);
            }

            foreach (UIObject name in mNameInputs)
            {
                name.DrawPanel(info);
                name.Draw(info);
            }

            info.spriteBatch.End();

            return mScreenTarget;
        }

        #endregion rDraw







        #region rUtility

        #endregion rUtility
    }
}

