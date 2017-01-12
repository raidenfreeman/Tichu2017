using System.Collections.Generic;
using System.Linq;
using Tichu.CardDataNamespace;

namespace Tichu.AnalysisResults
{
    public class FullHousePairsWildcard : TichuTrick
    {
        public readonly int LowPairValue;

        public FullHousePairsWildcard(List<CardData> cards, int cardCount, int wildcardsCount) : base(cards, cardCount, wildcardsCount)
        {
            LowPairValue = cards.Min(x => x.NumericalValue);
        }
    }
}
