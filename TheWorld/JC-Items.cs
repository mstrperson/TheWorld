using System;
namespace TheWorld
{
    using static TextFormatter;

    public class Bomb : Item, ICarryableItem, IUseableItem
    {
        /// <summary>
        /// All bombs weigh 2 units.
        /// Trying to set this value does nothing.
        /// </summary>
        public int Weight { get => 2; set { } }

        /// <summary>
        /// Dice for how much dammage the Bomb does.
        /// </summary>
        public Dice Dammage { get; set; }


        /// <summary>
        /// Initialize a Bomb with the given Dammage dice.
        /// </summary>
        /// <param name="dammageDice"></param>
        public Bomb(Dice dammageDice)
        {
            Dammage = dammageDice;
        }

        public void Use(ref object target)
        {
            if(target is Creature)
            {
                int dmg = Dammage.Roll();
                PrintLinePositive("Your {0} explodes and does {1} dammage to the {2}", this.Name, dmg, ((Creature)target).Name);
                ((Creature)target).Stats.HPs -= dmg;
            }
            else
            {
                throw new WorldException("You can't use a Bomb on that target.", target);
            }

            throw new ItemDepletedException("Bomb is destroyed", this);
        }

        public void Use()
        {
            int dmg = Dammage.Roll();

            PrintLineDanger("The {0} explodes in your hand and deals you {1} dammage.", this.Name, dmg);

            TheGame.Player.Stats.HPs -= dmg;
            throw new ItemDepletedException("God that was stupid...", this);
        }
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

        public HealingPotion(int heal = 10, int use = 5)
        {
            HealValue = heal;
            UseCount = use;
        }

        /// <summary>
        /// Use this potion to heal yourself.
        /// </summary>
        public void Use()
        {
            TheGame.Player.Stats.HPs += HealValue;

            PrintLinePositive("You use the {0} and heal yourself for {1} hit points", this.Name, HealValue);

            if (--UseCount <= 0) // short hand, subtract one from UseCount and then compare.
                throw new ItemDepletedException(string.Format("Your {0} has run out.", this.Name), this);
        }


        /// <summary>
        /// Use this potion to heal a Creature.
        /// </summary>
        /// <param name="target">target must be of type Creature.</param>
        public void Use(ref object target)
        {
            if (target is Creature)
            {
                // This is an in-line cast.
                // target is an "object" but I need to access it's Creature attributes
                ((Creature)target).Stats.HPs += HealValue;

                PrintLineWarning("You use the {0} and heal {2} for {1} hit points", this.Name, HealValue, ((Creature)target).Name);

                if (--UseCount <= 0)
                    throw new ItemDepletedException(string.Format("Your {0} has run out.", this.Name), this);
            }
            else
            {
                throw new WorldException(string.Format("You can't use this item on that..."), target);
            }
        }
    }
}
