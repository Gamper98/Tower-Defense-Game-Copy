using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TDGame.Model;
using TDGame.Persistance;
using TDGame.Persistance.Fields;
using TDGame.Persistance.Fields.Towers;

namespace TDGame.View
{
    public partial class GameForm : Form
    {
        #region Fields

        private TDModel _model; // játékmodell
        private Button[,] _buttonGrid; // gombrács
        private Timer _timer; // időzítő
        private enum GameTableSize { Small, Medium, Large };
        private GameTableSize difficulty; //nehézségi szint - táblaméret
        private Int32 ButtonSelected = 0;
        private Int32 SelectedX = 1000;
        private Int32 SelectedY = 1000;



        #endregion

        #region Images

        private Bitmap _enemyImage;
        private Bitmap _baseImage;
        private Bitmap _basewithturretImage;
        private Bitmap _basewithlifeImage;
        private Bitmap _turretImage;
        private Bitmap _normaltowerImage;
        private Bitmap _bomberImage;
        private Bitmap _mineImage;
        private Bitmap _explImage;
        private Bitmap _fixturretImage;
        private Bitmap _fixnormaltowerImage;
        private Bitmap _fixbomberImage;
        private Bitmap _fixmineImage;

        #endregion

        #region Constructors

        /// <summary>
        /// Játékablak példányosítása.
        /// </summary>
        public GameForm()
        {
            InitializeComponent();
        }

        #endregion

        #region Form event handlers
        /// <summary>
        /// Játékablak betöltésének eseménykezelője.
        /// </summary>
        private void GameForm_Load(Object sender, EventArgs e)
        {

            // modell létrehozása és az eseménykezelők társítása
            _model = new TDModel();
            _model.GameAdvanced += new EventHandler<TDEventArgs>(Game_OnGameAdvanced);
            _model.GameOver += new EventHandler<TDEventArgs>(Game_GameLost);
            _model.GameOver += new EventHandler<TDEventArgs>(Game_GameOver);
            _model.CatastropheHappened += new EventHandler<TDEventArgs>(Game_Catastrophe);

            // időzítő létrehozása
            _timer = new Timer();
            _timer.Interval = 1000;
            _timer.Tick += new EventHandler(Timer_Tick);

            difficulty = GameTableSize.Medium;

            // játéktábla és menük inicializálása
            GenerateTable();
            SetupMenus(); 

            // új játék indítása
            Load_Table();
            SetupTable();
            _timer.Start();
        }

        #endregion

        #region Game event handlers


        /// <summary>
        /// Játék előrehaladásának eseménykezelője.
        /// </summary>
        private void Game_OnGameAdvanced(Object sender, TDEventArgs e)
        {
            // játékidő, arany, hullám frissítése
            CurrentTimeLabel.Text = TimeSpan.FromSeconds(e.GameTime).ToString("g");
            CurrentGoldLabel.Text = e.Gold.ToString("g");

            if(e.CountDown < 0 && -15 < e.CountDown)
            {
                CountdownLabel.Text = "A hullámból ennyi idő van hátra: " + (15 + e.CountDown);
            }
            else if(e.CountDown > 0)
            {
                CountdownLabel.Text = "A következő hullámig ennyi idő van hátra: " + (e.CountDown);
            }
            else
            {
                CountdownLabel.Text = "";
            }

            if (SelectedX != 1000 && SelectedY != 1000)
            {
                textTowerUpdate();
                textMineUpdate();
                textEnemyUpdate();
                textBaseUpdate();
            }
            else
            {
                TypeLabel.Text = "";
                HPLabel.Text = "";
                LevelLabel.Text = "";
                UpgradeButton.Visible = false;
                DestroyButton.Visible = false;
            }

            SetupTable();
        }

        /// <summary>
        /// Játék megnyerésének eseménykezelője.
        /// </summary>
        private void Game_GameOver(Object sender, TDEventArgs e)
        {
            //SetupTable();
            _timer.Stop();

            if (e.IsWon) // győzelemtől függő üzenet megjelenítése
            {
                MessageBox.Show("You have beaten all the enemies!",
                                "TDGame",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Asterisk);
            }
            Load_Table();
        }
        
        /// <summary>
        /// Játék elvesztésének eseménykezelője.
        /// </summary>
        private void Game_GameLost(Object sender, TDEventArgs e)
        {
            //SetupTable();
            _timer.Stop();

            if (!e.IsWon) // vereségtől függő üzenet megjelenítése
            {
                MessageBox.Show("Enemies have breached your base! You lost!",
                                "TDGame",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Asterisk);
            }
            Load_Table();
        }

