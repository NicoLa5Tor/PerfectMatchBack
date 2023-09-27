﻿using Microsoft.EntityFrameworkCore;
using PerfectMatchBack.Models;
using PerfectMatchBack.Services.Contract;
using System.Security.Cryptography.Xml;

namespace PerfectMatchBack.Services.Implementation
{
    public class CityService : ICityService
    {
        private PerfectMatchContext _context;
        public CityService(PerfectMatchContext context)
        {
            _context = context; 
        }
        public async Task<List<City>> ListCity()
        {
            try
            {
                var list = await _context.Cities.Include(id => id.IdDepartmentNavigation).ToListAsync();
                return list;    

            }catch (Exception) {
                return new();
            }
        }
    }
}
