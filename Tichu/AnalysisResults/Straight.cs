using System.Collections.Generic;

namespace Tichu.AnalysisResults
{
    /// <summary>
    /// Represents 5 or more cards with incremental values
    /// </summary>
    public class Straight : AnalysisResult
    {
        public Straight(List<CardData> cards, int cardCount, int wildcardsCount) : base(cards, cardCount, wildcardsCount)
        {
        }
    }
}
