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
        Randomise
    }

    /// <summary>
    /// Manages user inputs.
    /// </summary>
    static class InputManager
    {
        #region rMembers

        static Dictionary<Controls, InputBindSet> mInputBindings = new Dictionary<Controls, InputBindSet>();

        #endregion rMembers





        #region rInitialisation

        /// <summary>
        /// Set default input bindings.
        /// </summary>
        public static void SetDefaultBindings()
        {

            // Arrow Keys
            mInputBindings.Add(Controls.Left, new InputBindSet(new KeyBinding(Keys.Left)));
            mInputBindings.Add(Controls.Right, new InputBindSet(new KeyBinding(Keys.Right)));
            mInputBindings.Add(Controls.Up, new InputBindSet(new KeyBinding(Keys.Up)));
            mInputBindings.Add(Controls.Down, new InputBindSet(new KeyBinding(Keys.Down)));

            // Menu controls
            mInputBindings.Add(Controls.Confirm, new InputBindSet(new KeyBinding(Keys.Space)));
            mInputBindings.Add(Controls.Backspace, new InputBindSet(new KeyBinding(Keys.Back)));
            mInputBindings.Add(Controls.Randomise, new InputBindSet(new KeyBinding(Keys.Tab)));
        }

        #endregion rInititialisation






        #region rKeySense

        /// <summary>
        /// Update input.
        /// </summary>
        /// <param name="gameTime">Frame time</param>
        public static void Update(GameTime gameTime)
        {
            foreach (KeyValuePair<Controls, InputBindSet> keyBindPair in mInputBindings)
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
            return mInputBindings[key].AnyKeyPressed();
        }


        /// <summary>
        /// Was a key held recently?
        /// </summary>
        /// <param name="key">Key to check keybind dictionary for</param>
        /// <returns>True if key was down in most recent update and previous update</returns>
        public static bool KeyHeld(Controls key)
        {
            return mInputBindings[key].AnyKeyHeld();
        }


        /// <summary>
        /// Was key released recently?
        /// </summary>
        /// <param name="key">Key to check keybind dictionary for</param>
        /// <returns>True if key was up in most recent update and down in previous update</returns>
        public static bool KeyReleased(Controls key)
        {
            return mInputBindings[key].AnyKeyReleased();
        }

        #endregion rKeySense
    }
}
