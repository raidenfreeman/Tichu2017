using System.Collections.Generic;

namespace Tichu.AnalysisResults
{
    /// <summary>
    /// Represents a pair of cards with the same value
    /// </summary>
    public class Pair : AnalysisResult
    {
        public Pair(List<CardData> cards, int cardCount, int wildcardsCount) : base(cards, cardCount, wildcardsCount)
        {
        }
    }
}
