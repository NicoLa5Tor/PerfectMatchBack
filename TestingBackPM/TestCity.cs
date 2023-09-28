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
    public class TestCity
    {
        private readonly PerfectMatchContext _context;
        private readonly ICityService _service;

        public TestCity()
        {
            _context = new PerfectMatchContext();
            _service = new CityService(_context);
        }
        [Fact]
        public async Task GetListCity()
        {
            var result = await _service.ListCity();
            var resultCity = Assert.IsType<List<City>>(result);
            Assert.True(resultCity.Count > 0);
        }
    }
}
