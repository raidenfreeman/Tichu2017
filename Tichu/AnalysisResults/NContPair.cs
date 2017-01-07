using System.Collections.Generic;

namespace Tichu.AnalysisResults
{
    /// <summary>
    /// Represents a N amount of pairs of incremental (by 1) values
    /// </summary>
    public class NContPair : AnalysisResult
    {
        public NContPair(List<CardData> cards, int cardCount, int wildcardsCount) : base(cards, cardCount, wildcardsCount)
        {
        }


    }
}
