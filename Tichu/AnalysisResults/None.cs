using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tichu.AnalysisResults
{
    /// <summary>
    /// Represents no valid combination
    /// </summary>
    public class None : AnalysisResult
    {
        public None(List<CardData> cards, int cardCount, int wildcardsCount) : base(cards, cardCount, wildcardsCount)
        {

        }

        protected override int CalculateCombinationValue()
        {
            return 0;
        }

        protected override int CalculateHighestCardValue()
        {
            return 0;
        }

        protected override int CalculateLowestCardValue()
        {
            return 0;
        }

    }
}
