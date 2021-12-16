using System;
using System.Collections.Generic;

namespace TheWorld
{
	/// <summary>
	/// World builder is responsible for all World creation.  
	/// It is a static class because it is only used once at the beginning of the program to construct the world.
    ///
    /// TODO:  Create your own world!
    ///
    /// TODO:  Easy Achievement (1):
    /// Create 4 Areas which are linked together somehow.
    ///
    /// TODO:  Easy Achievement (2):
    /// Populate each of your Areas with at least 2 Objects that the player can interact with.
    ///
    /// TODO:  Easy Achievement (3):
    /// Create an Area which is accessible by a one-way entrance.
    /// (i.e. You fall through a hole and must find another way out)
    ///
    /// TODO:  Moderate Achievement (4):
    /// Draw a Map of your World on Paper or in a graphics program.
    /// Doesn't have to be crazy art work, just a visual representation of how
    /// the world is connected together.  Can be completely conceptual. 
    /// This is sometimes called "Story boarding"
    /// 
	/// </summary>
	public static class WorldBuilder
	{
		/// <summary>
		/// Builds the world. This is the method where you design your world.  Create Areas, Populate those Areas, and then link those areas together.
		/// If an area is particularly complicated, you may consider writing a helper method to break that part out.
		/// 
		/// This method returns the starting Area which is linked to the rest of the World.
		/// </summary>
		/// <returns>The starting area linked to the rest of the world.</returns>
		public static Area BuildWorld()
		{
			// This is going to be the area where the player starts.
			Area start = new Area() { Name = "The Field", Description = "A wide grassy field with not much to see." };

			// I can create a new Item and add it directly into the Area without having a separate variable for it!  Convenient!
			start.AddItem(new Boulder()
                {
				    Name = "Boulder",
				    Description = "It's a big granite boulder.  It has a weird glyph carved into it, but you can't make any sense of it."
			    },
                "boulder"
            );

			// Doing it again--no separate variable for the new item.  It goes directly into the created area.
			start.AddItem(new Item()
                {
                    Name = "Grass",
                    Description = "Grass... Lots of Grass... Like... Everywhere."
                },
				"grass"
            );

			// I can do that with any kind of object that I can create entirely in one command.
			// Don't forget that last word is the Unique Identifier.  So I can't have more than one thing in my area named "bunny"
			start.AddCreature(new Creature()
                {
				    Name = "Bunny Rabbit",
				    Description = "A cute bunny.  Looks pretty tasty actually...",
				    Stats = new StatChart() { Level = 1, MaxHPs = 10, HPs = 10, Atk = new Dice(Dice.Type.D4), Def = new Dice(Dice.Type.D4), Exp = 3 }
			    },
                "bunny"
            );

			// Here's a second area.
			Area stream = new Area()
            {
				Name = "Stream",
				Description = "A burbling stream.  There are some rocks that look like you could cross them to get to the other side."
			};

			// I will populate it with items and creatures in the same way...
			stream.AddItem(new Item()
                {
				    Name = "Lizard",
				    Description = "A funny lizard with a black stripe down its back.  It doesn't look intimidated by your presence," +
				    " but it doesn't look very interested either. Upon closer inspection, it might not be alive..."
			    },
                "lizard"
            );

			stream.AddCreature(new Creature()
                {
				    Name = "Frog",
				    Description = "A crazy big frog!  It looks like it could eat a bird if it caught one.  It also doesn't look happy.",
				    Stats = new StatChart() { MaxHPs = 10, HPs = 10, Atk = new Dice(Dice.Type.D6), Def = new Dice(Dice.Type.D4), Level = 1, Exp = 5 }
			    },
                "frog"
            );

			// These two lines LINK the two areas together.  Don't forget to go both ways or you'll end up with a dead end
			// and no way out!!!
			start.AddNeighbor(stream, "north");
			stream.AddNeighbor(start, "south");
			stream.AddNeighbor(SpecialZone(ref start, "cliff"), "climb_up");

			// Go back to the Main method and tell it where to start the game!
			return start;
		}


        /// <summary>
        /// My contribution to the World for an Easy Achievement!
        /// </summary>
        /// <returns></returns>
        public static Area SpecialZone(ref Area connectionPoint, string neighborName)
        {
			Area myZoneStart = new Area()
			{
				Name = "Danger Zone",
				Description = "A zone of ... danger?"
			};

            // Added a Giant Bat enemy to this area.
			myZoneStart.AddCreature(new Creature()
			    {
				    Name = "Bat-zilla",
				    Description = "Pretty sure I'm going to die....",
				    Stats = new StatChart()
				    {
					    Atk = new Dice("2d12+6"),
					    Def = new Dice("2d8+3"),
					    Exp = 150000,
					    Level = 35,
					    HPs = 400,
					    MaxHPs = 400
				    }
			    },
                "bat"
            );

            // It's a treasure chest with a potion inside.
            // USE this chest to get the potion out.
			myZoneStart.AddItem(new TreasureChest()
			    {
				    Name = "Heavy Trunk",
				    Description = "A very hefty looking wooden chest.  The lock appears to be open.",
				    Contents = new Dictionary<string, ICarryableItem>()
				    {
					    { "potion", HealingPotion.StandardHealingPotion }
				    }
			    },
                "heavy_trunk"
            );

            // Using this SurpriseBox is ... very surprising............
            // Requires the USE command be implemented!
			myZoneStart.AddItem(new SurpriseBox()
			    {
				    Name = "Ornate Jewelry Box",
				    Description = "An Ornate Silver-inlayed Walnut Jewelry box.  Should I open ('use') it?",
				    Value = new Money("250g")
			    },
                "jewelry_box"
            );

			connectionPoint.AddNeighbor(myZoneStart, neighborName);

			return myZoneStart;
        }

	}
}

