﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Tichu.TurnManagerNamespace.Tests
{
    [TestClass()]
    public class TurnManagerTests
    {
        const int firstID = 3;
        const int secondID = 54;
        const int thirdID = 8;
        const int fourthID = 12;
        const int nonExistentPlayerID = 131241413;

        [TestMethod()]
        [ExpectedException(typeof(System.ArgumentException))]
        public void TurnManager_Constructor_DuplicatePlayerIDs()
        {
            List<int> playerIDs = new List<int> { firstID, secondID, firstID, thirdID, fourthID };
            TurnManager manager = new TurnManager(playerIDs);
        }

        [TestMethod()]
        public void AdvanceTurn_TurnNumber_TurnNumberIncreasedBy1()
        {
            List<int> playerIDs = new List<int> { firstID, secondID, thirdID, fourthID };
            TurnManager manager = new TurnManager(playerIDs);
            var initialTurn = manager.TurnCount;
            manager.TryAdvanceTurn(playerIDs[0]);
            int newTurnNumber = manager.TurnCount;
            Assert.AreEqual(initialTurn + 1, newTurnNumber);
        }

        [TestMethod()]
        public void AdvanceTurn_EventGetsFired()
        {
            List<int> playerIDs = new List<int> { firstID, secondID, thirdID, fourthID };
            TurnManager manager = new TurnManager(playerIDs);
            bool success = false;
            manager.TurnAdvanced += (int id) => { success = true; };
            manager.TryAdvanceTurn(playerIDs[0]);
            Assert.AreEqual(success, true);
        }

        [TestMethod()]
        public void AdvanceTurn_EventReturnsTheNextPlayerID()
        {
            Assert.AreNotEqual(firstID, secondID);
            List<int> playerIDs = new List<int> { firstID, secondID, thirdID, fourthID };
            TurnManager manager = new TurnManager(playerIDs);
            int returnedID = firstID;
            manager.TurnAdvanced += (int id) => { returnedID = id; };
            manager.TryAdvanceTurn(playerIDs[0]);
            Assert.AreEqual(secondID, returnedID);
        }

        [TestMethod()]
        public void AdvanceTurn_CyclesOverToZero()
        {
            Assert.AreNotEqual(firstID, fourthID);
            List<int> playerIDs = new List<int> { firstID, secondID, thirdID, fourthID };
            TurnManager manager = new TurnManager(playerIDs);
            manager.TryAdvanceTurnToPlayer(playerIDs[0], fourthID);
            int returnedID = fourthID;
            manager.TurnAdvanced += (int id) => { returnedID = id; };
            manager.TryAdvanceTurn(playerIDs[3]);
            Assert.AreEqual(firstID, returnedID);
        }

        [TestMethod()]
        public void AdvanceTurnExplicitlyToPlayer_EventReturnsTheCorrectPlayerID()
        {
            List<int> playerIDs = new List<int> { firstID, secondID, thirdID, fourthID };
            TurnManager manager = new TurnManager(playerIDs);
            int returnedPlayerID = 0;
            manager.TurnAdvanced += (int id) => { returnedPlayerID = id; };
            manager.TryAdvanceTurnToPlayer(playerIDs[0], fourthID);
            Assert.AreEqual(returnedPlayerID, fourthID);
        }

        [TestMethod()]
        [ExpectedException(typeof(System.ArgumentException))]
        public void AdvanceTurnExplicitlyToPlayer_InvalidPlayerID()
        {
            List<int> playerIDs = new List<int> { firstID, secondID, thirdID, fourthID };
            TurnManager manager = new TurnManager(playerIDs);
            manager.TryAdvanceTurnToPlayer(playerIDs[0], nonExistentPlayerID);
        }

        [TestMethod()]
        public void AdvanceTurn_WrongPlayerCalling_TurnNotAdvancing()
        {
            List<int> playerIDs = new List<int> { firstID, secondID, thirdID, fourthID };
            TurnManager manager = new TurnManager(playerIDs);
            var initialTurn = manager.TurnCount;
            Assert.AreEqual(manager.TryAdvanceTurn(playerIDs[1]), false);
            int newTurnNumber = manager.TurnCount;
            Assert.AreEqual(initialTurn, newTurnNumber);
        }

        [TestMethod()]
        public void AdvanceTurn_WrongPlayerCalling_EventNotFired()
        {
            List<int> playerIDs = new List<int> { firstID, secondID, thirdID, fourthID };
            TurnManager manager = new TurnManager(playerIDs);
            bool success = false;
            manager.TurnAdvanced += (int id) => { success = true; };
            manager.TryAdvanceTurn(playerIDs[2]);
            Assert.AreEqual(success, false);
        }
    }
}