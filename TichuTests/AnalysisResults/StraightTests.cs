using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using Tichu.CardDataNamespace;

namespace Tichu.AnalysisResults.Tests
{
    [TestClass()]
    public class StraightTests
    {
        CardAnalyzer ca = new CardAnalyzer();
        CardFactory cf = new CardFactory();
        List<CardData> cards;

        [TestMethod()]
        public void Straight_RegularStraight_TrickValueIsHighestCardValue()
        {
            cards = new List<CardData> {
            cf.CreateCard(3, CardSuit.Hearts),
            cf.CreateCard(2, CardSuit.Spades),
            cf.CreateCard(5, CardSuit.Spades),
            cf.CreateCard(7, CardSuit.Spades),
            cf.CreateCard(6, CardSuit.Spades),
            cf.CreateCard(8, CardSuit.Spades),
            cf.CreateCard(4, CardSuit.Spades) };
            Assert.AreEqual(ca.AnalyzeCards(cards).TrickValue, cards.Max(x => x.NumericalValue));
        }

        [TestMethod()]
        public void Straight_StraightWithAce_TrickValueIsAceValue()
        {
            const int AceNumericalValue = 14;
            cards = new List<CardData> {
            cf.CreateCard(1, CardSuit.Hearts),
            cf.CreateCard(9, CardSuit.Spades),
            cf.CreateCard(11, CardSuit.Spades),
            cf.CreateCard(10, CardSuit.Spades),
            cf.CreateCard(13, CardSuit.Spades),
            cf.CreateCard(8, CardSuit.Spades),
            cf.CreateCard(12, CardSuit.Spades) };
            Assert.AreEqual(ca.AnalyzeCards(cards).TrickValue, AceNumericalValue);
        }

        [TestMethod()]
        public void Straight_StraightWithAceAndPhoenix_TrickValueIsAceValue()
        {
            const int AceNumericalValue = 14;
            cards = new List<CardData> {
            cf.CreateCard(1, CardSuit.Hearts),
            cf.CreateCard(9, CardSuit.Spades),
            cf.CreateCard(11, CardSuit.Spades),
            cf.CreateCard(10, CardSuit.Spades),
            cf.CreateCard(13, CardSuit.Spades),
            cf.CreateCard(8, CardSuit.Spades),
            cf.CreatePhoenix(), //phoenix counts as 7
            cf.CreateCard(12, CardSuit.Spades) };

            Assert.AreEqual(ca.AnalyzeCards(cards).TrickValue, AceNumericalValue);
        }

        [TestMethod()]
        public void Straight_StraightWithPhoenixInTheMiddle_TrickValueIsTheHighestCardValue()
        {
            cards = new List<CardData> {
            cf.CreateCard(9, CardSuit.Spades),
            cf.CreateCard(11, CardSuit.Spades),
            cf.CreateCard(13, CardSuit.Spades),
            cf.CreateCard(8, CardSuit.Spades),
            cf.CreatePhoenix(), //phoenix counts as 10
            cf.CreateCard(12, CardSuit.Spades) };

            Assert.AreEqual(13, cards.Max(x => x.NumericalValue));

            Assert.AreEqual(ca.AnalyzeCards(cards).TrickValue, 13);
        }

        [TestMethod()]
        public void Straight_StraightWithPhoenixUnecessary_TrickValueIsTheHighestCardValuePlusOne()
        {
            cards = new List<CardData> {
            cf.CreateCard(9, CardSuit.Spades),
            cf.CreateCard(10, CardSuit.Spades),
            cf.CreateCard(7, CardSuit.Spades),
            cf.CreateCard(8, CardSuit.Spades),
            cf.CreatePhoenix(), //phoenix counts as 11
            cf.CreateCard(6, CardSuit.Spades) };

            Assert.AreEqual(11, cards.Max(x => x.NumericalValue) + 1);

            Assert.AreEqual(ca.AnalyzeCards(cards).TrickValue, 11);
        }

        [TestMethod()]
        public void Straight_StraightWithMahjong_TrickValueIsTheHighestCardValue()
        {
            cards = new List<CardData> {
            cf.CreateCard(4, CardSuit.Spades),
            cf.CreateCard(2, CardSuit.Spades),
            cf.CreateCard(3, CardSuit.Spades),
            cf.CreateCard(5, CardSuit.Spades),
            cf.CreateMajhong(),
            cf.CreateCard(6, CardSuit.Spades) };

            Assert.AreEqual(6, cards.Max(x => x.NumericalValue));

            Assert.AreEqual(ca.AnalyzeCards(cards).TrickValue, 6);
        }

        [TestMethod()]
        public void Straight_14CardStraightWithMahjong_TrickValueIsTheHighestCardValue()
        {
            cards = new List<CardData>
            {
                cf.CreateCard(9,CardSuit.Spades),
                cf.CreateCard(5,CardSuit.Hearts),
                cf.CreateCard(3,CardSuit.Diamonds),
                cf.CreateCard(7,CardSuit.Hearts),
                cf.CreateCard(4,CardSuit.Clubs),
                cf.CreateCard(6,CardSuit.Clubs),
                cf.CreateCard(2,CardSuit.Hearts),
                cf.CreateMajhong(),
                cf.CreateCard(8,CardSuit.Clubs),
                cf.CreateCard(10,CardSuit.Diamonds),
                cf.CreateCard(13,CardSuit.Hearts),
                cf.CreateCard(12,CardSuit.Clubs),
                cf.CreateCard(1,CardSuit.Clubs),
                cf.CreateCard(11,CardSuit.Hearts)
            };

            Assert.AreEqual(14, cards.Max(x => x.NumericalValue));

            Assert.AreEqual(ca.AnalyzeCards(cards).TrickValue, 14);
        }
    }
}