using Microsoft.EntityFrameworkCore;
using PerfectMatchBack.Models;
using PerfectMatchBack.Services.Contract;
using PerfectMatchBack.Services.Implementation;

namespace TestingBackPM
{
    public class TestUser
    {
        private readonly PetFectMatchContext _context;
        private readonly IUserService _userService;
        private User user;
        public TestUser()
        {
            _context = new PetFectMatchContext();
            _userService = new UserService(_context);
            user = new() {  IdCity = 1, IdRole = 1, Name = "Test", BirthDate = DateTime.Now, IdAccessNavigation = new Access() { Password = "123" }, Email = "prueba@prueba.com" }; 
        }

        [Fact]
        public async Task AddUserT()
        {
            var result = await _userService.addUser(user);
            var userTest = Assert.IsType<User?>(result);
            Assert.True(userTest != null);
            Assert.Equal(result,user);
        }
        [Fact]
        public async Task GetUser()
        {
            await GetTestUser();
            var result = await _userService.getUser(user.IdUser);
            var userTest = Assert.IsType<User>(result);
            Assert.True(userTest != null);
            Assert.Equal(result?.IdUser, user.IdUser);
        }
        [Fact]
        public async Task UpdateUser()
        {
            await GetTestUser();
            user.Email = "prueba@Test.com";
            var result = await _userService.updateUser(user);
            var userTest = Assert.IsType<bool>(result);
            Assert.True(userTest == true);
        }
        [Fact]
        public async Task GetListUsers()
        {
            var result = await _userService.listUser();
            var userTest = Assert.IsType<List<User>>(result);
            Assert.True(userTest.Count > 0);
        }
        [Fact]
        public async Task DeleteUser()
        {
            await GetTestUser();
            var result = await _userService.deleteUser(user);
            var userTest = Assert.IsType<bool>(result);
            Assert.True(userTest != false);
        }
        private async Task GetTestUser()
        {
            user = ( await _context.Users.ToListAsync()).Where(x=>x.Name=="Test").LastOrDefault();
        }
    }
}