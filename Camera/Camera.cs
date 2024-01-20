using Microsoft.Xna.Framework.Graphics;
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
            mSortMode = SpriteSortMode.Deferred;
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
        private const int DEFAULT_SPEED = 4;

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
            mPosition.Y -= DEFAULT_SPEED * Utility.GetDeltaTime(gameTime);

            //if (mTargetEntity.GetVelocity().Y <= 0)
            //{
            //    float VelY = mTargetEntity.GetVelocity().Y;
            //    mPosition.Y += Math.Clamp(VelY, -4, 0) * Utility.GetDeltaTime(gameTime);
            //}
            //else
            //{
            //    mPosition.Y -= 4 * Utility.GetDeltaTime(gameTime);
            //}

            if (mPosition.Y < END_POINT) { mPosition.Y = END_POINT; }

            ClampEntityToBounds(mTargetEntity);

            // Cast position to ints to prevent jerky sub pixel movement
            mPosition.X = (int)Math.Round(mPosition.X);
            mPosition.Y = (int)Math.Round(mPosition.Y);
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


        /// <summary>
        /// Clamp target entity to bounds of camera view
        /// </summary>
        /// <param name="entity"></param>
        public void ClampEntityToBounds(Entity entity)
        {
            Rect2f view = new Rect2f(new Vector2(GetPos().X, GetPos().Y), ScreenManager.GetActiveScreen().GetScreenSize().Width, ScreenManager.GetActiveScreen().GetScreenSize().Height); // Screen size TBC

            float posX = Math.Clamp(entity.GetPos().X, view.min.X, view.max.X - entity.ColliderBounds().Width);
            float posY = entity.GetPos().Y;

            if (mPosition.Y > END_POINT)
            {
                posY = Math.Clamp(posY, view.min.Y, view.max.Y - entity.ColliderBounds().Height);
            }
            else
            {
                if (posY > view.max.Y - entity.ColliderBounds().Height)
                {
                    posY = view.max.Y - entity.ColliderBounds().Height;
                }
            }

            entity.SetPos(new Vector2(posX, posY));
        }

        #endregion rUtility
    }
}
