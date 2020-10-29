using System;
using System.Collections.Generic;
using System.Configuration;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Oblig2.Data;
using Oblig2.Models.Entities;
using Oblig2.Models.Repositories;

namespace ProductionUnitTest
{
    [TestClass]
    public class BlogRepositoryTest
    {
        private Mock<IBlogRepository> _repository;
        private Blog _blog;
        private Mock<UserManager<ApplicationUser>> _userManagerMock;


        [TestInitialize]
        public void SetUp()
        {
            _repository = new Mock<IBlogRepository>(_userManagerMock);
            _blog = SetupFakeBlog();
            _userManagerMock = new Mock<UserManager<ApplicationUser>>();

            _userManagerMock = TestHelpers.MockUserManager<ApplicationUser>();
            _userManagerMock.Setup(x => x.GetUserId(It.IsAny<ClaimsPrincipal>())).Returns("1234");
        }

        
       /* [TestMethod]
        public void GetAll_ShouldReturnAllBlogs()
        {
            var blogs = _repository.Object.GetAll();

            Ass

        } */

        
        private static Blog SetupFakeBlog()
        {
            var user = new ApplicationUser{ Id = "1" };

            var owner = new ApplicationUser{ Id = "2" };

            var fakeBlog = new Blog()
            {
                Owner = owner,
                BlogId = 2,
                UserName = "test",
                Name = "test",
                Description = "test",
                Subscribers = new List<BlogApplicationUser>
                {
                    new BlogApplicationUser
                    {
                        User = user
                    }
                }
            };

            return fakeBlog;
        }
    }
}
