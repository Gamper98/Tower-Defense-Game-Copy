using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDGame.Persistance.Fields
{
	/// <summary>
	/// Bányák típusa
	/// </summary>
	public class Mine : Field
	{
		#region Fields

		private Int32 _price;
		private Int32 _earning;
		private Int32 _upgradePrice;

		#endregion Properties

		#region Properties

		/// <summary>
		/// Ár lekérdezése
		/// </summary>
		public Int32 Price { get { return _price; } }
		/// <summary>
		/// Arany/forduló lekérdezése
		/// </summary>
		public Int32 Earning { get { return _earning; } }
		/// <summary>
		/// Bánya fejlesztés árának lekérdezése
		/// </summary>
		public Int32 UpgradePrice { get { return _upgradePrice; } }

		#endregion

		#region Constructor

		/// <summary>
		/// Mine példányosítása
		/// </summary>
		/// <param name="x">Függőleges kordináta</param>
		/// <param name="y">Vízszintes kordináta</param>
		public Mine(Int32 x, Int32 y, Difficulty dif)
		{
			_health = 9 - ((Int32)dif / 2);
			_x = x;
			_y = y;
			_level = 1;
			_price = 2 + (Int32)dif / 2;
			_earning = 2 - ((Int32)dif / 11);
			_upgradePrice = 8 + (Int32)dif / 2;
		}

		#endregion

		#region Public methods

		/// <summary>
		/// Fejlesztés függvénye
		/// </summary>
		public void LevelUp() 
		{
			_level++;
			_health += 1;
			_earning += 1;
			_upgradePrice++;
		}

		#endregion
	}
}
