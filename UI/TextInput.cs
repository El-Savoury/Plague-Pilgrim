namespace Plague_Pilgrim
{
    /// <summary>
    /// Text box that lets player type to input text
    /// </summary>
    class TextInput : UIObject
    {
        #region rConstants

        string CURSOR = "_";
        int CHAR_LIMIT = 12;

        #endregion rConstants





        #region rMembers

        Keys[] mLastPressedKeys = new Keys[2];
        List<string> mNames = new List<string>();
        int mRemoveCounter = 2;

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
        public override void Update()
        {
            if (InputManager.KeyPressed(Controls.Up)) { ToggleActive(); }

            if (mActive)
            {
                HandlePressedKeys();
                RandomiseName();
            }

            base.Update();
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

            if (mActive && !IsOverCharLimit())
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
            mActive = !mActive;
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
        /// Is either shift key pressed?
        /// </summary>
        /// <returns>True if capslock or shift keys are pressed</returns>
        private bool IsShiftPressed()
        {
            return mLastPressedKeys.Contains(Keys.LeftShift) ||
                   mLastPressedKeys.Contains(Keys.RightShift);
        }


        private void HandlePressedKeys()
        {
            Keys[] pressedKeys = Keyboard.GetState().GetPressedKeys();

            // Check for new key press
            foreach (Keys key in pressedKeys)
            {
                if (key == Keys.Back)
                {
                    RemoveText();
                }
                else if (!mLastPressedKeys.Contains(key) && !IsOverCharLimit())
                {
                    if (IsLetterPressed(key))
                    {
                        AddLetter(key);
                    }
                    else if (IsNumberPressed(key))
                    {
                        AddNumber(key);
                    }
                }
            }

            mLastPressedKeys = pressedKeys;
        }


        /// <summary>
        /// Converts Key letter value to string and adds it to current text
        /// </summary>
        /// <param name="key">Keyboard key pressed by player</param>
        private void AddLetter(Keys key)
        {
            string letter = key.ToString();

            // Handle space bar press
            if (key == Keys.Space)
            {
                letter = " ";
            }

            // Handle capslock and shift keys for capital letters
            bool isCapsOn = Keyboard.GetState().CapsLock;

            if (IsShiftPressed())
            {
                letter = isCapsOn ? letter.ToLower() : letter;
            }
            else if (!IsShiftPressed())
            {
                letter = isCapsOn ? letter : letter.ToLower();
            }

            mCurrentText += letter;
        }


        /// <summary>
        ///  Converts Key number value to string and adds it to current text
        /// </summary>
        /// <param name="key"></param>
        private void AddNumber(Keys key)
        {
            mCurrentText += key.ToString().Substring(1); // Remove 'd' from number string 
        }

        #endregion rUtility
    }
}
