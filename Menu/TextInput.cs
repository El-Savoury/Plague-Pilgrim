namespace Plague_Pilgrim
{
    /// <summary>
    /// Text box that lets player type to input text
    /// </summary>
    internal class TextInput : TextBox
    {
        #region rConstants

        int CHAR_LIMIT = 200;
        string CURSOR = "_";

        #endregion rConstants





        #region rMembers

        Color mColour;
        string mCurrentText = string.Empty;
        bool mIsActive = true;
        int mAddCounter = 3;
        int mRemoveCounter = 2;
        List<string> mNames = new List<string>();


        #endregion rMembers






        #region rInitialisation

        /// <summary>
        /// Constructor
        /// </summary>
        public TextInput(Vector2 pos, Vector2 size) : base(pos, size)
        {
            mPosition = pos;

            // Add male names
            using (StreamReader reader = new StreamReader("Content/Names/male_names.txt"))
            {
                string name;
                while ((name = reader.ReadLine()) != null)
                {
                    mNames.Add(name);
                }
            }

            // Add female names
            using (StreamReader reader = new StreamReader("Content/Names/female_names.txt"))
            {
                string name;
                while ((name = reader.ReadLine()) != null)
                {
                    mNames.Add(name);
                }
            }
        }

        #endregion rInitialisation





        #region rUpdate

        /// <summary>
        /// Update text input
        /// </summary>
        public void Update()
        {
            RandomiseName();
            mColour = mIsActive ? Color.White : Color.Gray;

            if (InputManager.KeyPressed(Controls.Up)) { ToggleActive(); }

            if (mIsActive)
            {
                Keys[] keys = Keyboard.GetState().GetPressedKeys();
                string currentKey = GetKeyAsString(keys);

                if (currentKey == "back")
                {
                    RemoveText();
                }
                else if (!IsOverCharLimit())
                {
                    AddText(currentKey);
                }
            }
        }



        #endregion rUpdate






        #region rDraw

        /// <summary>
        /// Draw current text
        /// </summary>
        /// <param name="info">Info needed by Monogame to draw</param>
        public override void Draw(DrawInfo info)
        {
            string text = mCurrentText;

            if (mIsActive && !IsOverCharLimit())
            {
                text = mCurrentText + CURSOR;
            }

            Draw2D.DrawString(info, FontManager.GetFont("monogram"), text, mPosition, mColour);
        }

        #endregion rDraw






        #region rUtility

        /// <summary>
        /// Gets a name at random from name list
        /// </summary>
        private void RandomiseName()
        {
            if (InputManager.KeyPressed(Controls.Randomise))
            {
                Random rand = new Random();

                if (mNames != null)
                {
                    mCurrentText = mNames[rand.Next(0, mNames.Count)];
                }
            }
        }


        /// <summary>
        /// Toggles the text input box between active and inactive
        /// </summary>
        private void ToggleActive()
        {
            mIsActive = !mIsActive;
        }


        /// <summary>
        /// Returns the currently pressed key as a string
        /// </summary
        /// <returns>String value of key</returns>
        private string GetKeyAsString(Keys[] keys)
        {
            if (keys.Length != 0)
            {
                foreach (Keys key in keys)
                {
                    // Check for letter key pressed
                    if (IsLetterPressed(key))
                    {
                        if (key == Keys.Space)
                        {
                            return " ";
                        }

                        if (key == Keys.Back)
                        {
                            return "back";
                        }

                        // Handle capslock and shift keys for capital letters
                        bool isCapsOn = Keyboard.GetState().CapsLock;

                        if (IsShiftPressed())
                        {
                            return isCapsOn ? GetSecondKeyPress(keys).ToLower() : GetSecondKeyPress(keys).ToString();
                        }
                        else if (!IsShiftPressed())
                        {
                            return isCapsOn ? key.ToString() : key.ToString().ToLower();
                        }
                    }
                    // Check for number key pressed
                    else if (IsNumberPressed(key))
                    {
                        return key.ToString().Substring(1); // Remove 'D' from start of number string.
                    }
                }
            }
            return string.Empty;
        }


        /// <summary>
        /// When more than one key is pressed return the key that was pressed second to account for shift presses
        /// </summary>
        /// <param name="keys"></param>
        /// <returns>Key at 2nd index of pressed keys array</returns>
        private string GetSecondKeyPress(Keys[] keys)
        {
            if (keys.Length > 2) //Check if shift and only 1 key is pressed? 
            {
                return keys[1].ToString();
            }
            return keys[0].ToString();
        }


        /// <summary>
        /// Adds a letter to current text
        /// </summary>
        /// <param name="letter">Letter string to add</param>
        private void AddText(string letter)
        {
            if (mAddCounter == 0)
            {
                mAddCounter = 3;
            }
            else if (mAddCounter == 3)
            {
                mCurrentText += letter;
                mAddCounter--;
            }
            else
            {
                mAddCounter = 0;
            }
        }

        /// <summary>
        /// Removes the last letter of current text
        /// </summary>
        private void RemoveText()
        {
            if (mCurrentText.Length != 0)
            {
                if (mRemoveCounter == 0)
                {
                    mRemoveCounter = 2;
                }
                else if (mRemoveCounter == 2)
                {
                    mCurrentText = mCurrentText.Remove(mCurrentText.Length - 1, 1);
                    mRemoveCounter--;
                }
                else
                {
                    mRemoveCounter = 0;
                }
            }
        }


        /// <summary>
        /// Checks if text is longer than character limit
        /// </summary>
        /// <returns>True if over limit</returns>
        private bool IsOverCharLimit()
        {
            return mCurrentText.Length > CHAR_LIMIT;
        }


        /// <summary>
        /// Is a letter being pressed on keyboard?
        /// </summary>
        /// <returnsTrue if letter key pressed</returns>
        private bool IsLetterPressed(Keys key)
        {
            return (int)key >= 64 && (int)key <= 91 || key == Keys.Back || key == Keys.Space;
        }


        /// <summary>
        /// Is a number being pressed on keyboard?
        /// </summary>
        /// <returns>True if number key pressed</returns>
        private bool IsNumberPressed(Keys key)
        {
            return (int)key >= 48 && (int)key <= 57 || (int)key >= 96 && (int)key <= 105;
        }


        /// <summary>
        /// Is shift or capslock pressed?
        /// </summary>
        /// <returns>True if capslock or shift keys are pressed</returns>
        private bool IsShiftPressed()
        {
            return Keyboard.GetState().IsKeyDown(Keys.LeftShift) ||
                   Keyboard.GetState().IsKeyDown(Keys.RightShift);
        }

        #endregion rUtility
    }
}
