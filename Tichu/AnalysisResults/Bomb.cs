using System.Collections.Generic;
using Tichu.CardDataNamespace;

namespace Tichu.AnalysisResults
{
    /// <summary>
    /// Represents four cards of the same value, or a straight flush
    /// </summary>
    public class Bomb : TichuTrick
    {
        public Bomb(List<CardData> cards, int cardCount, int wildcardsCount) : base(cards, cardCount, wildcardsCount)
        {
        }

        protected override int CalculateCombinationValue()
        {
            return base.CardCount * RuleVariables._maxCardValue * 10 + base.HighestCardValue;
        }
    }
}
