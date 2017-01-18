using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Tichu.Tests
{
    [TestClass()]
    public class GameManagerTests
    {
        [TestMethod()]
        public void SetupGame_FourPlayers_GameStateInitializedCorrectly()
        {
            GameManager g = new GameManager();
            g.SetupGame(new List<int> { 9, 5, 6, 12 }, new List<string> { "George", "John", "James", "Lily" });
            var a = g.Players.Where(x => x.Hand.Count == 0);
            var b = a.FirstOrDefault();
            Assert.AreEqual(9, g.turnManager.CurrentPlayerID);
            Assert.AreEqual(14, g.Players[2].Hand.Count);
            Assert.AreEqual(null, g.activeTrick);
        }
    }
}