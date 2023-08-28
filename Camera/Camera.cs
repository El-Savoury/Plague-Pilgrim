namespace Plague_Pilgrim
{
    /// <summary>
	/// Struct to specify the camera properties
	/// </summary>
	struct CameraSpec
    {
        public CameraSpec()
        {
            mPosition = Vector2.Zero;
            mRotation = 0.0f;
            mZoom = 1.0f;
        }

        public Vector2 mPosition;
        public float mRotation;
        public float mZoom;
    }


    /// <summary>
	/// Options for scaling and ordering
	/// </summary>
	struct SpriteBatchOptions
    {
        public SpriteBatchOptions()
        {
            mSortMode = SpriteSortMode.FrontToBack;
            mBlend = BlendState.AlphaBlend;
            mSamplerState = SamplerState.PointClamp;
            mDepthStencilState = DepthStencilState.Default;
            mRasterizerState = RasterizerState.CullNone;
        }

        public SpriteSortMode mSortMode;
        public BlendState mBlend;
        public SamplerState mSamplerState;
        public DepthStencilState mDepthStencilState;
        public RasterizerState mRasterizerState;
    }


    /// <summary>
	/// Camera for drawing the viewport
	/// </summary>
    internal class Camera
    {

        #region rMembers

        private CameraSpec mCameraSpec;
        private SpriteBatchOptions mSpriteBatchOptions;

        #endregion rMembers






        #region rInitialisation

        public Camera()
        {
            mCameraSpec = new CameraSpec();
            mSpriteBatchOptions = new SpriteBatchOptions();
        }

        #endregion rInitialisation







        #region rUpdate

        /// <summary>
        /// Update camera
        /// </summary>
        public void Update(GameTime gameTime)
        {
            //Move(new Vector2(0.0f, -3.0f), gameTime);
        }

        #endregion rUpdate





        #region rDraw


        /// <summary>
        /// Caulate perspective matrix
        /// </summary>
        Matrix CalculateMatrix(Vector2 ViewPortSize)
        {
            return Matrix.CreateTranslation(-(int)mCameraSpec.mPosition.X, -(int)mCameraSpec.mPosition.Y, 0.0f);
        }


        /// <summary>
        /// Start the sprite batch
        /// </summary>
        public void StartSpriteBatch(DrawInfo info, Vector2 viewPortSize)
        {
            info.spriteBatch.Begin(mSpriteBatchOptions.mSortMode,
                                    mSpriteBatchOptions.mBlend,
                                    mSpriteBatchOptions.mSamplerState,
                                    mSpriteBatchOptions.mDepthStencilState,
                                    mSpriteBatchOptions.mRasterizerState,
                                    null,
                                    CalculateMatrix(viewPortSize));
        }


        /// <summary>
        /// End the sprite batch
        /// </summary>
        /// <param name="info"></param>
        public void EndSpriteBatch(DrawInfo info)
        {
            info.spriteBatch.End();
        }

        #endregion rDraw





        #region rUtility
        
        /// <summary>
        /// Set camera spec
        /// </summary>
        public void SetSpec(CameraSpec spec)
        {
            mCameraSpec = spec;
        }


        /// <summary>
        /// Set sprite batch options
        /// </summary>
        public void SetOptions(SpriteBatchOptions options)
        {
            mSpriteBatchOptions = options;
        }


        /// <summary>
		/// Get the current spec
		/// </summary>
		public CameraSpec GetCurrentSpec()
        {
            return mCameraSpec;
        }


        /// <summary>
        /// Get position of camera
        /// </summary>
        public Vector2 GetPosition()
        {
            return new Vector2(mCameraSpec.mPosition.X, mCameraSpec.mPosition.Y);
        }

        #endregion rUtility
    }
}
