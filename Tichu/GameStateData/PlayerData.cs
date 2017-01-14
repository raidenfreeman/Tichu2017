namespace Tichu.GameStateDataNamespace
{
    public struct PlayerData
    {

        //Data means immutable information
        //State means mutable information
        public int ID { get; }
        public string DisplayName { get; }

        public PlayerData(int id, string displayName)
        {
            ID = id;
            DisplayName = displayName;
        }

    }
}
