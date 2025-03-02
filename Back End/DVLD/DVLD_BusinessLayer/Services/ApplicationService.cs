using DVLD_BusinessLayer.interfaces;
using DVLD_BusinessLayer.Mapper;
using DVLD_DataAccessLayer.DTOS;
using DVLD_DataAccessLayer.Interfaces;
using DVLD_DataAccessLayer.Models;
using DVLD_DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusinessLayer.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly IApplicationRepository _applicationRepository;
        private readonly IAppTypeService appTypeService;

        public ApplicationService(IApplicationRepository applicationRepository, IAppTypeService appTypeService)
        {
            _applicationRepository = applicationRepository;
            this.appTypeService = appTypeService;
        }
        public int CreateNewApplication(CreateAppDto app)
        {
            var ApplicationType = appTypeService.GetApplicationTypeInfoById(app.ApplicationTypeId);

            var application = app.DtoToApplication(ApplicationType.ApplicationFees);

            return _applicationRepository.CreateNewApplication(application);

        }

        public bool DeleteApplication(int id)
        {
            return _applicationRepository.DeleteApplication(id);
        }

        public List<Application> GetAllApplications()
        {
            return this._applicationRepository.GetAllApplications();
        }

        public Application GetApplicationById(int id)
        {
          return _applicationRepository.GetApplicationById(id);
        }

        public bool isApplicationExist(int id)
        {
            return _applicationRepository.isApplicationExist(id);
        }

        public bool UpdateApplication(Application application)
        {
            return _applicationRepository.UpdateApplication(application);
        }
        public bool DoesPersonHaveActiveApplication(int PersonID, int ApplicationTypeID)
        {
            return _applicationRepository.GetActiveApplicationID(PersonID, ApplicationTypeID) != -1;
        }
        public bool UpdateStatus(int ApplicationId, int statusId)
        {
            return _applicationRepository.UpdateStatus(ApplicationId, statusId);
        }
    }
}
