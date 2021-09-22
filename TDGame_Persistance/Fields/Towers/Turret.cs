using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDGame.Persistance.Fields.Towers
{
	/// <summary>
	/// Turret típusa
	/// </summary>
	public class Turret : Tower
	{
		#region Constructor

		/// <summary>
		/// Turret példányosítása
		/// </summary>
		/// <param name="x">Függőleges kordináta</param>
		/// <param name="y">Vízszintes kordináta</param>
		public Turret(Int32 x, Int32 y, Difficulty dif)
		{
			_health = 11 - (Int32)dif / 2;
			_x = x;
			_y = y;
			_level = 1;
			_damage = 9 - (Int32)dif / 2;
			_range = 2 - (Int32)dif / 9;
			_price = 5 + (Int32)dif / 2;
			_upgradePrice = 7 + (Int32)dif / 2;
			_dmgArea = new List<(Int32, Int32)>() { (x , y) };
		}

		#endregion

		#region Public methods

		/// <summary>
		/// Fejlesztés függvénye
		/// </summary>
		public override void LevelUp()
		{
			_level++;
			_health += 2;
			_damage += 1;
			_upgradePrice++;
		}

		#endregion
	}
}
