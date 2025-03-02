using DVLD_DataAccessLayer.DTOS;
using DVLD_DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusinessLayer.interfaces
{
    public interface IApplicationService
    {
        public int CreateNewApplication(CreateAppDto app);
        public Application GetApplicationById(int id);
        public List<Application> GetAllApplications();
        public bool UpdateApplication(Application application);
        public bool DeleteApplication(int id);
        public bool isApplicationExist(int id);
        public bool DoesPersonHaveActiveApplication(int PersonID, int ApplicationTypeID);
        public bool UpdateStatus(int ApplicationId, int statusId);

    }
}
