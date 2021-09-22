using System;
using NUnit.Framework;
using TDGame.Model;
using TDGame.Persistance;
using TDGame.Persistance.Fields;
using TDGame.Persistance.Fields.Towers;
using System.Linq;

namespace TDGame.Test
{
    [TestFixture]
    public class TDGameModelTest
    {
        private TDModel _model;

        public TDGameModelTest()
        {
            _model = new TDModel();

            _model.GameAdvanced += new EventHandler<TDEventArgs>(Model_GameAdvanced);
            _model.GameOver += new EventHandler<TDEventArgs>(Model_GameOver);
            _model.CatastropheHappened += new EventHandler<TDEventArgs>(Model_Catastrophe);
        }

        [Test]
        public void TDModelNewGameMediumTest()
        {
            _model.Difficulty = Difficulty.Medium;
            _model.NewGame();

            Assert.AreEqual(40, _model.Gold, "starting gold test"); // kezdeti arany
            Assert.AreEqual(Difficulty.Medium, _model.Difficulty, "diff test"); // a nehézség beállítódott
            Assert.AreEqual(150, _model.GameTime, "time test"); // alapból ennyi időnk van

            Int32 baseNum = 0;
            Int32 i = 0;
            foreach (Base b in _model.Table.Bases)
            {
                if (b.X == 0 && b.Y == i)
                {
                    baseNum++;
                }
                i++;
            }
            Assert.AreEqual(10, baseNum, "base count 1"); // megfelelő helyen lévő házak száma
            Assert.AreEqual(10, _model.Table.Bases.Count, "base count 2"); // nincs több ház az adott mennyiségnél
            Assert.AreEqual(0, _model.Table.Enemies.Count, "starting enemy"); // kezdetben nincs ellenség
            Assert.AreEqual(0, _model.Table.Towers.Count, "starting tower"); // kezdetben nincs torony
            Assert.AreEqual(0, _model.Table.Mines.Count, "starting mine"); // kezdetben nincs bánya
            Assert.AreEqual(10, _model.Table.MaxX, "MaxX test"); // megfelelő méretű a tábla
            Assert.AreEqual(10, _model.Table.MaxY, "MaxY test");
        }

        [Test]
        public void TDModelNewGameEasyTest()
        {
            _model.Difficulty = Difficulty.Easy;
            _model.NewGame();

            Assert.AreEqual(40, _model.Gold); // kezdeti arany
            Assert.AreEqual(Difficulty.Easy, _model.Difficulty); // a nehézség beállítódott
            Assert.AreEqual(120, _model.GameTime); // alapból ennyi időnk van

            Int32 baseNum = 0;
            Int32 i = 0;
            foreach (Base b in _model.Table.Bases)
            {
                if (b.X == 0 && b.Y == i)
                {
                    baseNum++;
                }
                i++;
            }
            Assert.AreEqual(8, baseNum); // megfelelő helyen lévő házak száma
            Assert.AreEqual(8, _model.Table.Bases.Count); // nincs több ház az adott mennyiségnél
            Assert.AreEqual(0, _model.Table.Enemies.Count); // kezdetben nincs ellenség
            Assert.AreEqual(0, _model.Table.Towers.Count); // kezdetben nincs torony
            Assert.AreEqual(0, _model.Table.Mines.Count); // kezdetben nincs bánya
            Assert.AreEqual(8, _model.Table.MaxX); // megfelelő méretű a tábla
            Assert.AreEqual(8, _model.Table.MaxY);
        }

        [Test]
        public void TDModelNewGameHardTest()
        {
            _model.Difficulty = Difficulty.Hard;
            _model.NewGame();

            Assert.AreEqual(40, _model.Gold); // kezdeti arany
            Assert.AreEqual(Difficulty.Hard, _model.Difficulty); // a nehézség beállítódott
            Assert.AreEqual(180, _model.GameTime); // alapból ennyi időnk van

            Int32 baseNum = 0;
            Int32 i = 0;
            foreach (Base b in _model.Table.Bases)
            {
                if (b.X == 0 && b.Y == i)
                {
                    baseNum++;
                }
                i++;
            }
            Assert.AreEqual(12, baseNum); // megfelelő helyen lévő házak száma
            Assert.AreEqual(12, _model.Table.Bases.Count); // nincs több ház az adott mennyiségnél
            Assert.AreEqual(0, _model.Table.Enemies.Count); // kezdetben nincs ellenség
            Assert.AreEqual(0, _model.Table.Towers.Count); // kezdetben nincs torony
            Assert.AreEqual(0, _model.Table.Mines.Count); // kezdetben nincs bánya
            Assert.AreEqual(12, _model.Table.MaxX); // megfelelő méretű a tábla
            Assert.AreEqual(12, _model.Table.MaxY);
        }

