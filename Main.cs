namespace Plague_Pilgrim
{
    /// <summary>
    /// Program top level
    /// </summary>
    public class Main : Game
    {
        #region rConstants

        private const double FRAME_RATE = 30d;

        #endregion rConstants






        #region rMembers

        private GraphicsDeviceManager mGraphics;
        private SpriteBatch mSpriteBatch;
        private Texture2D mDummyTexture;

        // Hack to access main class
        private static Main mSelf;

        #endregion rMembers






        #region rInitialisation

        /// <summary>
        /// Program Constructor
        /// </summary>
        public Main()
        {
            mSelf = this;

            // XNA
            mGraphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            // Set FPS
            IsFixedTimeStep = true;
            TargetElapsedTime = System.TimeSpan.FromSeconds(1d / FRAME_RATE);
        }


        /// <summary>
        /// Init program
        /// </summary>
        protected override void Initialize()
        {
            mGraphics.PreferredBackBufferWidth = 800;
            mGraphics.PreferredBackBufferHeight = 800;

            mGraphics.IsFullScreen = false;
            mGraphics.ApplyChanges();

            Window.AllowUserResizing = true;
            Window.Title = "Plague Pilgrim";

            InputManager.SetControls();

            base.Initialize();
        }


        /// <summary>
        /// Load all game content
        /// </summary>
        protected override void LoadContent()
        {
            mSpriteBatch = new SpriteBatch(GraphicsDevice);

            mDummyTexture = new Texture2D(GraphicsDevice, 1, 1);
            mDummyTexture.SetData(new Color[] { Color.White });

            FontManager.LoadAllFonts(Content);
            ScreenManager.LoadAllScreens(mGraphics);
            ScreenManager.ActivateScreen(ScreenType.Gameplay);
        }
        #endregion rInitialisation






        #region rUpdate

        /// <summary>
        /// Update game
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Update(GameTime gameTime)
        {
            // Record elapsed time
            TimeManager.Update(gameTime);

            // Update active screen.
            Screen screen = ScreenManager.GetActiveScreen();

            InputManager.Update(gameTime);

            if (screen != null)
            {
                screen.Update(gameTime);
            }

            base.Update(gameTime);
        }

        #endregion rUpdate





        #region rDraw

        /// <summary>
        /// Draw game to render target
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Draw(GameTime gameTime)
        {
            DrawInfo frameInfo;

            frameInfo.graphics = mGraphics;
            frameInfo.spriteBatch = mSpriteBatch;
            frameInfo.gameTime = gameTime;
            frameInfo.device = GraphicsDevice;

            GraphicsDevice.Clear(Color.CornflowerBlue);

            // Draw active screen.
            Screen screen = ScreenManager.GetActiveScreen();

            if (screen != null)
            {
                RenderTarget2D screenTargetRef = screen.DrawToRenderTarget(frameInfo);
                GraphicsDevice.SetRenderTarget(null);


                mSpriteBatch.Begin(SpriteSortMode.BackToFront,
                                                        BlendState.AlphaBlend,
                                                        SamplerState.PointClamp,
                                                        DepthStencilState.Default,
                                                        RasterizerState.CullNone);
                DrawScreenAsTexture(frameInfo, screenTargetRef);
                mSpriteBatch.End();
            }

            base.Draw(gameTime);
        }


        /// <summary>
        /// Draw render target containing active screen as a single texture
        /// </summary>
        /// <param name="info">Info needed to draw</param>
        /// <param name="screen">Screen to draw</param>
        private void DrawScreenAsTexture(DrawInfo info, RenderTarget2D screen)
        {
            Draw2D.DrawTexture(info, screen, Vector2.Zero);
        }

        #endregion rDraw






        #region rUtility

        /// <summary>
        /// Get the graphics device.
        /// </summary>
        public static GraphicsDevice GetGraphicsDevice()
        {
            return mSelf.GraphicsDevice;
        }


        /// <summary>
        /// Get the content manager.
        /// </summary>
        public static ContentManager GetContentManager()
        {
            return mSelf.Content;
        }


        /// <summary>
        /// Get a dummy white texture.
        /// </summary>
        public static Texture2D GetDummyTexture()
        {
            return mSelf.mDummyTexture;
        }

        #endregion rUtility
    }
}