using System;
using System.Collections.Generic;

namespace TheWorld
{
	/// <summary>
	/// The Player playing the game!
    ///
    /// TODO:  Hard Achievement
    /// Write a LevelUp method wich increases the characters stats
    /// based on formulae that you divise when a character earns a certain amount
    /// of experience by defeating enemies or completing "quests".
    ///
    /// 
	/// </summary>
	public class Player
	{
        /// <summary>
        /// This player's name.
        /// </summary>
		public string Name
		{
			get;
			protected set;
		}

        /// <summary>
        /// This player's stats
        /// </summary>
		public StatChart Stats
		{
			get;
			set;
		}

        /// <summary>
        /// How much money does the player have?
        /// </summary>
        public Money MoneyPouch
        {
            get;
            protected set;
        }

		/// <summary>
		/// The items. In Stacks.  By Name.
        ///
        /// TODO: Hard Achievement
        /// Encapsulate this Backpack into a Specialized container class.
        /// The Backpack class should include additional properties such as:
        ///
        /// int MaxCapacity  //how much weight can it hold?
        /// int CurrentWeight //Calculates the weight of all items currently in it
        /// bool Contains(string itemName)
        ///
        /// void Add(ICarryable item, string uid)
        /// void Remove(string uid)
        /// void Use(
		/// </summary>
		public Dictionary<string, List<ICarryableItem>> Backpack;


		public Player(string name)
		{
			Name = name;
			Backpack = new Dictionary<string, List<ICarryableItem>>();
		}

		/// <summary>
		/// Put the Item in your backpack.
		/// </summary>
		/// <param name="item">Item.</param>
		public void PickUp(ICarryableItem item)
		{
            // if you already have some of this item, put it with those.
			if(Backpack.ContainsKey(item.Name))
				Backpack [item.Name].Add(item);
            // otherwise start a new stack
			else
				Backpack.Add(item.Name, new List<ICarryableItem>() { item });
		}

        /// <summary>
        /// TODO:  Moderate Achievement
        /// Get a neatly formatted string that shows the names of each of
        /// the Item in your Inventory.
        /// </summary>
        /// <returns></returns>
        public string ListInventory()
        {
			throw new NotImplementedException();
        }
	}
}