        [Test]
        public void TDModelStepGameTest()
        {
            _model.NewGame();

            Enemy e = new Enemy(_model.Table.MaxX - 1, 0, _model.Table.Dif);
            _model.Table.AddEnemy(e);

            _model.StepGame();

            Assert.AreEqual(_model.Table.MaxX - 2, _model.Table.Enemies[0].X); // elmozdult-e az ellenség a megfelelő helyre
            Assert.AreEqual(0, _model.Table.Enemies[0].Y);

            Tower nt = new NormalTower(_model.Table.MaxX - 3, 0, _model.Table.Dif); // tornyot állítunk az ellenség elé
            Enemy e2 = new Enemy(_model.Table.MaxX - 1, 1, _model.Table.Dif);
            Tower bt = new Bomber(_model.Table.MaxX - 3, 1, _model.Table.Dif); // minden toronytípust tesztelünk
            Enemy e3 = new Enemy(_model.Table.MaxX - 1, 2, _model.Table.Dif);
            Tower tur = new Turret(_model.Table.MaxX - 3, 2, _model.Table.Dif);
            Enemy e4 = new Enemy(_model.Table.MaxX - 1, 3, _model.Table.Dif);
            Mine m = new Mine(_model.Table.MaxX - 3, 3, _model.Table.Dif);
            Enemy e5 = new Enemy(_model.Table.MaxX - 1, 4, _model.Table.Dif); // majd a házig léptetjük

            // különböző típusok táblához adása
            _model.Table.AddTower(nt);
            _model.Table.AddTower(bt);
            _model.Table.AddTower(tur);
            _model.Table.AddMine(m);
            _model.Table.AddEnemy(e2);
            _model.Table.AddEnemy(e3);
            _model.Table.AddEnemy(e4);
            _model.Table.AddEnemy(e5);

            _model.StepGame(); // minden ellenség a tornyok/bánya elé lép

            for (Int32 i = 0; i <= 3; i++)
            {
                Assert.AreEqual(_model.Table.MaxX - 2, _model.Table.Enemies[i].X);
                Assert.AreEqual(i, _model.Table.Enemies[i].Y);
            }

            _model.StepGame();

            // ellenőrizzük, hogy minden ellenség a helyén maradt-e
            for (Int32 i = 0; i <= 3; i++)
            {
                Assert.AreEqual(_model.Table.MaxX - 2, _model.Table.Enemies[i].X);
                Assert.AreEqual(i, _model.Table.Enemies[i].Y);
            }

            for (Int32 i = 0; i <= 50; i++) // 50 létetést végzünk
            {
                _model.StepGame();
            }

            // ellenőrizzük, hogy minden egység a helyén maradt-e
            for (Int32 i = 0; i <= 3; i++)
            {
                Assert.AreEqual(_model.Table.MaxX - 2, _model.Table.Enemies[i].X);
                Assert.AreEqual(i, _model.Table.Enemies[i].Y);
            }
            Assert.AreEqual(1, _model.Table.Enemies[4].X); // a ház előtt megállt-e az ellenség0
            Assert.AreEqual(4, _model.Table.Enemies[4].Y);
        }

