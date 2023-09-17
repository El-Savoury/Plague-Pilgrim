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


        /// <summary>
        /// Swap two numbers using a tuple
        /// </summary>
        public static void Swap(ref float x, ref float y)
        {
            (x, y) = (y, x);
        }


        /// <summary>
        /// Gets the maximum in a list
        /// </summary>
        public static T GetMax<T>(ref List<T> list, IComparer<T> comparer)
        {
            T maxValue = list[0];

            for (int i = 1; i < list.Count; i++)
            {
                if (comparer.Compare(list[i], maxValue) > 0)
                {
                    maxValue = list[i];
                }
            }

            return maxValue;
        }


        /// <summary>
        /// Gets the minimum in a list
        /// </summary>
        public static T GetMin<T>(ref List<T> list, IComparer<T> comparer)
        {
            T minValue = list[0];

            for (int i = 1; i < list.Count; i++)
            {
                if (comparer.Compare(list[i], minValue) < 0)
                {
                    minValue = list[i];
                }
            }

            return minValue;
        }
    }
}
