using DVLD_BusinessLayer.DTOS;
using DVLD_DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessLayer.Interfaces
{
    public interface ICountryRepository
    {
        public List<Country> GetAllCountries();
    }
}
