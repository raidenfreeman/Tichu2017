using System.Collections.Generic;
using Tichu.AnalysisResults;

namespace Tichu.GameStateDataNamespace
{
    class TableState
    {
        public List<PlayerData> Players { get; private set; }
        public TichuTrick TrickOnTable { get; set; }
    }
}
