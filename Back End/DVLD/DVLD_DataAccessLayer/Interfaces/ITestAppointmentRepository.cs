using DVLD_DataAccessLayer.DTOS;
using DVLD_DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessLayer.Interfaces
{
    public interface ITestAppointmentRepository
    {
        public TestAppointment GetTestAppointmentInfoById(int TestAppointmentId);
        public TestAppointment GetLastTestAppointment(int LocalDrivingLicenseApplicationID, int TestTypeID);
        public List<GetAllTestAppointmentDto> GetAllTestAppointments();
        public List<GetAppointmentByTestTypeDto> GetApplicationTestAppointmentsPerTestType(int LocalDrivingLicenseApplicationID, int TestTypeID);
        public bool UpdateTestAppointment(TestAppointment testAppointment);
        public int AddNewTestAppointment(TestAppointment testAppointment);
        public bool UpdateTestAppointmentDate(int TestAppointmentID, DateTime newDate);


    }
}
