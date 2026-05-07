using E_Commerce_Proj.Abstracts.Feedback;
using E_Commerce_Proj.Controllers;
using E_Commerce_Proj.DTOs.FavouriteDTOs;
using E_Commerce_Proj.DTOs.Product;
using E_Commerce_Proj.Reposetories.FavouriteReposetories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreProject.Test.ControllersTesting
{
    public class FavouriteControllerTest
    {
        private readonly Mock<IFavouriteRepo> _mockRepo;
        private readonly FavouriteController _controller;

        public FavouriteControllerTest()
        {
            _mockRepo = new Mock<IFavouriteRepo>();
            _controller = new FavouriteController(_mockRepo.Object);
        }

        [Fact]
        public async Task GetAllFavouriteList_ReturnOk_WhenFavouriteListExists()
        {
            var ProductDto = new List<DisplayProductDTO>
            {
                new DisplayProductDTO
                {
                    Id = 1,
                    Name = "Test",
                    Quantity =5
                }
            };
            int TestId = 1;
            _mockRepo.Setup(d => d.GetAllFavouriteProductsAsync(TestId)).ReturnsAsync(ProductDto);

            var res = await _controller.GetAllFavouriteList(TestId);

            Assert.IsType<OkObjectResult>(res);
        }

        [Fact]
        public async Task GetAllFavouriteList_ReturnNotFound_WhenFavouriteListIsEmpty()
        {
            int TestId = 1;
            _mockRepo.Setup(d => d.GetAllFavouriteProductsAsync(TestId)).ReturnsAsync((List<DisplayProductDTO>)null);

            var res = await _controller.GetAllFavouriteList(TestId);

            Assert.IsType<NotFoundObjectResult>(res);
        }

        [Fact]
        public async Task AddItemToFavouriteList_ReturnOk_WhenProductAddedToFavouriteList()
        {
            var ItemFavourite = new AddItemToFavouriteList();
            string successMessage = "Product Added To Favourite List";

            _mockRepo.Setup(a => a.AddToFavouriteAsync(ItemFavourite.userId, ItemFavourite.productId))
                     .ReturnsAsync(successMessage);

            var res = await _controller.AddItemToFavouriteList(ItemFavourite);

            Assert.IsType<OkObjectResult>(res);
        }


        [Fact]
        public async Task AddItemToFavouriteList_ReturnNotFound_WhenProductNotAddedToFavouriteList()
        {
            var ItemFavourite = new AddItemToFavouriteList();
            string errorMessage = "Product Not Added To Favourite List";

            _mockRepo.Setup(a => a.AddToFavouriteAsync(ItemFavourite.userId, ItemFavourite.productId))
                     .ReturnsAsync(errorMessage);

            var res = await _controller.AddItemToFavouriteList(ItemFavourite);

            Assert.IsType<NotFoundObjectResult>(res);
        }

        [Fact]
        public async Task RemoveItem_ReturnOk_WhenItemRemoved()
        {
            var FavouriteList = new AddItemToFavouriteList();
            string successMessage = "Done";
            _mockRepo.Setup(a => a.RemoveFromFavouriteAsync(FavouriteList.userId, FavouriteList.productId))
                     .ReturnsAsync(successMessage);

            var res = await _controller.RemoveItem(FavouriteList);

            Assert.IsType<OkObjectResult>(res);
        }

        [Fact]
        public async Task RemoveItem_ReturnNotFound_WhenItemNotRemoved()
        {
            var FavouriteList = new AddItemToFavouriteList();
            string successMessage = "Error!";
            _mockRepo.Setup(a => a.RemoveFromFavouriteAsync(FavouriteList.userId, FavouriteList.productId))
                     .ReturnsAsync(successMessage);

            var res = await _controller.RemoveItem(FavouriteList);

            Assert.IsType<NotFoundObjectResult>(res);
        }

        [Fact]
        public async Task ClearList_ReturnOk_WhenListCleared()
        {
            int TestId = 1;
            string successMessage = "Favourite List Cleared";
            _mockRepo.Setup(c => c.ClearFavouriteListAsync(TestId)).ReturnsAsync(successMessage);

            var res = await _controller.ClearList(TestId);

            Assert.IsType<OkObjectResult>(res);
        }

        [Fact]
        public async Task ClearList_ReturnNotFound_WhenListNotCleared()
        {
            int TestId = 1;
            string successMessage = "Favourite List Not Cleared";
            _mockRepo.Setup(c => c.ClearFavouriteListAsync(TestId)).ReturnsAsync(successMessage);

            var res = await _controller.ClearList(TestId);

            Assert.IsType<NotFoundObjectResult>(res);
        }
    }
}
