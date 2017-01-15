using System.Collections.Generic;
using Tichu.CardDataNamespace;

namespace Tichu.AnalysisResults
{
    /// <summary>
    /// Represents no valid combination
    /// </summary>
    public class Invalid : TichuTrick
    {
        public Invalid(List<CardData> cards, int cardCount, int wildcardsCount) : base(cards, cardCount, wildcardsCount)
        {

        }

        protected override int CalculateTrickValue()
        {
            return 0;
        }

        protected override int CalculateHighestCardValue()
        {
            return 0;
        }

    }
}
