using System.Collections.Generic;
using System.Linq;

namespace Tichu.AnalysisResults
{
    /// <summary>
    /// Represents 3 cards of the same value with 2 cards of the same value
    /// </summary>
    public class FullHouse : AnalysisResult
    {
        public readonly int TripleValue;
        public readonly int PairValue;
        public FullHouse(List<CardData> cards, int cardCount, int wildcardsCount) : base(cards, cardCount, wildcardsCount)
        {

        }

        /*public FullHouse(List<CardData> cards, int cardCount, int wildcardsCount, FullHouse previousTrick) : base(cards, cardCount, wildcardsCount)
        {
            TripleValue = previousTrick.TripleValue;
        }*/

        protected override List<CardData> InitializeOrderedCards(List<CardData> cards)
        {
            if (base.WildcardsCount == 0)
                return base.InitializeOrderedCards(cards);

            var groups = cards.GroupBy(x => x.NumericalValue).ToList();
            if (groups.Where(x => x.Count() == 2) != null)
            {
                //Debug.Log("2s");
                //HighestCardValue cards.Last()
            }
            return base.InitializeOrderedCards(cards);
        }

        //protected override int CalculateCombinationValue()
        //{
        /*
            int tripleValue, pairValue;
            var groups = OrderedCards.GroupBy(x => x.NumericalValue).ToList();
            if (groups.Count == 2)
            {
                tripleValue = groups.Where(x => x.Count() == 3).First().First().NumericalValue;
                pairValue = groups.Where(x => x.Count() == 2).First().First().NumericalValue;
            }
            else
            {
                //if it contains a wildcard, we have 3 groups
                var subgroups = groups.Where(x => x.Count() == 2);
                //we make 2 subgroups without the wildcard
                int pair1 = subgroups.First().First().NumericalValue;
                int pair2 = subgroups.ElementAt(1).First().NumericalValue;
                if (pair1 > pair2)
                {
                    tripleValue = pair1;
                    pairValue = pair2;
                }
                else
                {
                    tripleValue = pair2;
                    pairValue = pair1;
                }
            }
            return tripleValue * 10 * RuleVariables._maxCardValue + pairValue;*/
        //}
    }
}
