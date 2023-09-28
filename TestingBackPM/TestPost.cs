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
    public class TestPost
    {
        private readonly PetFectMatchContext _context;
        private readonly IPostService _postService;
        private  Publication post;
        public TestPost()
        {
            _context = new PetFectMatchContext();
            _postService = new PostService(_context);
            post = new() { AnimalName = "",IdBreed=2,IdAnimalType=2,Age=0,IdCity=2,IdGender=1,IdOwner=1,Weight=0 };
        }
        [Fact]
        public async Task AddPost()
        {
            var Result = await _postService.addPublication(post);
            var postResult = Assert.IsType<Publication>(Result);
            Assert.True(postResult!=null);
        }
        [Fact]
        public async Task GetPost()
        {
           await GetLastPost();
           var Result = await _postService.GetPublication(post.IdPublication);
            var postResult = Assert.IsType<Publication>(Result);
            Assert.Equal(post.IdPublication, postResult.IdPublication);
        }
        [Fact]
        public async Task GetListPosts()
        {
            var Result = await _postService.listPublication();
            var postResult = Assert.IsType<List<Publication>>(Result);
            Assert.True(postResult.Count>0);
        }
        [Fact]
        public async Task UpdatePost()
        {
            await GetLastPost();
            post.Description= "DescriptionTest";
            var Result = await _postService.updatePublication(post);
            var postResult = Assert.IsType<bool>(Result);
            Assert.True(postResult !=false);
        }
        [Fact]
        public async Task DeletePost()
        {
            await GetLastPost();
            var Result = await _postService.deletePublication(post);
            var postResult = Assert.IsType<bool>(Result);
            Assert.True(postResult!=false);
        }

        internal async Task GetLastPost()
        {
            post = (await _context.Publications.ToListAsync()).LastOrDefault();

        }
    }
}
