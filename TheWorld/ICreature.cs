namespace TheWorld
{
    public interface ICreature
    {
        /// <summary>
        /// Creature's name
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Creature's description.
        /// </summary>
        public string Description
        {
            get;
            set;
        }

        /// <summary>
        /// Stats of the creature.
        /// </summary>
        public StatChart Stats
        {
            get;
            set;
        }
    }
}