using E_Commerce_Proj.Abstracts.Feedback;
using E_Commerce_Proj.Controllers;
using E_Commerce_Proj.DTOs.Feedback;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreProject.Test.ControllersTesting
{
    public class FeedbackControllerTest
    {
        private readonly Mock<IFeedbackRepo> _mockRepo;
        private readonly FeedbackController _controller;

        public FeedbackControllerTest()
        {
            _mockRepo = new Mock<IFeedbackRepo>();
            _controller = new FeedbackController(_mockRepo.Object);
        }

        [Fact]
        public async Task AddFeed_ReturnOk_WhenFeedbackAdded()
        {
            var FeedbackDto = new AddFeedBackDTO();
            string successMessage = "Feedback Was Added";
            _mockRepo.Setup(a => a.AddFeedbackAsync(FeedbackDto)).ReturnsAsync(successMessage);

            var res = await _controller.AddFeed(FeedbackDto);

            Assert.IsType<OkResult>(res);
        }


        [Fact]
        public async Task AddFeed_ReturnBadRequest_WhenFeedbackIsNull()
        {
            var FeedbackDto = new AddFeedBackDTO();
            _mockRepo.Setup(a => a.AddFeedbackAsync(FeedbackDto)).ReturnsAsync((string)null);

            var res = await _controller.AddFeed(FeedbackDto);

            Assert.IsType<BadRequestObjectResult>(res);
        }

        [Fact]
        public async Task GetAllFeedbacks_ReturnOk_WhenAllFeedbacksExists()
        {
            var Feedbacks = new List<DisplayFeedback>
                {
                new DisplayFeedback
                {
                    Id = 1,
                    Comment = "Test"
                }
            };
            _mockRepo.Setup(g => g.GetAllFeedbacks()).ReturnsAsync(Feedbacks);

            var res = await _controller.GetAllFeedbacks();

            Assert.IsType<OkObjectResult>(res);
        }

        [Fact]
        public async Task GetAllFeedbacks_ReturnNotFound_WhenFeedbacksAreNull()
        {
            _mockRepo.Setup(g => g.GetAllFeedbacks()).ReturnsAsync((List<DisplayFeedback>)null);

            var res = await _controller.GetAllFeedbacks();

            Assert.IsType<NotFoundObjectResult>(res);
        }

        [Fact]
        public async Task DeleteFeed_ReturnOk_whenFeedDeleted()
        {
            int TestId = 1;
            string successMessage = "Feedback Was Deleted";
            _mockRepo.Setup(d => d.DeleteFeedbackAsync(TestId)).ReturnsAsync(successMessage);

            var res = await _controller.DeleteFeed(TestId);

            Assert.IsType<OkObjectResult>(res);
        }

        [Fact]
        public async Task DeleteFeed_ReturnNotFound_whenFeedIsNull()
        {
            int TestId = 1;
            _mockRepo.Setup(d => d.DeleteFeedbackAsync(TestId)).ReturnsAsync((string)null);

            var res = await _controller.DeleteFeed(TestId);

            Assert.IsType<NotFoundObjectResult>(res);
        }


        [Fact]
        public async Task DisplayAllFeedbacksPerOneUser_ReturnOk_WhenAllFeedExists()
        {
            int TestUserId = 1;
            var Feedback = new List<DisplayFeedback>
            { new DisplayFeedback
            {
                Id = TestUserId,
                Comment = "Test"
            }
            };
            _mockRepo.Setup(d => d.DisplayAllFeedbacksFromOneUser(TestUserId)).ReturnsAsync(Feedback);

            var res = await _controller.DisplayAllFeedbacksPerOneUser(TestUserId);

            Assert.IsType<OkObjectResult>(res);
        }


        [Fact]
        public async Task DisplayAllFeedbacksPerOneUser_ReturnNotFound_WhenAllFeedbackeAreNull()
        {
            int TestUserId = 1;
            _mockRepo.Setup(d => d.DisplayAllFeedbacksFromOneUser(TestUserId)).ReturnsAsync((List<DisplayFeedback>)null);

            var res = await _controller.DisplayAllFeedbacksPerOneUser(TestUserId);

            Assert.IsType<NotFoundObjectResult>(res);
        }

    }
}
