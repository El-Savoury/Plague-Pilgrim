namespace Plague_Pilgrim
{
    internal class TitleScreen : Screen
    {
        #region rMembers

        NineSliceBox box = new NineSliceBox(Vector2.Zero, new Vector2(100,100));

        #endregion rMembers






        #region rInitialisation

        /// <summary>
        /// Title screen constructor.
        /// </summary>
        /// <param name="graphics">Graphics device</param>
        public TitleScreen(GraphicsDeviceManager graphics) : base(graphics)
        {
        }


        /// <summary>
        /// Load content required for title screen.
        /// </summary>
        public override void LoadContent()
        {

        }

        #endregion rInitialisation






        #region rUpdate

        public override void Update(GameTime gameTime)
        {
            if (InputManager.KeyPressed(Controls.Confirm))
            {
                ScreenManager.ActivateScreen(ScreenType.Gameplay);
            }
        }

        #endregion rUpdate





        #region rDraw

        public override RenderTarget2D DrawToRenderTarget(DrawInfo info)
        {
            info.device.SetRenderTarget(mScreenTarget);
            info.device.Clear(Color.CornflowerBlue);

            info.spriteBatch.Begin(SpriteSortMode.FrontToBack,
                                  BlendState.AlphaBlend,
                                  SamplerState.PointClamp,
                                  DepthStencilState.Default,
                                  RasterizerState.CullNone);

            // TODO: DRAW TITLE SCREEN 
            box.Draw(info);

            info.spriteBatch.End();

            return mScreenTarget;
        }

        #endregion rDraw
    }
}
