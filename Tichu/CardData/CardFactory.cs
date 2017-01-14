using System;
using System.Collections.Generic;
using System.Linq;

namespace Tichu.CardDataNamespace
{
    public class CardFactory
    {

        public CardData CreateMajhong()
        {
            return new CardData(RuleVariables._mahjongID, RuleVariables._majongValue, CardSuit.Special);
        }
        public CardData CreateDragon()
        {
            return new CardData(RuleVariables._dragonID, RuleVariables._dragonValue, CardSuit.Special);
        }

        public CardData CreatePhoenix()
        {
            return new CardData(RuleVariables._phoenixID, RuleVariables._specialCardNumericalValue, CardSuit.Special, true);
        }

        public CardData CreateDogs()
        {
            return new CardData(RuleVariables._dogID, RuleVariables._specialCardNumericalValue, CardSuit.Special);
        }

        public CardData CreateCard(int value, CardSuit suit)
        {
            if (suit == CardSuit.Special)
            {
                throw new ArgumentException("Cannot create Special cards through this function.", nameof(suit));
            }

            int id = CalculateID(value, suit);

            int numericalValue = value == 1 ? 14 : value;

            return new CardData(id, numericalValue, suit);
        }

        internal CardData CreatePhoenixSubstitute(int value)
        {
            return new CardData(RuleVariables._phoenixID, value, CardSuit.Special);
        }

        int CalculateID(int value, CardSuit suit)
        {
            if (value < 1 || value > 13)
            {
                throw new System.ArgumentOutOfRangeException(nameof(value));
            }
            return value + ((int)suit * 13) - 1;
        }

        public List<CardData> CreateTichuDeck()
        {
            var deck = new List<CardData>();
            foreach (var suit in Enum.GetValues(typeof(CardSuit)).Cast<CardSuit>())
            {
                if (suit == CardSuit.Special)
                {
                    deck.Add(CreateDragon());
                    deck.Add(CreateMajhong());
                    deck.Add(CreatePhoenix());
                    deck.Add(CreateDogs());
                }
                else
                {
                    for (int j = 1; j < 14; j++)
                    {
                        deck.Add(CreateCard(j, suit));
                    }
                }
            }
            return deck;
        }
    }
}