        [Test]
        public void TDModelBuildTest()
        {
            _model.NewGame();
            NormalTower t = new NormalTower(3, 3, _model.Table.Dif);
            _model.Build(t);
            Int32 towerNum = 0;
            Boolean isGoodPlace = false;
            foreach (Tower tower in _model.Table.Towers)
            {
                if(tower.X == 3 && tower.Y == 3)
                {
                    isGoodPlace = true;
                }
                towerNum++;
            }
            Assert.AreEqual(31, _model.Gold);
            Assert.AreEqual(1, towerNum);
            Assert.IsTrue(isGoodPlace);

            t = new NormalTower(1, 3, _model.Table.Dif);
            _model.Build(t);            
            t = new NormalTower(2, 3, _model.Table.Dif);
            _model.Build(t);               
            t = new NormalTower(2, 4, _model.Table.Dif);
            _model.Build(t);            
            t = new NormalTower(3, 4, _model.Table.Dif);
            _model.Build(t);

            //minden marad ugyanaz, mivel nincs pénz az építésre
            towerNum = 0;
            isGoodPlace = false;
            foreach (Tower tower in _model.Table.Towers)
            {
                if (tower.X == 3 && tower.Y == 4)
                {
                    isGoodPlace = true;
                }
                towerNum++;
            }
            Assert.AreEqual(4, _model.Gold);
            Assert.AreEqual(4, towerNum);
            Assert.IsFalse(isGoodPlace);

            //tesztelés bányákkal is
            _model.NewGame();
            Mine m = new Mine(3, 3, _model.Table.Dif);
            _model.Build(m);
            Int32 mineNum = 0;
            isGoodPlace = false;
            foreach (Mine mine in _model.Table.Mines)
            {
                if (mine.X == 3 && mine.Y == 3)
                {
                    isGoodPlace = true;
                }
                mineNum++;
            }
            Assert.AreEqual(33, _model.Gold);
            Assert.AreEqual(1, mineNum);
            Assert.IsTrue(isGoodPlace);

            m = new Mine(1, 2, _model.Table.Dif);
            _model.Build(m);
            m = new Mine(2, 4, _model.Table.Dif);
            _model.Build(m);
            m = new Mine(1, 4, _model.Table.Dif);
            _model.Build(m);
            m = new Mine(1, 3, _model.Table.Dif);
            _model.Build(m);
            m = new Mine(3, 4, _model.Table.Dif);
            _model.Build(m);
            mineNum = 0;
            isGoodPlace = false;
            foreach (Mine mine in _model.Table.Mines)
            {
                if (mine.X == 3 && mine.Y == 4)
                {
                    isGoodPlace = true;
                }
                mineNum++;
            }
            Assert.AreEqual(5, _model.Gold);
            Assert.AreEqual(5, mineNum);
            Assert.IsFalse(isGoodPlace);

        }
        
        [Test]
        public void TDModelUpgradeTest()
        {
            _model.NewGame();

            Mine m = new Mine(1, 1, _model.Table.Dif);
            _model.Build(m);

            //nincs pénz a fejlesztésre (13 szükséges hozzá)
            _model.Upgrade(1, 1, 0);
            Assert.AreEqual(19, _model.Gold);

            //pénz gyűjtése (ekkor még nem érhetnek ellenségek a bányához)
            _model.AdvanceTime();
            _model.AdvanceTime();
            _model.AdvanceTime();
            _model.AdvanceTime();
            _model.AdvanceTime();
            _model.AdvanceTime();
            _model.AdvanceTime();
            _model.AdvanceTime();

            int expectedGold = _model.Gold - m.UpgradePrice;
            _model.Upgrade(1, 1, 0);
            Assert.AreEqual(expectedGold-1, _model.Gold);
        }

        [Test]
        public void TDModelSellUnitTest()
        {
            _model.NewGame();
            NormalTower t = new NormalTower(3, 3, _model.Table.Dif);
            _model.Build(t);

            _model.SellUnit(3, 3);
            Boolean empty = !_model.Table.Towers.Any();

            Assert.IsTrue(empty);
            Assert.AreEqual(35, _model.Gold);


            _model.NewGame();
            Mine m = new Mine(3, 3, _model.Table.Dif);
            _model.Build(m);

            _model.SellUnit(3, 3);
            empty = !_model.Table.Mines.Any();

            Assert.IsTrue(empty);
            Assert.AreEqual(36, _model.Gold);
        }
        
