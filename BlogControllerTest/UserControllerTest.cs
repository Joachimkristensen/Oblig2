using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Oblig2.Authorization;
using Oblig2.Controllers;
using Oblig2.Models.Entities;
using Oblig2.Models.Repositories;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProductionUnitTest
{
    [TestClass]
    public class UserControllerTest
    {
        [TestInitialize]
        public void SetUp()
        {
            _repository = new Mock<IBlogRepository>();
            _userManagerMock = new Mock<UserManager<ApplicationUser>>();

            _userManagerMock = TestHelpers.MockUserManager<ApplicationUser>();
            _userManagerMock.Setup(x => x.GetUserId(It.IsAny<ClaimsPrincipal>())).Returns("1234");

            var authHandler = new EntityAuthorizationHandler(_userManagerMock.Object);
            var authService = TestHelpers.BuildAuthorizationService(services =>
            {
                services.AddScoped(sp => _repository.Object);
                services.AddScoped<IAuthorizationHandler>(sp => authHandler);
                services.AddAuthorization();
            });

            _controller = new UserController(_repository.Object, authService);
        }

        [TestCleanup]
        public void CleanUp()
        {

        }

        [TestMethod]
        public async Task Index_ReturnsNotNullResult()
        {
            _controller.ControllerContext = TestHelpers.FakeControllerContext(true);
            var result = await _controller.Index() as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task Index_UserHasSubscribedToThreeBlogs_ReturnsThreeBlogs()
        {
            SetupMockToReturnThreeBlogsFromGetSubscribedBlogsMethod(_repository);

            var user = new ApplicationUser { Id = "1", UserName = "test" };

            _controller.ControllerContext = TestHelpers.FakeControllerContext(true, user.Id, user.UserName, "Admin");

            var result = await _controller.Index() as ViewResult;
            var blogs = result.ViewData.Model as List<BlogApplicationUser>;

            Assert.AreEqual(3, blogs.Count, "Got wrong number of blogs");

        }


        private void SetupMockToReturnThreeBlogsFromGetSubscribedBlogsMethod(Mock<IBlogRepository> repository)
        {
            var user = new ApplicationUser { Id = "1" };

            repository.Setup(mock => mock.GetSubscribedBlogs(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(new List<BlogApplicationUser>()
            {
               new BlogApplicationUser()
               {
                   Blog = new Blog()
                   {

                   }
               },
               new BlogApplicationUser()
               {
                   Blog = new Blog()
                   {

                   }
               },
               new BlogApplicationUser()
               {
                   Blog = new Blog()
                   {

                   }
               },
            });
        }

        private Mock<IBlogRepository> _repository;
        private UserController _controller;
        private Mock<UserManager<ApplicationUser>> _userManagerMock;
    }
}