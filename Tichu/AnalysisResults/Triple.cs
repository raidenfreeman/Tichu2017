using System.Collections.Generic;
using Tichu.CardDataNamespace;

namespace Tichu.AnalysisResults
{
    /// <summary>
    /// Represents 3 cards of the same value
    /// </summary>
    public class Triple : AnalysisResult
    {
        public Triple(List<CardData> cards, int cardCount, int wildcardsCount) : base(cards, cardCount, wildcardsCount)
        {
        }
    }
}