        [Test]
        public void TDModelAdvanceTimeTest()
        {

            //idő csökkentése
            _model.NewGame();
            Int32 time = _model.GameTime;

            _model.AdvanceTime();
            Assert.AreEqual(time - 1, _model.GameTime);
            _model.AdvanceTime();
            Assert.AreEqual(time - 2, _model.GameTime);

            //bányák pénztermelése
            _model.NewGame();
            Mine m = new Mine(1, 1, _model.Table.Dif);
            _model.Build(m);
            Int32 actualGold = _model.Gold;

            _model.AdvanceTime();
            Assert.AreEqual(actualGold, _model.Gold);
            _model.AdvanceTime();
            Assert.AreEqual(actualGold + m.Earning, _model.Gold);
            _model.AdvanceTime();
            Assert.AreEqual(actualGold + m.Earning + 1, _model.Gold); //3 másodpercenkénti +1 arany

            //NormalTower sebzésének tesztelése
            _model.NewGame();
            Enemy e = new Enemy(_model.Table.MaxX - 1, 0, Difficulty.Medium);
            Int32 enemyHP = e.Health;
            _model.Table.AddEnemy(e);
            _model.AdvanceTime();
            e = new Enemy(_model.Table.MaxX - 1, 0, Difficulty.Medium);
            _model.Table.AddEnemy(e);
            _model.AdvanceTime();
            e = new Enemy(_model.Table.MaxX - 1, 0, Difficulty.Medium);
            _model.Table.AddEnemy(e);
            NormalTower nt = new NormalTower(_model.Table.MaxX - 4, 0, Difficulty.Medium); //3-at sebez, 4 élete van
            Int32 towerHP = nt.Health;
            _model.Table.AddTower(nt);
            _model.AdvanceTime();
            //csak a legelsőt sebzi
            Assert.AreEqual(towerHP-e.Damage, _model.Table.Towers[0].Health);
            Assert.AreEqual(enemyHP-nt.Damage, _model.Table.Enemies[0].Health);
            Boolean notAttacked = true;
            for (Int32 i = 1; i < _model.Table.Enemies.Count; i++) //az első enemyt nem vizsgáljuk
            {
                if (_model.Table.Enemies[i].Health < enemyHP) notAttacked = false; 
            }
            Assert.IsTrue(notAttacked);

            _model.AdvanceTime();
            Assert.IsTrue(!_model.Table.Towers.Any()); //lerombolják az enemyk a tornyot

            _model.NewGame();
            nt = new NormalTower(_model.Table.MaxX - 4, 0, Difficulty.Medium);
            e = new Enemy(_model.Table.MaxX - 1, 0, Difficulty.Medium);
            _model.Table.AddEnemy(e);
            _model.Table.AddTower(nt);
            _model.AdvanceTime();
            Assert.AreEqual(towerHP, _model.Table.Towers[0].Health);
            Assert.AreEqual(enemyHP-nt.Damage, _model.Table.Enemies[0].Health);
            _model.AdvanceTime();
            Assert.AreEqual(towerHP, _model.Table.Towers[0].Health);
            Assert.AreEqual(enemyHP-2*nt.Damage, _model.Table.Enemies[0].Health);
            _model.AdvanceTime();
            Assert.AreEqual(towerHP-e.Damage, _model.Table.Towers[0].Health);

            //Bomber sebzésének tesztelése
            _model.NewGame();
            e = new Enemy(_model.Table.MaxX - 1, 0, Difficulty.Medium);
            enemyHP = e.Health;
            _model.Table.AddEnemy(e);
            Bomber bt = new Bomber(_model.Table.MaxX - 2, 0, Difficulty.Medium); //2-t sebez, 3 élete van
            towerHP = bt.Health;
            _model.Table.AddTower(bt);
            _model.AdvanceTime();

            Assert.IsTrue(!_model.Table.Towers.Any());
            Assert.AreEqual(enemyHP, _model.Table.Enemies[0].Health);

            _model.NewGame();
            bt = new Bomber(_model.Table.MaxX - 4, 0, Difficulty.Medium);
            e = new Enemy(_model.Table.MaxX - 1, 0, Difficulty.Medium);
            _model.Table.AddEnemy(e);
            _model.Table.AddTower(bt);
            _model.AdvanceTime();
            Assert.AreEqual(towerHP, _model.Table.Towers[0].Health);
            Assert.AreEqual(enemyHP - bt.Damage, _model.Table.Enemies[0].Health);
            _model.AdvanceTime();
            Assert.AreEqual(towerHP, _model.Table.Towers[0].Health);
            Assert.AreEqual(enemyHP - 2 * bt.Damage, _model.Table.Enemies[0].Health);
            _model.AdvanceTime();
            Assert.IsTrue(!_model.Table.Towers.Any());

            //Turret sebzésének tesztelése
            _model.NewGame();
            Turret t = new Turret(_model.Table.MaxX-3, 1, Difficulty.Medium);
            towerHP = t.Health;
            e = new Enemy(_model.Table.MaxX - 1, 0, Difficulty.Medium);
            Mine mine = new Mine(_model.Table.MaxX - 2, 1, Difficulty.Medium);
            _model.Table.AddMine(mine);
            _model.Table.AddTower(t);
            _model.Table.AddEnemy(e);
            _model.AdvanceTime();
            Assert.AreEqual(enemyHP, _model.Table.Enemies[0].Health); //még nem sebződött
            Assert.AreEqual(towerHP, _model.Table.Towers[0].Health);
            _model.AdvanceTime();
            Assert.AreEqual(enemyHP-t.Damage, _model.Table.Enemies[0].Health);
            Assert.AreEqual(towerHP, _model.Table.Towers[0].Health);

            _model.NewGame();
            e = new Enemy(_model.Table.MaxX - 1, 2, Difficulty.Medium);
            t = new Turret(_model.Table.MaxX - 3, 1, Difficulty.Medium);
            _model.Table.AddEnemy(e);
            _model.AdvanceTime();
            _model.AdvanceTime();
            _model.AdvanceTime();
            _model.Table.AddTower(t);
            _model.AdvanceTime();
            Assert.AreEqual(enemyHP - t.Damage, _model.Table.Enemies[0].Health);
        }

