using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using PerfectMatchBack.Controllers;
using PerfectMatchBack.Models;
using PerfectMatchBack.Services.Contract;
using PerfectMatchBack.Services.Implementation;
using PerfectMatchBack.Utilitles;

namespace TestingBackPM
{
    public class TestUser
    {
        private readonly Mock<IUserService> _service;
        private readonly IAccessService _accessService;
        private readonly UserController controller;

        public TestUser()
        {
            _service = new Mock<IUserService>();
            var config = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>());
            IMapper mapper = config.CreateMapper();
            controller = new UserController(mapper, _service.Object, _accessService );
        }


        [Fact]
        public async Task ListUsers()
        {
            _service.Setup(s => s.listUser()).ReturnsAsync(new List<User> { { new User { } } });
            var result = await controller.ListUsers();
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task ListSellers()
        {
            _service.Setup(s => s.listSellers()).ReturnsAsync(new List<User> { { new User { } } });
            var result = await controller.ListSellers();
            Assert.IsType<OkObjectResult>(result);
        }
    }
}