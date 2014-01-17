using CommonDomain.Core;
using Trading.EventSourcing.Auctions.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trading.EventSourcing.Auctions
{
    public class Auction : AggregateBase
    {
        private List<Bid> _bids;

        internal Auction()
        {
            _bids = new List<Bid>();

            Register<AuctionCreated>((e) => OnAuctionCreated(e));
            Register<BidPlaced>((e) => OnBidPlaced(e));
        }

        public Auction(Item item, Guid userId, uint minimumPrice)
            : this()
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            if (userId == Guid.Empty)
            {
                throw new ArgumentNullException("userId");
            }

            var createdEvent = new AuctionCreated()
            {
                AuctionId = Guid.NewGuid(),
                Time = DateTime.Now,
                Item = item,
                MinimumPrice = minimumPrice,
                UserId = userId
            };
            
            base.RaiseEvent(createdEvent);
        }

        public DateTime Created { get; private set; }

        public Guid CreatedBy { get; private set; }

        public Item Item { get; private set; }

        public uint MinimumPrice { get; private set; }

        public IEnumerable<Bid> Bids 
        {
            get { return _bids; }
        }

        private void OnAuctionCreated(AuctionCreated auctionCreated)
        {
            Id = auctionCreated.AuctionId;
            Item = auctionCreated.Item;
            MinimumPrice = auctionCreated.MinimumPrice;
            Created = auctionCreated.Time;
            CreatedBy = auctionCreated.UserId;
        }

        public uint GetCurrentPrice()
        {
            if (_bids.Count == 0)
            {
                return MinimumPrice;
            }
            else
            {
                Bid lastBid = _bids.Last();
                return lastBid.Amount;
            }
        }

        public void PlaceBid(Guid userId, uint amount)
        {
            if (userId == Guid.Empty)
            {
                throw new ArgumentNullException("userId");
            }

            uint currentPrice = GetCurrentPrice();

            if (amount <= currentPrice)
            {
                throw new ArgumentException("Amount is too low");
            }

            var bidPlacedEvent = new BidPlaced()
            {
                AuctionId = Id,
                Time = DateTime.Now,
                Amount = amount,
                UserId = userId
            };

            base.RaiseEvent(bidPlacedEvent);
        }

        private void OnBidPlaced(BidPlaced bidPlacedEvent)
        {
            var bid = new Bid(bidPlacedEvent.UserId, bidPlacedEvent.Amount, bidPlacedEvent.Time);

            _bids.Add(bid);
        }
    }
}
