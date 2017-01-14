using System.Collections.Generic;
using System.Linq;
using Tichu.CardDataNamespace;

namespace Tichu.AnalysisResults
{
    /// <summary>
    /// The abstract result of analysis describing the combination of cards
    /// </summary>
    public abstract class TichuTrick
    {

        public List<CardData> OrderedCards { get; private set; }
        public readonly int CardCount;
        public readonly int TrickValue;
        public readonly int HighestCardValue;
        public readonly int WildcardsCount;

        /// <summary>
        /// Constructs a response of analysis
        /// </summary>
        /// <param name="cards">This class works only with 1 wildcard in each trick.</param>
        internal TichuTrick(List<CardData> cards, int cardCount, int wildcardsCount)
        {
            if (wildcardsCount > 1)
            {
                throw new System.ArgumentException("More than one wildcards support, is not yet implemented", nameof(wildcardsCount));
            }
            CardCount = cardCount;
            OrderedCards = OrderCards(cards, wildcardsCount);
            HighestCardValue = CalculateHighestCardValue();
            TrickValue = CalculateTrickValue();
        }

        protected virtual List<CardData> OrderCards(List<CardData> cards, int WildcardCount)
        {
            return cards.OrderBy(x => x.NumericalValue).ToList().CopyList();
        }

        protected virtual int CalculateTrickValue()
        {
            return CalculateHighestCardValue();
        }

        protected virtual int CalculateHighestCardValue()
        {
            return OrderedCards.Last().NumericalValue;
        }

    }

}
