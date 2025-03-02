using DVLD_BusinessLayer.interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DVLD.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountryService _countryService;
        public CountryController(ICountryService countryService)
        {
            _countryService = countryService;
        }
        [HttpGet]
        public IActionResult getAllCountries()
        {
            var countries = _countryService.getAllCountry();

            if (countries == null) 
                return NotFound("there is no Countries");

            return Ok(countries);
        }
    }
}
