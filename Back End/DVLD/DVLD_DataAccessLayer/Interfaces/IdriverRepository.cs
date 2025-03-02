using DVLD_DataAccessLayer.DTOS;
using DVLD_DataAccessLayer.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessLayer.Interfaces
{
    public interface IdriverRepository
    {
        public DriverDto GetDriverInfoByDriverID(int DriverID);
        public DriverDto GetDriverInfoByPersonID(int PersonID);
        public List<DriverDto> GetAllDrivers();
        public int AddNewDriver(int PersonID, int CreatedByUserID);
        public bool UpdateDriver(int DriverID, int PersonID, int CreatedByUserID);
    }
}
