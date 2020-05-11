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
		/// </summary>
		private static List<string> CommandWords = new List<string>()
		{
			"go", "look", "help", "quit", "examine", "fight", "use"
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


			if (!CommandWords.Contains(cmdWord))
			{
				PrintLineWarning("I don't understand...(type \"help\" to see a list of commands I know.)");
				return;
			}

			if (cmdWord.Equals("look"))
			{
				ProcessLookCommand(parts);
			}
			else if (cmdWord.Equals("go"))
			{
				ProcessGoCommand(parts);
			}
			else if (cmdWord.Equals("fight"))
			{
				ProcessFightCommand(parts);
			}
            else if (cmdWord.Equals("help"))
            {
                // TODO:  Implement this to show a new player how to use commands!
            }
            else if (cmdWord.Equals("use"))
            {
				ProcessUseCommand(parts);
            }

            // TODO: Many Achievements
            // Implement more commands like "use" and "get" and "talk"
		}

        private static void ProcessUseCommand(string[] parts)
        {
            if(parts.Length == 1)
            {
				PrintLineWarning("Use what?");
				return;
            }

			string itemName = parts[1];


            if(parts.Length == 2)
            {
                if(CurrentArea.HasItem(itemName))
                {
					Item item = CurrentArea.GetItem(itemName);
                    if(item is IUseableItem)
                    {
						try
						{
                            // "cast" as an IUseableItem
							((IUseableItem)item).Use();
						}
                        catch (ItemDepletedException ide)
                        {
							PrintLineSpecial(ide.Message);
							CurrentArea.DeleteItem(itemName);
                        }
                        catch(WorldException we)
                        {
							PrintLineDanger(we.Message);
                        }
                    }
                    else
                    {
						PrintLineWarning("I can't use {0}...", item.Name);
                    }
                }

                else if(Player.Backpack.ContainsKey(itemName))
                {
					
					if (Player.Backpack[itemName].First() is IUseableItem)
					{
						try
						{
							((IUseableItem)Player.Backpack[itemName].First()).Use();
						}
						catch (ItemDepletedException ide)
						{
							PrintLineSpecial(ide.Message);
							Player.Backpack[itemName].RemoveAt(0);
                            if(Player.Backpack[itemName].Count <= 0)
                            {
								PrintLineWarning("You used your last {0}", itemName);
								Player.Backpack.Remove(itemName);
                            }
						}
						catch (WorldException we)
						{
							PrintLineDanger(we.Message);
						}
					}
					else
					{
						PrintLineWarning("I can't use {0}...", Player.Backpack[itemName].First().Name);
					}
				}

                else if(CurrentArea.CreatureExists(itemName))
                {
					PrintLineWarning("That is a living creature!");
                }
                else
                {
					PrintLineWarning("I don't see that around....");
                }
				return;
            }
			string targetName = parts[2];

			object target;

			if (CurrentArea.HasItem(targetName))
				target = CurrentArea.GetItem(targetName);
			else if (CurrentArea.CreatureExists(targetName))
				target = CurrentArea.GetCreature(targetName);
            else
            {
				PrintLineWarning("I don't see {0} around here...", targetName);
				return;
            }


            if (CurrentArea.HasItem(itemName))
			{
				Item item = CurrentArea.GetItem(itemName);
				if (item is IUseableItem)
				{
					try
					{
						((IUseableItem)item).Use(ref target);
					}
					catch (ItemDepletedException ide)
					{
						PrintLineSpecial(ide.Message);
						CurrentArea.DeleteItem(itemName);
					}
					catch (WorldException we)
					{
						PrintLineDanger(we.Message);
					}
				}
				else
				{
					PrintLineWarning("I can't use {0}...", item.Name);
				}
			}

			else if (Player.Backpack.ContainsKey(itemName))
			{

				if (Player.Backpack[itemName].First() is IUseableItem)
				{
					try
					{
						((IUseableItem)Player.Backpack[itemName].First()).Use(ref target);
					}
					catch (ItemDepletedException ide)
					{
						PrintLineSpecial(ide.Message);
						Player.Backpack[itemName].RemoveAt(0);
						if (Player.Backpack[itemName].Count <= 0)
						{
							PrintLineWarning("You used your last {0}", itemName);
							Player.Backpack.Remove(itemName);
						}
					}
					catch (WorldException we)
					{
						PrintLineDanger(we.Message);
					}
				}
				else
				{
					PrintLineWarning("I can't use {0}...", Player.Backpack[itemName].First().Name);
				}
			}

			else if (CurrentArea.CreatureExists(itemName))
			{
				PrintLineWarning("That is a living creature!");
			}
			else
			{
				PrintLineWarning("I don't see that around....");
			}
			
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
			CombatResult result = DoCombat(ref creature);

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
                    if(parts[1].Equals("backpack"))
                    {
						PrintLine(Neutral, Player.ListInventory());
                    }
                    else
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
					Area temp = CurrentArea;

					// move to that area if the command is understood.
					CurrentArea = CurrentArea.GetNeighbor(parts[1]);

                    if(temp is IActiveArea)
                    {
						((IActiveArea)temp).OnExit();
                    }

                    if(CurrentArea is IActiveArea)
                    {
						((IActiveArea)CurrentArea).OnEnter();
                    }
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
