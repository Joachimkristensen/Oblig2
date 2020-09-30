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
using Oblig2.Models;
using Oblig2.Models.Entities;
using Oblig2.Models.Repositories;
using ProductUnitTest;

namespace BlogControllerTest
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
        public void GetCreate_WithFiveBlogs_ExistsInViewModel()
        {
            SetupMockWithBlogEditViewModel(_repository);

            var viewResult = (ViewResult)_controller.Create();

            var productsEditViewModel = viewResult.Model as BlogEditViewModel;
            Assert.IsNotNull(productsEditViewModel);
            // Assert.AreEqual(2, productsEditViewModel.Categories.Count);
            // Assert.AreEqual(2, productsEditViewModel.Manufacturers.Count);
        }

        [TestMethod]
        public void Save_NewBlog_SaveIsCalled()
        {
            _repository.Setup(mock => mock.Save(It.IsAny<BlogEditViewModel>(), null));

            var result = _controller.Create(new BlogEditViewModel());

            _repository.VerifyAll();
        }

        [TestMethod]
        public void Create_Non_AuthorizedUser_ShouldShowLoginView()
        {
            var mockRepo = new Mock<IGenericRepository>();
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
            var viewModel = new BlogEditViewModel()
            {
                Name = "My blog",
                Description = "Description of blog"
            };

            var result = Task.FromResult(await controller.Create(viewModel) as RedirectToActionResult);

            Assert.IsNotNull(result, "RedirectToIndex needs to redirect to the Index action");
            Assert.AreEqual("Index", result.Result.ActionName);
        }

        [TestMethod]
        public async Task CreateViewIsReturnedWhenInputIsNotValidAsync()
        {
            var viewModel = new BlogEditViewModel()
            {
                Name = "",
                Description = "",
            };
            var controller = new BlogController(_repository.Object);

            var validationContext = new ValidationContext(viewModel, null, null);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(viewModel, validationContext, validationResults, true);
            foreach (var validationResult in validationResults)
                controller.ModelState.AddModelError(validationResult.MemberNames.First(),
                    validationResult.ErrorMessage);

            var result = await controller.Create(viewModel);

            Assert.IsNotNull(Task.FromResult(result));
            Assert.IsTrue(validationResults.Count > 0);
        }


        private static void SetupMockToReturnFiveBlogsFromGetAllMethod(Mock<IGenericRepository> repository)
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
        private static void SetupMockToReturnZeroBlogsFromGetAllMethod(Mock<IGenericRepository> repository)
        {
            repository.Setup(mock => mock.GetAll()).Returns(new List<Blog>());
        }

        private static void SetupMockWithBlogEditViewModel(Mock<IGenericRepository> repository)
        {
            repository.Setup(mock => mock.GetBlogEditViewModel()).Returns(new BlogEditViewModel());
        }

        private BlogController _controller;
        private Mock<IGenericRepository> _repository;
    }
}
