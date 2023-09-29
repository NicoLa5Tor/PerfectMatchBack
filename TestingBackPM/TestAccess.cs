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
    public class TestAccess
    {
        private readonly PetFectMatchContext _context;
        private readonly IAccessService _accessService;
        private Access access;
        public TestAccess()
        {
            _context = new PetFectMatchContext();
            _accessService = new AccessService(_context);
            access = new() {Password="test"};
        }

        [Fact]
        public async Task AddAccess()
        {
            var result = await _accessService.createAccess(access);
            var accessTest = Assert.IsType<Access?>(result);
            Assert.True(accessTest != null);
        }
        [Fact]
        public async Task GetAccess()
        {
            await GetTestAccess();
            var result = await _accessService.getAccess(access.IdAccess);
            var accessTest = Assert.IsType<Access?>(result);
            Assert.True(accessTest != null);
        }
        [Fact]
        public async Task UpdateUser()
        {
            await GetTestAccess();
            var result = await _accessService.updateAccess(access);
            var userTest = Assert.IsType<bool>(result);
            Assert.True(userTest == true);
        }
        [Fact]
        public async Task GetListAccess()
        {
            var result = await _accessService.listAccess();
            var accessTest = Assert.IsType<List<Access>?>(result);
            Assert.True(accessTest.Count > 0);
        }
        [Fact]
        public async Task DeleteAccess()
        {
            await GetTestAccess();
            var result = await _accessService.deleteAccess(access);
            var userTest = Assert.IsType<bool>(result);
            Assert.True(userTest != false);
        }
        private async Task GetTestAccess()
        {
            access = (await _context.Accesses.ToListAsync()).Where(x => x.Password == "test").LastOrDefault();
        }
    }
}
