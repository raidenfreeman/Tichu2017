namespace Tichu.CardDataNamespace
{
    public class CardFactory
    {

        //CardData CreateMajhong()
        //{

        //}
        //CardData CreateDragon()
        //{

        //}

        //CardData CreatePhoenix()
        //{

        //}

        //CardData CreateDogs()
        //{

        //}

        public CardData CreateCard(int value, CardSuit suit)
        {
            int id = CalculateID(value, suit);

            int numericalValue = value == 1 ? 14 : value;

            return new CardData(id, numericalValue, suit);
        }

        int CalculateID(int value, CardSuit suit)
        {
            if (value < 1 || value > 13)
            {
                throw new System.ArgumentOutOfRangeException(nameof(value));
            }
            return value + ((int)suit * 14) - 1;
        }
    }
}
