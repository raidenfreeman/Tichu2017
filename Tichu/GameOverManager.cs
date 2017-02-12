using System;
using System.Collections.Generic;
using System.Linq;
using Tichu.CardDataNamespace;
using Tichu.GameStateDataNamespace;

namespace Tichu
{
    public class GameOverManager
    {
        private GameManager gameManager;

        public GameOverManager(GameManager gameManager)
        {
            //TODO: FIX THIS
            throw new NotImplementedException();
            this.gameManager = gameManager;
        }

        public int CalculateTotalCardValue(List<CardData> cards)
        {
            return cards.Sum(x => x.PointValue);
        }

        private int? FirstPlayerID = null;
        private void OnAdvanceTurn(int turn)
        {
            if (FirstPlayerID == null)
            {
                var tempPlayer = gameManager.Players.Where(x => x.Hand.Count == 0).ToList();
                if (tempPlayer.Count == 1)
                {
                    FirstPlayerID = tempPlayer.First().ID;
                }
            }
            if (CheckForGameOver(gameManager.Players))
            {

            }
        }

        private bool CheckForGameOver(List<PlayerData> players)
        {
            //if all 3 players are out of cards
            if (players.Count(x => x.Hand.Count == 0) == 3)
            {
                return true;
            }

            var playersOutOfCards = players.Where(x => x.Hand.Count == 0);
            //if more than 1 players are out of cards
            if (playersOutOfCards.Count() > 1)
            {
                //if they are in the same team
                var team = playersOutOfCards.First().Team;
                if (playersOutOfCards.All(x => x.Team == team))
                {
                    //end the game, it's 1-2
                    return true;
                }
            }
            return false;
        }

        private int[] CalculateTeamScores(List<PlayerData> players)
        {
            if (players.Count(x => x.Hand.Count == 0) == 3)
            {
                var remainingPlayer = players.First(x => x.Hand.Count != 0);
                //give the cards won to the first player
                players.First(x => x.ID == FirstPlayerID).CardsWon.AddRange(remainingPlayer.CardsWon);
                //give the cards in your hand to the opposing team
                players.First(x => x.Team != remainingPlayer.Team).CardsWon.AddRange(remainingPlayer.Hand);
                int[] teamscore = { 0, 0 };
                foreach (var player in players)
                {
                    teamscore[player.Team - 1] += player.Score;
                }
                return teamscore;
            }
            else
            {
                var a = players.First(x => x.Hand.Count == 0).Team;
                int[] r = { 0, 0 };
                r[a - 1] = RuleVariables.OneTwoVictoryPoints;
                return r;
            }
        }
    }
}
