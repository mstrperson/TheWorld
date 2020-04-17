using System;
using System.Collections.Generic;
namespace TheWorld
{
    public partial class Creature
    {
        /// <summary>
        /// A Level 1 Wolf
        /// </summary>
        public static Creature L1Wolf => new Creature()
        {
            Name = "Wolf",
            Description = "Bigger than a dog... Angry teeth... yep, that's a wolf!",
            Stats = new StatChart()
            {
                Atk = new Dice("1d4"),
                Def = new Dice("1d4"),
                Exp = 12,
                HPs = 4,
                Level = 1,
                MaxHPs = 4
            }
        };
        /// <summary>
        /// A Level 2 Wolf
        /// </summary>
        public static Creature L2Wolf => new Creature()
        {
            Name = "Wolf",
            Description = "Bigger than a dog... Angry teeth... yep, that's a wolf!",
            Stats = new StatChart()
            {
                Atk = new Dice("1d4+1"),
                Def = new Dice("1d6"),
                Exp = 24,
                HPs = 8,
                Level = 2,
                MaxHPs = 8
            }
        };

        /// <summary>
        /// A level 4 Spectre, super scary!
        /// </summary>
        public static Creature L4Ghost => new Creature()
        {
            Name = "Spectre",
            Description = "What was that chill?  why do I hear whispering?",
            Stats = new StatChart()
            {
                Atk = new Dice("2d4"),
                Def = new Dice("1d12"),
                Exp = 64,
                HPs = 20,
                Level = 4,
                MaxHPs = 20
            }
        };

        /// <summary>
        /// A group of 4 wolves, two level 2, two level 1
        /// </summary>
        public static List<Creature> WolfPack => new List<Creature>() { L2Wolf, L2Wolf, L1Wolf, L1Wolf };
    }
}
