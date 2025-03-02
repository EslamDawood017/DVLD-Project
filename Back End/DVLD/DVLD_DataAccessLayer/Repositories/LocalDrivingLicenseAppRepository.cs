using DVLD_DataAccessLayer.DTOS;
using DVLD_DataAccessLayer.Interfaces;
using DVLD_DataAccessLayer.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using static System.Net.Mime.MediaTypeNames;


namespace DVLD_DataAccessLayer.Repositories
{
    public class LocalDrivingLicenseAppRepository : ILocalDrivingLicenseApplicationRepository
    {

        private readonly IConfiguration _configuration;
        private string ConnectionString = "";
        public LocalDrivingLicenseAppRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            ConnectionString = _configuration.GetConnectionString("Default");
        }
        public int AddLocalDrivingLicenseApplication(LocalDrivingLicenseApplications application)
        {
            int ApplicationID = -1;
            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = @"INSERT INTO [dbo].[LocalDrivingLicenseApplications]
                            ([ApplicationID]
                            ,[LicenseClassID])
                     VALUES
                            (@ApplicationID
                            ,@LicenseClassID);
                        SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationID", application.ApplicationID);
            command.Parameters.AddWithValue("@LicenseClassID", application.LicenseClassID);
            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    ApplicationID = insertedID;
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


            return ApplicationID;
        }
        public bool IsThereAnActiveApplication(int PersonId, int licenseClassId)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = @"select top 1 found = 1  from  Applications inner join LocalDrivingLicenseApplications 
                on Applications.ApplicationID = LocalDrivingLicenseApplications.ApplicationID 
                where ApplicantPersonID = @ApplicantPersonID and LicenseClassID = @LicenseClassID and  ApplicationStatus = 1 ;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicantPersonID", PersonId);
            command.Parameters.AddWithValue("@LicenseClassID", licenseClassId);
            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                isFound = reader.HasRows;

