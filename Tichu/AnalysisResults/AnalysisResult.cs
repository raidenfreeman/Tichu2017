using System.Collections.Generic;
using System.Linq;
using Tichu.CardDataNamespace;

namespace Tichu.AnalysisResults
{
    /// <summary>
    /// The abstract result of analysis describing the combination of cards
    /// </summary>
    public abstract class AnalysisResult
    {

        //Warning: this class works only with 1 wildcard in each combination.

        public List<CardData> OrderedCards { get; protected set; }
        public readonly int CardCount;
        public int CombinationValue { get; protected set; }
        public int HighestCardValue { get; protected set; }
        public int LowestCardValue { get; protected set; }
        public int WildcardsCount { get; protected set; }

        /// <summary>
        /// Constructs a response of analysis
        /// </summary>
        /// <param name="cards">this class works only with 1 wildcard in each combination.</param>
        /// <param name="combination"></param>
        public AnalysisResult(List<CardData> cards, int cardCount, int wildcardsCount)
        {
            CardCount = cardCount;
            WildcardsCount = wildcardsCount;
            OrderedCards = InitializeOrderedCards(cards);
            CombinationValue = CalculateCombinationValue();
            HighestCardValue = CalculateHighestCardValue();
            LowestCardValue = CalculateLowestCardValue();
        }

        protected virtual List<CardData> InitializeOrderedCards(List<CardData> cards)
        {
            return cards.CopyList();
        }

        protected virtual int CalculateCombinationValue()
        {
            return CalculateHighestCardValue();
        }

        protected virtual int CalculateLowestCardValue()
        {
            return OrderedCards.First().NumericalValue;
        }

        protected virtual int CalculateHighestCardValue()
        {
            return OrderedCards.Last().NumericalValue;
        }

    }

}
