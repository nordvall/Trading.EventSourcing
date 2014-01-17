using CommonDomain;
using CommonDomain.Persistence;
using CommonDomain.Persistence.EventStore;
using Trading.EventSourcing.Auctions.Events;
using Trading.EventSourcing.Auctions.EventStore;
using Trading.EventSourcing.Infrastructure;
using NEventStore;
using NEventStore.Dispatcher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trading.EventSourcing.Infrastructure;
using Trading.EventSourcing.Auctions;

namespace Trading.EventSourcing.Auctions.EventStore
{
    public class AuctionRepository : EventStoreRepository
    {
        public AuctionRepository(IStoreEvents eventStore)
            : base(
                eventStore, 
                new AggregateFactory<Auction>(() => { return new Auction(); }), 
                new AuctionConflictDetector())
        {

        }

        public static AuctionRepository Create(MemoryBus bus)
        {
            IStoreEvents store = Wireup.Init()
                .UsingInMemoryPersistence()
                .UsingAsynchronousDispatchScheduler(new BusDispatcher(bus))
                .Build();

            var repository = new AuctionRepository(store);

            return repository;
        }
    }
}
