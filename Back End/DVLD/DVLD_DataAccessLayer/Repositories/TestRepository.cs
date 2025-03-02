using DVLD_DataAccessLayer.DTOS;
using DVLD_DataAccessLayer.Interfaces;
using DVLD_DataAccessLayer.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Azure.Core.HttpHeader;

namespace DVLD_DataAccessLayer.Repositories
{
    public class TestRepository : ITestRepository
    {
        private readonly IConfiguration _configuration;
        private string ConnectionString = "";
        public TestRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            ConnectionString = _configuration.GetConnectionString("Default");
        }
        public Test GetTestInfoByID(int TestID)
        {
            Test test = null;

            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = "SELECT * FROM Tests WHERE TestID = @TestID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestID", TestID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    // The record was found;
                    test = new Test
                    {
                        TestAppointmentID = (int)reader["TestAppointmentID"],
                        CreatedByUserID = (int)reader["CreatedByUserID"],
                        TestResult = (int)reader["TestResult"],
                        Notes = (reader["Notes"] == DBNull.Value) ? "" : (string)reader["Notes"]

                    };   
                }
                else
                {
                    // The record was not found
                    test = null;
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                test = null;
            }
            finally
            {
                connection.Close();
            }

            return test;
        }
        public TestDto GetLastTestByPersonAndTestTypeAndLicenseClass(int PersonID, int LicenseClassID, int TestTypeID)
        {
            

            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = @"SELECT  top 1 Tests.TestID, 
                Tests.TestAppointmentID, Tests.TestResult, 
			    Tests.Notes, Tests.CreatedByUserID, Applications.ApplicantPersonID
                FROM            LocalDrivingLicenseApplications INNER JOIN
                                         Tests INNER JOIN
                                         TestAppointments ON Tests.TestAppointmentID = TestAppointments.TestAppointmentID ON LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID INNER JOIN
                                         Applications ON LocalDrivingLicenseApplications.ApplicationID = Applications.ApplicationID
                WHERE        (Applications.ApplicantPersonID = @PersonID) 
                        AND (LocalDrivingLicenseApplications.LicenseClassID = @LicenseClassID)
                        AND ( TestAppointments.TestTypeID=@TestTypeID)
                ORDER BY Tests.TestAppointmentID DESC";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            TestDto testdto = null;

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    testdto = new TestDto
                    {
                        TestID = (int)reader["TestID"],
                        TestAppointmentID = (int)reader["TestAppointmentID"],
                        TestResult = (bool)reader["TestResult"],
                        Notes = (reader["Notes"] == DBNull.Value) ? "" : (string)reader["Notes"],
                        CreatedByUserID = (int)reader["CreatedByUserID"], 
                        ApplicantPersonID = (int)reader["ApplicantPersonID"]
                    };

                }
                else
                {
                    testdto = null; 
                }

                reader.Close();


            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                testdto = null;
            }
            finally
            {
                connection.Close();
            }

            return testdto;
        }
        public List<Test> GetAllTests()
        {
           List<Test> tests = new List<Test>();

            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = "SELECT * FROM Tests order by TestID";

            SqlCommand command = new SqlCommand(query, connection);
            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    tests.Add(new Test
                    {
                        TestID = (int)reader["TestID"],
                        CreatedByUserID = (int)reader["CreatedByUserID"],
                        TestResult = (int)reader["TestResult"], 
                        TestAppointmentID = (int)reader["TestAppointmentID"],
                        Notes = (reader["Notes"] == DBNull.Value) ? "" : (string)reader["Notes"]

                    });
                }

                reader.Close();


            }

            catch (Exception ex)
            {
                // Console.WriteLine("Error: " + ex.Message);
                tests = null;
            }
            finally
            {
                connection.Close();
            }

            return tests;
        }
        public int AddNewTest(Test newTest)
        {
            int TestID = -1;

            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = @"Insert Into Tests (TestAppointmentID,TestResult,
                                                Notes,   CreatedByUserID)
                            Values (@TestAppointmentID,@TestResult,
                                                @Notes,   @CreatedByUserID);
                            
                                UPDATE TestAppointments 
                                SET IsLocked=1 where TestAppointmentID = @TestAppointmentID;

                                SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestAppointmentID", newTest.TestAppointmentID);
            command.Parameters.AddWithValue("@TestResult", newTest.TestResult);

            if (newTest.Notes != "" && newTest.Notes != null)
                command.Parameters.AddWithValue("@Notes", newTest.Notes);
            else
                command.Parameters.AddWithValue("@Notes", System.DBNull.Value);



            command.Parameters.AddWithValue("@CreatedByUserID", newTest.CreatedByUserID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    TestID = insertedID;
                }
            }

            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);

            }

            finally
            {
                connection.Close();
            }


            return TestID;

        }
        public bool UpdateTest(Test updatedTest)
        {

            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = @"Update  Tests  
                            set TestAppointmentID = @TestAppointmentID,
                                TestResult=@TestResult,
                                Notes = @Notes,
                                CreatedByUserID=@CreatedByUserID
                                where TestID = @TestID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestID", updatedTest.TestID);
            command.Parameters.AddWithValue("@TestAppointmentID", updatedTest.TestAppointmentID);
            command.Parameters.AddWithValue("@TestResult", updatedTest.TestResult);
            if (string.IsNullOrEmpty(updatedTest.Notes))
                command.Parameters.AddWithValue("@Notes", DBNull.Value);
            else
                command.Parameters.AddWithValue("@Notes", updatedTest.Notes);

            command.Parameters.AddWithValue("@CreatedByUserID", updatedTest.CreatedByUserID);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                return false;
            }

            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }
        public byte GetPassedTestCount(int LocalDrivingLicenseApplicationID)
        {
            byte PassedTestCount = 0;

            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = @"SELECT PassedTestCount = count(TestTypeID)
                         FROM Tests INNER JOIN
                         TestAppointments ON Tests.TestAppointmentID = TestAppointments.TestAppointmentID
						 where LocalDrivingLicenseApplicationID =@LocalDrivingLicenseApplicationID and TestResult=1";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);


            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && byte.TryParse(result.ToString(), out byte ptCount))
                {
                    PassedTestCount = ptCount;
                }
            }

            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);

            }

            finally
            {
                connection.Close();
            }

            return PassedTestCount;
        }
    
    }
}
