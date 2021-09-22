using System;

namespace TDGame.Model
{
    /// <summary>
    /// TDGame eseményargumentum típusa.
    /// </summary>
    public class TDEventArgs : EventArgs
    {
        private Int32 _gameTime;
        private Int32 _gold;
        private Boolean _isWon;
        private Int32 _wave;
        private Int32 _countDown;
        private (Int32, Int32) _catastrophePlace;

        /// <summary>
        /// Játékidő lekérdezése.
        /// </summary>
        public Int32 GameTime { get { return _gameTime; } }

        /// <summary>
        /// Arany mennyiségének lekérdezése.
        /// </summary>
        public Int32 Gold { get { return _gold; } }

        /// <summary>
        /// Győzelem lekérdezése.
        /// </summary>
        public Boolean IsWon { get { return _isWon; } }

        /// <summary>
        /// Támadási hullám lekérdezése.
        /// </summary>
        public Int32 Wave { get { return _wave; } }

        /// <summary>
        /// Támadási hullámig lévő idő (a támadási hullám közben eltelt időt negatívan tárolja).
        /// </summary>
        public Int32 CountDown { get { return _countDown; } }

        /// <summary>
        /// A katasztrófa területének bal felső mezőjét adja meg.
        /// </summary>
        public (Int32, Int32) CatastrophePlace { get { return _catastrophePlace; } }

        /// <summary>
        /// TDGame eseményargumentum példányosítása.
        /// </summary>
        /// <param name="isWon">Győzelem lekérdezése.</param>
        /// <param name="gameTime">Játékidő.</param>
        /// <param name="gold">Arany.</param>
        /// <param name="wave">Támadási hullám.</param>
        /// <param name="countDown">Visszaszámlálás.</param>
        /// <param name="catastrophePlace">Katasztrófa helye.</param>
        public TDEventArgs(Boolean isWon, Int32 gameTime, Int32 gold, Int32 wave, Int32 countDown, (Int32, Int32) catastrophePlace)
        {
            _isWon = isWon;
            _gameTime = gameTime;
            _gold = gold;
            _wave = wave;
            _countDown = countDown;
            _catastrophePlace = catastrophePlace;
        }
    }
}
