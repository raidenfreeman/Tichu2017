using System.Collections.Generic;
using System.Linq;
using Tichu.CardDataNamespace;

namespace Tichu.AnalysisResults
{
    /// <summary>
    /// Represents 5 or more cards with incremental values
    /// </summary>
    public class Straight : TichuTrick
    {
        //public List<CardData> OrderedStraightCards;

        internal Straight(List<CardData> cards, int cardCount, int wildcardsCount) : base(cards, cardCount, wildcardsCount)
        {

        }

        protected override List<CardData> OrderCards(List<CardData> cards, int wildcardCount)
        {
            if (wildcardCount == 0)
            {
                return base.OrderCards(cards, wildcardCount);
            }
            else
            {
                //remove the wildcard and order the cards
                var orderedCardsWithoutWildCard = cards.Where(x => !x.IsWildcard).OrderBy(x => x.NumericalValue).ToList();
                int totalCardsLeft = orderedCardsWithoutWildCard.Count;
                int startingValue = orderedCardsWithoutWildCard.First().NumericalValue;
                //take all the continuously incremental by 1 cards and count them
                int continousCards = orderedCardsWithoutWildCard.TakeWhile((x, next) => x.NumericalValue == next + startingValue).Count();

                //if all the cards are one more than their previous
                if (continousCards == totalCardsLeft)
                {
                    //All cards given, are continous
                    if (orderedCardsWithoutWildCard.Last().NumericalValue != RuleVariables.AceMaxValue) //if the last card is not an ace
                    {
                        //add the phoenix at the end
                        orderedCardsWithoutWildCard.Add(new CardFactory().CreatePhoenix());
                    }
                    else
                    {
                        //insert the phoenix at the head/start of the list
                        orderedCardsWithoutWildCard.Insert(0, new CardFactory().CreatePhoenix());
                    }
                }
                else //there is a gap between the cards, filled by the wildcard
                {
                    //insert the wildcard, at the gap
                    orderedCardsWithoutWildCard.Insert(continousCards - 1, new CardFactory().CreatePhoenix());
                }
                return orderedCardsWithoutWildCard;
            }
        }

        protected override int CalculateHighestCardValue()
        {
            var lastCard = OrderedCards.Last();

            if (lastCard.IsWildcard)
            {
                //if the last card is the phoenix, return one more than the card before the phoenix
                return OrderedCards[CardCount - 2].NumericalValue + 1; //return the penultimate's value +1
            }
            else
            {
                //otherwise just treat it normally
                return base.CalculateHighestCardValue();
            }
        }
    }
}
