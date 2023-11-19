namespace Plague_Pilgrim
{
    /// <summary>
    /// Simple timer/stopwatch class.
    /// </summary>
    public class Timer
    {
        #region rMembers
        private bool mRunning;
        private double mElapsedTimeMs = 0.0;

        #endregion rMembers






        #region rInitialisation

        /// <summary>
        /// Constructor.
        /// </summary>
        public Timer()
        {
            TimeManager.RegisterTimer(this);

            mElapsedTimeMs = 0.0;
            mRunning = false;

        }

        #endregion rInitialisation 






        #region rUpdate

        /// <summary>
        /// Update timer.
        /// </summary>
        /// <param name="gameTime">Frame time</param>
        public void Update(GameTime gameTime)
        {
            if (mRunning)
            {
                mElapsedTimeMs += gameTime.ElapsedGameTime.TotalMilliseconds;
            }
        }


        /// <summary>
        /// Start the timer
        /// </summary>
        public void Start()
        {
            mRunning = true;
        }


        /// <summary>
        /// Stop the timer.
        /// </summary>
        public void Stop()
        {
            mRunning = false;
        }


        /// <summary>
        /// Reset timer to zero.
        /// </summary>
        public void Reset()
        {
            mElapsedTimeMs = 0.0;
        }


        /// <summary>
        /// Stop the timer and reset it.
        /// </summary>
        public void StopReset()
        {
            mRunning = false;
            mElapsedTimeMs = 0.0;
        }

        #endregion rUpdate






        #region rUtility

        /// <summary>
        /// Is the timer running?
        /// </summary>
        /// <returns></returns>
        public bool IsRunning()
        {
            return mRunning;
        }


        /// <summary>
        /// Get elapsed time.
        /// </summary>
        /// <returns>Time in milliseconds</returns>
        public double GetElapsedMs()
        {
            return mElapsedTimeMs;
        }


        /// <summary>
        /// Set elapsed milliseconds.
        /// </summary>
        public void SetElapsedMs(double elapsed)
        {
            mElapsedTimeMs = elapsed;
        }

        #endregion rUtility
    }








    /// <summary>
    /// Time manager that updates all timers automatically
    /// </summary>
    public static class TimeManager
    {
        #region rMembers

        static List<Timer> mTimers = new List<Timer>();

        #endregion rMembers






        #region rUpdate

        public static void Update(GameTime gametime)
        {
            foreach (Timer timer in mTimers)
            {
                timer.Update(gametime);
            }
        }

        #endregion rUpdate





        #region rRegistry

        /// <summary>
        /// Register a timer to be updated automatically.
        /// </summary>
        /// <param name="timer">Timer to add</param>
        public static void RegisterTimer(Timer timer)
        {
            mTimers.Add(timer);
        }


        /// <summary>
        /// Remove timer from registry.
        /// </summary>
        /// <param name="timer">Timer to remove</param>
        public static void RemoveTimer(Timer timer)
        {
            mTimers.Remove(timer);
        }

        #endregion rRegistry
    }
}