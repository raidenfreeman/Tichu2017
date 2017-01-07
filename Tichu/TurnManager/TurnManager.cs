using System;

namespace Tichu.TurnManager
{
    public class TurnManager
    {
        /// <summary>
        /// Keeps track of how many turns have passed
        /// </summary>
        public int TurnCount { get; private set; }

        public event Action TurnAdvanced;

        public void AdvanceTurn()
        {
            TurnCount += 1;
            TurnAdvanced?.Invoke();
        }
    }
}
