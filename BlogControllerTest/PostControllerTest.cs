using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Oblig2.Authorization;
using Oblig2.Controllers;
using Oblig2.Models.Entities;
using Oblig2.Models.Repositories;
using Oblig2.Models.ViewModels;
using ProductUnitTest;

namespace ProductionUnitTest
{
    [TestClass]
    public class PostControllerTest
    {
        [TestInitialize]
        public void SetUp()
        {
            _repository = new Mock<IPostRepository>();
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

            _controller = new PostController(_repository.Object, authService);
        }


        [TestCleanup]
        public void CleanUp()
        {

        }

        [TestMethod]
        public async Task Index_ReturnsNotNullResult()
        {
            var result = await _controller.Index(2) as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task Index_ThreePostsWithIdTwo_ExistsInView()
        {
            SetupMockToReturnThreePostsWithBlogIdTwo(_repository);

            var result = await _controller.Index(2) as ViewResult;

            var blog = result.ViewData.Model as Blog;
            var posts = blog.Posts;
           
            Assert.AreEqual(3, posts.Count, "Got wrong number of posts"); 
        }

        [TestMethod]
        public async Task Index_ZeroPosts_ViewIsNotNull()
        {
            SetupMockToReturnZeroPostsFromGetAllMethod(_repository);

            var result = Task.FromResult(await _controller.Index(1) as ViewResult);

            var blog = result.Result.Model as Blog;

            var posts = blog.Posts;
            Assert.AreEqual(0, posts.Count, "Got wrong number of posts");
        }

        [TestMethod]
        public void GetCreate_ReturnsNotNullResult()
        {
            var result = _controller.Create() as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task PostCreate_NewPost_SaveIsCalled()
        {
            await _controller.Create(new Post(), It.IsAny<int>());

            _repository.Verify(mock => mock.Save(It.IsAny<Post>(), null), Times.Once);
        }

        [TestMethod]
        public void Create_Non_AuthorizedUser_ShouldShowLoginView()
        {
            var mockRepo = new Mock<IPostRepository>();
            var controller = new PostController(mockRepo.Object)
            {
                ControllerContext = MockHelpers.FakeControllerContext(false)
            };

            var result = controller.Create() as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsNull(result.ViewName);
        }

        [TestMethod]
        public async Task Create_AuthorizedUser_RedirectActionRedirectsToIndexActionAsync()
        {
            var controller = new PostController(_repository.Object)
            {
                ControllerContext = MockHelpers.FakeControllerContext(false)
            };

            var tempData =
                new TempDataDictionary(controller.ControllerContext.HttpContext, Mock.Of<ITempDataProvider>());
            controller.TempData = tempData;

            var post = new Post()
            {
                Name = "My post",
                Description = "Description of post"
            };

            var result = Task.FromResult(await controller.Create(post, It.IsAny<int>()) as RedirectToActionResult);

            Assert.IsNotNull(result, "RedirectToIndex needs to redirect to the Index action");
            Assert.AreEqual("Index", result.Result.ActionName);
        }

        [TestMethod]
        public async Task CreateViewIsReturnedWhenInputIsNotValidAsync()
        {
            var post = new Post()
            {
                Name = "",
                Description = "",
            };
            var controller = new PostController(_repository.Object);

            var validationContext = new ValidationContext(post, null, null);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(post, validationContext, validationResults, true);
            foreach (var validationResult in validationResults)
                controller.ModelState.AddModelError(validationResult.MemberNames.First(),
                    validationResult.ErrorMessage);

            var result = await controller.Create(post, It.IsAny<int>());

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
        public async Task Details_PostWithAnyId_ExistsInViewModel()
        {
            SetupMockToReturnOnePostViewModelFromGetWithIdMethod(_repository);

            var expectedName = "Name here";
            var expectedDescription = "Description here";

            var result = Task.FromResult(await _controller.Details(It.IsAny<int>()) as ViewResult);

            Assert.IsNotNull(result, "View Result is null");

            var blog = (PostEditViewModel)result.Result.Model;

            Assert.AreEqual(blog.Name, expectedName, "Post names are different");
            Assert.AreEqual(blog.Description, expectedDescription, "Post names are different");
        }

       /* [TestMethod]
        public async Task Edit_AuthorizedUser_EditIsCalled()
        {
            SetupMockToReturnOnePostFromGetPostMethod(_repository);

            var user = new IdentityUser("test") { Id = "1" };

            var fakePost = SetupFakePost();

            _controller.ControllerContext =
                TestHelpers.FakeControllerContext(true, user.Id, user.UserName, "Admin");

            var tempData = new TempDataDictionary(
                _controller.ControllerContext.HttpContext,
                Mock.Of<ITempDataProvider>()
            );

            _controller.TempData = tempData;

            await _controller.Edit(fakePost, fakePost.PostId);

            _repository.Verify(repository => repository.Edit(fakePost), Times.Once);
        } */

        [TestMethod]
        public void DeletePost_NonAuthorizedUser_RedirectsToLoginView()
        {
            var owner = new ApplicationUser {Id = "12345"};

            var user = new ApplicationUser { Id = "1", UserName = "Test"};

            var fakePost = new Post()
            {
                BlogId = 3,
                PostId = 6,
                Name = "Title",
                Description = "Text",
                Owner = owner,
                Blog = new Blog()
                {
                    Name = "name",
                    Description = "desc",
                }
            };

            _repository.Setup(x => x.GetPost(fakePost.PostId)).ReturnsAsync(fakePost);

            _controller.ControllerContext =
                TestHelpers.FakeControllerContext(true, user.Id, user.UserName, "Admins");
            
            var tempData = new TempDataDictionary(
                _controller.ControllerContext.HttpContext,
                Mock.Of<ITempDataProvider>()
                );
            _controller.TempData = tempData;
            var result = _controller.Delete(fakePost.PostId).Result as ChallengeResult;
            Assert.IsNotNull(result, "Should not be null");
        }

        private static void SetupMockToReturnThreePostsWithBlogIdTwo(Mock<IPostRepository> repository)
        {
            repository.Setup(mock => mock.GetAll(2)).ReturnsAsync(new Blog
            {
                Posts = new List<Post>
                {
                    new Post(),
                    new Post(),
                    new Post()
                }
            });
        }

        private static void SetupMockToReturnZeroPostsFromGetAllMethod(Mock<IPostRepository> repository)
        {
            repository.Setup(mock => mock.GetAll(1)).ReturnsAsync(new Blog()
            {
                Posts = new List<Post>()
            });
        }

        private static void SetupMockToReturnOnePostViewModelFromGetWithIdMethod(Mock<IPostRepository> repository)
        {
            repository.Setup(mock => mock.GetPostEditViewModel(It.IsAny<int>())).ReturnsAsync(new PostEditViewModel()
            {
                Name = "Name here",
                Description = "Description here"
            });
        }

        private static void SetupMockToReturnOnePostFromGetPostMethod(Mock<IPostRepository> repository)
        {
            var post = SetupFakePost();
            repository.Setup(mock => mock.GetPost(It.IsAny<int>())).ReturnsAsync(post);
        }

        private static void SetupMockToReturnAuthorizationResultSuccess(Mock<IAuthorizationService> service)
        {
            service.Setup(mock => mock.AuthorizeAsync(
                  It.IsAny<ClaimsPrincipal>(),
                  It.IsAny<Post>(),
                  EntityOperations.Update)).ReturnsAsync(AuthorizationResult.Success);
        }

        private static Post SetupFakePost()
        {
            var owner = new ApplicationUser {Id = "1234"};

            var fakePost = new Post()
            {
                BlogId = 3,
                PostId = 6,
                Name = "Title",
                Description = "Text",
                Owner = owner,
                Blog = new Blog()
                {
                    Name = "name",
                    Description = "desc"
                }
            };

            return fakePost;
        }

        private PostController _controller;
        private Mock<IPostRepository> _repository;
        private Mock<UserManager<ApplicationUser>> _userManagerMock;
        private ClaimsPrincipal _user;

    }
}
