using E_Commerce_Proj.Controllers;
using E_Commerce_Proj.DTOs.CartDTOs;
using E_Commerce_Proj.Reposetories.CartReposetories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreProject.Test.ControllersTesting
{
    public class CartControllerTest
    {
        private readonly Mock<ICartRepo> _mockRepo;
        private readonly CartController _controller;

        public CartControllerTest()
        {
            _mockRepo = new Mock<ICartRepo>();
            _controller = new CartController(_mockRepo.Object);
        }

        [Fact]
        public async Task AddItemToCart_ReturnOf_WhenItemAddSuccessfully()
        {
            var ItemDto = new AddCartItemDTO
            {
                ProductId = 1,
                Quantity = 1
            };
            string successMessage = "Item Added To Cart Successfully";

            _mockRepo.Setup(a => a.AddItemToCartAsync(It.IsAny<AddCartItemDTO>())).ReturnsAsync(successMessage);

            var resuilt = await _controller.AddItemToCart(ItemDto);

            Assert.IsType<OkObjectResult>(resuilt);
        }

        [Fact]
        public async Task AddItemToCart_ReturnBadRequest_WhenSomethingWentWrong()
        {
            var itemDto = new AddCartItemDTO();
            string errorMessage = "Something Went Wrong";
            _mockRepo.Setup(a => a.AddItemToCartAsync(It.IsAny<AddCartItemDTO>())).ReturnsAsync(errorMessage);

            var result = await _controller.AddItemToCart(itemDto);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task GetCartItemsPerUser_ReturnOk_WhenItemsExists()
        {
            int TestUserId = 1;
            var CartItems = new List<DisplayCartItemDTO>
                {
                new DisplayCartItemDTO
                {
                    ProductName = "Test",
                    Id = 1,
                    Quentity = 2
                }
            };
            _mockRepo.Setup(g => g.GetCartItemsPerUserAsync(TestUserId)).ReturnsAsync(CartItems);

            var result = await _controller.GetCartItemsPerUser(TestUserId);


            Assert.IsType<OkObjectResult>(result);

        }


        [Fact]
        public async Task ClearCart_ReturnOk_WhenCartItemsCleared()
        {
            int TestUserId = 1;
            string successMessage = "Cart Was Cleared";
            _mockRepo.Setup(c => c.ClearCartAsync(TestUserId)).ReturnsAsync(successMessage);


            var result = await _controller.ClearCart(TestUserId);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task ClearCart_ReturnBadRequest_WhenCartItemsNotCleared()
        {
            int TestUserId = 1;
            string errorMessage = "Something Went Wrong";
            _mockRepo.Setup(c => c.ClearCartAsync(TestUserId)).ReturnsAsync(errorMessage);


            var result = await _controller.ClearCart(TestUserId);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task RemoveItemFromCart_ReturnOk_WhenItemRemoved()
        { 
            string successMessage = "Item Was Removed";
            var ItemDto = new RemoveItemFromCartDTO();
            _mockRepo.Setup( r => r.RemoveItemFromCartAsync(ItemDto.userId, ItemDto.productId))
                     .ReturnsAsync(successMessage);


            var result = await _controller.RemoveItemFromCart(ItemDto);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task RemoveItemFromCart_ReturnBadRequest_WhenNotRemoved()
        {
            var ItemDto = new RemoveItemFromCartDTO();
            string errorMessage = "Something Went Wrong";
            _mockRepo.Setup(r => r.RemoveItemFromCartAsync(ItemDto.userId, ItemDto.productId))
                     .ReturnsAsync(errorMessage);


            var result = await _controller.RemoveItemFromCart(ItemDto);

            Assert.IsType<BadRequestObjectResult>(result);
        }


        [Fact]
        public async Task UpdateItemQuentity_ReturnOk_WhenQuantityUpdated()
        {
            var ItemDto = new AddCartItemDTO();
            string successMessage = "Quantity Was Updated";
            _mockRepo.Setup( u => u.UpdateItemQuentityAsync(It.IsAny<AddCartItemDTO>())).ReturnsAsync(successMessage);

            var result = await _controller.UpdateItemQuentity(ItemDto);

            Assert.IsType<OkObjectResult>(result);
        }


        [Fact]
        public async Task UpdateItemQuentity_ReturnBadRequest_WhenQuantityNotUpdated()
        {
            var ItemDto = new AddCartItemDTO();
            string errorMessage = "Something Went Wrong";
            _mockRepo.Setup(u => u.UpdateItemQuentityAsync(It.IsAny<AddCartItemDTO>())).ReturnsAsync(errorMessage);

            var result = await _controller.UpdateItemQuentity(ItemDto);

            Assert.IsType<BadRequestObjectResult>(result);
        }

    }
}
