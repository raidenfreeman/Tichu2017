using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Tichu.AnalysisResults;
using Tichu.CardDataNamespace;

namespace Tichu.Tests
{
    [TestClass()]
    public class CardAnalyzerTests
    {
        #region Bomb

        [TestMethod()]
        public void CardAnalyzer_FourOfAKind_RecognizeBomb()
        {
            CardFactory cf = new CardFactory();
            List<CardData> cards = new List<CardData> {
                cf.CreateCard(5, CardSuit.Spades),
                cf.CreateCard(5, CardSuit.Hearts),
                cf.CreateCard(5, CardSuit.Diamonds),
                cf.CreateCard(5, CardSuit.Clubs)
            };
            CardAnalyzer CA = new CardAnalyzer();
            Assert.IsInstanceOfType(CA.AnalyzeCards(cards), typeof(Bomb));
        }

        [TestMethod()]
        public void CardAnalyzer_StraightFlushAceTo6_FailBomb()
        {
            //Ace is not 1 in tichu, it's only 14!!
            CardFactory cf = new CardFactory();
            List<CardData> cards = new List<CardData> {
                cf.CreateCard(1, CardSuit.Clubs),
                cf.CreateCard(4, CardSuit.Clubs),
                cf.CreateCard(6, CardSuit.Clubs),
                cf.CreateCard(5, CardSuit.Clubs),
                cf.CreateCard(3, CardSuit.Clubs),
                cf.CreateCard(2, CardSuit.Clubs)
            };
            CardAnalyzer CA = new CardAnalyzer();
            Assert.IsNotInstanceOfType(CA.AnalyzeCards(cards), typeof(Bomb));
        }

        [TestMethod()]
        public void CardAnalyzer_StraightFlush2To6_RecognizeBomb()
        {
            CardFactory cf = new CardFactory();
            List<CardData> cards = new List<CardData> {
                cf.CreateCard(4, CardSuit.Hearts),
                cf.CreateCard(6, CardSuit.Hearts),
                cf.CreateCard(5, CardSuit.Hearts),
                cf.CreateCard(3, CardSuit.Hearts),
                cf.CreateCard(2, CardSuit.Hearts)
            };
            CardAnalyzer CA = new CardAnalyzer();
            Assert.IsInstanceOfType(CA.AnalyzeCards(cards), typeof(Bomb));
        }

        [TestMethod()]
        public void CardAnalyzer_Straight2To6_FailBomb()
        {
            CardFactory cf = new CardFactory();
            List<CardData> cards = new List<CardData> {
                cf.CreateCard(4, CardSuit.Hearts),
                cf.CreateCard(7, CardSuit.Diamonds),
                cf.CreateCard(5, CardSuit.Hearts),
                cf.CreateCard(3, CardSuit.Hearts),
                cf.CreateCard(2, CardSuit.Hearts),
                cf.CreateCard(6, CardSuit.Hearts)
            };
            CardAnalyzer CA = new CardAnalyzer();
            Assert.IsNotInstanceOfType(CA.AnalyzeCards(cards), typeof(Bomb));
        }

        #endregion

        #region Single
        [TestMethod()]
        public void RecognizeSingle()
        {
            Assert.Fail();
        }
        #endregion

        #region Pair

        [TestMethod()]
        public void RecognizePair()
        {
            Assert.Fail();
        }
        #endregion

        #region NPair
        [TestMethod()]
        public void RecognizeNPair()
        {
            Assert.Fail();
        }
        #endregion

        #region NContinousPair
        [TestMethod()]
        public void RecognizeNContinousPair()
        {
            Assert.Fail();
        }
        #endregion

        #region ThreeOfAkind
        [TestMethod()]
        public void RecognizeThreeOfAKind()
        {
            Assert.Fail();
        }
        #endregion

        #region FourOfAKind
        [TestMethod()]
        public void RecognizeFourOfAKind()
        {
            Assert.Fail();
        }
        #endregion

        #region NOfAKind
        [TestMethod()]
        public void RecognizeNOfAKind()
        {
            Assert.Fail();
        }
        #endregion

        #region Straight
        [TestMethod()]
        public void RecognizeStraight()
        {
            Assert.Fail();
        }
        #endregion

        #region Flush
        [TestMethod()]
        public void RecognizeFlush()
        {
            Assert.Fail();
        }
        #endregion

        #region StraightFlush
        [TestMethod()]
        public void RecognizeStraightFlush()
        {
            Assert.Fail();
        }
        #endregion

        #region FullHouse
        [TestMethod()]
        public void RecognizeFullHouse()
        {
            Assert.Fail();
        }
        #endregion
    }
}