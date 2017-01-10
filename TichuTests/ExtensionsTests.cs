using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Tichu.CardDataNamespace;

namespace Tichu.Tests
{
    [TestClass()]
    public class ExtensionsTests
    {
        [TestMethod()]
        public void ShuffleTest_Count()
        {
            var list = new List<CardData>();
            CardFactory cf = new CardFactory();
            list.Add(cf.CreateCard(1, CardSuit.Clubs));
            list.Add(cf.CreateCard(2, CardSuit.Diamonds));
            list.Add(cf.CreateCard(1, CardSuit.Clubs));
            list.Add(cf.CreateCard(4, CardSuit.Hearts));
            list.Add(cf.CreateCard(5, CardSuit.Spades));

            var newList = list.CopyList();
            newList.Shuffle();

            Assert.AreEqual(list.Count, newList.Count);
        }

        [TestMethod()]
        public void ShuffleTest_Order()
        {
            var originalList = new List<CardData>();
            CardFactory cf = new CardFactory();
            originalList.Add(cf.CreateCard(1, CardSuit.Clubs));
            originalList.Add(cf.CreateCard(2, CardSuit.Diamonds));
            originalList.Add(cf.CreateCard(8, CardSuit.Clubs));
            originalList.Add(cf.CreateCard(4, CardSuit.Hearts));
            originalList.Add(cf.CreateCard(5, CardSuit.Spades));

            var newList = originalList.CopyList();
            newList.Shuffle();

            Assert.AreNotEqual(originalList, newList);
        }
    }
}