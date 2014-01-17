using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trading.EventSourcing.Auctions
{
    public class Bid
    {
        public Bid(Guid userId, uint amount, DateTime time)
        {
            if (userId == Guid.Empty)
            {
                throw new ArgumentNullException("userId");
            }

            if (amount == 0)
            {
                throw new ArgumentNullException("description");
            }

            if (time == DateTime.MinValue)
            {
                throw new ArgumentNullException("time");
            }

            UserId = userId;
            Amount = amount;
            Time = time;
        }

        public Guid UserId { get; private set; }

        public uint Amount { get; private set; }

        public DateTime Time { get; private set; }
    }
}
