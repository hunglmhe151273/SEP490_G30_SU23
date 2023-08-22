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
    public class SupplierControllerTest
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
        public void GetAllSuppliers_Returns_IActionResult()
        {
            var supplierController = new SuppliersController();
            var data = supplierController.Index();
            Assert.IsNotNull(data);
        }

        [Test]
        public void CreateSupplier_Returns_IActionResult()
        {
            var supplierController = new SuppliersController();
            Supplier sTest = new Supplier
            {
                SupplierId = 10,
                Address = "Cau Giay, Ha Noi",
                SupplierName = "Nha cung cap 1",
                Phone = "19001001",
                Status = true,
                Description = "Best supplier!",
                Email = "supplier1@gmail.com",
                ProvinceCode = 1,
                Province = "1",
                DistrictCode = 1,
                District = "1",
                WardCode = 1,
                Ward = "1",
            };
            var data = supplierController.Create(sTest);
            Assert.IsTrue(data != null);
        }
    }
}
