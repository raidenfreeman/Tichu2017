using System.Collections.Generic;

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
