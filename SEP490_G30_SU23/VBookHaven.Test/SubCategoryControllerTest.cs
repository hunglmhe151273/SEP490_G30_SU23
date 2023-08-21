/*using FluentAssertions;*/
using Microsoft.AspNetCore.Mvc;
using System.Transactions;
using VBookHaven.Models;
using VBookHaven_Admin.Areas.Admin.Controllers;

namespace VBookHaven.Test
{
    [TestFixture]
    public class SubCategoryControllerTest
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
        public void GetAllSubCategories_Returns_IActionResult()
        {
            var subCategoryController = new SubCategoriesController();
            var data = subCategoryController.Index();
            Assert.IsNotNull(data);
        }

        [Test]
        public void CreateNewSubCategories_Return_IActionResult()
        {
            var subCategoryController = new SubCategoriesController();
            SubCategory test = new SubCategory
            {
                SubCategoryName = "Test1",
                CategoryId = 1,
            };
            var data = subCategoryController.Create(test);
            Assert.IsTrue(data != null);
        }
    }
}
