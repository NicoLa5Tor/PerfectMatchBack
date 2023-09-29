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
    public class TestAnimalType
    {
        private readonly PetFectMatchContext _context;
        private readonly Mock<IAnimalTypeService> _service;
        private readonly AnimalTypeController controller;

        public TestAnimalType()
        {
            _context = new PetFectMatchContext();
            
            var config = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>());
            IMapper mapper = config.CreateMapper();
            _service = new Mock<IAnimalTypeService>();
            controller = new AnimalTypeController(mapper, _service.Object);
        }

        [Fact]
        public async Task ListAnimalType()
        {
            var expectedData = new List<AnimalType> { new AnimalType {  }, new AnimalType { } };
            _service.Setup(s => s.listAnimalType()).ReturnsAsync(expectedData);

            var result = await controller.ListAnimalType();

            Assert.IsType<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.Equal(200, okResult.StatusCode);

            var listDTO = okResult.Value as List<AnimalTypeDTO>;
            Assert.NotNull(listDTO);
            Assert.Equal(expectedData.Count, listDTO.Count);
        }
    }
}
