using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDGame.Persistance.Fields
{
	/// <summary>
	/// Tornyok absztrakt típusa
	/// </summary>
	public abstract class Tower : Field
	{
		#region Fields

		protected Int32 _damage;
		protected Int32 _range;
		protected Int32 _price;
		protected List<(Int32, Int32)> _dmgArea;
		protected Int32 _upgradePrice;

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
		/// <summary>
		/// Ár lekérdezése
		/// </summary>
		public Int32 Price { get { return _price; } }
		/// <summary>
		/// sebzésminta visszaadása
		/// </summary>
		public List<(Int32, Int32)> DmgArea { get { return _dmgArea; } }
		/// <summary>
		/// torony fejlesztés árának lekérdezése
		/// </summary>
		public Int32 UpgradePrice { get { return _upgradePrice; } }

		#endregion

		#region Public methods
		/// <summary>
		/// LevelUp Függvény absztrakt típusa
		/// </summary>
		public abstract void LevelUp();

        #endregion
    }
}
