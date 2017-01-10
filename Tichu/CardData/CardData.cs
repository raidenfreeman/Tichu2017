namespace Tichu.CardDataNamespace
{
    public enum CardSuit { Spades = 0, Clubs = 1, Hearts = 2, Diamonds = 3, Special = 4 };
    public class CardData
    {
        public readonly int ID;// { get; private set; }
        public readonly int NumericalValue;// { get; private set; }
        public readonly CardSuit Suit;// { get; private set; }
        public readonly bool IsWildcard;// { get; private set; }

        internal CardData(int id, int value, CardSuit suit, bool isWildcard = false)
        {
            ID = id;
            NumericalValue = value;
            Suit = suit;
            IsWildcard = isWildcard;
        }

        public override string ToString()
        {
            string cardName = NumericalValue.ToString();
            if (NumericalValue == 14)
            {
                cardName = "Ace";
            }
            if (ID == RuleVariables._dogID)
            {
                cardName = "Dogs";
            }
            if (ID == RuleVariables._mahjongID)
            {
                cardName = "Mahjong";
            }
            if (ID == RuleVariables._dragonID)
            {
                cardName = "Dragon";
            }
            if (ID == RuleVariables._phoenixID)
            {
                cardName = "Phoenix";
            }
            if (Suit == CardSuit.Special)
            {
                return cardName + ", ID: " + ID + ", Wildcard: " + IsWildcard;
            }
            else
            {
                return cardName + " of " + Suit + ", ID: " + ID + ", Wildcard: " + IsWildcard;
            }
        }
    }
}
