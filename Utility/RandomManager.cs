namespace Plague_Pilgrim
{
    /// <summary>
    /// Manager to store a single seeded random for all classes to use.
    /// </summary>
    static class RandomManager
    {
        #region rMembers

        static Random mWorldRandom = new Random();

        #endregion rMembers






        #region rSeedConfig

        /// <summary>
        /// Set the random seed
        /// </summary>
        /// <param name="seed">Seed to set</param>
        public static void SeedRandom(int seed)
        {
            mWorldRandom = new Random(seed);
        }

        /// <summary>
        /// Return seeded random
        /// </summary>
        /// <returns>Global random</returns>
        public static Random GetWorldSeed()
        {
            return mWorldRandom;
        }

        #endregion rSeedConfig






        #region rUtility

        /// <summary>
        /// Returns a head or tails result for basic random decisions
        /// </summary>
        /// <returns>Either 0 or 1</returns>
        public static int FlipCoin()
        {
            return mWorldRandom.Next(0, 2);
        }

        #endregion rUtlity
    }
}
