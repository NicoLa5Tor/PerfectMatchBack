using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PerfectMatchBack.Controllers;
using PerfectMatchBack.Models;
using PerfectMatchBack.Services.Contract;
using PerfectMatchBack.Services.Implementation;
using PerfectMatchBack.Utilitles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingBackPM
{
    public class TestRole
    {
        private readonly Mock<IRoleService> _service;
        private readonly RoleController controller;

        public TestRole() 
        {
            _service = new Mock<IRoleService>();
            var config = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>());
            IMapper mapper = config.CreateMapper();
            controller = new RoleController(mapper, _service.Object);
        }
        [Fact]
        public async Task ListRoles()
        {
            _service.Setup(s => s.listRole()).ReturnsAsync(new List<Role> { new Role { } }); 
            var result = await controller.ListRoles();
            Assert.IsType<OkObjectResult>(result);
        }

    }
}
