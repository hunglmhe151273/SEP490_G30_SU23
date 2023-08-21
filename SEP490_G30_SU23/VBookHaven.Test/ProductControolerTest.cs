using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using VBookHaven.Models;
using VBookHaven_Admin.Areas.Admin.Controllers;

namespace VBookHaven.Test
{
    [TestFixture]
    public class ProductControolerTest
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
        public void GetAllProduct_Returns_IActionResult()
        {
            var productController = new ProductController();
            var data = productController.Index();
            Assert.IsNotNull(data);
        }

        [Test]
        public void AddBook_Return_IActionResult()
        {
            var productController = new ProductController();
            Product pTest = new Product
            {
                Name = "Book1",
                Barcode = "0123012",
                Unit = "Chiec",
                UnitInStock = 0,
                PurchasePrice = 12000,
                RetailPrice = 15000,
                RetailDiscount = 0.2,
                WholesalePrice = 13000,
                WholesaleDiscount = 0.1,
                Size = "20x30x30",
                Weight = "1kg",
                Description = "Good Book",
                Status = true,
                IsBook = true,
                SubCategoryId = 1
            };
            Book bTest = new Book
            {
                Pages = 340,
                Language = "VietNam",
                Publisher = "Hung Le",
            };
            ProductManagementViewModel modelTest = new ProductManagementViewModel
            {
                Product = pTest,
                Book = bTest,
                Stationery = null,
            };
            var data = productController.AddBook(modelTest);
            Assert.IsTrue(data != null);
        }

        [Test]
        public void AddStationery_Return_IActionResult()
        {
            var productController = new ProductController();
            Product pTest = new Product
            {
                Name = "Stationery1",
                Barcode = "0125552",
                Unit = "Hop",
                UnitInStock = 0,
                PurchasePrice = 10000,
                RetailPrice = 12000,
                RetailDiscount = 0.2,
                WholesalePrice = 11000,
                WholesaleDiscount = 0.1,
                Size = "2X14",
                Weight = "80g",
                Description = "Good Stationery",
                Status = true,
                IsBook = false,
                SubCategoryId = 2
            };
            Stationery sTest = new Stationery
            {
                Material = "Nhua",
                Origin = "Viet Nam",
                Brand = "Thien Long"
            };
            ProductManagementViewModel modelTest = new ProductManagementViewModel
            {
                Product = pTest,
                Book = null,
                Stationery = sTest,
            };
            var data = productController.AddStationery(modelTest);
            Assert.IsTrue(data != null);
        }
    }
}
