using System;
using System.Collections.Generic;
using System.Linq;
using Tichu.AnalysisResults;
using Tichu.CardDataNamespace;
using Tichu.GameStateDataNamespace;
using Tichu.TurnManagerNamespace;

namespace Tichu
{
    public class GameManager
    {
        public event Action<List<CardData>> BoardCardsChanged;
        public event Action GameOver;

        public List<PlayerData> Players { get; private set; }
        private CardFactory cardFactory;
        public TurnManager turnManager { get; private set; }
        public TichuTrick activeTrick { get; private set; }

        public void SetupGame(List<int> PlayerIDs, List<string> PlayerNames)
        {
            if (PlayerIDs.Count != RuleVariables.NumberOfPlayers || PlayerNames.Count != RuleVariables.NumberOfPlayers)
            {
                throw new ArgumentException("Must provide data for 4 players");
            }
            cardFactory = new CardFactory();
            Players = new List<PlayerData>(RuleVariables.NumberOfPlayers);
            for (int i = 0; i < RuleVariables.NumberOfPlayers; i++)
            {
                Players.Add(new PlayerData(PlayerIDs[i], PlayerNames[i], (i + 1) % 2));
            }
            var deck = cardFactory.CreateTichuDeck();
            deck.Shuffle();
            DealCardsToPlayers(Players, deck);
            turnManager = new TurnManager(PlayerIDs);
            activeTrick = null;
            turnManager.TurnAdvanced += CheckForGameOver;
        }

        void DealCardsToPlayers(List<PlayerData> players, List<CardData> deck)
        {
            for (int i = 0; i < RuleVariables.NumberOfPlayers; i++)
            {
                var firstCardIndex = i * 14;
                players[i].Hand.AddRange(deck.GetRange(firstCardIndex, 14));
            }
        }


        /// <summary>
        /// Replaces the trick on the board
        /// </summary>
        /// <param name="newActiveTrick">The new trick to place on the board</param>
        internal void ReplaceActiveTrick(TichuTrick newActiveTrick)
        {
            activeTrick = newActiveTrick;
            BoardCardsChanged?.Invoke(activeTrick.OrderedCards);
        }

        private void CheckForGameOver(int turn)
        {
            if (Players.Count(x => x.Hand.Count == 0) <= 1)
            {

            }
        }
    }
}
