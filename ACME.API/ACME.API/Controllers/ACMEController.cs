using ACME.DAL.DTOS;
using ACME.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace ACME.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ACMEController : ControllerBase
    {
       private readonly IRepository _repository;

        public ACMEController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("GetCountries")]
        public async Task<List<CountryDTO>> GetCountries()
        {
            return  await _repository.GetCountries();
        }

        [HttpGet("GetStates")]
        public async Task<List<string>> GetStates()
        {
            return await _repository.GetStates();
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(ApplicationDTO application)
        {
            if (application.country.countryId.Equals(Appconstants.AustralianCountryId))
            {
                //checks & assigns the valid PostCode ID to Applications table which is PK of Postcodes table.
                int validPostCodeId = await _repository.IsValidPostCode(application);
                if (validPostCodeId != 0)
                 application.postCodeId = validPostCodeId;
                else
                    return BadRequest(Appconstants.invalidPostCode);
            }

            if (await _repository.Register(application))
                return StatusCode(Appconstants.httpSuccess);
            else
                return BadRequest(Appconstants.InternalError);


        }
    }
}
