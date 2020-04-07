using System;
using System.Collections.Generic;
using System.Linq;

namespace TheWorld
{
    /// <summary>
    /// This is a treasure chest that contains items.
    /// If you USE the chest, it will depost all the items
    /// into the Players Inventory!
    /// </summary>
    public class TreasureChest : Item, IUseableItem
    {
        public Dictionary<string, ICarryableItem> Contents;

        /// <summary>
        /// Can't use it this way~
        /// </summary>
        /// <param name="target"></param>
        public void Use(ref object target)
        {
            throw new WorldException("You can't use a Treasure Chest this way... ", target);
        }

        /// <summary>
        /// Put all the items in this chest into the players inventory!
        /// </summary>
        public void Use()
        {
            foreach (string key in Contents.Keys)
            {
                TheGame.Player.PickUp(Contents[key]);
            }

            // because the PickUp method made a COPY of each item.
            Contents.Clear();
        }
    }
}
