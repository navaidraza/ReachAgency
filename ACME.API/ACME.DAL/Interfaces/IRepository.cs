using ACME.DAL.DTOS;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ACME.DAL.Interfaces
{
    public interface IRepository
    {
        Task<List<CountryDTO>> GetCountries();
        Task<List<string>> GetStates();
        Task<bool> Register(ApplicationDTO application);
        Task<int> IsValidPostCode(ApplicationDTO application);
    }
}
