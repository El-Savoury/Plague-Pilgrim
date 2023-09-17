namespace Plague_Pilgrim
{
    /// <summary>
    /// Gameplay screen
    /// </summary>
    internal class GameplayScreen : Screen
    {
        #region rMembers

        Player mPlayer;
        Camera mCamera;
        Rect2f mRect;

        #endregion rMembers






        #region rInitialisation

        /// <summary>
        /// Game screen constructor
        /// </summary>
        /// <param name="graphics">Graphics device</param>
        public GameplayScreen(GraphicsDeviceManager graphics) : base(graphics)
        {
            TileManager.InitTileMap(Vector2.Zero);

            mRect = new Rect2f(new Vector2(20, 20), new Vector2(100, 100));
        }


        /// <summary>
        /// Load content required for gameplay
        /// </summary>
        public override void LoadContent()
        {
            TileManager.LoadTileMap();

            mPlayer = new Player(TileManager.GetTileTopLeft(new Point(TileManager.GetSize().X / 2, TileManager.GetSize().Y - 1))); // Spawn player in middle tile of tile map bottom row 
            mPlayer.LoadContent(Main.GetContentManager());

            mCamera = new Camera(new Vector2(0.0f, mPlayer.GetPosition().Y + 16 - GetScreenSize().Height));
        }

        #endregion rInitialisation






        #region rUpdate

        /// <summary>
        /// Update game screen
        /// </summary>
        /// <param name="gameTime">Frame time</param>
        public override void Update(GameTime gameTime)
        {
            if (InputManager.KeyPressed(Controls.Confirm)) { TileManager.LoadTileMap(); }

            mPlayer.Update(gameTime);
            //mCamera.Update(gameTime);

            Ray2f ray = new Ray2f(mPlayer.GetCentrePos(), mPlayer.GetDirection());

            if (Collision2D.RayVsRect(ray,mRect).Collided)
            {
               
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
            info.device.Clear(Color.CornflowerBlue);

            mCamera.StartSpriteBatch(info);

            TileManager.Draw(info);


            Draw2D.DrawRect(info, mRect, Color.Blue);


            mPlayer.Draw(info);

            mCamera.EndSpriteBatch(info);

            return mScreenTarget;
        }

        #endregion rDraw







        #region rUtility

        #endregion rUtility
    }
}
