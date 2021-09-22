using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDGame.Persistance
{
	/// <summary>
	/// Mezők absztrakt típusa
	/// </summary>
	public abstract class Field
	{
		#region Fields

		protected Int32 _health;
		protected Int32 _x;
		protected Int32 _y;
		protected Int32 _level;

		#endregion

		#region Properties

		/// <summary>
		/// Élet lekérdezése
		/// </summary>
		public Int32 Health { get { return _health; } }
		/// <summary>
		/// Élet csökkentése
		/// </summary>
		/// <param name="value">Csökkentés mértéke</param>
		public void DecreaseHealth(Int32 value)
		{
			_health = _health - value;
            if (_health < 0)
                _health = 0;
		}
		/// <summary>
		/// Vízszintes kordináta lekérdezése
		/// </summary>
		public Int32 X { get { return _x; } }
		/// <summary>
		/// Függőleges kordináta lekérdezése
		/// </summary>
		public Int32 Y { get { return _y; } }
		/// <summary>
		/// Szintjének a lekérdezése
		/// </summary>
		public Int32 Level { get { return _level; } }

        #endregion

    }
}