                reader.Close();
            }
            catch
            {
                isFound = false;
            }
            finally
            {
                connection.Close();
            }
            return isFound;
        }
        public LocalDrivingLicenseApplications GetLocalDrivingLicenseApplicationsById(int LocalDrivingLicenseApplicationID)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);

            LocalDrivingLicenseApplications applications = new LocalDrivingLicenseApplications();

            string query = "SELECT * FROM LocalDrivingLicenseApplications WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    // The record was found
                    applications.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
                    applications.ApplicationID = (int)reader["ApplicationID"];
                    applications.LicenseClassID = (int)reader["LicenseClassID"];
                }
                else
                {
                    // The record was not found
                    applications = null;
                }

                reader.Close();


            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                applications = null;
            }
            finally
            {
                connection.Close();
            }

            return applications;
        }
        public LocalDrivingLicenseApplications GetLocalDrivingLicenseApplicationsByApplicationId(int ApplicationID)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);

            LocalDrivingLicenseApplications applications = new LocalDrivingLicenseApplications();

            string query = "SELECT * FROM LocalDrivingLicenseApplications WHERE ApplicationID = @ApplicationID";
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    // The record was found

                    applications.ApplicationID = (int)reader["ApplicationID"];
                    applications.LicenseClassID = (int)reader["LicenseClassID"];



                }
                else
                {
                    // The record was not found
                    applications = null;
                }

                reader.Close();


            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                applications = null;
            }
            finally
            {
                connection.Close();
            }

            return applications;
        }
        public List<getLocalDrivingLicenseApplicationDto> GetAllLocalDrivingLicenseApplications()
        {
            List<getLocalDrivingLicenseApplicationDto> applications = new List<getLocalDrivingLicenseApplicationDto>();

            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = @"SELECT *
                              FROM LocalDrivingLicenseApplications_View
                              order by ApplicationDate Desc";




            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();


                while (reader.Read())
                {
                    var app = new getLocalDrivingLicenseApplicationDto
                    {
                        LocalDrivingLicenseApplicationID = (int)reader["LocalDrivingLicenseApplicationID"],
                        NationalNo = (string)reader["NationalNo"],
                        ApplicationDate = (DateTime)reader["ApplicationDate"],
                        ClassName = (string)reader["ClassName"],
                        FullName = (string)reader["FullName"],
                        Status = (string)reader["Status"],
                        PassedTestCount = (int)reader["PassedTestCount"]
                    };

                    applications.Add(app);
                }

                reader.Close();


            }

            catch (Exception ex)
            {
                applications = null;
            }
            finally
            {
                connection.Close();
            }

            return applications;
        }
        public bool UpdateLocalDrivingLicenseApplication(LocalDrivingLicenseApplications application)
        {

            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = @"Update  LocalDrivingLicenseApplications  
                            set ApplicationID = @ApplicationID,
                                LicenseClassID = @LicenseClassID
                            where LocalDrivingLicenseApplicationID=@LocalDrivingLicenseApplicationID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", application.LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("ApplicationID", application.ApplicationID);
            command.Parameters.AddWithValue("LicenseClassID", application.LicenseClassID);


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
        public bool DeleteLocalDrivingLicenseApplication(int LocalDrivingLicenseApplicationID)
        {

            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = @"Delete LocalDrivingLicenseApplications 
                                where LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

            try
            {
                connection.Open();

                rowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                // Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {

                connection.Close();

            }

            return (rowsAffected > 0);

        }
        public bool DoesPassTestType(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {


            bool Result = false;

            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = @" SELECT top 1 TestResult
                            FROM LocalDrivingLicenseApplications INNER JOIN
                                 TestAppointments ON LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID INNER JOIN
                                 Tests ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID
                            WHERE
                            (LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID) 
                            AND(TestAppointments.TestTypeID = @TestTypeID)
                            ORDER BY TestAppointments.TestAppointmentID desc";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && bool.TryParse(result.ToString(), out bool returnedResult))
                {
                    Result = returnedResult;
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

            return Result;

        }
        public bool DoesAttendTestType(int LocalDrivingLicenseApplicationID, int TestTypeID)

        {


            bool IsFound = false;

            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = @" SELECT top 1 Found=1
                            FROM LocalDrivingLicenseApplications INNER JOIN
                                 TestAppointments ON LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID INNER JOIN
                                 Tests ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID
                            WHERE
                            (LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID) 
                            AND(TestAppointments.TestTypeID = @TestTypeID)
                            ORDER BY TestAppointments.TestAppointmentID desc";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null)
                {
                    IsFound = true;
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

            return IsFound;

        }
        public byte TotalTrialsPerTest(int LocalDrivingLicenseApplicationID, int TestTypeID)

        {


            byte TotalTrialsPerTest = 0;

            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = @" SELECT TotalTrialsPerTest = count(TestID)
                            FROM LocalDrivingLicenseApplications INNER JOIN
                                 TestAppointments ON LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID INNER JOIN
                                 Tests ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID
                            WHERE
                            (LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID) 
                            AND(TestAppointments.TestTypeID = @TestTypeID)
                       ";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && byte.TryParse(result.ToString(), out byte Trials))
                {
                    TotalTrialsPerTest = Trials;
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

            return TotalTrialsPerTest;

        }
        public bool IsThereAnActiveScheduledTest(int LocalDrivingLicenseApplicationID, int TestTypeID)

        {

            bool Result = false;

            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = @" SELECT top 1 Found=1
                            FROM LocalDrivingLicenseApplications INNER JOIN
                                 TestAppointments ON LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID 
                            WHERE
                            (LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID)  
                            AND(TestAppointments.TestTypeID = @TestTypeID) and isLocked=0
                            ORDER BY TestAppointments.TestAppointmentID desc";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();


                if (result != null)
                {
                    Result = true;
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

            return Result;

        }

        public int GetPersonIdForLocalDrivingLicense(int LocalDrivingLicenseApplicationID)
        {
            int PersonId = -1;

            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = @"select People.PersonID from 
                                LocalDrivingLicenseApplications inner join Applications
                                on
                                LocalDrivingLicenseApplications.ApplicationID = Applications.ApplicationID
                                inner join People on Applications.ApplicantPersonID = People.PersonID 
                                where LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID ;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    PersonId = insertedID;
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

            return PersonId;
        }

        public getApplicationDto GetApplicationInfoByLocalDrivingLicenseId(int LocalDrivingLicesneAppId)
        {
            

            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = @"SELECT 
                            Applications.ApplicationID,
                            Applications.ApplicationDate,
                            Applications.LastStatusDate,
                            Applications.PaidFees,
                            ApplicationTypes.ApplicationTypeTitle,
                            Users.UserName,
                            CASE
                                WHEN Applications.ApplicationStatus = 1 THEN 'New'
                                WHEN Applications.ApplicationStatus = 2 THEN 'Cancelled'
                                WHEN Applications.ApplicationStatus = 3 THEN 'Completed'
                              END AS Status
                            FROM 
                              LocalDrivingLicenseApplications
                            INNER JOIN Applications ON LocalDrivingLicenseApplications.ApplicationID = Applications.ApplicationID
                            INNER JOIN ApplicationTypes ON Applications.ApplicationTypeID = ApplicationTypes.ApplicationTypeID
                            INNER JOIN Users ON Applications.CreatedByUserID = Users.UserID
                            WHERE 
                              LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID;";




            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicesneAppId);

            getApplicationDto application = null;

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                

                while (reader.Read())
                {
                    application = new getApplicationDto
                    {
                        ApplicationID = (int)reader["ApplicationID"],
                        ApplicationTypeTitle = (string)reader["ApplicationTypeTitle"],
                        ApplicationDate = (DateTime)reader["ApplicationDate"],
                        LastStatusDate = (DateTime)reader["LastStatusDate"],
                        PaidFees = (decimal)reader["PaidFees"],
                        UserName = (string)reader["UserName"],
                        Status = (string)reader["Status"]  
                    };

                    
                }

                reader.Close();


            }

            catch (Exception ex)
            {
                application = null;
            }
            finally
            {
                connection.Close();
            }

            return application;
        }
    }
}
