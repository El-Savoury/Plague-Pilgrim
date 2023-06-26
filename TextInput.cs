namespace Plague_Pilgrim
{
    internal class TextInput
    {
        #region rConstants

        int CHAR_LIMIT = 200;
        string CURSOR = "_";

        #endregion rConstants





        #region rMembers

        Vector2 mPosition;
        string mCurrentText = string.Empty;
        bool mIsActive = true;
        int mAddCounter = 3;
        int mRemoveCounter = 2;

        #endregion rMembers






        #region rInitialisation

        /// <summary>
        /// Constructor
        /// </summary>
        public TextInput(Vector2 pos)
        {
            mPosition = pos;
        }

        #endregion rInitialisation





        #region rUpdate

        public void Update()
        {
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

        public void Draw(DrawInfo info)
        {
            string text = mCurrentText;

            if (mIsActive && !IsOverCharLimit())
            {
                text = mCurrentText + CURSOR;
            }

            Draw2D.DrawString(info, FontManager.GetFont("monogram"), text, mPosition, Color.White); ;
        }

        #endregion rDraw






        #region rUtility


        private void ToggleActive()
        {
            mIsActive = !mIsActive;
        }

        /// <summary>
        /// Sets whether text input is active or not
        /// </summary>
        /// <param name="active">True to activate, false to deactivate</param>
        public void SetActive(bool active)
        {
            mIsActive = active;
        }


        /// <summary>
        /// Returns a key value only if it's a letter or number to avoid returning shift or capslock
        /// </summary
        /// <returns>char or int value of key</returns>
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

                        bool isCapsOn = Keyboard.GetState().CapsLock;

                        if (IsShiftPressed())
                        {
                            return isCapsOn ? HandleShiftPress(keys).ToLower() : HandleShiftPress(keys).ToString();
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
        /// When shift is pressed returns the next pressed key as a captial letter
        /// </summary>
        /// <param name="keys"></param>
        /// <returns>Upper or lower case string</returns>
        private string HandleShiftPress(Keys[] keys)
        {
            if (keys.Length > 2 && keys.Length < 3) //Check if shift and only 1 key is pressed? 
            {
                return keys[1].ToString();
            }
            else
            {
                return keys[0].ToString();
            }
        }


        /// <summary>
        /// Adds a letter to current text
        /// </summary>
        /// <param name="letter"></param>
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
                mAddCounter--;
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
                    mRemoveCounter--;
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
