namespace Plague_Pilgrim
{
    /// <summary>
	/// Game specific utility functions
	/// </summary>
    static class Utility
    {
        /// <summary>
        /// Get delta time from gameTime object
        /// </summary>
        /// <param name="gameTime">Frame time</param>
        /// <returns>Time between last frame and current frame in tens of seconds</returns>
        public static float GetDeltaTime(GameTime gameTime)
        {
            return (float)gameTime.ElapsedGameTime.TotalSeconds * 10.0f;
        }
    }
}
