﻿using System;
using System.Collections.Generic;

namespace TheWorld
{
	/// <summary>
	/// A generic item in the world
	/// </summary>
	public class Item
	{
        /// <summary>
        /// Name of the Item
        /// </summary>
		public string Name
		{
			get;
			set;
		}

        /// <summary>
        /// A description of the item.
        /// </summary>
		public string Description
		{
			get;
			set;
		}

        /// <summary>
        /// How much is the Item worth?
        /// </summary>
		public Money Value
		{
			get;
			set;
		}
	}

    // TODO: Moderate Achievement
    // Build a "Book" class which is an Item that is both Carryable and Useable.
    // The Use method should print a short bit of text which is the "Story" or 
    // maybe some Plot element in your game.



    /// <summary>
    /// An Item that you can put in the world and then use it!
    ///
    /// TODO: Moderate Achievement
    /// Requires:  Implement the Use Command.
    /// Add this Item to the world somewhere and then USE it!
    /// </summary>
    public class SurpriseBox : Item, IUseableItem
    {
        private bool UsedOnce;

        public SurpriseBox()
        {
            UsedOnce = false;
        }

        public void Use()
        {
            if (!UsedOnce)
            {
                TheGame.CurrentArea.AddCreature(new Creature()
                {
                    Name = "Giant Dragonfly",
                    Description = "Holy shit how did that thing fit in that box! It's like 10 feet long!",
                    Stats = new StatChart()
                    {
                        Atk = new Dice(Dice.Type.D10, 2, 5),
                        Def = new Dice(Dice.Type.D8, modifier: 2),
                        HPs = 36,
                        Exp = 1000,
                        Level = 10,
                        MaxHPs = 36
                    }
                },
                    "giant_dragonfly"
                );

                UsedOnce = true;

                TextFormatter.PrintLineSpecial("A Giant Dragonfly crawls out of the box.{0}You notice in the back of your mind that the box seems way too small to have contained this thing.... (press Enter to coninue)");
                Console.ReadLine();

                TheGame.SurpriseFight("giant_dragonfly");
            }
        }

        /// <summary>
        /// It's a surprise!  It doesn't matter if you use it /on/ something or not.
        /// </summary>
        /// <param name="target"></param>
        public void Use(ref object target) => Use();
    }


}

