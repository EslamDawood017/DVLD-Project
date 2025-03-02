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
    public class AppTypeService : IAppTypeService
    {
        private readonly IApplicationTypeRepository _applicationTypeRepository;

        public AppTypeService(IApplicationTypeRepository applicationTypeRepository)
        {
            _applicationTypeRepository = applicationTypeRepository;
        }
        public int AddNewApplicationType(ApplicationType applicationType)
        {
            return _applicationTypeRepository.AddNewApplicationType(applicationType);
        }

        public List<ApplicationType> GetAllApplicationTypes()
        {
            return _applicationTypeRepository.GetAllApplicationTypes();
        }

        public ApplicationType GetApplicationTypeInfoById(int AppId)
        {
            return _applicationTypeRepository.GetApplicationTypeInfoById(AppId);
        }

        public bool UpdateApplicationType(ApplicationType applicationType)
        {
            return _applicationTypeRepository.UpdateApplicationType(applicationType);
        }
    }
}
