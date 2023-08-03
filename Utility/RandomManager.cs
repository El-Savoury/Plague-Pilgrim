namespace Plague_Pilgrim
{
    internal class SeededRandom
    {
        #region rMembers

        private int mSeed;

        #endregion rMembers





        #region rSeedConfig

        /// <summary>
		/// Construct random from random variable
		/// </summary>
        public SeededRandom()
        {
            mSeed = new Random().Next();
        }

        #endregion rSeedConfig
    }


    /// <summary>
	/// Simple manager to store a single random for all classes to use.
	/// </summary>
    static class RandomManager
    {
        static SeededRandom mWorldRandom = new SeededRandom();

        static SeededRandom GetWorldSeed()
        {
            return mWorldRandom;
        }
    }
}
