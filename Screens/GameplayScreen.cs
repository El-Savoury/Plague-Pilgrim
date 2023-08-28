﻿namespace Plague_Pilgrim
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
            TileManager.InitTileMap(Vector2.Zero);
            mCamera = new Camera();
        }


        /// <summary>
        /// Load content required for gameplay
        /// </summary>
        public override void LoadContent()
        {
            TileManager.LoadTileMap();

            mPlayer = new Player(TileManager.GetTileTopLeft(new Point(TileManager.GetSize().X / 2, TileManager.GetSize().Y - 1))); // Spawn player in middle tile of tile map bottom row 
            mPlayer.LoadContent(Main.GetContentManager());
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
            mCamera.Update(gameTime);
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

            Vector2 viewPortSize = new Vector2(GetScreenSize().Width, GetScreenSize().Height);

            mCamera.StartSpriteBatch(info, viewPortSize);
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
