namespace Tichu.CardDataNamespace
{
    public enum CardSuit { Spades = 0, Clubs = 1, Hearts = 2, Diamonds = 3, Special = 4 };
    public struct CardData
    {
        public readonly int ID;// { get; private set; }
        public readonly int NumericalValue;// { get; private set; }
        public readonly CardSuit Suit;// { get; private set; }
        public readonly bool IsWildcard;// { get; private set; }
        public readonly int PointValue;

        internal CardData(int id, int value, CardSuit suit, bool isWildcard = false)
        {
            ID = id;
            NumericalValue = value;
            Suit = suit;
            IsWildcard = isWildcard;
            switch (value)
            {
                case 5:
                    PointValue = RuleVariables.FivePointValue;
                    break;
                case 10:
                    PointValue = RuleVariables.TenPointValue;
                    break;
                case 13:
                    PointValue = RuleVariables.KingPointValue;
                    break;
                default:
                    PointValue = 0;
                    break;
            }
            if (id == RuleVariables._dragonID)
            {
                PointValue = RuleVariables.DragonPointValue;
            }
            if (id == RuleVariables._phoenixID)
            {
                PointValue = RuleVariables.PhoenixPointValue;
            }
        }

        public override string ToString()
        {
            string cardName = NumericalValue.ToString();
            switch (NumericalValue)
            {
                case 11:
                    cardName = "Jack";
                    break;
                case 12:
                    cardName = "Queen";
                    break;
                case 13:
                    cardName = "King";
                    break;
                case 14:
                    cardName = "Ace";
                    break;
            }
            switch (ID)
            {
                case RuleVariables._dogID:
                    cardName = "Dogs";
                    break;
                case RuleVariables._mahjongID:
                    cardName = "Mahjong";
                    break;
                case RuleVariables._dragonID:
                    cardName = "Dragon";
                    break;
                case RuleVariables._phoenixID:
                    cardName = "Phoenix";
                    break;
                default:
                    break;
            }
            if (Suit == CardSuit.Special)
            {
                return cardName;
            }
            else
            {
                return cardName + " of " + Suit;
            }
        }

        public string ToStringDebug(bool showPointValue = false, bool showID = false, bool showIsWildcard = false)
        {
            string finalString = ToString();
            if (showPointValue)
            {
                finalString += ", Points: " + PointValue;
            }
            if (showID)
            {
                finalString += ", ID: " + ID;
            }
            if (showIsWildcard)
            {
                finalString += ", Wildcard: " + IsWildcard;
            }
            return finalString;
        }
    }
}
