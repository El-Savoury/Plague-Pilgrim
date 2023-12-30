namespace Plague_Pilgrim
{
    /// <summary>
    /// Dictionary of inventory items
    /// </summary>
    public class Inventory
    {
        private Dictionary<string, int> mInventory;

        public Inventory()
        {
            mInventory = new Dictionary<string, int>();
        }

        /// <summary>
        /// Add item to inventory
        /// </summary>
        public void AddItem(string name, int qty)
        {
            if (HasItem(name)) { mInventory[name] += qty; }
            else { mInventory.Add(name, qty); }
        }


        /// <summary>
        /// Remove item from inventory
        /// </summary>
        public void RemoveItem(string itemName, int qty)
        {
            if (HasItem(itemName))
            {
                mInventory[itemName] -= qty;

                if (mInventory[itemName] < 0) { mInventory[itemName] = 0; }
            }
        }


        /// <summary>
        /// Does this inventory contain the specfied item?
        /// </summary>
        /// <param name="itemName">Item name</param>
        /// <returns></returns>
        public bool HasItem(string itemName)
        {
            return mInventory.ContainsKey(itemName);
        }


        /// <summary>
        /// Get the contents of the inventory
        /// </summary>
        /// <returns>Dictionary of items</returns>
        public Dictionary<string, int> Contents()
        {
            return mInventory;
        }
    }


    /// <summary>
    /// Manages global and party member inventories.
    /// </summary>
    public static class InventoryManager
    {
        #region rMembers

        public static Inventory mGlobalInventory = new Inventory();

        #endregion rMembers




        #region rInitialisation

        /// <summary>
        /// Load default global inventory values.
        /// </summary>
        public static void LoadGlobalInventory()
        {
            mGlobalInventory.AddItem("Food", 0);
            mGlobalInventory.AddItem("Herbs", 0);
            mGlobalInventory.AddItem("Vinegar", 0);
            mGlobalInventory.AddItem("Gold", 0);
        }

        #endregion rInitialisation







        #region rDraw

        public static void Draw(DrawInfo info, int x, int y)
        {
            Vector2 pos = new Vector2(x, y);

            foreach (KeyValuePair<string, int> item in mGlobalInventory.Contents())
            {
                Draw2D.DrawString(info, FontManager.GetFont("monogram"), $"{item.Value} {item.Key}", pos, Color.White);
                pos.Y += 30;
            }
        }

        #endregion rDraw







        #region rUtility

        /// <summary>
        /// Add an item to specified inventory
        /// </summary>
        /// <param name="name">Item name</param>
        /// <param name="qty">Quantity to add</param>
        public static void AddItem(Inventory inventory, string name, int qty)
        {
            inventory.AddItem(name, qty);
        }


        /// <summary>
        /// Remove item from inventory
        /// </summary>
        /// <param name="name">Item name</param>
        /// <param name="qty">Quantity to remove</param>
        public static void RemoveItem(Inventory inventory, string name, int qty)
        {
            inventory.RemoveItem(name, qty);
        }

        #endregion rUtility
    }
}