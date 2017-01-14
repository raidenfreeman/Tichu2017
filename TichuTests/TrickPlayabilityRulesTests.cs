using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Tichu.AnalysisResults;
using Tichu.CardDataNamespace;

namespace Tichu.Tests
{
    [TestClass()]
    public class TrickPlayabilityRulesTests
    {
        CardFactory cf = new CardFactory();
        TrickPlayabilityRules tpr = new TrickPlayabilityRules();
        CardAnalyzer ca = new CardAnalyzer();
        List<CardData> oldList = new List<CardData>();
        List<CardData> currentList = new List<CardData>();

        public BombFourOfAKind CreateBombFourOfAKind(int value)
        {
            var list = new List<CardData>
            {
                cf.CreateCard(value, CardSuit.Spades),
                cf.CreateCard(value, CardSuit.Hearts),
                cf.CreateCard(value, CardSuit.Diamonds),
                cf.CreateCard(value, CardSuit.Clubs)
            };
            return (BombFourOfAKind)ca.AnalyzeCards(list);
        }

        public BombStraight CreateBombStraight()
        {
            var list = new List<CardData>
            {
                cf.CreateCard(4, CardSuit.Hearts),
                cf.CreateCard(6, CardSuit.Hearts),
                cf.CreateCard(5, CardSuit.Hearts),
                cf.CreateCard(3, CardSuit.Hearts),
                cf.CreateCard(2, CardSuit.Hearts)
            };
            return (BombStraight)ca.AnalyzeCards(list);
        }

        public BombStraight CreateLongerBombStraight()
        {
            var list = new List<CardData>
            {
                cf.CreateCard(4, CardSuit.Hearts),
                cf.CreateCard(6, CardSuit.Hearts),
                cf.CreateCard(5, CardSuit.Hearts),
                cf.CreateCard(3, CardSuit.Hearts),
                cf.CreateCard(2, CardSuit.Hearts),
                cf.CreateCard(7, CardSuit.Hearts)
            };
            return (BombStraight)ca.AnalyzeCards(list);
        }

        public Single CreateSingle(int value)
        {
            var list = new List<CardData> { cf.CreateCard(value, CardSuit.Hearts) };
            return (Single)ca.AnalyzeCards(list);
        }

        public Single CreateDragon()
        {
            var list = new List<CardData> { cf.CreateDragon() };
            return (Single)ca.AnalyzeCards(list);
        }

        public Single CreateDAWG()
        {
            var list = new List<CardData> { cf.CreateDogs() };
            return (Single)ca.AnalyzeCards(list);
        }
        public Single CreatePhoenix()
        {
            var list = new List<CardData> { cf.CreatePhoenix() };
            return (Single)ca.AnalyzeCards(list);
        }

        [TestMethod()]
        [TestCategory("Bomb")]
        [TestCategory("Single")]
        public void IsTrickPlayable_BombOverSingle_True()
        {
            oldList = new List<CardData>
            {
                cf.CreateCard(3,CardSuit.Diamonds)
            };
            var oldTrick = ca.AnalyzeCards(oldList);
            var newTrick = CreateBombStraight();
            Assert.IsTrue(tpr.IsTrickPlayable(newTrick, oldTrick));
        }

        [TestMethod()]
        [TestCategory("Single")]
        public void IsTrickPlayable_DragonOverSingle_True()
        {
            var dragon = CreateDragon();
            var single = CreateDAWG();
            Assert.IsTrue(tpr.IsTrickPlayable(dragon, single));
        }

        [TestMethod()]
        [TestCategory("Bomb")]
        public void IsTrickPlayable_BombOverDragon_True()
        {
            Assert.IsTrue(tpr.IsTrickPlayable(CreateBombStraight(), CreateDragon()));
        }

        [TestMethod()]
        [TestCategory("Bomb")]
        public void IsTrickPlayable_BombStraightOverBomb4_True()
        {
            Assert.IsTrue(tpr.IsTrickPlayable(CreateBombStraight(), CreateBombFourOfAKind(5)));
        }

        [TestMethod()]
        [TestCategory("Bomb")]
        public void IsTrickPlayable_Bomb4OverLowerBomb4_True()
        {
            Assert.IsTrue(tpr.IsTrickPlayable(CreateBombFourOfAKind(5), CreateBombFourOfAKind(3)));
        }

