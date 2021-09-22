using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDGame.Persistance.Fields.Towers
{
	/// <summary> 
	/// Bomber típusa
	/// </summary>
	public class Bomber : Tower
	{
		#region Constructor

		/// <summary>
		/// Bomber példányosítása
		/// </summary>
		/// <param name="x">Függőleges kordináta</param>
		/// <param name="y">Vízszintes kordináta</param>
		public Bomber(Int32 x, Int32 y, Difficulty dif)
		{
			_health = 8 - (Int32)dif / 2;
			_x = x;
			_y = y;
			_level = 1;
			_damage = 7 - (Int32)dif / 2;
			_range = 1;
			_price = 6 + (Int32)dif / 2;
			_upgradePrice = 8 + (Int32)dif / 2;
			_dmgArea = new List<(Int32, Int32)>() { (x + 1, y), (x + 2, y), (x + 3, y) };
		}

		#endregion

		#region Public methods

		/// <summary>
		/// Fejlesztés függvénye
		/// </summary>
		public override void LevelUp()
		{
			_level++;
			_health += 1;
			_damage += 1;
			if (_level == 4)
				_range++;
			_upgradePrice++;
		}

		#endregion

	}
}
