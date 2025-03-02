using DVLD_BusinessLayer.interfaces;
using DVLD_DataAccessLayer.DTOS;
using DVLD_DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusinessLayer.Services
{
    public class DriverService : IDriverService
    {
        private readonly IdriverRepository _driverRepository;

        public DriverService(IdriverRepository driverRepository)
        {
            _driverRepository = driverRepository;
        }
        public int AddNewDriver(int PersonID, int CreatedByUserID)
        {
            return _driverRepository.AddNewDriver(PersonID, CreatedByUserID);
        }

        public List<DriverDto> GetAllDrivers()
        {
           return _driverRepository.GetAllDrivers();
        }

        public DriverDto GetDriverInfoByDriverID(int DriverID)
        {
            return _driverRepository.GetDriverInfoByDriverID(DriverID);
        }

        public DriverDto GetDriverInfoByPersonID(int PersonID)
        {
            return _driverRepository.GetDriverInfoByPersonID(PersonID);
        }

        public bool UpdateDriver(int DriverID, int PersonID, int CreatedByUserID)
        {
            return _driverRepository.UpdateDriver(DriverID, PersonID, CreatedByUserID);
        }
    }
}
