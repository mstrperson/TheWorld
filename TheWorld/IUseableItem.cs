using System;
namespace TheWorld
{
    public interface IUseableItem
    {
        /// <summary>
        /// use this Item on a Target.
        ///
        /// If the item is Depleted (no longer useable)
        /// throw new ItemDepletedException("used up message", this);
        /// </summary>
        /// <param name="target"></param>
        void Use(ref object target);

        /// <summary>
        /// Use this Item on Yourself.
        /// 
        /// If the item is Depleted (no longer useable)
        /// throw new ItemDepletedException("used up message", this);
        /// </summary>
        void Use();

    }

    public class Rope: Item, IUseableItem
    {
        private Area ClimbTo;

        public Rope(Area climbTo)
        {
            ClimbTo = climbTo;
        }

        public void Use()
        {
            TextFormatter.PrintLineSpecial("You climb up the rope!");
            TheGame.CurrentArea = ClimbTo;

            ClimbTo.LoadArea();
        }

        public void Use(ref object target) => Use();
    }

    /// <summary>
    /// Exception should be thrown when a UseableItem is depleted (if that is possible)
    /// This will allow you to be able to deal with the depleted item when it is done.
    /// </summary>
    public class ItemDepletedException : WorldException
    {
        public ItemDepletedException(string message, IUseableItem item) : base(message, item)
        {

        }
    }
}
