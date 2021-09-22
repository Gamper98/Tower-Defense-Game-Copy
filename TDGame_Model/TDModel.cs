using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TDGame.Persistance;
using TDGame.Persistance.Fields;
using TDGame.Persistance.Fields.Towers;

namespace TDGame.Model
{
    /// <summary>
    /// TDGame játék típusa.
    /// </summary>
    public class TDModel
    {
        #region Fields

        private Difficulty _difficulty;
        private Int32 _gold;
        private Int32 _gameTime;
        private TDTable _table;
        private Random _random;
        private Int32 _wave;
        private Int32 _countDown; //minden wave után 5 másodperc szünet
        private Boolean _normalMode; //3 másodpercenként egy ellenség jön

        #endregion

        #region Properties

        /// <summary>
        /// A játék nehézségének lekérdezése/állítása.
        /// </summary>
        public Difficulty Difficulty { get { return _difficulty; } set { _difficulty = value; } }
        /// <summary>
        /// Arany mennyiségének lekérdezése.
        /// </summary>
        public Int32 Gold { get { return _gold; } }
        /// <summary>
        /// A játéktábla lekérdezése.
        /// </summary>
        public TDTable Table { get { return _table; } }
        /// <summary>
        /// A játékidő lekérdezése.
        /// </summary>
        public Int32 GameTime { get { return _gameTime; } }
        /// <summary>
        /// A hullám lekérdezése.
        /// </summary>
        public Int32 Wave { get { return _wave; } }
        /// <summary>
        /// A játék végének lekérdezése.
        /// </summary>
        public Boolean IsGameOver
        {
            get
            {
                Boolean r = false;
                foreach (Base b in _table.Bases)
                {
                    if (b.Health == 0) r = true;
                }
                return r;
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// TDGame játék példányosítása.
        /// </summary>
        public TDModel()
        {
            _difficulty = Difficulty.Medium;
            _gold = 0;
            _gameTime = 0;
            _table = new TDTable();
            // bázisok létrehozása
            for (Int32 i = 0; i < _table.MaxY; i++)
            {
                _table.AddBase(new Base(0, i));
            }
            _random = new Random();
            _wave = 1;
            _countDown = -16;
            _normalMode = true;
        }

        #endregion

        #region Public game methods

        /// <summary>
        /// Új játék kezdése.
        /// </summary>
        public void NewGame()
        {
            _gold = 40;
            _gameTime = (int)_difficulty * 15;
            _table = new TDTable(_difficulty);
            _wave = 1;
            _countDown = -16;

            ///bázis hozzáadása
            for (Int32 i = 0; i < Table.MaxY; i++)
            {
                Table.AddBase(new Base(0, i));
            }
            _normalMode = true;
        }
        /// <summary>
        /// Játék léptetése.
        /// </summary>
        public void StepGame()
        {
            if (IsGameOver)
                return;
            //Ellenségek mozgása
            foreach (Enemy e in _table.Enemies)
            {
                Boolean canMove = true;
                foreach (Tower t in _table.Towers)
                {
                    canMove = canMove && EnemycanMove(t,e);
                }
                foreach (Mine m in _table.Mines)
                {
                    canMove = canMove && EnemycanMove(m, e);
                }
                foreach (Base b in _table.Bases)
                {
                    canMove = canMove && EnemycanMove(b, e);
                }
                foreach (Enemy e2 in _table.Enemies)
                {
                    canMove = canMove && EnemycanMove(e2, e);
                }
                if (canMove)
                {
                    e.moveForward();
                }
            }
        }
        /// <summary>
        /// Építés.
        /// </summary>
        /// <param name="f">Az épület típusa.</param>
        public void Build(Field f)
        {
            if (f.X >= Table.MaxX - 2) return;

            if (f is Tower && _gold >= (f as Tower).Price)
            {
                _table.AddTower((Tower)f);
                _gold -= (f as Tower).Price;
            }
            else if (f is Mine && _gold >= (f as Mine).Price)
            {
                _table.AddMine((Mine)f);
                _gold -= (f as Mine).Price;
            }
        }
        /// <summary>
        /// Játékidő léptetése
        /// </summary>
        public void AdvanceTime()
        {
            _gameTime--;
            Dmg();
            if (_gameTime == 0)
            {
                OnGameOver(true);
            }
            StepGame();
            waveCountDown();
            wave();
            //mineok után arany adása körönként
            foreach (Mine m in _table.Mines)
            {
                if (_gameTime % 2 == 0)
                    _gold += m.Earning;
            }
            //időnkénti hullámok
            if (_gameTime == 60 || _gameTime == 120 || _gameTime == 150)
            {
                _wave++;
                _countDown = 10;
            }
            //passzív kereslet
            if (_gameTime % 3 == 0) _gold += 1;

            OnGameAdvanced();
            //katasztrófa
            if (_random.Next(50) == 0) Catastrophe();

            if (IsGameOver)
                OnGameOver(false);
        }
        /// <summary>
        /// Torony fejlesztése
        /// </summary>
        /// <param name="x">A torony függőleges koordinátája.</param>
        /// <param name="y">A torony vízszintes koordinátája.</param>
        /// <param name="option">bázis opciója</param>
        public void Upgrade(Int32 x, Int32 y, Int32 option)
        {
            foreach (Tower t in _table.Towers)
            {
                if (t.X == x && t.Y == y && t.UpgradePrice <= _gold)
                {
                    t.LevelUp();
                    _gold -= t.UpgradePrice;
                }
            }
            foreach (Mine m in _table.Mines)
            {
                if (m.X == x && m.Y == y && m.UpgradePrice <= _gold)
                {
                    m.LevelUp();
                    _gold -= m.UpgradePrice;
                }
            }
            foreach (Base b in _table.Bases)
            {
                if (b.X == x && b.Y == y && b.UpgradePrice <= _gold)
                {
                    b.LevelUp(option);
                    _gold -= b.UpgradePrice;

                }
            }
        }
        /// <summary>
        /// Sebzés
        /// </summary>
        private void Dmg()
        {
            dmgEnemy();
            _table.DeadRemoval();

            foreach (Tower t in _table.Towers)
            {
                dmgTowers(t);
            }
            dmgBase();
            _table.DeadRemoval();
        }
        /// <summary>
        /// Adott egység eladása
        /// </summary>
        /// <param name="x">Az egység függőleges koordinátája.</param>
        /// <param name="y">Az egység vízszintes koordinátája.</param>
        public void SellUnit(Int32 x, Int32 y)
        {
            SellTower(x, y);
            SellMine(x, y);
        }

        #endregion

        #region Private game methods

        /// <summary>
        /// Ellenség tud-e mozogni
        /// </summary>
        /// <param name="f">Adott mező</param>
        /// <param name="e">Adott ellenség</param>
        /// <returns>false ha van elötte valami egyébként true</returns>
        private Boolean EnemycanMove(Field f, Enemy e)
        {
            if (f.X == e.X - 1 && f.Y == e.Y)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// hullámok visszaszámolója és módjának kezelóje
        /// </summary>
        private void waveCountDown()
        {
            if (_countDown > 0)
            {
                _countDown--;
            }
            else if (_countDown == 0)
            {
                _normalMode = false;
                _countDown--;
            }
            else if (_countDown < 0 && -15 < _countDown)
            {
                _countDown--;
            }
            else if (_countDown == -15)
            {
                _countDown = -16;
                _normalMode = true;
            }
        }
        /// <summary>
        /// hullámok elindítása
        /// </summary>
        private void wave()
        {
            if (_gameTime % (3 - _wave / 2) == 0 && !_normalMode) // az enemyk létrehozásának gyakoriságát szabályozza _wave változóban
            {
                GenerateEnemies();
            }
            else if (_gameTime % 3 == 0 && _normalMode)
            {
                GenerateEnemies();
            }
        }
        /// <summary>
        /// Ellenségek sebzése
        /// </summary>
        private void dmgEnemy()
        {
            foreach (Enemy e in _table.Enemies)
            {
                foreach (Tower t in _table.Towers)
                {
                    dmgObjectsByEnemy(t, e);
                }

                foreach (Mine m in _table.Mines)
                {
                    dmgObjectsByEnemy(m, e);
                }

                foreach (Base b in _table.Bases)
                {
                    dmgObjectsByEnemy(b, e);
                }
            }
        }
        /// <summary>
        /// Adott field adott enemy általi sebzése
        /// </summary>
        /// <param name="f">Adott mező</param>
        /// <param name="e">Adott ellenség</param>
        private void dmgObjectsByEnemy(Field f, Enemy e)
        {
            if (e.X - 1 == f.X && e.Y == f.Y)
            {
                f.DecreaseHealth(e.Damage);
            }
        }
        /// <summary>
        /// Adott torony sebzésmintájának ellenörzése
        /// </summary>
        /// <param name="t">Adott torony</param>
        private void dmgTowers(Tower t)
        {
            foreach ((Int32 x, Int32 y) in t.DmgArea)
            {
                if (t.X == x && t.Y == y) //Turret sebzésminta mégnézése
                {
                    dmgTower(t, x, y);
                    return;
                }
                foreach (Enemy e in _table.Enemies)//NormalTower és Bomber sebzésminta megnézése
                {
                    if (e.X == x && e.Y == y)
                    {
                        dmgTower(t, x, y);
                        return;
                    }
                }
            }
        }
        /// <summary>
        /// Adott torony sebzése
        /// </summary>
        /// <param name="t">Adott torony</param>
        /// <param name="x">Sebzés középpontjának függőleges kordinátája</param>
        /// <param name="y">Sebzés középpontjának vízszintes kordinátája</param>
        private void dmgTower(Tower t, Int32 x, Int32 y)
        {
            foreach (Enemy e in _table.Enemies)
            {
                if (Math.Abs(e.X - x) <= t.Range && Math.Abs(e.Y - y) <= t.Range)
                {
                    e.DecreaseHealth(t.Damage);
                    if (e.Health == 0) _gold += 1;
                }
            }
        }
        /// <summary>
        /// Bázis sebzése
        /// </summary>
        private void dmgBase()
        {
            foreach (Base b in _table.Bases)
            {
                foreach (Enemy e in _table.Enemies)
                {
                    if (e.Y == b.Y)
                    {
                        e.DecreaseHealth(b.Damage);
                    }
                }
            }
        }
        /// <summary>
        /// adott torony eladása
        /// </summary>
        /// <param name="x">Az egység függőleges koordinátája.</param>
        /// <param name="y">Az egység vízszintes koordinátája.</param>
        private void SellTower(Int32 x, Int32 y)
        {
            foreach (Tower t in _table.Towers)
            {
                if (t.X == x && t.Y == y)
                {
                    _gold += t.Price / 2;
                    Table.RemoveTower(x, y);
                    return;
                }
            }
        }
        /// <summary>
        /// adott bánya eladása
        /// </summary>
        /// <param name="x">Az egység függőleges koordinátája.</param>
        /// <param name="y">Az egység vízszintes koordinátája.</param>
        private void SellMine(Int32 x, Int32 y)
        {
            foreach (Mine m in _table.Mines)
            {
                if (m.X == x && m.Y == y)
                {
                    _gold += m.Price / 2;
                    Table.RemoveMine(x, y);
                    return;
                }
            }
        }
        /// <summary>
        /// Ellenségek létrehozása.
        /// </summary>
        private void GenerateEnemies()
        {
            Int32 y = _random.Next(_table.MaxY);
            Boolean freePlace = true;
            foreach (Enemy e in _table.Enemies)
            {
                if (e.X == (_table.MaxX - 1) && e.Y == y)
                    freePlace = false;
            }
            if (freePlace) _table.AddEnemy(new Enemy(_table.MaxX - 1, y, Table.Dif));
            if (_wave > 2 && !_normalMode)
            {
                Int32 newY = _random.Next(_table.MaxY);
                while (y == newY)
                {
                    newY = _random.Next(_table.MaxY);
                }
                freePlace = true;
                foreach (Enemy e in _table.Enemies)
                {
                    if (e.X == (_table.MaxX - 1) && e.Y == newY)
                        freePlace = false;
                }
                if (freePlace) _table.AddEnemy(new Enemy(_table.MaxX - 1, newY, Table.Dif));
            }
            OnGameAdvanced();
        }
        /// <summary>
        /// Katasztófa bekövetkezése.
        /// </summary>
        private void Catastrophe()
        {
            Int32 x = _random.Next(_table.MaxX);
            Int32 y = _random.Next(_table.MaxY);
            foreach (Enemy e in _table.Enemies)
            {
                CatastropheDmg(e, x, y);
            }
            foreach (Tower t in _table.Towers)
            {
                CatastropheDmg(t, x, y);
            }
            foreach (Mine m in _table.Mines)
            {
                CatastropheDmg(m, x, y);
            }
            _table.DeadRemoval();
            OnCatastropheHappened(x, y);
        }
        /// <summary>
        /// Katasztrófa sebzése
        /// </summary>
        /// <param name="e">Adott típusú Field</param>
        /// <param name="x">Katasztrófa középpontjának függőleges kordinátája</param>
        /// <param name="y">Katasztrófa középpontjának vízszintes kordinátája</param>
        private void CatastropheDmg(Field e, Int32 x, Int32 y)
        {
            if (x <= e.X && e.X <= Math.Min(x + 2, _table.MaxX - 1) && y <= e.Y && e.Y <= Math.Min(y + 2, _table.MaxY - 1))
            {
                e.DecreaseHealth(e.Health);
            }
        }

        #endregion

        #region Events

        /// <summary>
        /// Játék előrehaladásának eseménye.
        /// </summary>
        public event EventHandler<TDEventArgs> GameAdvanced;
        /// <summary>
        /// Játék végének eseménye.
        /// </summary>
        public event EventHandler<TDEventArgs> GameOver;
        /// <summary>
        /// Katasztrófa eseménye.
        /// </summary>
        public event EventHandler<TDEventArgs> CatastropheHappened;

        #endregion

        #region Private event methods

        /// <summary>
        /// Játékidő változás eseményének kiváltása.
        /// </summary>
        private void OnGameAdvanced()
        {
            if (GameAdvanced != null)
                GameAdvanced(this, new TDEventArgs(false, _gameTime, _gold, _wave, _countDown, (-1, -1)));
        }
        /// <summary>
        /// Játék vége eseményének kiváltása.
        /// </summary>
        /// <param name="isWon">Vesztettünk-e a játékban.</param>
        private void OnGameOver(Boolean isWon)
        {
            if (GameOver != null)
                GameOver(this, new TDEventArgs(isWon, _gameTime, _gold, _wave, _countDown, (-1, -1)));
        }
        /// <summary>
        /// Katasztrófa eseményének kiváltása.
        /// </summary>
        /// <param name="x">A katasztrófa helyének x koordinátája.</param>
        /// <param name="y">A katasztrófa helyénel y koordinátája.</param>
        private void OnCatastropheHappened(Int32 x, Int32 y)
        {
            if (CatastropheHappened != null)
                CatastropheHappened(this, new TDEventArgs(false, _gameTime, _gold, _wave, _countDown, (x, y)));
        }

        #endregion
    }
}

