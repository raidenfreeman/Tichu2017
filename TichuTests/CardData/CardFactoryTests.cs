using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Tichu.CardDataNamespace.Tests
{
    [TestClass()]
    public class CardFactoryTests
    {
        CardFactory cf = new CardFactory();
        [TestMethod()]
        public void CreateCardTest()
        {
            var card = cf.CreateCard(5, CardSuit.Hearts);
            Assert.AreEqual("5 of Hearts, ID: 30, Wildcard: False", card.ToStringDebug(false, true, true));
            Assert.AreEqual(5, card.PointValue);
        }

        [TestMethod()]
        public void CreateTichuDeckTest()
        {
            var a = cf.CreateTichuDeck();
            Assert.AreEqual(a.Count, 56);
        }

        [TestMethod()]
        public void TotalPointsInDeck()
        {
            var d = cf.CreateTichuDeck();
            Assert.AreEqual(d.Sum(x => x.PointValue), 100);
        }
    }
}