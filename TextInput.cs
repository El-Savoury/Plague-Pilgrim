﻿namespace Plague_Pilgrim
{
    internal class TextInput
    {
        #region rConstants

        int CHAR_LIMIT = 15;
        string CURSOR = "_";

        #endregion rConstants





        #region rMembers

        Vector2 mPosition;
        Color mColour;
        string mCurrentText = string.Empty;
        bool mIsActive = true;
        int mAddCounter = 2;
        int mRemoveCounter = 2;
        List<String> mNames = new List<string>();

        
        #endregion rMembers






        #region rInitialisation

        /// <summary>
        /// Constructor
        /// </summary>
        public TextInput(Vector2 pos)
        {
            mPosition = pos;

            // Add male names
            using (StreamReader reader = new StreamReader("Content/Names/males_names.txt"))
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

        public void Draw(DrawInfo info)
        {
            string text = mCurrentText;

            if (mIsActive && !IsOverCharLimit())
            {
                text = mCurrentText + CURSOR;
            }

            Draw2D.DrawString(info, FontManager.GetFont("monogram"), text, mPosition, mColour); ;
        }

        #endregion rDraw






        #region rUtility

        private void RandomiseName()
        {
            if (InputManager.KeyReleased(Controls.Randomise))
            {
                Random rand = new Random();
                mCurrentText = mNames[rand.Next(0, mNames.Count)];
            }
        }

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

                        if (IsUpperCase())
                        {
                            return HandleShiftPress(keys);
                        }
                        else if (!IsUpperCase())
                        {
                            return key.ToString().ToLower();
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
                mAddCounter = 2;
            }
            else if (mAddCounter == 2)
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
        private bool IsUpperCase()
        {
            return Keyboard.GetState().CapsLock ||
                   Keyboard.GetState().IsKeyDown(Keys.LeftShift) ||
                   Keyboard.GetState().IsKeyDown(Keys.RightShift);
        }

        #endregion rUtility
    }
}
