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

        #endregion rMembers






        #region rInitialisation

        /// <summary>
        /// Game screen constructor
        /// </summary>
        /// <param name="graphics">Graphics device</param>
        public GameplayScreen(GraphicsDeviceManager graphics) : base(graphics)
        {
            TileManager.InitTileMap(new Vector2(0, -Tile.TILE_SIZE));
        }


        /// <summary>
        /// Load content required for gameplay
        /// </summary>
        public override void LoadContent()
        {
            TileManager.LoadTileMap();
            
            // Spawn player in middle tile, bottom row 
            Point spawnTile = new Point(TileManager.GetSize().X / 2, -1);
            mPlayer = new Player(TileManager.GetTilePos(spawnTile));
            mPlayer.LoadContent();

            mCamera = new Camera(new Vector2(0, -GetScreenSize().Height));
            mCamera.TargetEntity(mPlayer);
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

            mCamera.Update(gameTime);
            mPlayer.Update(gameTime);
            
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
            mPlayer.Draw(info);

            mCamera.EndSpriteBatch(info);

            return mScreenTarget;
        }

        #endregion rDraw







        #region rUtility

        #endregion rUtility
    }
}
