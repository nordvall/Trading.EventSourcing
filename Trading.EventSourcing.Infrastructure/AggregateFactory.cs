using CommonDomain;
using CommonDomain.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trading.EventSourcing.Infrastructure
{
    /// <summary>
    /// Used by EventStore to construct an aggregate object, before applying the events
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AggregateFactory<T> : IConstructAggregates where T : IAggregate
    {
        private Func<T> _createAggregate;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="createAggregate">A delegate that returns an object of type T</param>
        public AggregateFactory(Func<T> createAggregate)
        {
            _createAggregate = createAggregate;
        }

        public IAggregate Build(Type type, Guid id, IMemento snapshot)
        {
            if (type != typeof(T))
            {
                throw new NotSupportedException("Aggregate type is not supported");
            }

            T aggregate = _createAggregate.Invoke();

            return aggregate;
        }
    }
}
