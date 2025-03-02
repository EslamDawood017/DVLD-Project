using DVLD_BusinessLayer.interfaces;
using DVLD_DataAccessLayer.DTOS;
using DVLD_DataAccessLayer.Interfaces;
using DVLD_DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusinessLayer.Services
{
    public class TestAppointmentService : ITestAppointmentService
    {
        private readonly ITestAppointmentRepository _testAppointmentRepository;

        public TestAppointmentService(ITestAppointmentRepository testAppointmentRepository)
        {
            _testAppointmentRepository = testAppointmentRepository;
        }
        public int AddNewTestAppointment(TestAppointment testAppointment)
        {
            return _testAppointmentRepository.AddNewTestAppointment(testAppointment);
        }

        public List<GetAppointmentByTestTypeDto> GetApplicationTestAppointmentsPerTestType(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            return _testAppointmentRepository.GetApplicationTestAppointmentsPerTestType(LocalDrivingLicenseApplicationID, TestTypeID);
        }

        public List<GetAllTestAppointmentDto> GetTestAppointmentList()
        {
           return _testAppointmentRepository.GetAllTestAppointments();
        }
        public bool UpdateTestAppointmentDate(int TestAppointmentID, DateTime newDate)
        {
            return _testAppointmentRepository.UpdateTestAppointmentDate(TestAppointmentID, newDate);
        }
        public TestAppointment GetLastTestAppointment(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            return _testAppointmentRepository.GetLastTestAppointment(LocalDrivingLicenseApplicationID , TestTypeID);
        }

    }
}
