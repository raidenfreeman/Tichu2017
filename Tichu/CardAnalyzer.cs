using System.Collections.Generic;
using System.Linq;
using Tichu.AnalysisResults;
using Tichu.CardDataNamespace;

namespace Tichu
{

    //public enum Combinations { None, NPair, Flush, Straight, NOfAKind, FullHouse, StraightFlush }

    //public enum TichuCombinations { None, Single, Pair, NContPair, Triple, FullHouse, Straight, Bomb }

    public class CardAnalyzer
    {

        /// <summary>
        /// Finds which trick is formed by the cards
        /// </summary>
        /// <param name="cards">The list of cards to examine</param>
        /// <returns>The kind of trick along with its value and other info</returns>
        public TichuTrick AnalyzeCards(IEnumerable<CardData> cards)
        {
            int cardCount = cards.Count();

            List<CardData> orderedCards = cards.OrderBy(x => x.NumericalValue).ToList();

            int WildcardCount = cards.Where(x => x.IsWildcard).Count();

            if (cardCount == 0)
                return new None(orderedCards, cardCount, WildcardCount);

            if (IsBomb(cards))
                return new Bomb(orderedCards, cardCount, WildcardCount);

            switch (cardCount)
            {
                case 1:
                    return new Single(orderedCards, cardCount, WildcardCount);
                case 2:
                    return IsPair(cards, true) ? (TichuTrick)new Pair(orderedCards, cardCount, WildcardCount) : new None(orderedCards, cardCount, WildcardCount);
                case 3:
                    return IsThreeOfAKind(cards, true) ? (TichuTrick)new Triple(orderedCards, cardCount, WildcardCount) : new None(orderedCards, cardCount, WildcardCount);
                case 5:
                    if (IsFullHouse(cards, true))
                        return new FullHouse(orderedCards, cardCount, WildcardCount);
                    if (IsStraight(cards, 5, 14, true, false, true))
                        return new Straight(orderedCards, cardCount, WildcardCount);
                    return new None(orderedCards, cardCount, WildcardCount);
            }
            if (cardCount >= 5 && IsStraight(cards, cardCount, 14, true, false, true))
                return new Straight(orderedCards, cardCount, WildcardCount);
            if (IsNContinousPair(cards, (int)(cardCount * 0.5f), true))
                return new NContPair(orderedCards, cardCount, WildcardCount);
            return new None(orderedCards, cardCount, WildcardCount);
        }

        bool IsBomb(IEnumerable<CardData> cards)
        {
            //bombs can't use wildcards
            return (IsFourOfAKind(cards) || IsStraightFlush(cards, 5, 14));
        }

        bool IsSingle(IEnumerable<CardData> cards)
        {
            return cards.Count() == 1;
        }

        bool IsPair(IEnumerable<CardData> cards, bool useWildcards = false, bool wildcardsCopySpecial = false)
        {
            if (cards.Count() == 2) //if you have 2 cards
            {
                //if they have the same value they're a pair
                if (cards.First().NumericalValue == cards.Last().NumericalValue)
                    return true;

                //if you have any wildcards and they are allowed
                if (useWildcards && cards.Count(x => x.IsWildcard == true) != 0)
                {
                    if (wildcardsCopySpecial) //if they can copy any card, they always create pairs
                        return true;
                    else //if you have NO Special cards, that are not wildcards
                        if (cards.Where(x => x.IsWildcard == false).Count(x => x.Suit == CardSuit.Special) == 0)
                        return true;
                }
            }
            //if you got here, there's more/less cards or they don't match
            return false;
        }


        /// <summary>
        /// Recognizes a N amount of pairs
        /// </summary>
        bool IsNPair(IEnumerable<CardData> cards, int N, bool useWildcards = false)
        {
            //If the cards are not of 2 times N, they can't be N-pair
            if (cards.Count() != N * 2)
                return false;

            int wildCardsCount = 0;

            List<CardData> sortedCards;

            if (useWildcards)
            {
                //count the wildcards
                wildCardsCount = NumberOfWildcards(cards);
                //remove the wildcards and sort the cards
                sortedCards = cards.Where(x => x.IsWildcard != true).OrderBy(x => x.NumericalValue).ToList();
            }
            else
                sortedCards = cards.OrderBy(x => x.NumericalValue).ToList();

            //if you have any special cards, they can't be part of pairs
            if (sortedCards.Count(x => x.Suit == CardSuit.Special) != 0)
                return false;

            int i = 0;
            int maxIterations = sortedCards.Count - 1; //We iterate over pairs, so the max iterations are 1 less than the cards that we've got
            List<CardData> cardPair = new List<CardData>(2);
            while (i < maxIterations)
            {
                cardPair.Add(sortedCards.ElementAt(i));
                cardPair.Add(sortedCards.ElementAt(i + 1));
                if (IsPair(cardPair))
                    i += 2;
                else
                {
                    if (useWildcards && wildCardsCount > 0)
                    {
                        wildCardsCount--;
                        i++;
                    }
                    else
                        return false;
                }
                cardPair.Clear();
            }
            return true;
        }


