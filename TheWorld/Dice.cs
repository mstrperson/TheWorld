using System;

namespace TheWorld
{
    /// <summary>
    /// Represents a Dice for a DnD style game!
    /// </summary>
	public class Dice
	{

        /// <summary>
        /// The random number generator for the dice!
        /// </summary>
        private static Random _godPlaysDice = new Random();

        /// <summary>
        /// Type of Die to throw.
        /// Enums can be used to limit the number of possible values for a variable.
        /// In this case, the enum will be treated like an Integer, but only the defined values
        /// are allowed.
        /// </summary>
        public enum Type
        {
            Coin    = 2,
            D4      = 4,
            D6      = 6,
            D8      = 8,
            D10     = 10,
            D12     = 12,
            D20     = 20,
            D100    = 100
        }

        #region Static Dice Methods

        /// <summary>
        /// Roll a number of the specified type of dice.
        /// </summary>
        /// <param name="type">Type.</param>
        /// <param name="count">numbre of dice to throw.</param>
        /// <returns>The Sum of all dice thrown</returns>
        public static int Roll(Dice.Type type, int count = 1, int modifier = 0)
		{
			int total = 0;
			for(int i = 0; i < count; i++)
			{
				total += _godPlaysDice.Next((int)type) + 1;  // +1 because Dice don't have a zero.
			}
			return total + modifier;
		}

        /// <summary>
        /// Shortcut for Roll(D20, 1).
        /// </summary>
        /// <returns></returns>
        public static int Roll20() => Roll(Type.D20);

        /// <summary>
        /// TODO:  Parallel Achievement
        /// Same as the constructor for Dice of the same type.
        /// Apply the same logic here for an additional 2x Easy Achievements
        /// OR
        /// Do the achievements here first for full credit and duplicate later.
        ///
        /// It all depends on how/if you want to use it~
        /// 
        /// </summary>
        /// <param name="dnDFormat"></param>
        /// <returns></returns>
        public static int Roll(string dnDFormat)
        {
            throw new NotImplementedException();
        }

        #endregion

        private Type _DiceType;
        /// <summary>
        /// Type of Dice(e.g. Coin, D6, D20 etc)
        /// </summary>
        public Type DiceType
        {
            get => _DiceType;

            protected set
            {
                if(!Enum.IsDefined(typeof(Dice.Type), value))
                {
                    throw new ArgumentException(string.Format("{0} is not a valid dice type...", value));
                }

                _DiceType = value;
            }
        }

        /// <summary>
        /// Number of Dice rolled.
        ///(e.g. 3d10)
        /// </summary>
        public int Count { get; protected set; }

        /// <summary>
        /// Modifier added to the Dice roll
        ///(e.g. 4d6 +5)
        /// </summary>
        public int Modifier { get; protected set; }
        
        /// <summary>
        /// Initialize a Dice type for you to roll
        /// for example 3d6 would be:
        /// new Dice(3, Type.D6)
        /// 
        /// and a 4d4+5 would be:
        /// new Dice(4, Type.D4, 5);
        /// 
        /// the modifier can also be negative:
        /// 3d10-4 =>  new Dice(3, Type.D10, -4)
        /// </summary>
        /// <param name="type"></param>
        /// <param name="count"></param>
        /// <param name="modifier"></param>
        public Dice(Type type, int count = 1, int modifier = 0)
        {
            DiceType = type;
            Count = count;
            Modifier = modifier;
        }

        /// <summary>
        /// TODO: Easy Achievement (1):
        ///
        /// Complete this Constructor.
        ///
        /// You should accept strings that look like: "4d4+10" and parse the
        /// correct Dice object fromthis string.
        ///
        /// e.g.  the above should translate to Dice(4, Type.D4, 10);
        ///
        /// TODO: Easy Achievement (2):
        ///
        /// If the given string is Not Valid, throw an ArgumentException.
        ///
        /// TODO: Hard Achievement (3):
        ///
        /// Use a Regular Expression (System.Text.RegularExpressions.Regex)
        /// to determine if the given string is Valid in ONE LINE of code!
        /// 
        /// </summary>
        /// <param name="dnDFormat"></param>
        public Dice(string dnDFormat)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Roll this dice!
        /// 
        /// literally, Dice.Roll(DiceType, Count) + Modifier
        /// </summary>
        /// <returns></returns>
        public int Roll() => Dice.Roll(DiceType, Count) + Modifier;

        /// <summary>
        /// TODO: Easy Achievement:
        /// 
        /// Complete this method so that it returns a DnD style dice notation
        /// e.g.  (new Dice(2, 8, 5)).ToString() should return: "2d8+5"
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            throw new NotImplementedException();
        }
    }
}

