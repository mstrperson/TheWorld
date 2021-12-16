using System;
using System.Collections.Generic;
using System.Diagnostics;

// this allows me to use the static methods defined in TextFormatter without typing "TextFormatter." every time.
using static TheWorld.TextFormatter;

namespace TheWorld
{
	public static partial class TheGame
	{
		public static Area CurrentArea;
		public static Player Player;

       

        // TODO: Moderate Achievement
        //
        // Implement a property called PlayedTime which is of type TimeSpan
        // and a private DateTime StartTime.
        // PlayedTime should be Calculated as DateTime.Now - StartTime.
        // You can use this to tell the Player how long they have been playing.
        //
        // Implement a command "played_time" which displays the current played time
        // in hh:mm:ss format.


		public static void Main(string[] args)
        {

            // Initialization
			PrintPositive("What is your name?  ");
			Player = new Player(Console.ReadLine());

            // Check out that second parameter?!?! WAHT!@
            int hps = Dice.Roll(Dice.Type.D6, modifier: 4);  // roll 1d6+4

            Player.Stats = new StatChart() {
                Level = 1,
                MaxHPs = hps,
                HPs = hps,
                Atk = new Dice(Dice.Type.D6),  // 1d6
                Def = new Dice(Dice.Type.D4),  // 1d4
                Exp = 0
			};


            Player.PickUp(new Bomb(new Dice(Dice.Type.D6))
            {
                Name = "Boom Stick",
                Description = "A really boomy stick.", Value = new Money() { Silver = 24 }
            });

            Player.PickUp(new HealingPotion()
            {
                Name = "Red Potion",
                Description = "A vial of murky red liquid.  I hope that isn't blood..."
            });

            // Create the World from the WorldBuilder class.
			CurrentArea = WorldBuilder.BuildWorld();

			string command = "";

            // TODO:  Easy Achievement:
            // Script an Intro Sequence setting the stage for your game.
            // Start your storyline off with a little more than just dropping
            // the player into the world with no idea what is going on....
            // ... or maybe that's what you want, nobody told me what was going on
            // when I landed in this world.

            // Display the short information about the Starting Area
			Console.WriteLine(CurrentArea);

            #region The Entire Game Happens HERE

            while (!command.ToLowerInvariant().Equals("quit"))
			{
				// Do the Game Loop!
				PrintSpecial(">> ");
				command = Console.ReadLine();

                // This command.ToLowerInvariant() is why all names for things must be in all lowercase.
                ParseCommand(command.ToLowerInvariant());

                // TODO: You can spice up the game by having things happen randomly
                // Add things here to insert them in between the users commands.
			}

			#endregion // THE GAME

			PrintLinePositive("Bye!");
		}
	}
}