        /// <summary>
        /// Returns true for continous pairs like 9,9,10,10,J,J
        /// </summary>
        bool IsNContinousPair(IEnumerable<CardData> cards, int N, bool useWildcards = false)
        {
            //If the cards are not of 2 times N, they can't be N-pair
            if (cards.Count() != N * 2)
                return false;

            int wildCardsCount = 0;

            List<CardData> sortedCards;

            if (useWildcards)
            {
                wildCardsCount = NumberOfWildcards(cards);
                sortedCards = cards.Where(x => x.IsWildcard != true).OrderBy(x => x.NumericalValue).ToList();
            }
            else
            {
                sortedCards = cards.OrderBy(x => x.NumericalValue).ToList();
            }

            //CONTINUITY CHECK
            var sortedCardsGroups = sortedCards.GroupBy(x => x.NumericalValue).ToList();
            int firstCardValue = sortedCardsGroups.First().First().NumericalValue;
            int currentGroupNumericalOffset = 0;

            foreach (var k in sortedCardsGroups)
            {
                if (useWildcards && k.Count() > 2 || !useWildcards && k.Count() != 2)
                {
                    return false;
                }
                if (k.First().NumericalValue != firstCardValue + currentGroupNumericalOffset)
                {
                    return false;
                }
                currentGroupNumericalOffset++;
            }
            //END CONT CHECK

            //if you have any special cards, they can't be part of pairs
            if (sortedCards.Count(x => x.Suit == CardSuit.Special) != 0)
            {
                return false;
            }

            int i = 0;
            int maxIterations = sortedCards.Count - 1; //We iterate over pairs, so the max iterations are 1 less than the cards that we've got
            List<CardData> cardPair = new List<CardData>(2);
            while (i < maxIterations)
            {
                cardPair.Add(sortedCards.ElementAt(i));
                cardPair.Add(sortedCards.ElementAt(i + 1));
                if (IsPair(cardPair))
                {
                    i += 2;
                }
                else
                {
                    if (useWildcards && wildCardsCount > 0)
                    {
                        wildCardsCount--;
                        i++;
                    }
                    else
                    {
                        return false;
                    }
                }
                cardPair.Clear();
            }
            return true;
        }

        bool IsThreeOfAKind(IEnumerable<CardData> cards, bool useWildcards = false)
        {
            return IsNOfAKind(3, cards, useWildcards);
        }
        bool IsFourOfAKind(IEnumerable<CardData> cards, bool useWildcards = false)
        {
            return IsNOfAKind(4, cards, useWildcards);
        }


        bool IsNOfAKind(int N, IEnumerable<CardData> cards, bool useWildcards = false)
        {
            if (cards.Count() == N)
            {
                List<CardData> sortedCards = cards.OrderByDescending(x => x.NumericalValue).ToList();

                if (useWildcards)
                    sortedCards = sortedCards.Where(x => x.IsWildcard == false).ToList();

                int firstCardValue = sortedCards.First().NumericalValue;

                foreach (CardData c in sortedCards)
                {
                    if (c.NumericalValue != firstCardValue)
                        return false;
                }
                return true;
            }
            else
                return false;
        }

        bool IsStraight(IEnumerable<CardData> cards, int minCardNumber = 5, int maxCardNumber = 5, bool useWildcards = false, bool aceLowValid = true, bool aceHighValid = false)
        {
            //prevent invalid arguments
            if (maxCardNumber < minCardNumber)
                return false;
            //throw new System.ArgumentException("Maximum cards cannot be less than minimum cards", "maxCardNumber");

            int totalCards = cards.Count();

            //if you have less or more cards than needed for a straight, ignore the hand
            if (totalCards < minCardNumber || totalCards > maxCardNumber)
                return false;

            int specialCardsCount = NumberOfSpecialCards(cards.ToList());
            int wildCardsCount = NumberOfWildcards(cards.ToList());

            bool containsMajong = cards.Any(x => x.ID == RuleVariables._mahjongID);

            if (containsMajong && specialCardsCount > 1)
            {
                //if there is any special card that is not a wildcard, return false.
                if (useWildcards && wildCardsCount != specialCardsCount - 1)
                    return false;
            }

            if (containsMajong)
            {
                if (totalCards > 14)
                    return false;
            }
            else
            if (totalCards > 13)
                return false;

            //if you only have one card, it counts as a straight
            if (totalCards < 2)
                return false;

            //sort the hand in ascending order of numerical values
            List<CardData> sortedCards = cards.OrderBy(x => x.NumericalValue).ToList();

            //if there are wildcards, remove them from the list
            if (useWildcards)
            {
                sortedCards = sortedCards.Where(x => !x.IsWildcard).ToList();
                totalCards -= wildCardsCount;
            }
            /*
            if (aceHighValid && sortedCards.First().NumericalValue == 1)
            {
                CardData aceData = sortedCards.First();

                List<CardData> aceHighList;
                //aceLowList = sortedCards;

                aceHighList = sortedCards.CopyList();

                aceHighList.RemoveAt(0);

                CardData aceHigh = new CardData(aceData.ID, RuleVariables.AceMaxValue, aceData.Suit, aceData.IsWildcard);
                aceHighList.Add(aceHigh);

                //check if the high ace hand is a straight
                if (!StraightEvaluationLoop(aceHighList, useWildcards, wildCardsCount))
                {
                    if (!aceLowValid)
                        return false; //!!!!!!!!!!! metraei to majong os asso, kai paei edo!!!!!!!!!!!!!!!!!!!!!!!!!
                }
                else
                    return true;
            }*/
            //if you can count ace as 1
            if (!StraightEvaluationLoop(sortedCards, useWildcards, wildCardsCount))
                return false;

            return true;
        }


