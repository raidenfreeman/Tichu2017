using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tichu.CardDataNamespace.Tests
{
    [TestClass()]
    public class CardFactoryTests
    {
        [TestMethod()]
        public void CreateCardTest()
        {
            CardFactory cf = new CardFactory();
            Assert.AreEqual("5 of Hearts, ID: 32, Wildcard: False", cf.CreateCard(5, CardSuit.Hearts).ToString());
        }

        [TestMethod()]
        public void CreateTichuDeckTest()
        {
            CardFactory cf = new CardFactory();
            var a = cf.CreateTichuDeck();
            return;
        }
    }
}