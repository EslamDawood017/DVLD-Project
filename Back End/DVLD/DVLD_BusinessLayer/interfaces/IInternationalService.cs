using DVLD_DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusinessLayer.interfaces
{
    public interface IInternationalService
    {
        public int CreateNewInternationalLicense(int LicenseId, int CreatedByUserId);
        public List<InternationalLicense> GetAllInternationalLicenses();
    }
}
