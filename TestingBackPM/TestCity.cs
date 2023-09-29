using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PerfectMatchBack.Controllers;
using PerfectMatchBack.DTOs;
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
    public class TestCity
    {
        private readonly Mock<ICityService> _service;
        private readonly CityController controller;

        public TestCity()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>());
            IMapper mapper = config.CreateMapper();
            _service = new Mock<ICityService>();
            controller = new CityController(mapper, _service.Object);
        }
        [Fact]
        public async Task ListCity_ReturnsOk_WhenServiceReturnsData()
        {
            var expectedData = new List<City> { new City { } };
            _service.Setup(s => s.listCity()).ReturnsAsync(expectedData);

            var result = await controller.ListCity();
            Assert.IsType<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.Equal(200, okResult.StatusCode);

            var listDTO = okResult.Value as List<CityDTO>;
            Assert.NotNull(listDTO);
            Assert.Equal(expectedData.Count, listDTO.Count);
        }

    }
}
