using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using VBookHaven_Admin.Areas.Admin.Controllers;

namespace VBookHaven.Test
{
    [TestFixture]
    public class PurchaseOrderControllerTest
    {
        private TransactionScope scope;

        [SetUp]
        public void Setup()
        {
            scope = new TransactionScope();
        }

        [TearDown]
        public void Test1()
        {
            scope.Dispose();
        }

        [Test]
        public void GetAllPurchaseOrders_Returns_IActionResult()
        {
            var purchaseController = new PurchaseOrderController();
            var data = purchaseController.Index();
            Assert.IsNotNull(data);
        }
    }
}
