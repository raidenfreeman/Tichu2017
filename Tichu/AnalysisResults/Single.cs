using System.Collections.Generic;
using Tichu.CardDataNamespace;

namespace Tichu.AnalysisResults
{
    /// <summary>
    /// Represents a single card
    /// </summary>
    public class Single : AnalysisResult
    {
        public Single(List<CardData> cards, int cardCount, int wildcardsCount) : base(cards, cardCount, wildcardsCount)
        {
        }
    }
}
