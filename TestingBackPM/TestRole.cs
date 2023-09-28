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
    public class TestRole
    {
        private readonly PetFectMatchContext _context;
        private readonly IRoleService _service;
        private readonly Role _role;
        public TestRole() 
        { 
            _context = new PerfectMatchContext();
            _service = new RoleService(_context);
            _role = new Role();
        }
        [Fact]
        public async Task GetListRoles()
        {
            var result = await _service.listRole();
            var roleResult = Assert.IsType<List<Role>>(result);
            Assert.True(roleResult.Count > 0);
        }
        
    }
}
