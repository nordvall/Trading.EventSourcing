using CommonDomain;
using CommonDomain.Core;
using Trading.EventSourcing.Auctions.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trading.EventSourcing.Auctions.EventStore
{
    class AuctionConflictDetector : ConflictDetector
    {
        public AuctionConflictDetector()
        {
            Register<BidPlaced, BidPlaced>((e1, e2) => { return false; });
        }

        private bool BidConflictHandler(object uncommitted, object committed)
        {
            return true;
        }
    }
}
