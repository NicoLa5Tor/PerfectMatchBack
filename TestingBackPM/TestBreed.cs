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
    public class TestBreed
    {
        private readonly PerfectMatchContext _context;
        private readonly IBreedService _service;
        public TestBreed()
        {
            _context = new PerfectMatchContext();
            _service = new BreedService(_context);
        }
        [Fact]
        public async Task GetListRoles()
        {
            var result = await _service.ListBreed();
            var breedResult = Assert.IsType<List<Breed>>(result);
            Assert.True(breedResult.Count > 0);
        }
    }
}