        [TestMethod()]
        [TestCategory("Bomb")]
        public void IsTrickPlayable_BombStraightOverLowerBombStraight_True()
        {
            currentList = new List<CardData>
            {
                cf.CreateCard(8, CardSuit.Hearts),
                cf.CreateCard(6, CardSuit.Hearts),
                cf.CreateCard(7, CardSuit.Hearts),
                cf.CreateCard(4, CardSuit.Hearts),
                cf.CreateCard(5, CardSuit.Hearts)
            };
            var b = ca.AnalyzeCards(currentList);
            Assert.IsTrue(tpr.IsTrickPlayable(b, CreateBombStraight()));
        }

        [TestMethod()]
        [TestCategory("Bomb")]
        public void IsTrickPlayable_BombStraightOverLongerBombStraight_True()
        {
            Assert.IsTrue(tpr.IsTrickPlayable(CreateLongerBombStraight(), CreateBombStraight()));
        }

        [TestMethod()]
        public void IsTrickPlayable_BombStraightOverHigherBombStraight_False()
        {
            currentList = new List<CardData>
            {
                cf.CreateCard(8, CardSuit.Hearts),
                cf.CreateCard(6, CardSuit.Hearts),
                cf.CreateCard(7, CardSuit.Hearts),
                cf.CreateCard(4, CardSuit.Hearts),
                cf.CreateCard(5, CardSuit.Hearts)
            };
            var b = ca.AnalyzeCards(currentList);
            Assert.IsFalse(tpr.IsTrickPlayable(CreateBombStraight(), b));
        }

        [TestMethod()]
        public void IsTrickPlayable_BombStraightOverLongerBombStraight_False()
        {
            Assert.IsFalse(tpr.IsTrickPlayable(CreateBombStraight(), CreateLongerBombStraight()));
        }

        [TestMethod()]
        public void IsTrickPlayable_Bomb4OverBombStraight_False()
        {
            Assert.IsFalse(tpr.IsTrickPlayable(CreateBombFourOfAKind(13), CreateLongerBombStraight()));
        }

        [TestMethod()]
        public void IsTrickPlayable_Bomb4OverHigherBomb4_False()
        {
            Assert.IsFalse(tpr.IsTrickPlayable(CreateBombFourOfAKind(7), CreateBombFourOfAKind(13)));
        }

        [TestMethod()]
        public void IsTrickPlayable_PairOver3ContPairs_False()
        {
            currentList = new List<CardData>
            {
                cf.CreateCard(8, CardSuit.Hearts),
                cf.CreateCard(8, CardSuit.Clubs),
                cf.CreateCard(7, CardSuit.Diamonds),
                cf.CreateCard(7, CardSuit.Hearts)
            };
            oldList = new List<CardData>
            {
                cf.CreateCard(8, CardSuit.Hearts),
                cf.CreateCard(8, CardSuit.Clubs),
                cf.CreateCard(7, CardSuit.Diamonds),
                cf.CreateCard(7, CardSuit.Hearts),
                cf.CreateCard(5, CardSuit.Hearts),
                cf.CreateCard(5, CardSuit.Clubs),
                cf.CreateCard(6, CardSuit.Hearts),
                cf.CreateCard(6, CardSuit.Spades)
            };
            Assert.IsFalse(tpr.IsTrickPlayable(ca.AnalyzeCards(currentList), ca.AnalyzeCards(oldList)));
        }

        [TestMethod()]
        public void IsTrickPlayable_SingleOverDragon_False()
        {
            Assert.IsFalse(tpr.IsTrickPlayable(CreateSingle(1), CreateDragon()));
        }

        [TestMethod()]
        public void IsTrickPlayable_PhoenixOverDragon_False()
        {
            Assert.IsFalse(tpr.IsTrickPlayable(CreatePhoenix(), CreateDragon()));
        }

        [TestMethod()]
        public void IsTrickPlayable_PhoenixOverSingle_True()
        {
            Assert.IsTrue(tpr.IsTrickPlayable(CreatePhoenix(), CreateSingle(3)));
        }

        [TestMethod()]
        public void IsTrickPlayable_PhoenixOverAce_True()
        {
            Assert.IsTrue(tpr.IsTrickPlayable(CreatePhoenix(), CreateSingle(1)));
        }
        [TestMethod()]
        public void IsTrickPlayable_PhoenixOverDogs_True()
        {
            Assert.IsTrue(tpr.IsTrickPlayable(CreatePhoenix(), CreateDAWG()));
        }

        [TestMethod()]
        public void IsTrickPlayable_DogOverNone_True()
        {
            currentList = new List<CardData>();
            Assert.IsTrue(tpr.IsTrickPlayable(CreateDAWG(), ca.AnalyzeCards(currentList)));
        }

        [TestMethod()]
        public void IsTrickPlayable_DogOverSingle_True()
        {
            Assert.IsFalse(tpr.IsTrickPlayable(CreateDAWG(), CreateSingle(4)));
        }
    }
}