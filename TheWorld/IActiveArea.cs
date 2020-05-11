using System;
namespace TheWorld
{
    public interface IActiveArea
    {
        /// <summary>
        /// What happens when you enter this Area
        /// </summary>
        void OnEnter();

        /// <summary>
        /// What happens when you Leave this area.
        /// </summary>
        void OnExit();
    }

    public class PitTrap : Area, IActiveArea
    {

        protected class Rope: Item, IUseableItem
        {
            public Area ClimbTo;

            public void Use()
            {
                TextFormatter.PrintLineSpecial("You climb up the rope...");
                TheGame.CurrentArea = ClimbTo;
            }

            public void Use(ref object target) => Use();
        }



        public PitTrap(Area parentArea) : base()
        {
            this.AddItem(new Rope() { ClimbTo = parentArea }, "rope");
            this.AddNeighbor(parentArea, "up");
        }

        public void OnEnter()
        {
            TextFormatter.PrintLineSpecial("You slip and fall into the pit....");
            TheGame.Player.Stats.HPs -= 5;
        }

        public void OnExit()
        {
            TheGame.CurrentArea = this;
            throw new WorldException("You attempt to climb the side of the pit but slip back down into it!");
        }

    }
}
