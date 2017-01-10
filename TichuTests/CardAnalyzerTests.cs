using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Tichu.AnalysisResults;
using Tichu.CardDataNamespace;

namespace Tichu.Tests
{
    [TestClass()]
    public class CardAnalyzerTests
    {
        CardFactory cf = new CardFactory();
        CardAnalyzer ca = new CardAnalyzer();
        List<CardData> cards;

        #region Bomb

        [TestMethod()]
        public void CardAnalyzer_FourOfAKind_RecognizeBomb()
        {
            cards = new List<CardData> {
                cf.CreateCard(5, CardSuit.Spades),
                cf.CreateCard(5, CardSuit.Hearts),
                cf.CreateCard(5, CardSuit.Diamonds),
                cf.CreateCard(5, CardSuit.Clubs)
            };
            Assert.IsInstanceOfType(ca.AnalyzeCards(cards), typeof(Bomb));
        }

        [TestMethod()]
        public void CardAnalyzer_PhoenixFourOfAKind_FailBomb()
        {
            cards = new List<CardData> {
                cf.CreateCard(5, CardSuit.Spades),
                cf.CreateCard(5, CardSuit.Hearts),
                cf.CreateCard(5, CardSuit.Diamonds),
                cf.CreatePhoenix()
            };
            Assert.IsNotInstanceOfType(ca.AnalyzeCards(cards), typeof(Bomb));
        }

        [TestMethod()]
        public void CardAnalyzer_StraightFlushAceTo6_FailBomb()
        {
            //Ace is not 1 in tichu, it's only 14!!
            cards = new List<CardData> {
                cf.CreateCard(1, CardSuit.Clubs),
                cf.CreateCard(4, CardSuit.Clubs),
                cf.CreateCard(6, CardSuit.Clubs),
                cf.CreateCard(5, CardSuit.Clubs),
                cf.CreateCard(3, CardSuit.Clubs),
                cf.CreateCard(2, CardSuit.Clubs)
            };
            Assert.IsNotInstanceOfType(ca.AnalyzeCards(cards), typeof(Bomb));
        }

        [TestMethod()]
        public void CardAnalyzer_StraightFlush2To6_RecognizeBomb()
        {
            cards = new List<CardData> {
                cf.CreateCard(4, CardSuit.Hearts),
                cf.CreateCard(6, CardSuit.Hearts),
                cf.CreateCard(5, CardSuit.Hearts),
                cf.CreateCard(3, CardSuit.Hearts),
                cf.CreateCard(2, CardSuit.Hearts)
            };
            Assert.IsInstanceOfType(ca.AnalyzeCards(cards), typeof(Bomb));
        }

        [TestMethod()]
        public void CardAnalyzer_PhoenixStraightFlush2To6_FailBomb()
        {
            cards = new List<CardData> {
                cf.CreateCard(4, CardSuit.Hearts),
                cf.CreateCard(6, CardSuit.Hearts),
                cf.CreatePhoenix(),
                cf.CreateCard(3, CardSuit.Hearts),
                cf.CreateCard(2, CardSuit.Hearts)
            };
            Assert.IsNotInstanceOfType(ca.AnalyzeCards(cards), typeof(Bomb));
        }

        [TestMethod()]
        public void CardAnalyzer_Straight2To6_FailBomb()
        {
            cards = new List<CardData> {
                cf.CreateCard(4, CardSuit.Hearts),
                cf.CreateCard(7, CardSuit.Diamonds),
                cf.CreateCard(5, CardSuit.Hearts),
                cf.CreateCard(3, CardSuit.Hearts),
                cf.CreateCard(2, CardSuit.Hearts),
                cf.CreateCard(6, CardSuit.Hearts)
            };
            Assert.IsNotInstanceOfType(ca.AnalyzeCards(cards), typeof(Bomb));
        }

        #endregion

        #region Single
        [TestMethod()]
        public void CardAnalyzer_TwoCards_FailSingle()
        {
            cards = new List<CardData> { cf.CreateCard(5, CardSuit.Clubs), cf.CreateDogs() };
            Assert.IsNotInstanceOfType(ca.AnalyzeCards(cards), typeof(Single));
        }

        [TestMethod()]
        public void CardAnalyzer_OneCard_RecognizeSingle()
        {
            cards = new List<CardData> { cf.CreateCard(1, CardSuit.Hearts) };
            Assert.IsInstanceOfType(ca.AnalyzeCards(cards), typeof(Single));
        }
        #endregion

        #region Pair

        [TestMethod()]
        public void CardAnalyzer_Pair_RecognizePair()
        {
            cards = new List<CardData> { cf.CreateCard(1, CardSuit.Diamonds), cf.CreateCard(1, CardSuit.Spades) };
            Assert.IsInstanceOfType(ca.AnalyzeCards(cards), typeof(Pair));
        }

