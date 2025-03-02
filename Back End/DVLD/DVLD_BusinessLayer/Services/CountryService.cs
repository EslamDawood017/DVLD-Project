using DVLD_BusinessLayer.interfaces;
using DVLD_DataAccessLayer.Interfaces;
using DVLD_DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusinessLayer.Services
{
    public class CountryService : ICountryService
    {
        private readonly ICountryRepository _countryService;
        public CountryService(ICountryRepository countryRepository)
        {
            _countryService = countryRepository;
        }



        public List<Country> getAllCountry()
        {
            return this._countryService.GetAllCountries();
        }
    }
}
