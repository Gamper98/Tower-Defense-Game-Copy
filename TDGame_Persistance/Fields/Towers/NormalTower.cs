using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDGame.Persistance.Fields.Towers
{
	/// <summary>
	/// Alap torony típusa
	/// </summary>
	public class NormalTower : Tower
	{
		#region Constructor

		/// <summary>
		/// NormalTower példányosítása
		/// </summary>
		/// <param name="x">Függőleges kordináta</param>
		/// <param name="y">Vízszintes kordináta</param>
		public NormalTower(Int32 x, Int32 y, Difficulty dif)
		{
			_health = 9 - (Int32)dif / 2;
			_x = x;
			_y = y;
			_level = 1;
			_damage = 8 - (Int32)dif / 2;
			_range = 0;
			_price = 4 + (Int32)dif / 2;
			_upgradePrice = 6 + (Int32)dif / 2;
			_dmgArea = new List<(Int32, Int32)>() { (x + 1, y), (x + 2, y), (x + 3, y) };
			if (dif == Difficulty.Easy)
				_dmgArea.Add((x + 4, y));
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
			_upgradePrice++;
		}

		#endregion
	}
}