        [TestMethod()]
        public void CardAnalyzer_PhoenixPair_RecognizePair()
        {
            cards = new List<CardData> { cf.CreatePhoenix(), cf.CreateCard(1, CardSuit.Spades) };
            Assert.IsInstanceOfType(ca.AnalyzeCards(cards), typeof(Pair));
        }

        [TestMethod()]
        public void CardAnalyzer_CardAndSpecial_FailPair()
        {
            cards = new List<CardData> { cf.CreateCard(3, CardSuit.Diamonds), cf.CreateMajhong() };
            Assert.IsNotInstanceOfType(ca.AnalyzeCards(cards), typeof(Pair));
        }

        [TestMethod()]
        public void CardAnalyzer_NotPair_FailPair()
        {
            cards = new List<CardData> { cf.CreateCard(3, CardSuit.Diamonds), cf.CreateCard(4, CardSuit.Diamonds) };
            Assert.IsNotInstanceOfType(ca.AnalyzeCards(cards), typeof(Pair));
        }

        [TestMethod()]
        public void CardAnalyzer_FourCards_FailPair()
        {
            cards = new List<CardData> { cf.CreateCard(3, CardSuit.Diamonds), cf.CreatePhoenix(), cf.CreateCard(3, CardSuit.Hearts), cf.CreateCard(3, CardSuit.Spades) };
            Assert.IsNotInstanceOfType(ca.AnalyzeCards(cards), typeof(Pair));
        }
        #endregion

        #region NContinousPair
        [TestMethod()]
        public void CardAnalyzer_3ContinousPair_RecognizeNContinousPair()
        {
            cards = new List<CardData> {
                cf.CreateCard(4, CardSuit.Diamonds),
                cf.CreateCard(3, CardSuit.Diamonds),
                cf.CreateCard(5, CardSuit.Hearts),
                cf.CreateCard(5, CardSuit.Spades),
                cf.CreateCard(3, CardSuit.Hearts),
                cf.CreateCard(4, CardSuit.Spades) };
            Assert.IsInstanceOfType(ca.AnalyzeCards(cards), typeof(NContPair));
        }

        [TestMethod()]
        public void CardAnalyzer_Phoenix2Pair_RecognizeNContinousPair()
        {
            cards = new List<CardData> {
                cf.CreateCard(4, CardSuit.Diamonds),
                cf.CreateCard(3, CardSuit.Diamonds),
                cf.CreatePhoenix(),
                cf.CreateCard(4, CardSuit.Spades) };
            Assert.IsInstanceOfType(ca.AnalyzeCards(cards), typeof(NContPair));
        }

        [TestMethod()]
        public void CardAnalyzer_ThreeOfAKindAndPairContinous_FailNContinousPair()
        {
            cards = new List<CardData> {
                cf.CreateCard(4, CardSuit.Diamonds),
                cf.CreateCard(3, CardSuit.Diamonds),
                cf.CreateCard(3, CardSuit.Hearts),
                cf.CreateCard(3, CardSuit.Clubs),
                cf.CreateCard(4, CardSuit.Spades) };
            Assert.IsNotInstanceOfType(ca.AnalyzeCards(cards), typeof(NContPair));
        }

        [TestMethod()]
        public void CardAnalyzer_TwoPairsNotContinous_FailNContinousPair()
        {
            cards = new List<CardData> {
                cf.CreateCard(7, CardSuit.Diamonds),
                cf.CreateCard(3, CardSuit.Diamonds),
                cf.CreateCard(7, CardSuit.Spades),
                cf.CreateCard(3, CardSuit.Spades) };
            Assert.IsNotInstanceOfType(ca.AnalyzeCards(cards), typeof(NContPair));
        }

        [TestMethod()]
        public void CardAnalyzer_TwoPairsPhoenixNotContinous_FailNContinousPair()
        {
            cards = new List<CardData> {
                cf.CreateCard(4, CardSuit.Diamonds),
                cf.CreateCard(8, CardSuit.Diamonds),
                cf.CreatePhoenix(),
                cf.CreateCard(8, CardSuit.Spades) };
            Assert.IsNotInstanceOfType(ca.AnalyzeCards(cards), typeof(NContPair));
        }
        #endregion

        #region ThreeOfAkind
        [TestMethod()]
        public void CardAnalyzer_ThreeOfAKind_RecognizeThreeOfAKind()
        {
            cards = new List<CardData> {
                cf.CreateCard(5, CardSuit.Hearts),
                cf.CreateCard(5, CardSuit.Spades),
                cf.CreateCard(5, CardSuit.Clubs)
            };
            Assert.IsInstanceOfType(ca.AnalyzeCards(cards), typeof(Triple));
        }

