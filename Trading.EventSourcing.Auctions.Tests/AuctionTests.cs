using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trading.EventSourcing.Auctions.Tests
{
    [TestFixture]
    public class AuctionTests
    {
        private Guid _dummyGuid = Guid.NewGuid();
        private Item _dummyItem = new Item("Title", "Description");

        [Test]
        public void Ctor_WhenInvokedWithNoItem_ArgumentExceptionIsThrown()
        {
            Assert.Throws<ArgumentNullException>(() => { new Auction(null, _dummyGuid, 1); });
        }

        [Test]
        public void Ctor_WhenInvokedWithNoUserGuid_ArgumentExceptionIsThrown()
        {
            Assert.Throws<ArgumentNullException>(() => { new Auction(_dummyItem, Guid.Empty, 1); });
        }

        [Test]
        public void Ctor_WhenInvokedWithCorrectValues_PropertiesAreSet()
        {
            var auction = new Auction(_dummyItem, _dummyGuid, 1);

            Assert.AreEqual(_dummyItem, auction.Item);
            Assert.AreEqual(_dummyGuid, auction.CreatedBy);
            Assert.AreNotEqual(Guid.Empty, auction.Id);
        }

        [Test]
        public void Ctor_WhenInvoked_ListOfBidsIsNotNull()
        {
            var auction = new Auction(_dummyItem, _dummyGuid, 1);

            Assert.IsNotNull(auction.Bids);
        }

        [Test]
        public void PlaceBid_WhenInvokedWithNoUserId_ArgumentExceptionIsThrown()
        {
            var auction = new Auction(_dummyItem, _dummyGuid, 1);

            Assert.Throws<ArgumentNullException>(() => { auction.PlaceBid(Guid.Empty, 1); });
        }

        [Test]
        public void PlaceBid_WhenInvokedWithNoAmount_ArgumentExceptionIsThrown()
        {
            var auction = new Auction(_dummyItem, _dummyGuid, 1);

            Assert.Throws<ArgumentException>(() => { auction.PlaceBid(Guid.NewGuid(), 0); });
        }

        [Test]
        public void PlaceBid_WhenInvokedAmountLowerThanMinimum_ArgumentExceptionIsThrown()
        {
            var auction = new Auction(_dummyItem, _dummyGuid, 20);

            var expectedTime = DateTime.Now;
            var userId = Guid.NewGuid();
            uint amount = 10;

            Assert.Throws<ArgumentException>(() => { auction.PlaceBid(userId, amount); });
        }

        [Test]
        public void PlaceBid_WhenInvokedWithCorrectBid_BidIsSaved()
        {
            var auction = new Auction(_dummyItem, _dummyGuid, 1);

            var expectedTime = DateTime.Now;
            var userId = Guid.NewGuid();
            uint amount = 2;

            auction.PlaceBid(userId, amount);

            // Assert
            var storedBid = auction.Bids.First();
            Assert.AreEqual(userId, storedBid.UserId);
            Assert.AreEqual(amount, storedBid.Amount);
            Assert.IsTrue(storedBid.Time >= expectedTime);
        }

        [Test]
        public void PlaceBid_WhenInvokedWithSameAmount_ArgumentExceptionIsThrown()
        {
            var auction = new Auction(_dummyItem, _dummyGuid, 1);

            var userId = Guid.NewGuid();
            uint amount = 2;

            // First bid
            auction.PlaceBid(userId, amount);

            // Second bid
            Assert.Throws<ArgumentException>(() => { auction.PlaceBid(userId, amount); });
        }
    }
}
