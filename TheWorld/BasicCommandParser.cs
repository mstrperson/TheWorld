using System;
using System.Collections.Generic;
using System.Linq;

namespace TheWorld
{
    // this allows me to use the static methods defined in TextFormatter without typing "TextFormatter." every time.
	using static TheWorld.TextFormatter;


	/// <summary>
	/// You might notice that this class has the same name as the one in
	/// Program.cs as well as in Combat.cs
	///
	/// This is allowed because the class has the "partial" attribute.  This
	/// means that the class has parts spread across multiple files because
	/// it is large and breaking it up into chunks makes it easier to follow.
	///
	/// This file contains the methods and properties that are only relevant to
	/// processing game commands.
	///
	/// The fact is, the entire game is really made up of just a bunch of calls
	/// to the ParseCommand method.  At least until you spice things up a bit!
	/// </summary>
	public static partial class TheGame
	{
		/// <summary>
		/// The command words.
		/// These are all the words that the game will accept as commands.
		/// You will need to add more words to make the game more interesting!
		/// Pair these with the Methods to execute when you type this command word!
		/// </summary>
		private static Dictionary<string, Action<string[]>> _commands = new Dictionary<string, Action<string[]>>()
		{
			{ "go", ProcessGoCommand },
			{ "look", ProcessLookCommand },
			{ "help", ProcessHelpCommand },
			{ "quit", delegate(string[] strings) {  }},
			{ "examine", delegate(string[] strings) {  } },
			{ "fight", ProcessFightCommand }
		};
	    
		
		
        /// <summary>
        /// TODO:  Easy Achievement
        /// Improve the readability of other code by completing this method.
        ///
        /// This should return True if and only if the CommandWords list contains
        /// the give cmdWord.
        ///
        /// Implement this method in appropriate places such as the ParseCommand method.
        /// 
        /// </summary>
        /// <param name="cmdWord"></param>
        /// <returns></returns>
		private static bool IsValidCommandWord(string cmdWord) => throw new NotImplementedException();

		/// <summary>
		/// Parses the command and do any required actions.
		/// </summary>
		/// <param name="command">Command as typed by the user.</param>
		private static void ParseCommand(string command)
		{
            // Break apart the command into individual words:
            // This is why command words and unique names for objects cannot contain spaces.
			string[] parts = command.Split(' ');
            // The first word is the command.
			string cmdWord = parts.First();


			if (!_commands.ContainsKey(cmdWord))
			{
				if (parts.Length > 1)
				{
					string target = parts[1];
					if (CurrentArea.HasItem(target))
					{
						Item item = CurrentArea.GetItem(target);
						if (item is IInteractable && ((IInteractable)item).Interactions.ContainsKey(cmdWord))
						{
							((IInteractable) item).Interactions[cmdWord](parts);
							return;
						}
					}
					else if (CurrentArea.CreatureExists(target))
					{
						Creature creature = CurrentArea.GetCreature(target);
						if (creature is IInteractable && ((IInteractable)creature).Interactions.ContainsKey(cmdWord))
						{
							((IInteractable) creature).Interactions[cmdWord](parts);
							return;
						}
					}
				}
				PrintLineWarning("I don't understand...(type \"help\" to see a list of commands I know.)");
				return;
			}

			// execute the command associated with the given command word!
			_commands[cmdWord](parts);

			// TODO: Many Achievements
			// Implement more commands like "use" and "get" and "talk"
		}


        /// <summary>
        /// TODO:  Write this Method
        /// Several Achievements inside.
        /// </summary>
        /// <param name="parts"></param>
        private static void ProcessHelpCommand(string[] parts)
        {
            if(parts.Length == 1)
            {
                // TODO:  Easy Achievement (1):
                // the whole command is just "help".  Print a generic help message that
                // tells the player what valid command words are and how to formulate them
                //
                // TODO:  Easy Achievement (2):
                // Print a helpful example that shows the Player an example command that
                // will work in the current Area.  (e.g. "look [something]" where that
                // something is a valid thing to look at in the CurrentArea.
            }
            if(parts.Length == 2)
            {
                // TODO: Moderate Achievement (3):
                // In this case, the user is looking for help with a specific command, so
                // you should verify that the second word in the string is a valid command word
                // then for each possible valid command word, print a useful help message that
                // explains what the command does and an example of how to use it.
                // If the second word is not a valid command, make sure your message is clearly
                // an Error message (Use the PrintWarning() method to make it obvious).
            }
        }

		/// <summary>
		/// Enter Combat mode.
		/// </summary>
		/// <param name="parts">Command as typed by the user split into individual words.</param>
		private static void ProcessFightCommand(string[] parts)
		{
			Creature creature;
			try
			{
				creature = CurrentArea.GetCreature(parts[1]);
			}
			catch (WorldException e)
			{
				if (CurrentArea.HasItem(parts[1]))
					PrintLineWarning("You can't fight with that...");
				else
					PrintLineDanger(e.Message);
				return;
			}

			// This method is part of the MainClass but is defined in a different file.
			// Check out the Combat.cs file.
			CombatResult result = DoCombat(creature);

			switch (result)
			{
				case CombatResult.Win:
					PrintLinePositive("You win!");
					Player.Stats.Exp += creature.Stats.Exp;
					CurrentArea.RemoveCreature(parts[1]);
                    // TODO: Part of a larger achievement
                    // After you gain Exp, how do you improve your stats?
                    // there should be some rules to how this works.
                    // But, you are the god of this universe.  You make the rules.

                    // TODO: Part of a larger achievement
                    // After defeating an Enemy, they should drop their Inventory
                    // into the CurrentArea so that the player can then PickUp those Items.
					break;
				case CombatResult.Lose:
					PrintLineDanger("You lose!");
                    // TODO:  Easy Achievement:
                    // What happens when you die?  Deep questions.
					break;
				case CombatResult.RunAway:
					// TODO: Moderate Achievement
					// Handle running away.  What happens if you run from a battle?
					break;
				default: break;
			}
		}

		/// <summary>
		/// What happens when the user types "look" as the command word.
		/// </summary>
		/// <param name="parts">Command Parts.</param>
		private static void ProcessLookCommand(string[] parts)
		{
			// If you just type "look" then LookAround()
			if (parts.Length == 1)
			{
				Console.WriteLine(CurrentArea.LookAround());
			}
			else
			{
				// try to find the thing that the user is looking at.
				try
				{
					// if it is there, print the appropriate description.
					Console.WriteLine(CurrentArea.LookAt(parts[1]));
				}
				catch (WorldException e)
				{
					// otherwise, print an appropriate error message.
					PrintLineDanger(e.Message);
				}
			}
		}

		/// <summary>
		/// Processes the go command.
		/// </summary>
		/// <param name="parts">Parts.</param>
		private static void ProcessGoCommand(string[] parts)
		{
			// If the user has not indicated where to go...
			if (parts.Length == 1)
				PrintLineWarning("Go where?");
			else
			{
				// try to find the neighbor the user has indicated.
				try
				{
					// move to that area if the command is understood.
					CurrentArea = CurrentArea.GetNeighbor(parts[1]);
				}
				catch (WorldException e)
				{
					// if GetNeighbor throws and exception, print the explanation.
					PrintLineDanger(e.Message);
				}
			}
		}
	}
}
