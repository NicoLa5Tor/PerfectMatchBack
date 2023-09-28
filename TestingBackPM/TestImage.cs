using Microsoft.EntityFrameworkCore;
using PerfectMatchBack.Models;
using PerfectMatchBack.Services.Contract;
using PerfectMatchBack.Services.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingBackPM
{
    public class TestImage
    {
        private readonly PetFectMatchContext _context;
        private readonly IImageService _service;
        private Image Image;
        public TestImage()
        {
            _context = new PetFectMatchContext();
            _service = new ImageService(_context);
            Image = new() { DataImage  ="test" ,IdPublication=2};
        }
        [Fact]
        public async Task AddImage()
        {
            var result = await _service.addImage(Image);
            var resultImage = Assert.IsType<Image?>(result);
            Assert.True(resultImage != null);
        }
        [Fact]
        public async Task GetImage()
        {
            await GetLastImage();
            var result = await _service.GetImage(Image.IdImage);
            var ImageResult = Assert.IsType<Image>(result);
            Assert.True(ImageResult !=  null);
        }
        [Fact]
        public async Task UpdateImage()
        {
            await GetLastImage();
            var result = await _service.Updatemgae(Image);
            var imageResult = Assert.IsType<bool>(result);
            Assert.True(imageResult != false);

        }
        [Fact]
        public async Task DeleteImage()
        {
            await GetLastImage();
            var result = await _service.removeImage(Image);
            var imageResult = Assert.IsType<bool>(result);
            Assert.True(imageResult != false);

        }

        private async Task GetLastImage()
        {
            Image = (await _context.Images.ToListAsync()).Where(x => x.DataImage=="test").FirstOrDefault();
        }
    }
}
