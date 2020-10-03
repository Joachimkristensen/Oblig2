using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Oblig2.Controllers;
using Oblig2.Models.Entities;
using Oblig2.Models.Repositories;
using Oblig2.Models.ViewModels;

namespace ProductionUnitTest
{
    [TestClass]
    public class PostControllerTest
    {
        [TestInitialize]
        public void SetUp()
        {
            _repository = new Mock<IPostRepository>();
            _controller = new PostController(_repository.Object);
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
            //SetupMockToReturnThreePostsWithBlogIdTwo(_repository);

            var result = await _controller.Index(2) as ViewResult;

            var viewModel = result.ViewData.Model as BlogEditViewModel;
            var posts = viewModel.Posts;
           
            Assert.AreEqual(3, posts.Count, "Got wrong number of posts"); 
        } 

     /*   private static void SetupMockToReturnThreePostsWithBlogIdTwo(Mock<IPostRepository> repository)
        {
            repository.Setup(mock => mock.GetAll(2)).Returns(new BlogEditViewModel
            {
                Posts = new List<Post>
                {
                    new Post(),
                    new Post(),
                    new Post()
                }
            });
           
        } */

        private PostController _controller;
        private Mock<IPostRepository> _repository;
    }
}
