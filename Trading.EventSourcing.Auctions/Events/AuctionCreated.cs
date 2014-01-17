using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trading.EventSourcing.Auctions.Events
{
    public class AuctionCreated : AuctionEvent
    {
        public Guid UserId { get; set; }

        public Item Item { get; set; }

        public uint MinimumPrice { get; set; }
    }
}
