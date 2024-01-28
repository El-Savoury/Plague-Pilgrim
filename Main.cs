namespace Plague_Pilgrim
{
    /// <summary>
    /// Program top level
    /// </summary>
    public class Main : Game
    {
        #region rConstants

        private const double FRAME_RATE = 30d;
        private const int MIN_WINDOW_HEIGHT = 480;
        private const float ASPECT_RATIO = 1.77774f;

        #endregion rConstants


        



        #region rMembers

        private GraphicsDeviceManager mGraphics;
        private SpriteBatch mSpriteBatch;
        private Texture2D mDummyTexture;
        private Rectangle mWindowedSize;

        // Hack to access main class
        private static Main mSelf;

        #endregion rMembers






        #region rInitialisation

        /// <summary>
        /// Program Constructor
        /// </summary>
        public Main()
        {
            // XNA
            mGraphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            // Set FPS
            IsFixedTimeStep = true;
            TargetElapsedTime = System.TimeSpan.FromSeconds(1d / FRAME_RATE);

            // Handle user window resizing
            Window.ClientSizeChanged += OnResize;

            mSelf = this;
        }


        /// <summary>
        /// Init program
        /// </summary>
        protected override void Initialize()
        {
            mGraphics.PreferredBackBufferWidth = 960 ;
            mGraphics.PreferredBackBufferHeight = 540;

            // SetWindowHeight(MIN_WINDOW_HEIGHT);
            mGraphics.IsFullScreen = false;
            mGraphics.ApplyChanges();

            Window.AllowUserResizing = true;
            Window.Title = "Plague Pilgrims";

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
            InputManager.Init();
            InventoryManager.Init();

            // Set first screen that opens when game is run
            ScreenManager.ActivateScreen(ScreenType.Title);
        }
        #endregion rInitialisation






        #region rUpdate

        /// <summary>
        /// Update game
        /// </summary>
        /// <param name="gameTime">Frame time</param>
        protected override void Update(GameTime gameTime)
        {
            // Record elapsed time
            TimeManager.Update(gameTime);

            HandleWindowControls();
            UpdateGame(gameTime);

            base.Update(gameTime);
        }


        /// <summary>
        /// Update everything on the active screen
        /// </summary>
        /// <param name="gameTime">Frame time</param>
        private void UpdateGame(GameTime gameTime)
        {
            Screen screen = ScreenManager.GetActiveScreen();

            InputManager.Update(gameTime);

            if (screen != null)
            {
                screen.Update(gameTime);
            }
        }


        /// <summary>
        /// Handle user pressing fullscreen or exit keys
        /// </summary>
        private void HandleWindowControls()
        {
            if (InputManager.KeyPressed(Controls.Fullscreen))
            {
                ToggleFullscreen();
            }

            if (InputManager.KeyPressed(Controls.Escape))
            {
                Exit();
            }
        }


        /// <summary>
		/// Enter/leave full screen
		/// </summary>
        private void ToggleFullscreen()
        {
            if (mGraphics.IsFullScreen)
            {
                mGraphics.IsFullScreen = false;
                mGraphics.PreferredBackBufferWidth = mWindowedSize.Width;
                mGraphics.PreferredBackBufferHeight = mWindowedSize.Height;
            }
            else
            {
                mWindowedSize = GraphicsDevice.PresentationParameters.Bounds;
                mGraphics.IsFullScreen = true;

                mGraphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
                mGraphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            }

            mGraphics.ApplyChanges();
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

            // Draw active screen
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
                DrawScreenPixelPerfect(frameInfo, screenTargetRef);
                mSpriteBatch.End();
            }

            base.Draw(gameTime);
        }



        private void DrawScreenPixelPerfect(DrawInfo info, RenderTarget2D target)
        {
            Rectangle screenRect = info.device.PresentationParameters.Bounds;

            float scaleX = MathF.Round((float)screenRect.Width / target.Width);
            float scaleY = MathF.Round((float)screenRect.Height / target.Height);
            float scale = Math.Min(scaleX, scaleY);

            int newWidth = (int)(target.Width * scale);
            int newHeight = (int)(target.Height * scale);

            int posX = (screenRect.Width - newWidth) / 2;
            int posY = (screenRect.Height - newHeight) / 2;

            Rectangle destRect = new Rectangle(posX, posY, newWidth, newHeight);

            Draw2D.DrawTexture(info, target, destRect);
        }


        /// <summary>
        /// Callback for re-sizing the screen
        /// </summary>
        /// <param name="sender">Sender of this event</param>
        /// <param name="eventArgs">Event args</param>
        private void OnResize(object sender, EventArgs eventArgs)
        {
            if (mGraphics.IsFullScreen) { return; }

            int minWidth = (int)(ASPECT_RATIO * MIN_WINDOW_HEIGHT);

            if (Window.ClientBounds.Height >= MIN_WINDOW_HEIGHT && Window.ClientBounds.Width >= minWidth)
            {
                return;
            }
            else
            {
                SetWindowHeight(MIN_WINDOW_HEIGHT);
            }

        }


        /// <summary>
		/// Set window height so it keeps the aspect ratio
		/// </summary>
		/// <param name="height">New window height</param>
        private void SetWindowHeight(int height)
        {
            mGraphics.PreferredBackBufferWidth = (int)(height * ASPECT_RATIO);
            mGraphics.PreferredBackBufferHeight = height;
            mGraphics.ApplyChanges();
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