using DVLD_DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusinessLayer.interfaces
{
    public interface IAppTypeService
    {
        public ApplicationType GetApplicationTypeInfoById(int AppId);
        public List<ApplicationType> GetAllApplicationTypes();
        public bool UpdateApplicationType(ApplicationType applicationType);
        public int AddNewApplicationType(ApplicationType applicationType);
    }
}
