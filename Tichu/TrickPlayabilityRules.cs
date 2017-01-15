using Tichu.AnalysisResults;

namespace Tichu
{
    public class TrickPlayabilityRules
    {
        public bool IsTrickPlayable(TichuTrick newTrick, TichuTrick existingTrick)
        {
            if (newTrick == null || existingTrick == null)
            {
                return false;
            }
            //Case: Anything over Bomb

            //If the existing trick is a bomb
            if (existingTrick is Bomb)
            {
                //check if the other is a bomb
                if (newTrick is Bomb)
                {
                    //if both are bombs, check who wins
                    return IsStrongerBomb(newTrick as Bomb, existingTrick as Bomb);
                }
                else
                {
                    return false;
                }

            }

            //Case: Bomb over Anything

            if (newTrick is Bomb)
            {
                return true;
            }

            //check if the tricks are compatible
            if (AreSameKind(newTrick, existingTrick))
            {
                if (newTrick is Single)
                {
                    return SingleResolution(newTrick, existingTrick);
                }
                //if so, check if the new trick's value is higher
                //if it isn't it can't be played
                return IsValueGreater(newTrick, existingTrick);
            }
            else
            {
                //if they are incompatible, they are not playable
                return false;
            }
        }

        private bool SingleResolution(TichuTrick newTrick, TichuTrick existingTrick)
        {
            //If the existing trick is a dragon, it's unbeatable by single cards
            if (existingTrick.TrickValue == RuleVariables.DragonPointValue)
            {
                return false;
            }
            //if you have a phoenix
            if (newTrick is PhoenixSingleTrick)
            {
                //you can beat anything up to an Ace
                if (existingTrick.TrickValue <= 14)
                {
                    return true;
                }
            }
            return newTrick.TrickValue > existingTrick.TrickValue;
        }

        private bool IsStrongerBomb(Bomb newTrick, Bomb existingTrick)
        {
            if (existingTrick is BombFourOfAKind)
            {
                if (newTrick is BombStraight)
                {
                    //straights beat four of a kind
                    return true;
                }
                else
                {
                    //both are Four of a kind Bombs, check who has the highest
                    return newTrick.TrickValue > existingTrick.TrickValue;
                }
            }
            else
            {
                //the existing trick is a straight
                if (newTrick is BombFourOfAKind)
                {
                    //you can't bomb a straightflush with a four of a kind
                    return false;
                }
                else
                {
                    //both are straight flushes
                    return newTrick.TrickValue > existingTrick.TrickValue;
                }
            }
        }

        private bool AreSameKind(TichuTrick newTrick, TichuTrick existingTrick)
        {
            if (newTrick is Single && existingTrick is Single)
            {
                return true;
            }
            return newTrick.GetType() == existingTrick.GetType();
        }

        private bool IsValueGreater(TichuTrick newTrick, TichuTrick existingTrick)
        {
            return newTrick.TrickValue > existingTrick.TrickValue;
        }
    }
}