        /// <summary>
        /// Returns true if <paramref name="sortedCards"/> is a straight.
        /// </summary>
        /// <param name="useWildcards">Whether to use wildcards or not</param>
        /// <param name="wildCardsCount">If using wildcards, the amount of wildcards to consider</param>
        /// <returns></returns>
        bool StraightEvaluationLoop(IEnumerable<CardData> sortedCards, bool useWildcards, int wildCardsCount)
        {

            int totalCards = sortedCards.Count();

            CardData previousCard = sortedCards.First();

            if (totalCards > 2)
            {
                for (int i = 1; i < totalCards; i++)
                {
                    CardData currentCard = sortedCards.ElementAt(i);

                    int cardDistance = currentCard.NumericalValue - previousCard.NumericalValue;

                    //given that the cards are sorted, their distance must always be at least 1. If it's 0, they're the same card, so it's not a straight.
                    if (cardDistance <= 0)
                        return false;

                    if (useWildcards)
                    {
                        while (wildCardsCount > 0 && cardDistance != 1)
                        {
                            cardDistance--;
                            wildCardsCount--;
                        }
                    }

                    if (cardDistance != 1)
                        return false;

                    previousCard = currentCard;

                }
            }
            return true;
        }

        bool IsFlush(IEnumerable<CardData> cards, int minCardNumber = 5, int maxCardNumber = 5, bool useWildcards = false)
        {
            if (maxCardNumber < minCardNumber)
                throw new System.ArgumentException("Maximum cards cannot be less than minimum cards", "maxCardNumber");
            if (cards.Count() < minCardNumber || cards.Count() > maxCardNumber)
                return false;

            int specialCardsCount = cards.Where(x => x.Suit == CardSuit.Special).Count();

            if (specialCardsCount != 0)
            {
                return false;/*
            if (useWildcards && (cards.Where(x => (x.IsWildcard == false) && (x.Suit == CardSuit.Special)).Count() != 0))
                return false;
            else
                return false;*/
            }

            List<CardData> cardsWithoutWildcards = cards.Where(x => x.IsWildcard == false).ToList();

            CardSuit firstSuit = cardsWithoutWildcards.First().Suit;

            foreach (CardData c in cardsWithoutWildcards)
            {
                if (firstSuit != c.Suit)
                    return false;
            }
            return true;
        }

        bool IsStraightFlush(IEnumerable<CardData> cards, int minCardNumber = 5, int maxCardNumber = 5, bool useWildcards = false)
        {
            if (maxCardNumber < minCardNumber)
                throw new System.ArgumentException("Maximum cards cannot be less than minimum cards", "maxCardNumber");
            if (IsFlush(cards, minCardNumber, maxCardNumber, useWildcards) && IsStraight(cards, minCardNumber, maxCardNumber, useWildcards))
                return true;
            else
                return false;
        }

        bool IsFullHouse(IEnumerable<CardData> cards, bool useWildcards = false)
        {
            if (cards.Count() != 5)
                return false;

            List<CardData> cardsWithoutWildcards;

            //if wildcards are allowed, remove them
            if (useWildcards)
                cardsWithoutWildcards = cards.Where(x => x.IsWildcard != true).ToList();
            else
                cardsWithoutWildcards = cards.ToList();

            //split the cards into groups based on their value
            var valueGroupsCount = cardsWithoutWildcards.GroupBy(x => x.NumericalValue).ToList().Count();
            if (valueGroupsCount != 2)
                return false;

            return true;
        }

        int NumberOfWildcards(IEnumerable<CardData> cards)
        {
            return cards.Count(x => x.IsWildcard);
        }

        int NumberOfSpecialCards(IEnumerable<CardData> cards)
        {
            return cards.Count(x => x.Suit == CardSuit.Special);
        }
    }
}