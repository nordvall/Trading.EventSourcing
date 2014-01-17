using MemBus;
using MemBus.Configurators;
using NEventStore;
using NEventStore.Dispatcher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trading.EventSourcing.Infrastructure
{
    /// <summary>
    /// Used by EventStore to publish events to a service bus
    /// </summary>
    public class BusDispatcher : IDispatchCommits
    {
        private MemoryBus _bus;

        public BusDispatcher(MemoryBus bus)
        {
            _bus = bus;
        }

        public void Dispatch(Commit commit)
        {
            List<EventMessage> events = commit.Events;
            foreach (EventMessage message in events)
            {
                var @event = message.Body;
                _bus.Publish(@event);
            }
        }

        public void Dispose()
        {

        }
    }
}
