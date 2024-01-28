namespace Plague_Pilgrim
{
    /// <summary>
    /// Party creation menu screen
    /// </summary>
    internal class PartyCreateScreen: Screen
    {
        #region rMembers

        RoleManager mRoleManager;
        List<NineSliceBox> mNameInputs = new List<NineSliceBox>();
        List<NineSliceBox> mRoleSelects = new List<NineSliceBox>();

        #endregion rMembers






        #region rInitialisation

        /// <summary>
        /// Game screen constructor
        /// </summary>
        /// <param name="graphics">Graphics device</param>
        public PartyCreateScreen(GraphicsDeviceManager graphics) : base(graphics)
        {
            mRoleManager = new RoleManager();
        }

        /// <summary>
        /// Load content required for gameplay
        /// </summary>
        public override void LoadContent()
        {
            mRoleManager.LoadContent();

            //mRoleSelects.Add(new TextSelect(Vector2.Zero, Vector2.Zero, mRoleManager.GetTitles()));
            //mRoleSelects.Add(new TextSelect(new Vector2(0, 50), Vector2.Zero, mRoleManager.GetTitles()));
            //mRoleSelects.Add(new TextSelect(new Vector2(0, 100), Vector2.Zero, mRoleManager.GetTitles()));
            //mRoleSelects.Add(new TextSelect(new Vector2(0, 150), Vector2.Zero, mRoleManager.GetTitles()));

            //mNameInputs.Add(new TextInput(new Vector2(120, 0), Vector2.Zero));
            //mNameInputs.Add(new TextInput(new Vector2(120, 50), Vector2.Zero));
            //mNameInputs.Add(new TextInput(new Vector2(120, 100), Vector2.Zero));
            //mNameInputs.Add(new TextInput(new Vector2(120, 150), Vector2.Zero));
        }

        #endregion rInitialisation






        #region rUpdate

        /// <summary>
        /// Update game screen
        /// </summary>
        /// <param name="gameTime">Frame time</param>
        public override void Update(GameTime gameTime)
        {
            foreach (NineSliceBox role in mRoleSelects)
            {
                role.Update();
            }

            foreach (NineSliceBox name in mNameInputs)
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

            //foreach (NineSliceBox role in mRoleSelects)
            //{
            //    role.DrawBox(info);
            //    role.Draw(info);
            //}

            //foreach (NineSliceBox name in mNameInputs)
            //{
            //    name.DrawBox(info);
            //    name.Draw(info);
            //}

            info.spriteBatch.End();

            return mScreenTarget;
        }

        #endregion rDraw







        #region rUtility

        #endregion rUtility
    }
}

