using System;
using System.Collections.Generic;
using System.Linq;

namespace Tichu.TurnManager
{
    /// <summary>
    /// Keeps track of which player's turn it is, and advances to the next with round robin.
    /// </summary>
    public class TurnManager
    {
        /// <summary>
        /// Keeps track of how many turns have passed
        /// </summary>
        public int TurnCount { get; private set; }

        /// <summary>
        /// Happens when the game advances to the next turn. Has the new current player's id  as an argument.
        /// </summary>
        public event Action<int> TurnAdvanced;

        /// <summary>
        /// A collection of all the players in the game
        /// </summary>
        private List<int> _playerIDs;

        /// <summary>
        /// The index in the collection _playerIDs, of the player whose turn it is
        /// </summary>
        private int _currentPlayerIndex;

        /// <summary>
        /// How many players this game has
        /// </summary>
        private int _numberOfPlayers;

        /// <summary>
        /// Constructs a new turn manager
        /// </summary>
        /// <param name="playerIDs">The IDs of the players in the game, in the order that they should play.</param>
        public TurnManager(ICollection<int> playerIDs)
        {
            if (playerIDs == null)
            {
                throw new ArgumentException(nameof(playerIDs) + " cannot be null.", nameof(playerIDs));
            }
            if (playerIDs.GroupBy(x => x).Any(x => x.Count() > 1))
            {
                throw new ArgumentException(nameof(playerIDs) + " must have only unique values.", nameof(playerIDs));
            }

            _playerIDs = new List<int>(playerIDs);
            _numberOfPlayers = playerIDs.Count;
        }

        /// <summary>
        /// Increments the turns by 1
        /// </summary>
        /// <returns>The new turn's number</returns>
        public int AdvanceTurn()
        {
            TurnCount += 1;
            _currentPlayerIndex += 1;
            if (_currentPlayerIndex == _numberOfPlayers)
            {
                _currentPlayerIndex = 0;
            }
            if (_currentPlayerIndex > _numberOfPlayers || _currentPlayerIndex < 0)
            {
                throw new ApplicationException("Impossible current player index");
            }
            TurnAdvanced?.Invoke(_playerIDs[_currentPlayerIndex]);
            return TurnCount;
        }

        /// <summary>
        /// Passes the next turn to a certain player
        /// </summary>
        /// <param name="playerID">The ID of the player to give the turn to</param>
        /// <returns>The new turn's number</returns>
        public int AdvanceTurnExplicitlyToPlayer(int playerID)
        {

            int playerIndex = _playerIDs.IndexOf(playerID);

            if (playerIndex < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(playerID));
            }
            TurnCount += 1;
            _currentPlayerIndex = playerIndex;
            TurnAdvanced?.Invoke(playerID);
            return TurnCount;
        }
    }
}
