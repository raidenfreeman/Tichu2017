using System.Collections.Generic;
using Tichu.CardDataNamespace;
using Tichu.GameStateDataNamespace;
using Tichu.TurnManagerNamespace;

namespace Tichu
{
    public class PlayerActions
    {
        private TableState _tableState;
        private CardAnalyzer cardAnalyzer;
        private TurnManager turnManager;

        public bool TryPlayCards(List<CardData> cards, int playerId)
        {
            if (turnManager.CurrentPlayerID != playerId)
            {
                return false;
            }
            var result = cardAnalyzer.AnalyzeCards(cards);
            return false;
        }

        public bool TryPass(int playerId)
        {
            return turnManager.TryAdvanceTurn(playerId);
        }
    }
}
