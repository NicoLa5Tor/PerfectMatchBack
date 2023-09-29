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
    public class TestImage
    {
        private readonly Mock<IImageService> _service;
        private readonly ImageController controller;

        public TestImage()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>());
            IMapper mapper = config.CreateMapper();
            _service = new Mock<IImageService>();
            controller = new ImageController(mapper, _service.Object);
        }
        [Fact]
        public async Task ListImage()
        {
            var expectedData = new List<Image> { new Image { } };
            _service.Setup(s => s.listImage()).ReturnsAsync(expectedData);
            var result = await controller.ListImage();
            Assert.IsType<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.Equal(200, okResult.StatusCode);

            var listDTO = okResult.Value as List<ImageDTO>;
            Assert.NotNull(listDTO);
            Assert.Equal(expectedData.Count, listDTO.Count);
        }

        [Fact]
        public async Task AddImage()
        {
            var imageToAdd = new Image {  };
            _service.Setup(s => s.addImage(It.IsAny<Image>())).ReturnsAsync(imageToAdd);
            var result = await controller.AddImage(new ImageDTO());
            Assert.IsType<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.Equal(200, okResult.StatusCode);

            var imageDTO = okResult.Value as ImageDTO;
            Assert.NotNull(imageDTO);

        }

        [Fact]
        public async Task UpdateImage()
        {

            var idImage = 1;
            var imageDTO = new ImageDTO { DataImage = "" };
            var existingImage = new Image { IdImage=1 ,DataImage="" };

            _service.Setup(s => s.GetImage(idImage)).ReturnsAsync(existingImage);
            _service.Setup(s => s.Updatemgae(existingImage)).ReturnsAsync(true);

            var result = await controller.UpdateImage(idImage, imageDTO);

            Assert.IsType<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.Equal(200, okResult.StatusCode);

            var resultImageDTO = okResult.Value as ImageDTO;
            Assert.NotNull(resultImageDTO);
        }
        [Fact]
        public async Task DeleteImage()
        {
            _service.Setup(s => s.GetImage(It.IsAny<int>())).ReturnsAsync(new Image());
            _service.Setup(s => s.removeImage(It.IsAny<Image>())).ReturnsAsync(true);


            var result = await controller.DeleteImage(1);

            Assert.IsType<OkResult>(result);
        }


    }
}
