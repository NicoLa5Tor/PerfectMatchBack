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
    public class TestGender
    {
        private readonly PerfectMatchContext _context;
        private readonly IGenderService _service;
        public TestGender()
        {
            _context = new PerfectMatchContext();
            _service = new GenderService(_context);
        }
        [Fact]
        public async Task GetListGender()
        {
            var result = await _service.ListGender();
            var resultGender = Assert.IsType<List<Gender>>(result);
            Assert.True(resultGender.Count>0);
        }
    }
}
