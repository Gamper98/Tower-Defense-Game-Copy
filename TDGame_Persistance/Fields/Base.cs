using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDGame.Persistance.Fields
{
	/// <summary>
	/// Base típusa
	/// </summary>
	public class Base : Field
	{
		#region Fields

		private Int32 _damage;
		private Int32 _upgradePrice;

		#endregion

		#region Properties

		/// <summary>
		/// Sebzés visszaadása
		/// </summary>
		public Int32 Damage { get { return _damage; } }
		/// <summary>
		/// Bázis fejlesztés árának lekérdezése
		/// </summary>
		public Int32 UpgradePrice { get { return _upgradePrice; } }
		#endregion

		#region Constructor

		/// <summary>
		/// Base példányosítása
		/// </summary>
		/// <param name="x">Függőleges kordináta</param>
		/// <param name="y">Vízszintes kordináta</param>
		public Base(Int32 x, Int32 y)
		{
			_health = 5;
			_x = x;
			_y = y;
			_level = 1;
			_damage = 0;
			_upgradePrice = 25;
		}

		#endregion

		#region Public methods

		/// <summary>
		/// Fejlesztés függvénye
		/// </summary>
		/// <param name="option">1 - Élet növelése, 2 - Sor sebzése</param>
		public void LevelUp(Int32 option)
		{
			if(_level == 1)
				switch (option)
				{
					case 1:
						_level++;
						_health += 10;
						break;
					case 2:
						_level++;
						_damage = 4;
						break;
					default:
						break;
				}
		}

		#endregion
	}
}
