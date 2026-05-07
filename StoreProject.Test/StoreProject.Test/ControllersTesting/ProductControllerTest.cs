using E_Commerce_Proj.Controllers;
using E_Commerce_Proj.DTOs.Product;
using E_Commerce_Proj.DTOs.ProductDTOs;
using E_Commerce_Proj.Reposetories.ProductReposetories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreProject.Test.ControllersTesting
{
    public class ProductControllerTest
    {
        private readonly Mock<IProductRepo> _mockRepo;
        private readonly ProductController _controller;

        public ProductControllerTest()
        {
            _mockRepo = new Mock<IProductRepo>();
            _controller = new ProductController(_mockRepo.Object);
        }

        [Fact]
        public async Task GetProduct_ReturnOk_WhenProductAdded()
        {
            int TestId = 1;
            var ProductDto = new DisplayProductDetailsDTO();
            _mockRepo.Setup(d => d.GetOneProductAsync(TestId)).ReturnsAsync(ProductDto);

            var product = await _controller.GetProduct(TestId);

            Assert.IsType<OkObjectResult>(product);
        }

        [Fact]
        public async Task GetProduct_ReturnNotFound_WhenProducIsNull()
        {
            int TestId = 1;
            _mockRepo.Setup(d => d.GetOneProductAsync(TestId)).ReturnsAsync(null as DisplayProductDetailsDTO);

            var product = await _controller.GetProduct(TestId);

            Assert.IsType<NotFoundResult>(product);
        }

        [Fact]
        public async Task GetAllProduct_ReturnOk_WhenallProductsExists()
        {
            var ProductDto = new List<DisplayProductDTO>
            {
                new DisplayProductDTO
                {
                    Id = 1,
                    Name = "Test",
                    Price = 450.0m
                }
            };
            _mockRepo.Setup(g => g.GetAllProductsAsync()).ReturnsAsync(ProductDto);

            var product = await _controller.GetAllProduct();

            Assert.IsType<OkObjectResult>(product);
        }

        [Fact]
        public async Task GetAllProduct_ReturnNotFound_WhenallProductsAreNull()
        {
            _mockRepo.Setup(g => g.GetAllProductsAsync()).ReturnsAsync((List<DisplayProductDTO>)null);

            var product = await _controller.GetAllProduct();

            Assert.IsType<NotFoundResult>(product);
        }

        [Fact]
        public async Task GetAllProduct_ReturnNotFound_WhenallProductsAreEmpty()
        {
            var emptyList = new List<DisplayProductDTO>();
            _mockRepo.Setup(g => g.GetAllProductsAsync()).ReturnsAsync(emptyList);

            var product = await _controller.GetAllProduct();

            Assert.IsType<NotFoundResult>(product);
        }

        [Fact]
        public async Task AddProduct_ReturnOk_WhenProductAdded()
        {
            var ProductDto = new AddProductDTO();
            string successMessage = "Product added successfully";
            _mockRepo.Setup(a => a.AddProductAsync(ProductDto)).ReturnsAsync(successMessage);

            var result = await _controller.AddProduct(ProductDto);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task AddProduct_ReturnBadRequest_WhenProductNotAdded()
        {
            var ProductDto = new AddProductDTO();
            string errorMessage = "Product not added";
            _mockRepo.Setup(a => a.AddProductAsync(ProductDto)).ReturnsAsync(errorMessage);

            var result = await _controller.AddProduct(ProductDto);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task UpdateProduct_ReturnOk_WhenProductUpdated()
        {
            var ProductDto = new UpdateProductDTO();
            string successMessage = "Product updated successfully.";
            _mockRepo.Setup(u => u.UpdateProductAsync(ProductDto.Id, ProductDto)).ReturnsAsync(successMessage);

            var result = await _controller.UpdateProduct(ProductDto);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task UpdateProduct_ReturnBadResqust_WhenProductNotUpdated()
        {
            var ProductDto = new UpdateProductDTO();
            string errorMessage = "Product not updated";
            _mockRepo.Setup(u => u.UpdateProductAsync(ProductDto.Id, ProductDto)).ReturnsAsync(errorMessage);

            var result = await _controller.UpdateProduct(ProductDto);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task DeleteProduct_ReturnOk_WhenProductDeleted()
        {
            int TestproductId = 1;
            string successMessage = "Product Deleted Successfully";
            _mockRepo.Setup(d => d.DeleteProductAsync(TestproductId)).ReturnsAsync(successMessage);

            var result = await _controller.DeleteProduct(TestproductId);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task DeleteProduct_ReturnBadRequest_WhenProductNotDeleted()
        {
            int TestproductId = 1;
            string errorMessage = "Product Not Deleted";
            _mockRepo.Setup(d => d.DeleteProductAsync(TestproductId)).ReturnsAsync(errorMessage);

            var result = await _controller.DeleteProduct(TestproductId);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task SearchProduct_ReturnOk_WhenProductFound()
        {
            var ProductList = new List <DisplayProductDTO>
            {
                new DisplayProductDTO
                {
                    Name = "Test",
                    Quantity = 59
                }
            };
            string Name = "Test";
            _mockRepo.Setup(s => s.SearchAboutProductAsync(Name)).ReturnsAsync(ProductList);

            var Products = await _controller.SearchProduct(Name);

            Assert.IsType<OkObjectResult>(Products);
        }

        [Fact]
        public async Task SearchProduct_ReturnNotFound_WhenProductIsNull()
        {
            string Name = "Test";
            _mockRepo.Setup(s => s.SearchAboutProductAsync(Name)).ReturnsAsync((List<DisplayProductDTO>)null);

            var Products = await _controller.SearchProduct(Name);

            Assert.IsType<NotFoundObjectResult>(Products);
        }

        [Fact]
        public async Task SearchProduct_ReturnNotFound_WhenProductIsEmpty()
        {
            var emptyList = new List<DisplayProductDTO>();
            string Name = "Test";
            _mockRepo.Setup(s => s.SearchAboutProductAsync(Name)).ReturnsAsync(emptyList);

            var Products = await _controller.SearchProduct(Name);

            Assert.IsType<NotFoundObjectResult>(Products);
        }

        //[Fact]
        //public async Task GetListOfProductCards_ReturnOk_WhenListProductsExists()
        //{
        //    int TestId = 1;
        //    var CardList = new List<DisplayProductCard>
        //    {
        //        new DisplayProductCard
        //        {
        //            Id = 1,
        //            Name = "Test",
        //            Price = 300.0m
        //        }
        //    };
        //    _mockRepo.Setup(g => g.GetProductSliderCategory(TestId)).ReturnsAsync(CardList);

        //    var res = await _controller.GetListOfProductCards(TestId);

        //    Assert.IsType<OkObjectResult>(res);
        //}


        [Fact]
        public async Task GetListOfProductCards_ReturnOk_WhenListProductsExists()
        {
            var CardList = new List<DisplayProductCard>();
            var Cart = new DisplayProductCard();
            CardList.Add(Cart);
            _mockRepo.Setup(g => g.GetProductSliderCategory(Cart.Id)).ReturnsAsync(CardList);

            var res = await _controller.GetListOfProductCards(Cart.Id);

            Assert.IsType<OkObjectResult>(res);
        }

        [Fact]
        public async Task GetProductsOverview_ReturnOk_WhenOverviewExists()
        {
            var Overview = new ProductsOverview();
            _mockRepo.Setup(g => g.GetAllProductsOverViewAsync()).ReturnsAsync(Overview);

            var res = await _controller.GetProductsOverview();

            Assert.IsType<OkObjectResult>(res);
        }
    }
}
