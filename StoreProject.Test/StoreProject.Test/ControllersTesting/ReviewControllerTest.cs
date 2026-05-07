using E_Commerce_Proj.Controllers;
using E_Commerce_Proj.DTOs.Review;
using E_Commerce_Proj.DTOs.ReviewDTOs;
using E_Commerce_Proj.Reposetories.ReviewReposetories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestPlatform.Common.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreProject.Test.ControllersTesting
{
    public class ReviewControllerTest
    {
        private readonly Mock<IReviewRepo> _mockRepo;
        private readonly ReviewController _controller;

        public ReviewControllerTest()
        {
            _mockRepo = new Mock<IReviewRepo>();
            _controller = new ReviewController(_mockRepo.Object);
        }

        [Fact]
        public async Task AddReview_ReturnOk_WhenReviewAdded()
        {
            var ReviewDto = new AddReviewDTO();
            string successMessage = "Review added successfully";
            _mockRepo.Setup(a => a.AddReviewAsync(ReviewDto.UserId, ReviewDto.ProductId, ReviewDto))
                     .ReturnsAsync(successMessage);

            var result = await _controller.AddReview(ReviewDto);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task AddReview_ReturnBadRequest_WhenReviewNotAdded()
        {
            var ReviewDto = new AddReviewDTO();
            string errorMessage = "Review not added";
            _mockRepo.Setup(a => a.AddReviewAsync(ReviewDto.UserId, ReviewDto.ProductId, ReviewDto))
                     .ReturnsAsync(errorMessage);

            var result = await _controller.AddReview(ReviewDto);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task UpdateReview_ReturnOk_WhenReviewUpdated()
        {
            var ReviewDto = new UpdateReviewDTO();
            string successMessage = "Review updated successfully";
            _mockRepo.Setup(a => a.UpdateReviewAsync(ReviewDto.UserId, ReviewDto.ReviewId, ReviewDto))
                     .ReturnsAsync(successMessage);

            var result = await _controller.UpdateReview(ReviewDto);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task UpdateReview_ReturnBadRequesr_WhenReviewNotUpdated()
        {
            var ReviewDto = new UpdateReviewDTO();
            string errorMessage = "Review not updated";
            _mockRepo.Setup(a => a.UpdateReviewAsync(ReviewDto.UserId, ReviewDto.ReviewId, ReviewDto))
                     .ReturnsAsync(errorMessage);

            var result = await _controller.UpdateReview(ReviewDto);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task DeleteReview_ReturnOk_WhenReviewDeleted()
        {
            int TestId = 1;
            string seccessMessage = "Review deleted successfully";
            _mockRepo.Setup(d => d.DeleteReviewAsync(TestId)).ReturnsAsync(seccessMessage);

            var result = await _controller.DeleteReview(TestId);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task DeleteReview_ReturnBadRequest_WhenReviewNotDeleted()
        {
            int TestId = 1;
            string errorMessage = "Review not deleted";
            _mockRepo.Setup(d => d.DeleteReviewAsync(TestId)).ReturnsAsync(errorMessage);

            var result = await _controller.DeleteReview(TestId);

            Assert.IsType<BadRequestResult>(result);
        }


        [Fact]
        public async Task GetAllReviewsByProductId_ReturnOk_WhenAllReviewsExists()
        {
            int TestId = 1;
            var ReviewDto = new List<DisplayReviewDTO>
            {
                new DisplayReviewDTO
                {
                    Id = TestId,
                    CustomerName = "Test"
                }
            };
            _mockRepo.Setup(g => g.GetAllReviewsByProductIdAsync(TestId)).ReturnsAsync(ReviewDto);


            var result = await _controller.GetAllReviewsByProductId(TestId);


            Assert.IsType<OkObjectResult> (result);
        }

        [Fact]
        public async Task GetAllReviewsByProductId_ReturnNotFound_WhenAllReviewsAreNull()
        {
            int TestId = 1;
            _mockRepo.Setup(g => g.GetAllReviewsByProductIdAsync(TestId)).ReturnsAsync(null as List<DisplayReviewDTO>);


            var result = await _controller.GetAllReviewsByProductId(TestId);


            Assert.IsType<NotFoundObjectResult>(result);
        }

    }
}