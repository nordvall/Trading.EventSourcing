using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trading.EventSourcing.Auctions.Events
{
    public class BidPlaced : AuctionEvent
    {
        public Guid UserId { get; set; }

        public uint Amount { get; set; }
    }
}
