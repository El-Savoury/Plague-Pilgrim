using System.Diagnostics.Tracing;

namespace Plague_Pilgrim
{
    /// <summary>
    /// Options for scaling and ordering
    /// </summary>
    struct SpriteBatchOptions
    {
        public SpriteBatchOptions()
        {
            mSortMode = SpriteSortMode.BackToFront;
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
    /// Camera for creating and drawing to the viewport
    /// </summary>
    internal class Camera
    {
        #region rConstants

        private const int END_POINT = 0;

        #endregion rConstants



        #region rMembers

        private Vector2 mPosition;
        private MovingEntity mTargetEntity;
        private SpriteBatchOptions mSpriteBatchOptions;

        #endregion rMembers







        #region rInitialisation

        public Camera(Vector2 pos)
        {
            mPosition = pos;
            mSpriteBatchOptions = new SpriteBatchOptions();
        }

        #endregion rInitialisation








        #region rUpdate

        /// <summary>
        /// Update camera
        /// </summary>
        public void Update(GameTime gameTime)
        {
            if (mTargetEntity.GetVelocity().Y < 0)
            {
                float velocityY = mTargetEntity.VelocityToDisplacement(gameTime).Y;

                mPosition.Y += Math.Clamp(velocityY, - 6.0f * Utility.GetDeltaTime(gameTime), 0);
            }
            else if (mTargetEntity.GetVelocity() != Vector2.Zero)
            {
                mPosition.Y -= 6.0f * Utility.GetDeltaTime(gameTime);
            }

            if (mPosition.Y < END_POINT)
            {
                mPosition.Y = END_POINT;
            }
        }

        #endregion rUpdate








        #region rDraw


        /// <summary>
        /// Caulate perspective matrix
        /// </summary>
        Matrix CalculateMatrix()
        {
            return Matrix.CreateTranslation(-(int)mPosition.X, -(int)mPosition.Y, 0.0f);
        }


        /// <summary>
        /// Start the sprite batch
        /// </summary>
        public void StartSpriteBatch(DrawInfo info)
        {
            info.spriteBatch.Begin(mSpriteBatchOptions.mSortMode,
                                    mSpriteBatchOptions.mBlend,
                                    mSpriteBatchOptions.mSamplerState,
                                    mSpriteBatchOptions.mDepthStencilState,
                                    mSpriteBatchOptions.mRasterizerState,
                                    null,
                                    CalculateMatrix());
        }


        /// <summary>
        /// End the sprite batch
        /// </summary>
        public void EndSpriteBatch(DrawInfo info)
        {
            info.spriteBatch.End();
        }

        #endregion rDraw








        #region rUtility

        /// <summary>
        /// Set sprite batch options
        /// </summary>
        public void SetOptions(SpriteBatchOptions options)
        {
            mSpriteBatchOptions = options;
        }


        /// <summary>
        /// Get position of camera
        /// </summary>
        public Vector2 GetPos()
        {
            return new Vector2(mPosition.X, mPosition.Y);
        }


        /// <summary>
        /// Set entity for camera to follow.
        /// </summary>
        public void TargetEntity(MovingEntity entity)
        {
            mTargetEntity = entity;
        }

        #endregion rUtility
    }
}
