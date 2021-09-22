using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDGame.Persistance.Fields
{
	/// <summary>
	/// Ellenségek típusa
	/// </summary>
	public class Enemy : Field
	{
		#region Fields

		private Int32 _damage;
		private Int32 _range;

		#endregion

		#region Properties

		/// <summary>
		/// Sebzés visszaadása
		/// </summary>
		public Int32 Damage { get { return _damage; } }
		/// <summary>
		/// Sebzési távolság visszaadása
		/// </summary>
		public Int32 Range { get { return _range; } }

		#endregion

		#region Constructor

		/// <summary>
		/// Enemy példányosítása
		/// </summary>
		/// <param name="x">Függőleges kordináta</param>
		/// <param name="y">Vízszintes kordináta</param>
		public Enemy(Int32 x, Int32 y, Difficulty dif)
		{
			_health = 2 + ((Int32)dif / 2);
			_x = x;
			_y = y;
			_level = 1;
			_damage = 0 + ((Int32)dif/3);
			_range = 0;
		}

		#endregion

		#region Public methods

		/// <summary>
		/// Ellenségek előre mozgatása
		/// </summary>
		public void moveForward()
		{
			_x--;
		}

		#endregion
	}
}
