using MemBus;
using MemBus.Configurators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trading.EventSourcing.Infrastructure
{
    /// <summary>
    /// Wrapper for MemBus
    /// </summary>
    public class MemoryBus
    {
        private IBus _bus;

        public MemoryBus()
        {
            _bus = BusSetup.StartWith<Conservative>().Construct();
        }

        public void Publish(object obj)
        {
            _bus.Publish(obj);
        }

        public void Subscribe<T>(Action<T> action)
        {
            _bus.Subscribe<T>(action);
        }
    }
}
