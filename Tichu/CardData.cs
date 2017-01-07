namespace Tichu
{
    public enum CardSuit { Spades = 0, Clubs = 1, Hearts = 2, Diamonds = 3, Special = 4 };
    public class CardData
    {
        public readonly int ID;// { get; private set; }
        public readonly int NumericalValue;// { get; private set; }
        public readonly CardSuit Suit;// { get; private set; }
        public readonly bool IsWildcard;// { get; private set; }

        private CardData()
        {

        }

        public CardData(int id, int value, CardSuit suit, bool isWildcard = false)
        {
            ID = id;
            NumericalValue = value;
            Suit = suit;
            IsWildcard = isWildcard;
        }
    }
}
