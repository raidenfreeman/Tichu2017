using System.Collections.Generic;
using System.Linq;
using Tichu.AnalysisResults;
using Tichu.CardDataNamespace;
using Tichu.TurnManagerNamespace;

namespace Tichu
{
    public class PlayerActions
    {
        private readonly GameManager _gameManager;
        private readonly CardAnalyzer _cardAnalyzer;
        private readonly TrickPlayabilityRules _trickPlayabilityRules;
        private readonly TurnManager _turnManager;

        public PlayerActions(GameManager gameManager, CardAnalyzer cardAnalyzer, TrickPlayabilityRules trickPlayabilityRules, TurnManager turnManager)
        {
            _gameManager = gameManager;
            _cardAnalyzer = cardAnalyzer;
            _trickPlayabilityRules = trickPlayabilityRules;
            _turnManager = turnManager;
        }

        public bool TryPlayCards(List<CardData> cards, int playerId)
        {
            if (_turnManager.CurrentPlayerID != playerId)
            {
                return false;
            }
            var result = _cardAnalyzer.AnalyzeCards(cards);
            if (result is Invalid)
            {
                return false;
            }
            if (cards.Count == 1 && cards.First().ID == RuleVariables._dogID && _gameManager.activeTrick == null)
            {
                if (_turnManager.TryAdvanceTurnToTeammate(playerId))
                {
                    _gameManager.ReplaceActiveTrick(result);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            if (_trickPlayabilityRules.IsTrickPlayable(result, _gameManager.activeTrick))
            {
                _gameManager.ReplaceActiveTrick(result);
                if (result is Bomb)
                {
                    _turnManager.ForceAdvanceTurnToPlayer(playerId);
                }
                else
                {
                    _turnManager.TryAdvanceTurn(playerId);
                }
                return true;
            }
            return false;
        }

        public bool TryPass(int playerId)
        {
            return _turnManager.TryAdvanceTurn(playerId);
        }
    }
}
