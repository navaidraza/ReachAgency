using ACME.DAL.DTOS;
using ACME.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACME.DAL.Repository
{
    public class Repository : IRepository
    {
        private readonly DatabaseContext _context;
        private Applications _application;

        public Repository(DatabaseContext context)
        {
            _context = context;
        }
        public async Task<List<CountryDTO>> GetCountries()
        {
            return await _context.Country.OrderBy(x=>x.CountryId)
                       .Select(x => new CountryDTO()
                       {
                           countryId = x.CountryId,
                           countryCode = x.CountryCode,
                           countryName=x.CountryName
                       }).ToListAsync();
               
        }
        public async Task<List<string>> GetStates()
        {
            var a = await _context.Postcodes.GroupBy(x => x.State).
               Select(y => y.Key).ToListAsync();

            return a;
        }
        public async Task<int> IsValidPostCode(ApplicationDTO application)
        {
            return await _context.Postcodes.Where(x => x.State == application.state.Trim()
               && x.Pcode == application.postCode.Trim()).Select(y => y.Id).FirstOrDefaultAsync();
             }
        public async Task<bool> Register(ApplicationDTO application)
        {
            _application = new Applications();

            _application.CountryId = application.country.countryId;
            _application.State = application.state;
            _application.PostcodeId = application.postCodeId;
            _application.FullName = application.fullName;
            _application.CreatedDate = DateTime.Now;

            await _context.Applications.AddAsync(_application);
            await _context.SaveChangesAsync();

            return true;
        }


    }
}
