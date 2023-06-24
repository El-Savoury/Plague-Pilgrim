namespace Plague_Pilgrim
{
    /// <summary>
	/// Represents a set of input bindings.
	/// </summary>
    internal class InputBindSet
    {
        #region rMembers

        List<InputBinding> mBindings;

        #endregion rMembers





        #region rInitialisation

        /// <summary>
        /// Initialise set with a list of bindings
        /// </summary>
        /// <param name="bindings"></param>
        public InputBindSet(params InputBinding[] bindings)
        {
            mBindings = new List<InputBinding>(bindings);
        }

        #endregion rInitialisation





        /// <summary>
        /// Update binding presses
        /// </summary>
        /// <param name="gameTime">Frame time</param>
        #region rUpdate

        public void Update(GameTime gameTime)
        {
            mBindings.ForEach(x => x.Update(gameTime));
        }

        #endregion rUpdate;






        #region rBindingPresses

        /// <summary>
        /// Any bindings held?
        /// </summary>
        /// <returns>True if any input in the list is pressed</returns>
        public bool AnyKeyHeld()
        {
            return mBindings.Exists(x => x.IsInputDown());
        }


        /// <summary>
        /// Any bindings pressed in the last update?
        /// </summary>
        /// <returns>True if any input in the list was pressed in the last udate</returns>
        public bool AnyKeyPressed()
        {
            return mBindings.Exists(x => x.IsInputPressed());
        }


        /// <summary>
        /// Any bindings released in last update?
        /// </summary>
        /// <returns></returns>
        public bool AnyKeyReleased()
        {
            return mBindings.Exists(x => x.IsInputReleased());
        }

        #endregion rBindingPresses
    }

}
