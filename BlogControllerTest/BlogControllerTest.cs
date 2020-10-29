using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Oblig2.Controllers;
using Oblig2.Models.Entities;
using Oblig2.Models.Repositories;
using Oblig2.Models.ViewModels;
using ProductUnitTest;

namespace ProductionUnitTest
{
    [TestClass]
    public class BlogControllerTest
    {
        [TestInitialize]
        public void SetUp()
        {
            _repository = new Mock<IBlogRepository>() ;
            _controller = new BlogController(_repository.Object);
        }

        [TestCleanup]
        public void CleanUp()
        {

        }

        [TestMethod]
        public void Index_ReturnsNotNullResult()
        {
            var result = _controller.Index() as ViewResult;

            Assert.IsNotNull(result, "View Result is null");
        }

        [TestMethod]
        public void Index_FiveBlogs_ExistsInView()
        {
            SetupMockToReturnFiveBlogsFromGetAllMethod(_repository);

            var result = (ViewResult) _controller.Index();

            CollectionAssert.AllItemsAreInstancesOfType((ICollection)result.ViewData.Model, typeof(Blog));

            var blogs = result.ViewData.Model as List<Blog>;
            Assert.AreEqual(5, blogs.Count, "Got wrong number of blogs");
        }

        [TestMethod]
        public void Index_ZeroBlogs_ViewIsNotNull()
        {
            SetupMockToReturnZeroBlogsFromGetAllMethod(_repository);

            var result = (ViewResult)_controller.Index();

            CollectionAssert.AllItemsAreInstancesOfType((ICollection)result.ViewData.Model, typeof(Blog));

            var blogs = result.ViewData.Model as List<Blog>;
            Assert.AreEqual(0, blogs.Count, "Got wrong number of blogs");
        }

        [TestMethod]
        public void GetCreate_ReturnsNotNullResult()
        {
            var result = _controller.Create() as ViewResult;

            Assert.IsNotNull(result, "View Result is null");
        }

        [TestMethod]
        public void Save_NewBlog_SaveIsCalled()
        {
            _repository.Setup(mock => mock.Save(It.IsAny<Blog>(), null));

            var result = _controller.Create(new Blog());

            _repository.VerifyAll();
        }

        [TestMethod]
        public void Create_Non_AuthorizedUser_ShouldShowLoginView()
        {
            var mockRepo = new Mock<IBlogRepository>();
            var controller = new BlogController(mockRepo.Object)
            {
                ControllerContext = MockHelpers.FakeControllerContext(false)
            };

            var result = controller.Create() as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsNull(result.ViewName);
        }

        [TestMethod]
        public async Task CreateRedirectActionRedirectsToIndexActionAsync()
        {
            var controller = new BlogController(_repository.Object)
            {
                ControllerContext = MockHelpers.FakeControllerContext(false)
            };

            var tempData =
                new TempDataDictionary(controller.ControllerContext.HttpContext, Mock.Of<ITempDataProvider>());
            controller.TempData = tempData;
            var blog = new Blog()
            {
                Name = "My blog",
                Description = "Description of blog"
            };

            var result = Task.FromResult(await controller.Create(blog) as RedirectToActionResult);

            Assert.IsNotNull(result, "RedirectToIndex needs to redirect to the Index action");
            Assert.AreEqual("Index", result.Result.ActionName);
        }

        [TestMethod]
        public async Task CreateViewIsReturnedWhenInputIsNotValidAsync()
        {
            var blog = new Blog()
            {
                Name = "",
                Description = "",
            };
            var controller = new BlogController(_repository.Object);

            var validationContext = new ValidationContext(blog, null, null);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(blog, validationContext, validationResults, true);
            foreach (var validationResult in validationResults)
                controller.ModelState.AddModelError(validationResult.MemberNames.First(),
                    validationResult.ErrorMessage);

            var result = await controller.Create(blog);

            Assert.IsNotNull(Task.FromResult(result));
            Assert.IsTrue(validationResults.Count > 0);
        }

        [TestMethod]
        public async Task DetailsReturnsNotNullResult()
        {
            var result = Task.FromResult(await _controller.Details(It.IsAny<int>()) as ViewResult);

            Assert.IsNotNull(result, "View Result is null");
        }

        [TestMethod]
        public async Task Details_BlogWithIdOne_ExistsInViewModel()
        {
            SetupMockToReturnOneBlogFromGetWithIdMethod(_repository);

            var expectedName = "Name here";
            var expectedDescription = "Description here";

            var result = Task.FromResult(await _controller.Details(1) as ViewResult);

            Assert.IsNotNull(result, "View Result is null");

            var blog = result.Result.Model as Blog;

            Assert.AreEqual(blog.Name, expectedName, "Blog names are different");
            Assert.AreEqual(blog.Description, expectedDescription, "Blog names are different");
        }

        private static void SetupMockToReturnFiveBlogsFromGetAllMethod(Mock<IBlogRepository> repository)
        {
            repository.Setup(mock => mock.GetAll()).Returns(new List<Blog>()
            {
                new Blog(),
                new Blog(),
                new Blog(),
                new Blog(),
                new Blog()
            });
        }

        private static void SetupMockToReturnZeroBlogsFromGetAllMethod(Mock<IBlogRepository> repository)
        {
            repository.Setup(mock => mock.GetAll()).Returns(new List<Blog>());
        }

        private static void SetupMockToReturnOneBlogFromGetWithIdMethod(Mock<IBlogRepository> repository)
        {
            repository.Setup(mock => mock.GetBlogWithId(1)).ReturnsAsync(new Blog()
            {
                Name = "Name here",
                Description = "Description here"
            });
        }

        private BlogController _controller;
        private Mock<IBlogRepository> _repository;
    }
}
