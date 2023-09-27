﻿using Microsoft.EntityFrameworkCore;
using PerfectMatchBack.Models;
using PerfectMatchBack.Services.Contract;

namespace PerfectMatchBack.Services.Implementation
{
    public class BreedService : IBreedService
    {
        private PerfectMatchContext _context;
        public BreedService (PerfectMatchContext context)
        {
            _context = context; 
        }

        public async Task<List<Breed>> ListBreed()
        {
            try
            {
                var list = await _context.Breeds.ToListAsync();
                return list;

            }catch (Exception) {
                return new();
            }
        }
    }
}
