namespace Tichu
{
    public static class RuleVariables
    {
        public const int NumberOfPlayers = 4;
        public const int _totalRegularCards = 52;

        public const int _totalSpecialCards = 4;

        public const int _totalRegularCardSuits = 4;

        public const int AceMaxValue = 14;

        public const int _totalCards = _totalRegularCards + _totalSpecialCards;

        public const int _cardsPerSuit = _totalRegularCards / _totalRegularCardSuits;

        public const int _specialCardNumericalValue = -1;

        //Card Info

        public const int _dragonID = 0 + _totalRegularCards;

        public const int _mahjongID = 1 + _totalRegularCards;

        public const int _phoenixID = 2 + _totalRegularCards;

        public const int _dogID = 3 + _totalRegularCards;

        public const int _dragonValue = 10000;

        public const int _majongValue = 1;

        public const int _maxCardValue = 14;

        public const int KingPointValue = 10;

        public const int FivePointValue = 5;

        public const int TenPointValue = 10;

        public const int DragonPointValue = 25;

        public const int PhoenixPointValue = -25;

        public const int OneTwoVictoryPoints = 200;
    }

}
