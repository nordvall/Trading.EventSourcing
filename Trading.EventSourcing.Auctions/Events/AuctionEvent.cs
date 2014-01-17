using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trading.EventSourcing.Auctions.Events
{
    public class AuctionEvent
    {
        public Guid AuctionId { get; set; }
        public DateTime Time { get; set; }
    }
}
