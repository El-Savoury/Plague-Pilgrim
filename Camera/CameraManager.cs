namespace Plague_Pilgrim
{
    // Class to manage camera movement and specs
    static class CameraManager
    {
        #region rMembers

        static Camera mCamera;
        static CameraSpec mCurrentSpec;
        static Vector2 mViewPortSize;

        #endregion rMembers






        #region rInitialisation

        public static void Init()
        {
            mCamera = new Camera();
            mCurrentSpec = mCamera.GetCurrentSpec();
        }

        #endregion rInitialisation







        #region rUpdate

        public static void Update(GameTime gameTime)
        {
            
        }

        #endregion rUpdate







        #region rUtility

        /// <summary>
        /// Set the size of the viewport i.e. the area of the game world that will be seen by player
        /// </summary>
        public static void SetViewPortSize(float width, float height)
        {
            mViewPortSize = new Vector2(width, height);
        }


        /// <summary>
        /// Move the camera
        /// </summary>
        /// <param name="velocity">Amount to move</param>
        public static void MoveCamera(Vector2 velocity, GameTime gameTime)
        {
            mCurrentSpec.mPosition += velocity * Utility.GetDeltaTime(gameTime);
        }


        /// <summary>
        /// Clamp movement of camera to within specified positions
        /// </summary>
        /// <param name="viewPortSize">Visible game area size</param>
        /// <param name="pos1">Top of area</param>
        /// <param name="pos2">Bottom of area</param>
        private static void ClampPosition(float pos1, float pos2)
        {
            if (mCurrentSpec.mPosition.Y < pos1) { mCurrentSpec.mPosition.Y = pos1; }

            if (mCurrentSpec.mPosition.Y + mViewPortSize.Y > pos2) { mCurrentSpec.mPosition.Y = pos2 - mViewPortSize.Y; }
        }

        #endregion rUtility
    }
}
