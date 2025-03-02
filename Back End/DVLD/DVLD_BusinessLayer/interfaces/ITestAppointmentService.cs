using DVLD_DataAccessLayer.DTOS;
using DVLD_DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusinessLayer.interfaces
{
    public interface ITestAppointmentService
    {
        public TestAppointment GetLastTestAppointment(int LocalDrivingLicenseApplicationID, int TestTypeID);
        public int AddNewTestAppointment(TestAppointment testAppointment);
        public List<GetAllTestAppointmentDto> GetTestAppointmentList();
        public List<GetAppointmentByTestTypeDto> GetApplicationTestAppointmentsPerTestType(int LocalDrivingLicenseApplicationID, int TestTypeID);
        public bool UpdateTestAppointmentDate(int TestAppointmentID, DateTime newDate);
    }
}
