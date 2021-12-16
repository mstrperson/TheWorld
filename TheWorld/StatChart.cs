using System;

namespace TheWorld
{
	public class StatChart
	{
		private int _hps;
		/// <summary>
		/// Gets or sets the Hit Points.
        /// HPs cannot exceed MaxHPs.
		/// </summary>
		public int HPs
		{
			get => _hps;
			set
            {
				_hps = value;
                if(_hps > MaxHPs)
                {
					_hps = MaxHPs;
                }
            }
		}

		/// <summary>
		/// Gets or sets the max Hit points.
		/// </summary>
		public int MaxHPs
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the level.
		/// </summary>
		public int Level
		{
			get;
			set;
		}

		/// <summary>
		/// Attack Power
		/// </summary>
		public Dice Atk
		{
			get;
			set;
		}

		/// <summary>
		/// Defense Modifier.
		/// </summary>
		/// <value>The def.</value>
		public Dice Def
		{
			get;
			set;
		}

        public int Exp
        {
            get;
            set;
        }

		/// <summary>
		/// Calculates the attack against an opponent.
		/// </summary>
		/// <returns>The attack value in HPs.</returns>
		/// <param name="opponent">Opponent's stat chart.</param>
		public int CalculateAttack(StatChart opponent)
		{
			// TODO: You need to write some calculations here.
			// Ideally, your calculations should have to do with
			// this object's attack power and the opponent's defense 
			// modifier.  You should also involve a dice roll probably!

			return Math.Max(this.Atk.Roll() - opponent.Def.Roll(), 0);
		}
	}
}

