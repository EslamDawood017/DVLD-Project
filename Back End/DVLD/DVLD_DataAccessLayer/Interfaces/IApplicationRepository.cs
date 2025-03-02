using DVLD_DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessLayer.Interfaces
{
    public interface IApplicationRepository
    {
        public int CreateNewApplication(Application application);
        public Application GetApplicationById(int ApplicationId);
        public List<Application> GetAllApplications();
        public bool UpdateApplication(Application application);
        public bool DeleteApplication(int applicationID);
        public bool isApplicationExist(int applicationID);
        public int GetActiveApplicationID(int PersonID, int ApplicationTypeID);
        public bool UpdateStatus(int ApplicationId, int statusId);
         
    }
}
