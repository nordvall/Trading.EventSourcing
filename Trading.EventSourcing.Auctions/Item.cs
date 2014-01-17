using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trading.EventSourcing.Auctions
{
    public class Item
    {
        public Item(string title, string description)
        {
            if (string.IsNullOrEmpty(title))
            {
                throw new ArgumentNullException("title");
            }

            if (string.IsNullOrEmpty(description))
            {
                throw new ArgumentNullException("description");
            }

            Title = title;
            Description = description;
        }

        public string Title { get; private set; }

        public string Description { get; private set; }
    }
}
