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
        void Use(object target);

        /// <summary>
        /// Use this Item on Yourself.
        /// 
        /// If the item is Depleted (no longer useable)
        /// throw new ItemDepletedException("used up message", this);
        /// </summary>
        void Use();

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
