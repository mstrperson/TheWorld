using System;
using System.Collections.Generic;

namespace TheWorld
{
	/// <summary>
	/// A generic item in the world
	/// </summary>
    /// 
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

    /// <summary>
    /// A Prototype Healing Item that might come in Handy.
    ///
    /// Notice that this Extends the Item class, AND implements both
    /// ICarryable and IUseableItem
    ///
    /// TODO:  Moderate Achievement
    ///
    /// Implement this Item in TheWorld
    /// Requires:  Get command, Use command
    ///
    /// Place this item somewhere in the world, let the Player find it, and use it.
    /// 
    /// </summary>
    public class HealingPotion : Item, ICarryableItem, IUseableItem
    {
        public static HealingPotion StandardHealingPotion => new HealingPotion()
        {
            Name = "Small Healing Potion",
            Description = "Potion heals you by a small ammount",
            HealValue = 10,
            UseCount = 3,
            Weight = 1,
            Value = new Money("10c")
        };

        /// <summary>
        /// How much does this thing weigh?
        /// What does that even mean?
        /// </summary>
        public int Weight { get; set; }

        public int UseCount { get; set; }

        /// <summary>
        /// How much does this potion Heal you by?
        /// </summary>
        public int HealValue { get; set; }

        /// <summary>
        /// Use this potion to heal yourself.
        /// </summary>
        public void Use()
        {
            TheGame.Player.Stats.HPs += HealValue;

            if(--UseCount <= 0) // short hand, subtract one from UseCount and then compare.
                throw new ItemDepletedException(string.Format("Your {0} has run out.", this.Name), this);
        }


        /// <summary>
        /// Use this potion to heal a Creature.
        /// </summary>
        /// <param name="target">target must be of type Creature.</param>
        public void Use(ref object target)
        {
            if(target is Creature)
            {
                Creature creature = (Creature)target;
                creature.Stats.HPs += HealValue;
                if (--UseCount <= 0)
                    throw new ItemDepletedException(string.Format("Your {0} has run out.", this.Name), this);
            }
            else
            {
                throw new WorldException(string.Format("You can't use this item on {0}...", this.Name), target);
            }
        }
    }
}