        private void Game_Catastrophe(Object sender, TDEventArgs e)
        {
            //_timer.Stop();
            for (Int32 i = e.CatastrophePlace.Item1; i < e.CatastrophePlace.Item1 + 3; i++)
            {
                for (Int32 j = e.CatastrophePlace.Item2; j < e.CatastrophePlace.Item2 + 3; j++)
                {
                    if(i < _model.Table.MaxX && j < _model.Table.MaxY && i != 0)
                    {
                        _buttonGrid[i, j].BackgroundImage = _explImage;
                    }
                }
            }
            //_timer.Stop();
            //_timer.Start();

        }

        #endregion

        #region Menu event handlers

        private /*async*/ void Load_Table()
        {
            DeleteTable();

            if (difficulty == GameTableSize.Small)
            {
                _model.Difficulty = Difficulty.Easy;
            }
            if (difficulty == GameTableSize.Medium)
            {
                _model.Difficulty = Difficulty.Medium;
            }
            if (difficulty == GameTableSize.Large)
            {
                _model.Difficulty = Difficulty.Hard;
            }
            _model.NewGame();

            GenerateTable();
            SetupTable();
            SetupMenus();

            _timer.Start();
        }


        #endregion

        #region Timer event handlers

        /// <summary>
        /// Időzítő eseménykezelője.
        /// </summary>
        private void Timer_Tick(Object sender, EventArgs e)
        {
            _model.AdvanceTime(); // játék léptetése
            //SetupTable();
        }


        #endregion

        #region Private methods

        /// <summary>
        /// Új játék eseménykezelője.
        ///
        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Load_Table();
        }


        /// <summary>
        /// Kilépés eseménykezelője.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Boolean restartTimer = _timer.Enabled;
            _timer.Stop();

            // megkérdezzük, hogy biztos ki szeretne-e lépni
            if (MessageBox.Show("Are you sure you want to quit the game?", "TDGame", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                // ha igennel válaszol
                Close();
            }
            else
            {
                if (restartTimer)
                    _timer.Start();
            }
        }

