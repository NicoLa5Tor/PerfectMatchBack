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
    public class TestBreed
    {
        private readonly Mock<IBreedService> _service;
        private readonly BreedController controller;

        public TestBreed()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>());
            IMapper mapper = config.CreateMapper();
            _service = new Mock<IBreedService>();
            controller = new BreedController(mapper, _service.Object);
        }
        [Fact]
        public async Task ListBreed()
        {

            // Configura el comportamiento del servicio mock para devolver datos simulados
            var expectedData = new List<Breed> { new Breed { } };
            _service.Setup(s => s.listBreed()).ReturnsAsync(expectedData);

            var result = await controller.ListBreed();

            Assert.IsType<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.Equal(200, okResult.StatusCode);

            var listDTO = okResult.Value as List<BreedDTO>;
            Assert.NotNull(listDTO);
            Assert.Equal(expectedData.Count, listDTO.Count);
        }
    }
}

