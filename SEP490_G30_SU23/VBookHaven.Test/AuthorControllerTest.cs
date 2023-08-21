using Microsoft.AspNetCore.Mvc;
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
    public class AuthorControllerTest
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
        public void GetAllAuthors_Returns_IActionResult()
        {
            var authorController = new AuthorController();
            var data = authorController.Index();
            Assert.IsNotNull(data);
        }

        [Test]
        public async Task AddAuthor_Returns_OkResult()
        {
            var authorController = new AuthorController();
            Author aTest = new Author
            {
                AuthorName = "Test",
                Description = "Good author",
                Status = true
            };
            var data = authorController.Add(aTest);
            var result = await data;
            Assert.IsInstanceOf<ObjectResult>(result);
        }

        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(1000)]
        public async Task ChangeAuthorStatus_Returns_404(int authorId)
        {
            var authorController = new AuthorController();
            var data = authorController.ChangeStatus(authorId);
            var result = await data;
            Assert.IsInstanceOf<ObjectResult>(result);
        }
    }
}