        private void easyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            difficulty = GameTableSize.Small;
            SetupMenus();
        }

        private void mediumToolStripMenuItem_Click(object sender, EventArgs e)
        {
            difficulty = GameTableSize.Medium;
            SetupMenus();
        }

        private void hardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            difficulty = GameTableSize.Large;
            SetupMenus();
        }

        private void NormalTowerButton_Click(object sender, EventArgs e)
        {
            ButtonSelected = 1;
            for (Int32 i = 0; i < _model.Table.MaxX; i++)
            {
                for (Int32 j = 0; j < _model.Table.MaxY; j++)
                {
                    if(i==0)
                    {
                        _buttonGrid[i, j].Enabled = false;
                    }
                    
                }
            }

            foreach (Tower t in _model.Table.Towers)
            {
                int x = t.X;
                int y = t.Y;


                _buttonGrid[x, y].Enabled = false;
            }

            foreach (Mine m in _model.Table.Mines)
            {
                int x = m.X;
                int y = m.Y;


                _buttonGrid[x, y].Enabled = false;
            }

            foreach (Enemy en in _model.Table.Enemies)
            {
                int x = en.X;
                int y = en.Y;


                _buttonGrid[x, y].Enabled = false;
            }



        }

        private void textTowerUpdate()
        {
            foreach (Tower t in _model.Table.Towers)
            {

                if (SelectedX == t.X && SelectedY == t.Y)
                {
                    UpgradeButton.Visible = true;
                    DestroyButton.Visible = true;
                    HPLabel.Text = "Health left: " + t.Health;
                    TypeLabel.Text = "Type of field: Tower";
                    LevelLabel.Text = "Level of field: " + t.Level;
                    return;
                }
            }
        }

        private void textMineUpdate()
        {
            foreach (Mine m in _model.Table.Mines)
            {
                if (SelectedX == m.X && SelectedY == m.Y)
                {
                    UpgradeButton.Visible = true;
                    DestroyButton.Visible = true;
                    HPLabel.Text = "Health left: " + m.Health;
                    TypeLabel.Text = "Type of field: Mine";
                    LevelLabel.Text = "Level of field: " + m.Level;
                    return;
                }
            }
        }

        private void textEnemyUpdate()
        {
            foreach (Enemy en in _model.Table.Enemies)
            {
                if (SelectedX == en.X && SelectedY == en.Y)
                {
                    UpgradeButton.Visible = false;
                    DestroyButton.Visible = false;
                    HPLabel.Text = "Health left: " + en.Health;
                    TypeLabel.Text = "Type of field: Enemy";
                    LevelLabel.Text = "";
                    return;
                }
            }
        }

        private void textBaseUpdate()
        {
            foreach (Base b in _model.Table.Bases)
            {
                if (SelectedX == b.X && SelectedY == b.Y)
                {
                    UpgradeButton.Visible = true;
                    DestroyButton.Visible = false;
                    HPLabel.Text = "Health left: " + b.Health;
                    TypeLabel.Text = "Type of field: Base";
                    LevelLabel.Text = "Level of field: " + b.Level;
                    return;
                }
            }
        }




        //táblán lévő gombokra való kattintás
        private void Button_Click(object sender, EventArgs e)
        {
            SelectedX = ((sender as Button).TabIndex - 100) / _model.Table.MaxX;
            SelectedY = ((sender as Button).TabIndex - 100) % _model.Table.MaxX;
            switch(ButtonSelected)
            {
                case 1:
                    {
                        _model.Build(new NormalTower(SelectedX, SelectedY, _model.Table.Dif));
                        break;
                    }
                case 2:
                    {
                        _model.Build(new Turret(SelectedX, SelectedY, _model.Table.Dif));
                        break;
                    }
                case 3:
                    {
                        _model.Build(new Bomber(SelectedX, SelectedY, _model.Table.Dif));
                        break;
                    }
                case 4:
                    {
                        _model.Build(new Mine(SelectedX, SelectedY, _model.Table.Dif));
                        break;
                    }
                case 0:
                    {
                        textTowerUpdate();
                        textMineUpdate();
                        textEnemyUpdate();
                        textBaseUpdate();
                        break;
                    }

            }
            //SelectedX = 1000;
            //SelectedY = 1000;
            ButtonSelected = 0;
            for (Int32 i = 0; i < _model.Table.MaxX; i++)
            {
                for (Int32 j = 0; j < _model.Table.MaxY; j++)
                {
                    _buttonGrid[i, j].Enabled = true;
                }
            }
        }



        private void DeleteTable()
        {
            for (Int32 i = 0; i < _model.Table.MaxX; i++)
            {
                for (Int32 j = 0; j < _model.Table.MaxY; j++)
                {
                    Controls.Remove(_buttonGrid[i, j]);
                }
            }
        }

        private void SetupMenus()
        {
            easyToolStripMenuItem.Checked = (difficulty == GameTableSize.Small);
            mediumToolStripMenuItem.Checked = (difficulty == GameTableSize.Medium);
            hardToolStripMenuItem.Checked = (difficulty == GameTableSize.Large);
        }

        private void GenerateTable()
        {
            _buttonGrid = new Button[_model.Table.MaxX, _model.Table.MaxY];
            for (Int32 i = 0; i < _model.Table.MaxX; i++)
                for (Int32 j = 0; j < _model.Table.MaxY; j++)
                {
                    _buttonGrid[i, j] = new Button();
                    _buttonGrid[i, j].Location = new Point(5 + (450 / _model.Table.MaxX) * j, 25 + (450 / _model.Table.MaxY) * i);
                    _buttonGrid[i, j].Size = new Size(450 / _model.Table.MaxX, 450 / _model.Table.MaxY);
                    _buttonGrid[i, j].Enabled = true;
                    _buttonGrid[i, j].TabIndex = 100 + i * _model.Table.MaxX + j;
                    _buttonGrid[i, j].FlatStyle = FlatStyle.Flat;
                    _buttonGrid[i, j].MouseClick += new MouseEventHandler(Button_Click);

                    Controls.Add(_buttonGrid[i, j]);

                }
            TypeLabel.Text = "";
            HPLabel.Text = "";
            LevelLabel.Text = "";

            Size ImgSize = new Size(450 / _model.Table.MaxX, 450 / _model.Table.MaxY);
            _enemyImage = new Bitmap((System.Drawing.Image)(TDGame.View.Properties.Resources.enemy), ImgSize);
            _baseImage = new Bitmap((System.Drawing.Image)(TDGame.View.Properties.Resources.basee), ImgSize);
            _basewithturretImage = new Bitmap((System.Drawing.Image)(TDGame.View.Properties.Resources.base_with_turret), ImgSize);
            _basewithlifeImage = new Bitmap((System.Drawing.Image)(TDGame.View.Properties.Resources.base_with_life), ImgSize);
            _turretImage = new Bitmap((System.Drawing.Image)(TDGame.View.Properties.Resources.turret), ImgSize);
            _normaltowerImage = new Bitmap((System.Drawing.Image)(TDGame.View.Properties.Resources.normaltower), ImgSize);
            _bomberImage = new Bitmap((System.Drawing.Image)(TDGame.View.Properties.Resources.bomber), ImgSize);
            _mineImage = new Bitmap((System.Drawing.Image)(TDGame.View.Properties.Resources.mine), ImgSize);
            _explImage = new Bitmap((System.Drawing.Image)(TDGame.View.Properties.Resources.explosion), ImgSize);

            Size fixImageSize = new Size(45, 45);
            _fixturretImage = new Bitmap((System.Drawing.Image)(TDGame.View.Properties.Resources.turret), fixImageSize);
            _fixnormaltowerImage = new Bitmap((System.Drawing.Image)(TDGame.View.Properties.Resources.normaltower), fixImageSize);
            _fixbomberImage = new Bitmap((System.Drawing.Image)(TDGame.View.Properties.Resources.bomber), fixImageSize);
            _fixmineImage = new Bitmap((System.Drawing.Image)(TDGame.View.Properties.Resources.mine), fixImageSize);

            NormalTowerButton.BackgroundImage = _fixnormaltowerImage;
            TurretButton.BackgroundImage = _fixturretImage;
            BomberButton.BackgroundImage = _fixbomberImage;
            MineButton.BackgroundImage = _fixmineImage;
    }

        private void SetupTable()
        {
            for (Int32 i = 0; i < _buttonGrid.GetLength(0); i++)
            {
                for (Int32 j = 0; j < _buttonGrid.GetLength(1); j++)
                {
                    _buttonGrid[i, j].BackgroundImage = null;
                    _buttonGrid[i, j].BackColor = Color.White;
                    _buttonGrid[i, j].FlatAppearance.BorderColor = Color.Black;
                    _buttonGrid[i, j].FlatAppearance.BorderSize = 1;
                }
            }
            foreach (Tower t in _model.Table.Towers)
            {
                int x = t.X;
                int y = t.Y;
                
                if(t is NormalTower)
                {
                    _buttonGrid[x, y].BackgroundImage = _normaltowerImage;
                }
                else if (t is Turret)
                {
                    _buttonGrid[x, y].BackgroundImage = _turretImage;
                }
                else
                {
                    _buttonGrid[x, y].BackgroundImage = _bomberImage;
                }
            }
            foreach (Enemy e in _model.Table.Enemies)
            {
                int x = e.X;
                int y = e.Y;

                _buttonGrid[x, y].BackgroundImage = _enemyImage;
            }
            foreach (Mine m in _model.Table.Mines)
            {
                int x = m.X;
                int y = m.Y;

                _buttonGrid[x, y].BackgroundImage = _mineImage;
            }
            foreach (Base b in _model.Table.Bases)
            {
                int x = b.X;
                int y = b.Y;

                if(b.Level == 2 && b.Damage != 0)
                {
                    _buttonGrid[x, y].BackgroundImage = _basewithturretImage;
                }
                else if(b.Level == 2 && b.Damage == 0)
                {
                    _buttonGrid[x, y].BackgroundImage = _basewithlifeImage;
                }
                else
                {
                    _buttonGrid[x, y].BackgroundImage = _baseImage;
                }
            }


            CurrentTimeLabel.Text = TimeSpan.FromSeconds(_model.GameTime).ToString("g");
            CurrentGoldLabel.Text = _model.Gold.ToString("g");
        }

        private void MineButton_Click(object sender, EventArgs e)
        {
            ButtonSelected = 4;
            for (Int32 i = 0; i < _model.Table.MaxX; i++)
            {
                for (Int32 j = 0; j < _model.Table.MaxY; j++)
                {
                    if (i == 0)
                    {
                        _buttonGrid[i, j].Enabled = false;
                    }

                }
            }

            foreach (Tower t in _model.Table.Towers)
            {
                int x = t.X;
                int y = t.Y;


                _buttonGrid[x, y].Enabled = false;
            }

            foreach (Mine m in _model.Table.Mines)
            {
                int x = m.X;
                int y = m.Y;


                _buttonGrid[x, y].Enabled = false;
            }

            foreach (Enemy en in _model.Table.Enemies)
            {
                int x = en.X;
                int y = en.Y;


                _buttonGrid[x, y].Enabled = false;
            }
        }

        private void TurretButton_Click(object sender, EventArgs e)
        {
            ButtonSelected = 2;
            for (Int32 i = 0; i < _model.Table.MaxX; i++)
            {
                for (Int32 j = 0; j < _model.Table.MaxY; j++)
                {
                    if (i == 0)
                    {
                        _buttonGrid[i, j].Enabled = false;
                    }

                }
            }

            foreach (Tower t in _model.Table.Towers)
            {
                int x = t.X;
                int y = t.Y;


                _buttonGrid[x, y].Enabled = false;
            }

            foreach (Mine m in _model.Table.Mines)
            {
                int x = m.X;
                int y = m.Y;


                _buttonGrid[x, y].Enabled = false;
            }

            foreach (Enemy en in _model.Table.Enemies)
            {
                int x = en.X;
                int y = en.Y;


                _buttonGrid[x, y].Enabled = false;
            }
        }

        private void BomberButton_Click(object sender, EventArgs e)
        {
            ButtonSelected = 3;
            for (Int32 i = 0; i < _model.Table.MaxX; i++)
            {
                for (Int32 j = 0; j < _model.Table.MaxY; j++)
                {
                    if (i == 0)
                    {
                        _buttonGrid[i, j].Enabled = false;
                    }

                }
            }

            foreach (Tower t in _model.Table.Towers)
            {
                int x = t.X;
                int y = t.Y;


                _buttonGrid[x, y].Enabled = false;
            }

            foreach (Mine m in _model.Table.Mines)
            {
                int x = m.X;
                int y = m.Y;


                _buttonGrid[x, y].Enabled = false;
            }

            foreach (Enemy en in _model.Table.Enemies)
            {
                int x = en.X;
                int y = en.Y;


                _buttonGrid[x, y].Enabled = false;
            }
        }

        private void destroyTower()
        {
            foreach (Tower t in _model.Table.Towers)
            {
                if (SelectedX == t.X && SelectedY == t.Y)
                {
                    _model.SellUnit(SelectedX, SelectedY);
                    SelectedX = 1000;
                    SelectedY = 1000;
                    return;
                }

            }
        }

        private void destroyMine()
        {
            foreach (Mine m in _model.Table.Mines)
            {
                if (SelectedX == m.X && SelectedY == m.Y)
                {
                    _model.SellUnit(SelectedX, SelectedY);
                    SelectedX = 1000;
                    SelectedY = 1000;
                    return;
                }

            }
        }


        private void DestroyButton_Click(object sender, EventArgs e)
        {
            destroyTower();
            destroyMine();
            SetupTable();
        }

        private void upgradeTower()
        {
            foreach (Tower t in _model.Table.Towers)
            {
                if (SelectedX == t.X && SelectedY == t.Y)
                {
                    _model.Upgrade(SelectedX, SelectedY, 0);
                    return;
                }
            }
        }

        private void upgradeMine()
        {
            foreach (Mine m in _model.Table.Mines)
            {
                if (SelectedX == m.X && SelectedY == m.Y)
                {
                    _model.Upgrade(SelectedX, SelectedY, 0);
                    return;
                }
            }
        }

        private void upgradeBase()
        {
            foreach (Base b in _model.Table.Bases)
            {
                if (SelectedX == b.X && SelectedY == b.Y)
                {
                    BaseLifeButton.Visible = true;
                    BaseTurretButton.Visible = true;
                    return;
                }
            }
        }

        private void UpgradeButton_Click(object sender, EventArgs e)
        {
            upgradeTower();
            upgradeMine();
            upgradeBase();
        }

        private void BaseLifeButton_Click(object sender, EventArgs e)
        {
            _model.Upgrade(SelectedX, SelectedY, 1);
            UpgradeButton.Visible = false;
            BaseLifeButton.Visible = false;
            BaseTurretButton.Visible = false;
            SelectedX = 1000;
            SelectedY = 1000;

        }

        private void BaseTurretButton_Click(object sender, EventArgs e)
        {
            _model.Upgrade(SelectedX, SelectedY, 2);
            UpgradeButton.Visible = false;
            BaseLifeButton.Visible = false;
            BaseTurretButton.Visible = false;
            SelectedX = 1000;
            SelectedY = 1000;
        }
    }

    #endregion
}
