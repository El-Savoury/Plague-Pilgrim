﻿namespace Plague_Pilgrim
{
    /// <summary>
    /// Designated area for drawing game elements.
    /// </summary>
    abstract class Screen
    {
        #region rConstants



        #endregion rConstants






        #region rMembers

        protected GraphicsDeviceManager mGraphics;
        protected RenderTarget2D mScreenTarget;
        private int mScreenWidth, mScreenHeight;

        #endregion rMembers






        #region rInitialisation

        /// <summary>
        /// Screen constructor.
        /// </summary>
        /// <param name="content">Monogame content manager</param>
        /// <param name="graphics">Graphics device manager</param>
        public Screen(GraphicsDeviceManager graphics)
        {
            mGraphics = graphics;
            mScreenWidth = graphics.PreferredBackBufferWidth;
            mScreenHeight = graphics.PreferredBackBufferHeight;
            mScreenTarget = new RenderTarget2D(graphics.GraphicsDevice, mScreenWidth, mScreenHeight);
        }


        /// <summary>
        /// Load content for this screen.
        /// </summary>
        public virtual void LoadContent() { }


        /// <summary>
        /// Called when the screen is activated.
        /// </summary>
        public virtual void OnActivate() { }


        /// <summary>
        /// Called when the screen is deactivated
        /// </summary>
        public virtual void OnDeactivate() { }

        #endregion rInitialisation





        #region rUpdate

        /// <summary>
        /// Update the screen.
        /// </summary>
        /// <param name="gameTime">Frame time</param>
        public abstract void Update(GameTime gameTime);

        #endregion rUpdate






        #region rDraw

        /// <summary>
        /// Draw screen to render target.
        /// </summary>
        /// <param name="info">Info needed to draw</param>
        /// <returns>Render target with screen drawn on</returns>
        public abstract RenderTarget2D DrawToRenderTarget(DrawInfo info);

        #endregion rDraw






        #region rUtility

        /// <summary>
        /// Get the size of the screen.
        /// </summary>
        /// <returns>Rectangle with width and height of screen</returns>
        public Rectangle GetScreenSize()
        {
            return new Rectangle(0,0, mScreenWidth,mScreenHeight);
        }

        #endregion rUtility
    }
}
