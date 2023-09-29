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
    public class TestGender
    {
        private readonly Mock<IGenderService> _service;
        private readonly GenderController controller;

        public TestGender()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>());
            IMapper mapper = config.CreateMapper();// Puedes crear un mock de IMapper si es necesario.
            _service = new Mock<IGenderService>();
            controller = new GenderController(mapper, _service.Object);
        }

        [Fact]
        public async Task ListGenders()
        {

            var expectedData = new List<Gender> { new Gender { } };
            _service.Setup(s => s.listGender()).ReturnsAsync(expectedData);

            var result = await controller.ListGenders();

            Assert.IsType<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.Equal(200, okResult.StatusCode);

            var listDTO = okResult.Value as List<GenderDTO>;
            Assert.NotNull(listDTO);
            Assert.Equal(expectedData.Count, listDTO.Count);
        }
    }
}
