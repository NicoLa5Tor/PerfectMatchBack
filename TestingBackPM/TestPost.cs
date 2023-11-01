using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public class TestPost
    {
        private readonly Mock<IPostService> _service;
        private readonly PublicationController controller;
        public TestPost()
        {
            _service = new Mock<IPostService>();
            var config = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>());
            IMapper mapper = config.CreateMapper();// Puedes crear un mock de IMapper si es necesario.
            controller = new PublicationController(mapper, _service.Object);
        }
        [Fact]
        public async Task ListPublications()
        {
            _service.Setup(s => s.ListPublication()).ReturnsAsync(new List<Publication> { new Publication { } });
            var result = await controller.ListPublications();

            Assert.IsType<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.Equal(200, okResult.StatusCode);

            var listDTO = okResult.Value as List<PublicationDTO>;
            Assert.NotNull(listDTO);
        }
        [Fact]
        public async Task AddPublication_ReturnsOk_WhenServiceAddsPublication()
        {

            var expectedPublication = new Publication { IdPublication = 1 }; 
            _service.Setup(s => s.AddPublication(It.IsAny<Publication>())).ReturnsAsync(expectedPublication);

            var model = new PublicationDTO(); 
            var result = await controller.AddPublication(model);

            Assert.IsType<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.Equal(200, okResult.StatusCode);

            var publicationDTO = okResult.Value as PublicationDTO;
            Assert.NotNull(publicationDTO);
            Assert.Equal(expectedPublication.IdPublication, publicationDTO.IdPublication);
        }
        [Fact]
        public async Task UpdatePublication()
        {
            _service.Setup(s => s.GetPublication(It.IsAny<int>())).ReturnsAsync(new Publication());
            _service.Setup(s => s.UpdatePublication(It.IsAny<Publication>())).ReturnsAsync(true);
            var model = new PublicationDTO();
            var result = await controller.UpdatePublication(1, model);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task DeletePublication()
        {
            _service.Setup(s => s.GetPublication(It.IsAny<int>())).ReturnsAsync(new Publication());
            _service.Setup(s => s.DeletePublication(It.IsAny<Publication>())).ReturnsAsync(true);
            var result = await controller.DeletePublication(1, _service.Object, null);
            Assert.IsType<OkResult>(result);
        }


    }
}
