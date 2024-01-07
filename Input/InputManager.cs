namespace Plague_Pilgrim
{
    enum Controls
    {
        // Arrow keys
        Left,
        Right,
        Up,
        Down,

        // Menu controls
        Confirm,
        Backspace,
        Randomise,
        Escape,

        // Window controls
        Fullscreen
    }

    /// <summary>
    /// Manages user inputs.
    /// </summary>
    static class InputManager
    {
        #region rMembers

        static Dictionary<Controls, InputKey> mInputKeys = new Dictionary<Controls, InputKey>();

        #endregion rMembers






        #region rInitialisation

        /// <summary>
        /// Initialise input manager
        /// </summary>
        public static void Init()
        {
            SetDefaultControls();
        }


        /// <summary>
        /// Set default input bindings.
        /// </summary>
        public static void SetDefaultControls()
        {
            // Arrow Keys
            mInputKeys.Add(Controls.Left, new InputKey(Keys.Left));
            mInputKeys.Add(Controls.Right, new InputKey(Keys.Right));
            mInputKeys.Add(Controls.Up, new InputKey(Keys.Up));
            mInputKeys.Add(Controls.Down, new InputKey(Keys.Down));

            // Menu controls
            mInputKeys.Add(Controls.Confirm, new InputKey(Keys.Enter));
            mInputKeys.Add(Controls.Backspace, new InputKey(Keys.Back));
            mInputKeys.Add(Controls.Randomise, new InputKey(Keys.Tab));
            mInputKeys.Add(Controls.Escape, new InputKey(Keys.Escape));

            // Window controls
            mInputKeys.Add(Controls.Fullscreen, new InputKey(Keys.F11));
        }

        #endregion rInititialisation






        #region rKeySense

        /// <summary>
        /// Update input.
        /// </summary>
        /// <param name="gameTime">Frame time</param>
        public static void Update(GameTime gameTime)
        {
            foreach (KeyValuePair<Controls, InputKey> keyBindPair in mInputKeys)
            {
                keyBindPair.Value.Update(gameTime);
            }
        }


        /// <summary>
        /// Was a key pressed recently?
        /// Checks input binding dictionary for specified control input and returns if it's paired key is pressed
        /// </summary>
        /// <param name="key">Key to check keybind dictionary for</param>
        /// <returns>True if key pressed in last update</returns>
        public static bool KeyPressed(Controls key)
        {
            return mInputKeys[key].IsInputPressed();
        }


        /// <summary>
        /// Was a key held recently?
        /// </summary>
        /// <param name="key">Key to check keybind dictionary for</param>
        /// <returns>True if key was down in most recent update and previous update</returns>
        public static bool KeyHeld(Controls key)
        {
            return mInputKeys[key].IsInputDown();
        }


        /// <summary>
        /// Was key released recently?
        /// </summary>
        /// <param name="key">Key to check keybind dictionary for</param>
        /// <returns>True if key was up in most recent update and down in previous update</returns>
        public static bool KeyReleased(Controls key)
        {
            return mInputKeys[key].IsInputReleased();
        }

        #endregion rKeySense
    }
}
