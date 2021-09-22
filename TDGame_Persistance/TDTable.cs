using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDGame.Persistance.Fields;

namespace TDGame.Persistance
{
	/// <summary>
	/// Nehézség választó típusa
	/// </summary>
	public enum Difficulty { Easy = 8, Medium = 10, Hard = 12 };
	/// <summary>
	/// Játéktábla típusa
	/// </summary>
	public class TDTable
	{
		#region Fields

		private List<Enemy> _enemies;
		private List<Mine> _mines;
		private List<Tower> _towers;
		private List<Base> _bases;
		private Difficulty _dif;
		private Int32 _maxX;
		private Int32 _maxY;

		#endregion

		#region Properties

		/// <summary>
		/// Ellenségeket tároló lista lekérdezése
		/// </summary>
		public List<Enemy> Enemies { get { return _enemies; } }
		/// <summary>
		/// Bányákat tároló lista lekérdezése
		/// </summary>
		public List<Mine> Mines { get { return _mines; } }
		/// <summary>
		/// Tornyokat tároló lista lekérdezése
		/// </summary>
		public List<Tower> Towers { get { return _towers; } }
		/// <summary>
		/// Bázisokat tároló lista lekérdezése
		/// </summary>
		public List<Base> Bases { get { return _bases; } }

		/// <summary>
		/// Nehézség lekérdezése és beállítása
		/// </summary>
		public Difficulty Dif { get { return _dif; } set { _dif = value; } }
		/// <summary>
		/// Táblaméret függőleges maxja
		/// </summary>
		public Int32 MaxX { get { return _maxX; } }
		/// <summary>
		/// Táblaméret vízszintes maxja
		/// </summary>
		public Int32 MaxY { get { return _maxY; } }
		/// <summary>
		/// Ellenségek listához adása
		/// </summary>
		/// <param name="enemy">Adott ellenség</param>
		public void AddEnemy(Enemy enemy)
		{
			if (enemy.Y < 0 || enemy.Y > _maxY) // 0 <= enemy.X <= _maxX
				return;
			if (enemy.X != _maxX - 1) // csak kezdőpozíción lehet enemy eleinte
				return;
			_enemies.Add(enemy);
		}
		/// <summary>
		/// Bányák listához adása
		/// </summary>
		/// <param name="mine">Adott bánya</param>
		public void AddMine(Mine mine)
		{
			if (mine.X < 0 || mine.X > _maxX - 2) //nem lehet ellenség spawnon és mellette
				return;
			if (mine.Y < 0 || mine.Y > _maxY) // 0 <= mine.Y <= _maxY
				return;
			_mines.Add(mine);
		}
		/// <summary>
		/// Tornyok listához adása
		/// </summary>
		/// <param name="tower">Adott torony</param>
		public void AddTower(Tower tower)
		{
			if (tower.X < 0 || tower.X > _maxX - 2) //nem lehet ellenség spawnon és mellette
				return;
			if (tower.Y < 0 || tower.Y > _maxY) // 0 <= tower.Y <= _maxY
				return;

			_towers.Add(tower);
		}
		/// <summary>
		/// Bázis listához adása
		/// </summary>
		/// <param name="bases">Adott bázis</param>
		public void AddBase(Base bases)
		{
			if (bases.X != 0) // csak kezdőpozíción lehet bázis
				return;
			if (bases.Y < 0 || bases.Y > _maxY) // 0 <= bases.Y <= _maxY
				return;

			_bases.Add(bases);
		}

		#endregion

		#region Constructors

		/// <summary>
		/// TDTable példányosítása
		/// </summary>
		public TDTable() : this(Difficulty.Easy) { }
		/// <summary>
		/// TDTable példányosítása
		/// </summary>
		/// <param name="dif">Nehézség mértéke</param>
		public TDTable(Difficulty dif)
		{
			_enemies = new List<Enemy>();
			_mines = new List<Mine>();
			_towers = new List<Tower>();
			_bases = new List<Base>();
			_dif = dif;
			_maxX = (Int32)_dif;
			_maxY = (Int32)_dif;
		}

		#endregion

		#region Public methods

		/// <summary>
		/// Elpusztítottak kiszedése
		/// </summary>
		public void DeadRemoval()
		{
			_enemies = _enemies.Where(x => x.Health != 0).ToList();
			_mines = _mines.Where(x => x.Health != 0).ToList();
			_towers = _towers.Where(x => x.Health != 0).ToList();
		}
		/// <summary>
		/// Adott torony kivétele
		/// </summary>
		/// <param name="x">Függőleges kordináta</param>
		/// <param name="y">Vízszintes kordináta</param>
		public void RemoveTower(Int32 x, Int32 y)
		{
			_towers = _towers.Where(t => t.X != x || t.Y != y).ToList();
		}
		/// <summary>
		/// Adott bánya törlése
		/// </summary>
		/// <param name="x">Függőleges kordináta</param>
		/// <param name="y">Vízszintes kordináta</param>
		public void RemoveMine(Int32 x, Int32 y)
		{
			_mines = _mines.Where(t => t.X != x || t.Y != y).ToList();
		}

		#endregion
	}
}