        private void Model_GameAdvanced(Object sender, TDEventArgs e)
        {
            Assert.IsTrue(_model.GameTime >= 0); // a játékidő nem lehet negatív

            Assert.AreEqual(e.Gold, _model.Gold); // a két értéknek egyeznie kell
            Assert.AreEqual(e.GameTime, _model.GameTime); // a két értéknek egyeznie kell
            Assert.IsFalse(e.IsWon); // még nem nyerték meg a játékot
        }

        private void Model_GameOver(Object sender, TDEventArgs e)
        {
            Assert.IsTrue(_model.IsGameOver);
            Assert.IsTrue(_model.Table.Bases.Where(b => b.Health == 0).ToList().Any() || e.GameTime == 0);
            Assert.IsFalse(e.IsWon);
        }

        private void Model_Catastrophe(Object sender, TDEventArgs e)
        {
            Assert.IsTrue(e.CatastrophePlace.Item1 >= 0 && e.CatastrophePlace.Item1 <= _model.Table.MaxX);
            Assert.IsTrue(e.CatastrophePlace.Item2 >= 0 && e.CatastrophePlace.Item2 <= _model.Table.MaxY);


            Boolean b = true;
            foreach (Enemy en in _model.Table.Enemies)
            {
                if (e.CatastrophePlace.Item1 <= en.X && en.X <= Math.Min(e.CatastrophePlace.Item1 + 2, _model.Table.MaxX - 1) && e.CatastrophePlace.Item2 <= en.Y && en.Y <= Math.Min(e.CatastrophePlace.Item2 + 2, _model.Table.MaxY - 1))
                {
                    if (en.Health > 0) b = false;
                }
            }
            foreach (Tower t in _model.Table.Towers)
            {
                if (e.CatastrophePlace.Item1 <= t.X && t.X <= Math.Min(e.CatastrophePlace.Item1 + 2, _model.Table.MaxX - 1) && e.CatastrophePlace.Item2 <= t.Y && t.Y <= Math.Min(e.CatastrophePlace.Item2 + 2, _model.Table.MaxY - 1))
                {
                    if (t.Health > 0) b = false;
                }
            }
            foreach (Mine m in _model.Table.Mines)
            {
                if (e.CatastrophePlace.Item1 <= m.X && m.X <= Math.Min(e.CatastrophePlace.Item1 + 2, _model.Table.MaxX - 1) && e.CatastrophePlace.Item2 <= m.Y && m.Y <= Math.Min(e.CatastrophePlace.Item2 + 2, _model.Table.MaxY - 1))
                {
                    if (m.Health > 0) b = false;
                }
            }
            Assert.IsTrue(b);
        }
    }
}
