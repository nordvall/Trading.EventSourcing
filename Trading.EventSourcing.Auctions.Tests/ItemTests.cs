using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trading.EventSourcing.Auctions.Tests
{
    [TestFixture]
    public class ItemTests
    {
        [Test]
        public void Ctor_WhenInvokedWithNoTitle_ArgumentExceptionIsThrown()
        {
            Assert.Throws<ArgumentNullException>(() => { new Item("", "description"); });
        }

        [Test]
        public void Ctor_WhenInvokedWithNoDescription_ArgumentExceptionIsThrown()
        {
            Assert.Throws<ArgumentNullException>(() => { new Item("title", ""); });
        }

        [Test]
        public void Ctor_WhenInvokedWithCorrectValues_PropertiesAreSet()
        {
            string title = "Title";
            string description = "Description";

            var item = new Item(title, description);

            Assert.AreEqual(title, item.Title);
            Assert.AreEqual(description, item.Description);
        }
    }
}
