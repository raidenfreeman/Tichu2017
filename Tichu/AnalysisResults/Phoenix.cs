using System.Collections.Generic;
using Tichu.CardDataNamespace;

namespace Tichu.AnalysisResults
{
    public class PhoenixSingleTrick : Single
    {
        public CardData substitutedCard { get; private set; }
        public PhoenixSingleTrick(List<CardData> cards, int cardCount, int wildcardsCount) : base(cards, cardCount, wildcardsCount)
        {

        }

        public void SubstituteCard(int value)
        {
            substitutedCard = new CardFactory().CreatePhoenixSubstitute(value);
        }
    }
}
