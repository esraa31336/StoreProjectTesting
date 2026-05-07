using E_Commerce_Proj.Controllers;
using E_Commerce_Proj.DTOs.CategoryDTOs;
using E_Commerce_Proj.Reposetories.CategoryReposetories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreProject.Test.ControllersTesting
{
    public class CategoryControllerTest
    {
        private readonly Mock<ICategoryRepo> _mockRepo;
        private readonly CategoryController _controller;

       public CategoryControllerTest()
        {
            _mockRepo = new Mock<ICategoryRepo>();
            _controller = new CategoryController(_mockRepo.Object);
        }


        [Fact]
        public async Task AddCategory_ReturnOk_WhenCategoryAdded()
        {
            var CategoryName = new AddCategoryDTO();
            string successMessage = "Category Added Successfully";
            _mockRepo.Setup( a => a.AddCategoryAsync(It.IsAny<AddCategoryDTO>())).ReturnsAsync(successMessage);


            var result = await _controller.AddCategory(CategoryName);


            Assert.IsType<OkObjectResult>(result);

        }

        [Fact]
        public async Task AddCategory_ReturnBadRequest_WhenCategoryNotAdded()
        {
            var CategoryName = new AddCategoryDTO();
            string successMessage = "Category Nt Added";
            _mockRepo.Setup(a => a.AddCategoryAsync(It.IsAny<AddCategoryDTO>())).ReturnsAsync(successMessage);


            var result = await _controller.AddCategory(CategoryName);


            Assert.IsType<BadRequestObjectResult>(result);
        }


        [Fact]
        public async Task GetOneCategory_ReturnOk_WhenOneCategoryGetten()
        {
            var CategoryName = new DisplayCategoryDTO();
            int TestId = 1;
            _mockRepo.Setup(g => g.GetOneCategoryProductsAsync(TestId)).ReturnsAsync(CategoryName);

            var category = await _controller.GetOneCategory(TestId);


            Assert.IsType<OkObjectResult>(category);
        }


        [Fact]
        public async Task GetOneCategory_ReturnNotFound_WhenNotCategoryGetten()
        {
            int TestId = 1;
            _mockRepo.Setup(g => g.GetOneCategoryProductsAsync(TestId)).ReturnsAsync((DisplayCategoryDTO)null);

            var category = await _controller.GetOneCategory(TestId);


            Assert.IsType<NotFoundResult>(category);
        }

        [Fact]
        public async Task GettAllCategories_ReturnOk_WhenCaregoriesExists()
        {
            var CategoriesList = new List<DisplayCategoryDTO>
            {
                new DisplayCategoryDTO {Id = 1, CategoryName = "Clothes"}
            };
            _mockRepo.Setup(d => d.GetAllCategoriesProductsAsync()).ReturnsAsync(CategoriesList);


            var categories = await _controller.GetAllCategories();

            Assert.IsType<OkObjectResult>(categories);
        }

        [Fact]
        public async Task GettAllCategories_ReturnNotFound_WhenCaregoriesIsNull()
        {
           
            _mockRepo.Setup(d => d.GetAllCategoriesProductsAsync()).ReturnsAsync((List<DisplayCategoryDTO>)null);


            var categories = await _controller.GetAllCategories();

            Assert.IsType<NotFoundResult>(categories);
        }

        [Fact]
        public async Task GettAllCategories_ReturnNotFound_WhenCaregoriesIsEmpty()
        {
            var EmptyList = new List<DisplayCategoryDTO>();
            _mockRepo.Setup(d => d.GetAllCategoriesProductsAsync()).ReturnsAsync(EmptyList);


            var categories = await _controller.GetAllCategories();

            Assert.IsType<NotFoundResult>(categories);
        }


        [Fact]
        public async Task UpdateCategory_ReturnOk_WhenCategoryUpdated()
        {
            var categoryDto = new DisplayCategoryDTO();
            var Update = new UpdateCategoryDTO();
            _mockRepo.Setup(d => d.UpdateCategoryAsync(Update.id, Update)).ReturnsAsync(categoryDto);

            var category = await _controller.UpdateCategory(Update);

            Assert.IsType<OkObjectResult>(category);
        }


        [Fact]
        public async Task UpdateCategory_ReturnNotFound_WhenCategoryIsNull()
        {
            var Update = new UpdateCategoryDTO();
            _mockRepo.Setup(d => d.UpdateCategoryAsync(Update.id, Update)).ReturnsAsync((DisplayCategoryDTO)null);

            var category = await _controller.UpdateCategory(Update);

            Assert.IsType<NotFoundResult>(category);
        }

        [Fact]
        public async Task DeleteCategory_ReturnOk_WhenCategoryDeleted()
        {
            int TestId = 1;
            string successMessage = "Category Was Deleted";
            _mockRepo.Setup(d => d.DeleteCategoryAsync(TestId)).ReturnsAsync(successMessage);

            var result = await _controller.DeleteCategory(TestId);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task DeleteCategory_ReturnNotFound_WhenCategoryNotFound()
        {
            int TestId = 1;
            string errorMessage = "Category Not Found";
            _mockRepo.Setup(d => d.DeleteCategoryAsync(TestId)).ReturnsAsync(errorMessage);

            var result = await _controller.DeleteCategory(TestId);

            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task DeleteCategory_ReturnNotFound_WhenCategoryIsEmpty()
        {
            int TestId = 1;
            string errorMessage = "Category Is Empty";
            _mockRepo.Setup(d => d.DeleteCategoryAsync(TestId)).ReturnsAsync(errorMessage);

            var result = await _controller.DeleteCategory(TestId);

            Assert.IsType<NotFoundObjectResult>(result);
        }


        [Fact]
        public async Task GetAllCategoriesNames_ReturnOk_WhenCategoriesNamesExists()
        {
            var CategoriesNamesDto = new List<DisplayCategoriesNamesDTO>
            {
                 new DisplayCategoriesNamesDTO
                {
                    Id = 1,
                    Name = "Test"

                }
            };
            _mockRepo.Setup(d => d.GetCategoriesNamesAsync()).ReturnsAsync(CategoriesNamesDto);


            var result = await _controller.GetAllCategoriesNames();

            Assert.IsType<OkObjectResult>(result);
        }


        [Fact]
        public async Task GetAllCategoriesNames_ReturnNotFound_WhenCategoriesNamesNotFound()
        {  
            _mockRepo.Setup(d => d.GetCategoriesNamesAsync()).ReturnsAsync((List<DisplayCategoriesNamesDTO>)null);


            var result = await _controller.GetAllCategoriesNames();

            Assert.IsType<NotFoundObjectResult>(result);
        }
    }
}
