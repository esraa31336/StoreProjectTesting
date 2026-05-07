using E_Commerce_Proj.Controllers;
using E_Commerce_Proj.DTOs.OrderDTOs;
using E_Commerce_Proj.Reposetories.OrderReposetories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using storeProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreProject.Tests.ControllerTest
{
    public class OrderControllerTest
    {
        private readonly Mock<IOrderRepo> _mockRepo;
        private readonly OrderController _controller;

        public OrderControllerTest()
        {
            _mockRepo = new Mock<IOrderRepo>();
            _controller = new OrderController(_mockRepo.Object);
        }

        [Fact]
        public async Task CreateOrder_ReturnOk_WhenOrderCreated()
        {
            //Arrange
            var OrderDto = new CreateOrderDTO();
            var OrderDetails = new DisplayOrderDetails();
            _mockRepo.Setup(r => r.CreateOrderAsync(It.IsAny<CreateOrderDTO>())).ReturnsAsync(OrderDetails);

            //Act
            var result = await _controller.CreateOrder(OrderDto);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task CreateOrder_ReturnNull_WhenOrderNotCreated()
        {
            
            var OrderDto = new CreateOrderDTO();
            
            _mockRepo.Setup(r => r.CreateOrderAsync(It.IsAny<CreateOrderDTO>())).ReturnsAsync((DisplayOrderDetails)null); ;

            
            var result = await _controller.CreateOrder(OrderDto);

            
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task DisplayAllOreders_RetunOk_WhenOrdersDisplayed()
        {

            var OrderList = new List<DisplayOrderDetails>
            { new DisplayOrderDetails
            {
                Id = 1, 
                Address = "Fayoum"
            }
            };
            _mockRepo.Setup(d => d.DisplayAllOrdersAsync()).ReturnsAsync(OrderList);


            var result = await _controller.DisplayAllOrders();


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task DisplayAllOreders_RetunNull_WhenOrdersNotDisplayed()
        {
            _mockRepo.Setup(d => d.DisplayAllOrdersAsync()).ReturnsAsync((List<DisplayOrderDetails>)null);


            var result = await _controller.DisplayAllOrders();


            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task DisplayOrderPerUser_RetunOk_WhenOrderDisplayed()
        {
            int TestUserId = 5;
            var orderList = new List<DisplayOrderDetails>
            {
                new DisplayOrderDetails
                {
                    Id = 1,
                    Address = "Cairo"
                }
            };
            _mockRepo.Setup(d => d.DisplayOrdersPerUserAsync(TestUserId)).ReturnsAsync(orderList);


            var result = await _controller.DisplayOrdersPerUser(TestUserId);

            Assert.IsType<OkObjectResult>(result);
            
        }

        [Fact]
        public async Task DisplayOrderPerUser_ReturnNotFound_WhenOrderExit()
        {
            int TestUserId = 5;
            _mockRepo.Setup(d => d.DisplayOrdersPerUserAsync(TestUserId)).ReturnsAsync((List<DisplayOrderDetails>)null);


            var result = await _controller.DisplayOrdersPerUser(TestUserId);


            Assert.IsType<NotFoundObjectResult>(result);

        }



        [Fact]
        public async Task CancelOrder_ReturnOk_WhenOrdersCanceled()
        {
            int TestOrderId = 1;
            string successMessage = "Order Canceled Successfully";
            
            _mockRepo.Setup(o => o.CancelOrderAsync(TestOrderId)).ReturnsAsync(successMessage);

            var result = await _controller.CancelOrder(TestOrderId);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task CancelOrder_ReturnMessage_WhenOrderNotFound()
        {
            int TestOrderId = 1;
            string Message = "Order not found.";

            _mockRepo.Setup(o => o.CancelOrderAsync(TestOrderId)).ReturnsAsync(Message);

            var result = await _controller.CancelOrder(TestOrderId);

            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task OrdersOverview_ReturnOk_WhenOverviewExists()
        {
            var Overview = new OrdersOverviewDTO();

            _mockRepo.Setup(o => o.GetOrdersOverviewAsync()).ReturnsAsync(Overview);

            var result = await _controller.OrdersOverview();


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task DisplayOrderDetails_ReturnOk_WhenOrderExists()
        {
            int TestOrderId = 1;
            var OrderDetails = new DisplayOrderDetails { Id = TestOrderId };

            _mockRepo.Setup(d => d.DisplayOrderDetailsAsync(TestOrderId)).ReturnsAsync(OrderDetails);


            var result = await _controller.DisplayOrderDetails(TestOrderId);


            Assert.IsType<OkObjectResult>(result);
        }


        [Fact]
        public async Task DisplayOrderDetails_ReturnNotFound_WhenOrderDoesNotExists()
        {
            int TestOrderId = 185;
            _mockRepo.Setup(d => d.DisplayOrderDetailsAsync(TestOrderId)).ReturnsAsync((DisplayOrderDetails)null);


            var result = await _controller.DisplayOrderDetails(TestOrderId);


            Assert.IsType<NotFoundObjectResult>(result);
        }

    }
}