        [TestMethod()]
        public void CardAnalyzer_ThreeOfAKindWithPhoenix_RecognizeThreeOfAKind()
        {
            cards = new List<CardData> {
                cf.CreateCard(5, CardSuit.Hearts),
                cf.CreateCard(5, CardSuit.Spades),
                cf.CreatePhoenix()
            };
            Assert.IsInstanceOfType(ca.AnalyzeCards(cards), typeof(Triple));
        }

        [TestMethod()]
        public void CardAnalyzer_ThreeOfAKindPlusPhoenix_FailThreeOfAKind()
        {
            cards = new List<CardData> {
                cf.CreateCard(5, CardSuit.Hearts),
                cf.CreateCard(5, CardSuit.Spades),
                cf.CreateCard(5, CardSuit.Clubs),
                cf.CreatePhoenix()
            };
            Assert.IsNotInstanceOfType(ca.AnalyzeCards(cards), typeof(Triple));
        }

        [TestMethod()]
        public void CardAnalyzer_FourCards_FailThreeOfAKind()
        {
            cards = new List<CardData> {
                cf.CreateCard(5, CardSuit.Hearts),
                cf.CreateCard(5, CardSuit.Spades),
                cf.CreateCard(5, CardSuit.Clubs),
                cf.CreateCard(5, CardSuit.Diamonds)
            };
            Assert.IsNotInstanceOfType(ca.AnalyzeCards(cards), typeof(Triple));
        }
        #endregion

        #region Straight
        [TestMethod()]
        public void CardAnalyzer_5CardStraight_RecognizeStraight()
        {
            cards = new List<CardData> {
                cf.CreateCard(9,CardSuit.Diamonds),
                cf.CreateCard(7,CardSuit.Spades),
                cf.CreateCard(11,CardSuit.Clubs),
                cf.CreateCard(10,CardSuit.Diamonds),
                cf.CreateCard(8,CardSuit.Hearts)
            };
            Assert.IsInstanceOfType(ca.AnalyzeCards(cards), typeof(Straight));
        }

