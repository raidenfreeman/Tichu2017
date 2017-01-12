using System.Collections.Generic;
using System.Linq;
using Tichu.CardDataNamespace;

namespace Tichu.AnalysisResults
{
    /// <summary>
    /// Represents 3 cards of the same value with 2 cards of the same value
    /// </summary>
    public class FullHouse : TichuTrick
    {
        public readonly int TripleValue;
        public readonly int PairValue;
        public FullHouse(List<CardData> cards, int cardCount, int wildcardsCount) : base(cards, cardCount, wildcardsCount)
        {
            var cardsWithoutWildcards = cards;
            if (wildcardsCount != 0)
            {
                cardsWithoutWildcards = cards.Where(x => !x.IsWildcard).ToList();
            }
            var groups = cardsWithoutWildcards.GroupBy(x => x.NumericalValue).ToList();

            TripleValue = groups.Where(x => x.Count() == 3).First().First().NumericalValue; //the group with 3 items has the Triple value
            PairValue = groups.Where(x => x.Count() != 3).First().First().NumericalValue; //the group with 1 item has the Pair value
        }

        protected override int CalculateTrickValue()
        {
            return TripleValue;
        }
    }
}
