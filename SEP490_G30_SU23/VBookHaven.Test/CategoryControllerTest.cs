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
    public class CategoryControllerTest
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
        public void GetAllCategories_Returns_IActionResult()
        {
            var categoryController = new CategoriesController();
            var data = categoryController.Index();
            Assert.IsNotNull(data);
        }

        [Test]
        public void CreateNewCategories_Return_IActionResult()
        {
            var categoryController = new CategoriesController();
            Category test = new Category
            {
                CategoryName = "Test1",
                Status = true,
                
            };
            var data = categoryController.Create(test);
            Assert.IsTrue(data != null);
        }
    }
}