        [TestMethod()]
        public void CardAnalyzer_10CardStraightWithMahjong_RecognizeStraight()
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
                cf.CreateCard(10,CardSuit.Diamonds)
            };
            Assert.IsInstanceOfType(ca.AnalyzeCards(cards), typeof(Straight));
        }

        [TestMethod()]
        public void CardAnalyzer_14CardStraightWithMahjong_RecognizeStraight()
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
            Assert.IsInstanceOfType(ca.AnalyzeCards(cards), typeof(Straight));
        }

        [TestMethod()]
        public void CardAnalyzer_15CardStraightWithPhoenixAndMahjong_FailStraight()
        {
            //You can't make a 15 with the phoenix
            cards = new List<CardData>
            {
                cf.CreateCard(9,CardSuit.Spades),
                cf.CreateCard(5,CardSuit.Hearts),
                cf.CreatePhoenix(),
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
            Assert.IsNotInstanceOfType(ca.AnalyzeCards(cards), typeof(Straight));
        }

        [TestMethod()]
        public void CardAnalyzer_6CardStraightwithPhoenixInTheMiddle_RecognizeStraight()
        {
            cards = new List<CardData>
            {
                cf.CreateCard(8,CardSuit.Spades),
                cf.CreateCard(5,CardSuit.Hearts),
                cf.CreatePhoenix(),
                cf.CreateCard(3,CardSuit.Diamonds),
                cf.CreateCard(7,CardSuit.Hearts),
                cf.CreateCard(4,CardSuit.Clubs)
            };
            Assert.IsInstanceOfType(ca.AnalyzeCards(cards), typeof(Straight));
        }

        [TestMethod()]
        public void CardAnalyzer_6CardStraightwithPhoenixInTheEnd_RecognizeStraight()
        {
            cards = new List<CardData>
            {
                cf.CreateCard(6,CardSuit.Spades),
                cf.CreateCard(5,CardSuit.Hearts),
                cf.CreatePhoenix(),
                cf.CreateCard(3,CardSuit.Diamonds),
                cf.CreateCard(7,CardSuit.Hearts),
                cf.CreateCard(4,CardSuit.Clubs)
            };
            Assert.IsInstanceOfType(ca.AnalyzeCards(cards), typeof(Straight));
        }

        [TestMethod()]
        public void CardAnalyzer_Bomb_FailStraight()
        {
            cards = new List<CardData> {
                cf.CreateCard(9,CardSuit.Diamonds),
                cf.CreateCard(11,CardSuit.Diamonds),
                cf.CreateCard(13,CardSuit.Diamonds),
                cf.CreateCard(12,CardSuit.Diamonds),
                cf.CreateCard(10,CardSuit.Diamonds)
            };
            Assert.IsNotInstanceOfType(ca.AnalyzeCards(cards), typeof(Straight));
        }

        [TestMethod()]
        public void CardAnalyzer_4CardStraight_FailStraight()
        {
            cards = new List<CardData>
            {
                cf.CreateCard(5,CardSuit.Hearts),
                cf.CreateCard(3,CardSuit.Diamonds),
                cf.CreateCard(6,CardSuit.Hearts),
                cf.CreateCard(4,CardSuit.Clubs)
            };
            Assert.IsNotInstanceOfType(ca.AnalyzeCards(cards), typeof(Straight));
        }

        [TestMethod()]
        public void CardAnalyzer_NotStraight_FailStraight()
        {
            cards = new List<CardData>
            {
                cf.CreateCard(12,CardSuit.Hearts),
                cf.CreateCard(3,CardSuit.Diamonds),
                cf.CreateCard(5,CardSuit.Hearts),
                cf.CreateCard(4,CardSuit.Clubs),
                cf.CreateCard(2,CardSuit.Clubs)
            };
            Assert.IsNotInstanceOfType(ca.AnalyzeCards(cards), typeof(Straight));
        }

        [TestMethod()]
        public void CardAnalyzer_NotStraightWithPhoenix_FailStraight()
        {
            cards = new List<CardData>
            {
                cf.CreateCard(12,CardSuit.Hearts),
                cf.CreatePhoenix(),
                cf.CreateCard(3,CardSuit.Diamonds),
                cf.CreateCard(5,CardSuit.Hearts),
                cf.CreateCard(4,CardSuit.Clubs),
                cf.CreateCard(2,CardSuit.Clubs)
            };
            Assert.IsNotInstanceOfType(ca.AnalyzeCards(cards), typeof(Straight));
        }
        #endregion


        #region FullHouse
        [TestMethod()]
        public void CardAnalyzer_ThreeOfAKindAndPair_RecognizeFullHouse()
        {
            cards = new List<CardData> {
                cf.CreateCard(4, CardSuit.Diamonds),
                cf.CreateCard(3, CardSuit.Diamonds),
                cf.CreateCard(3, CardSuit.Hearts),
                cf.CreateCard(3, CardSuit.Clubs),
                cf.CreateCard(4, CardSuit.Spades) };
            Assert.IsInstanceOfType(ca.AnalyzeCards(cards), typeof(FullHouse));
        }

        [TestMethod()]
        public void CardAnalyzer_PhoenixPairAndPair_RecognizeFullHouse()
        {
            cards = new List<CardData> {
                cf.CreatePhoenix(),
                cf.CreateCard(3, CardSuit.Diamonds),
                cf.CreateCard(4, CardSuit.Hearts),
                cf.CreateCard(3, CardSuit.Clubs),
                cf.CreateCard(4, CardSuit.Spades) };
            Assert.IsInstanceOfType(ca.AnalyzeCards(cards), typeof(FullHouse));
        }

        [TestMethod()]
        public void CardAnalyzer_PhoenixTripleAndSingle_RecognizeFullHouse()
        {
            cards = new List<CardData> {
                cf.CreatePhoenix(),
                cf.CreateCard(3, CardSuit.Diamonds),
                cf.CreateCard(3, CardSuit.Hearts),
                cf.CreateCard(3, CardSuit.Clubs),
                cf.CreateCard(4, CardSuit.Spades) };
            Assert.IsInstanceOfType(ca.AnalyzeCards(cards), typeof(FullHouse));
        }

        [TestMethod()]
        public void CardAnalyzer_TwoTriples_FailFullHouse()
        {
            cards = new List<CardData> {
                cf.CreateCard(4, CardSuit.Clubs),
                cf.CreateCard(3, CardSuit.Diamonds),
                cf.CreateCard(3, CardSuit.Hearts),
                cf.CreateCard(3, CardSuit.Clubs),
                cf.CreateCard(4, CardSuit.Spades),
                cf.CreateCard(4, CardSuit.Hearts) };
            Assert.IsNotInstanceOfType(ca.AnalyzeCards(cards), typeof(FullHouse));
        }

        [TestMethod()]
        public void CardAnalyzer_FourCards_FailFullHouse()
        {
            cards = new List<CardData> {
                cf.CreateCard(4, CardSuit.Clubs),
                cf.CreateCard(3, CardSuit.Diamonds),
                cf.CreateCard(3, CardSuit.Clubs),
                cf.CreateCard(4, CardSuit.Spades) };
            Assert.IsNotInstanceOfType(ca.AnalyzeCards(cards), typeof(FullHouse));
        }
        #endregion
    }
}