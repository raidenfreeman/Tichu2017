using System.Collections.Generic;
using Tichu.CardDataNamespace;

namespace Tichu.AnalysisResults
{
    /// <summary>
    /// Represents four cards of the same value, or a straight flush
    /// </summary>
    public sealed class BombStraight : Bomb
    {
        internal BombStraight(List<CardData> cards, int cardCount, int wildcardsCount) : base(cards, cardCount, wildcardsCount)
        {

        }

        protected override int CalculateTrickValue()
        {
            return HighestCardValue + 128 * CardCount;
        }
    }
}
