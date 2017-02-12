using System;

namespace Tichu
{
    public class CallbackTest
    {
        public bool TryPlayCards(int[] ids, Func<int> getExtraInfo)
        {
            if (ids != null)
            {
                return ids[0] == getExtraInfo();
            }
            return false;
        }
    }
}
