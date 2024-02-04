namespace Plague_Pilgrim
{
    internal class TitleScreen : Screen
    {
        #region rMembers

        TextBox box = new TextBox(new Vector2(100,100), "This text box thing is going to be bloody massive", new Vector2(200,200));

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

            info.spriteBatch.Begin();

            // TODO: DRAW TITLE SCREEN 
            box.Draw(info);

            info.spriteBatch.End();

            return mScreenTarget;
        }

        #endregion rDraw
    }
}
