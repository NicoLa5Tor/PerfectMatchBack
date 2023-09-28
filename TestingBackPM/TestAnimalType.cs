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
    public class TestAnimalType
    {
        private readonly PerfectMatchContext _context;
        private readonly IAnimalTypeService _service;
        public TestAnimalType()
        {
            _context = new PerfectMatchContext();
            _service = new AnimalTypeService(_context);
        }
        [Fact]
        public async Task GetListRoles()
        {
            var result = await _service.ListAnimalType();
            var animalTypeResult = Assert.IsType<List<AnimalType>>(result);
            Assert.True(animalTypeResult.Count > 0);
        }
    }
}
