using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheWorld
{
    using static TextFormatter;

    public static partial class TheGame
    {
        /// <summary>
        /// Enumeration for possible results of doing Combat.
        /// </summary>
        public enum CombatResult
        {
            Lose,
            Win,
            RunAway = -1
        };

        /// <summary>
        /// Commands usable in Combat.
        /// </summary>
        private static List<string> _combatCommands = new List<string>() { "attack", "defend", "use", "run" };

        /// <summary>
        /// Compute a display message for the creature's health.
        /// Does this method belong here?
        /// </summary>
        /// <param name="creature">the Creature you're talking about.</param>
        private static string HpMessage(ICreature creature)
        {
            float percentage =(float)creature.Stats.HPs /(float)creature.Stats.MaxHPs;

            if(percentage >= 1f) return "uninjured";
            else if(percentage > 0.8f) return "barely injured";
            else if(percentage > 0.6f) return "injured";
            else if(percentage > 0.4f) return "wounded";
            else return "badly wounded";
        }

        /// <summary>
        /// For those times when you really need to ambush the player...
        /// </summary>
        /// <param name="creatureName"></param>
        public static void SurpriseFight(string creatureName)
        {
            ProcessFightCommand(new string[] { "attack", creatureName });
        }

        /// <summary>
        /// Enter combat with a particular creature.
        /// </summary>
        /// <param name="creature">the Creature you're fighting. passed by reference so that it can be modified.</param>
        public static CombatResult DoCombat(ICreature creature)
        {
            // keep running until one of the exit conditions is met.
            // Run Away, Win or Lose.
            while(true)
            {
                bool playerDefending = false;

                // Roll a d20 to determine the creature's action.
                int creatureAction = Dice.Roll20();
                // if the roll is at most 4, defend.
                bool creatureDefending = creatureAction <= 4;
                
                Console.WriteLine("[{0} ({1} / {2})]", Player.Name, Player.Stats.HPs, Player.Stats.MaxHPs);
                Console.Write("[{0} ({1})] << ", creature.Name, HpMessage(creature));
                string command = Console.ReadLine();
                string[] parts = command.Split(' ');

                if(!_combatCommands.Contains(parts[0]))
                {
                    PrintLineWarning("That's not a valid combat command.");
                    continue;
                }

                // parse the combat command!
                // there are many things that can be improved about this.
                switch(parts[0])
                {
                    case "attack":
                        int dmg = Player.Stats.CalculateAttack(creature.Stats);
                        if(creatureDefending)
                        {
                            PrintLineWarning("{0} is prepared for your attack...", creature.Name);
                            dmg /= 2;
                        }
                        creature.Stats.HPs -= dmg;
                        PrintLinePositive("You attack and deal {0} dammage!", dmg);
                        break;
                    case "defend":
                        playerDefending = true;
                        PrintLinePositive("You brace for the oncomming attack.");
                        break;
                    case "use":
                        // TODO: Moderate Achievement (1)
                        // Write this!
                        // Requires that you have a UseableItem
                        // Search through your inventory for an IUseableItem with
                        // the provided keyword and then execute it's .Use method.
                        //
                        // TODO: Moderate Achievement (2)
                        // Implement an IUseableItem that takes the creature as
                        // the Target.   
                        break;
                    case "run":
                        if(Dice.Roll(Dice.Type.Coin) == 1)
                            return CombatResult.RunAway;

                        PrintLineDanger("You tried to run away but you failed!");
                        break;
                    // There is no "default" case here.  Why?
                }

                if(creatureAction <= 4)
                {
                    PrintLineWarning("The creature was defending and takes no action.");
                }
                else if(creatureAction <= 19)
                {
                    int dmg = creature.Stats.CalculateAttack(Player.Stats);
                    if(playerDefending)
                        dmg /= 2;

                    Player.Stats.HPs -= dmg;

                    PrintLineDanger("{0} attacks you and deals {1} dammage!", creature.Name, dmg);
                }
                else // rolled a 20
                {
                    int dmg = 2 * creature.Stats.CalculateAttack(Player.Stats);
                    if(playerDefending)
                        dmg /= 2;

                    Player.Stats.HPs -= dmg;
                    PrintLineDanger("{0} lands a devastating blow and deals {1} dammage!", creature.Name, dmg);
                }

                if(Player.Stats.HPs <= 0) return CombatResult.Lose;
                else if(creature.Stats.HPs <= 0) return CombatResult.Win;
            }
        }
    }
}
