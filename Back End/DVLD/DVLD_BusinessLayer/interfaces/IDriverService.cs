using DVLD_DataAccessLayer.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusinessLayer.interfaces
{
    public interface IDriverService
    {
        public DriverDto GetDriverInfoByDriverID(int DriverID);
        public DriverDto GetDriverInfoByPersonID(int PersonID);
        public List<DriverDto> GetAllDrivers();
        public int AddNewDriver(int PersonID, int CreatedByUserID);
        public bool UpdateDriver(int DriverID, int PersonID, int CreatedByUserID);
    }
}
