using System.Collections.Generic;
using System.Linq;
using Tichu.CardDataNamespace;

namespace Tichu.GameStateDataNamespace
{
    public struct PlayerData
    {

        //Data means immutable information
        //State means mutable information
        public int ID { get; }
        public string DisplayName { get; }
        public List<CardData> Hand { get; private set; }
        public List<CardData> CardsWon { get; private set; }
        public int Team { get; private set; }

        public PlayerData(int id, string displayName, int team)
        {
            ID = id;
            DisplayName = displayName;
            Hand = new List<CardData>();
            CardsWon = new List<CardData>();
            Team = team;
        }

        public int Score
        {
            get { return CardsWon.Sum(x => x.PointValue); }
        }

    }
}
