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

namespace DVLD_DataAccessLayer.Repositories
{
    public class TestAppointmentRepository : ITestAppointmentRepository
    {
        private readonly IConfiguration _configuration;
        private string ConnectionString = "";
        public TestAppointmentRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            ConnectionString = _configuration.GetConnectionString("Default");
        }
        public TestAppointment GetTestAppointmentInfoById(int TestAppointmentId)
        {

            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = "SELECT * FROM TestAppointments WHERE TestAppointmentID = @TestAppointmentID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentId);

            var Appointment = new TestAppointment();

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    Appointment.TestTypeID = (int)reader["TestTypeID"];
                    Appointment.LocalDrivingLicenseApplicationID = (int)reader["LocalDrivingLicenseApplicationID"];
                    Appointment.AppointmentDate = (DateTime)reader["AppointmentDate"];
                    Appointment.CreatedByUserID = (int)reader["CreatedByUserID"];
                    Appointment.PaidFees = (decimal)reader["PaidFees"];
                    Appointment.IsLocked = (bool)reader["IsLocked"];
                    if(reader["RetakeTestApplicationID"] == DBNull.Value)
                        Appointment.RetakeTestApplicationID = -1;
                    else
                        Appointment.RetakeTestApplicationID = (int)reader["RetakeTestApplicationID"];

                }
                else
                {
                    Appointment = null;
                }

                reader.Close();


            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                Appointment = null;
            }
            finally
            {
                connection.Close();
            }

            return Appointment;
        }
        public TestAppointment GetLastTestAppointment(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            var testAppointment = new TestAppointment();

            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = @"SELECT top 1 *
                FROM            TestAppointments
                WHERE        (TestTypeID = @TestTypeID) 
                AND (LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID) 
                order by TestAppointmentID Desc";


            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    // The record was found

                    testAppointment.TestAppointmentID = (int)reader["TestAppointmentID"];
                    testAppointment.AppointmentDate = (DateTime)reader["AppointmentDate"];
                    testAppointment.PaidFees = (decimal)reader["PaidFees"];
                    testAppointment.CreatedByUserID = (int)reader["CreatedByUserID"];
                    testAppointment.IsLocked = (bool)reader["IsLocked"];

                    if (reader["RetakeTestApplicationID"] == DBNull.Value)
                        testAppointment.RetakeTestApplicationID = -1;
                    else
                        testAppointment.RetakeTestApplicationID = (int)reader["RetakeTestApplicationID"];


                }
                else
                {
                    // The record was not found
                    testAppointment = null;
                }

                reader.Close();


            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                testAppointment = null;
            }
            finally
            {
                connection.Close();
            }

            return testAppointment;
        }
        public List<GetAllTestAppointmentDto> GetAllTestAppointments()
        {
            List<GetAllTestAppointmentDto> TestAppointments = new List<GetAllTestAppointmentDto>();

            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = @"select * from TestAppointments_View
                            order by AppointmentDate Desc";


            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())

                {
                    var appointemnt = new GetAllTestAppointmentDto
                    {
                        LocalDrivingLicenseApplicationID = (int)reader["LocalDrivingLicenseApplicationID"],
                        TestTypeTitle = (string)reader["TestTypeTitle"],
                        TestAppointmentID = (int)reader["TestAppointmentID"],
                        AppointmentDate = (DateTime)reader["AppointmentDate"],
                        ClassName = (string)reader["ClassName"],
                        FullName = (string)reader["FullName"],
                        PaidFees = (decimal)reader["PaidFees"],
                        IsLocked = (bool)reader["IsLocked"]

                    };

                    TestAppointments.Add(appointemnt);
                }

                reader.Close();


            }

            catch (Exception ex)
            {
                // Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return TestAppointments;
        }
        public List<GetAppointmentByTestTypeDto> GetApplicationTestAppointmentsPerTestType(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            List<GetAppointmentByTestTypeDto> applications = new List<GetAppointmentByTestTypeDto>();

            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = @"SELECT TestAppointmentID, AppointmentDate,PaidFees, IsLocked
                        FROM TestAppointments
                        WHERE  
                        (TestTypeID = @TestTypeID) 
                        AND (LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID)
                        order by TestAppointmentID desc;";


            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);


            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    applications.Add(new GetAppointmentByTestTypeDto
                    {
                        TestAppointmentID = (int)reader["TestAppointmentID"],
                        PaidFees = (decimal)reader["PaidFees"],
                        AppointmentDate = (DateTime)reader["AppointmentDate"], 
                        IsLocked = (bool)reader["IsLocked"]

                    });
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
        public int AddNewTestAppointment(TestAppointment testAppointment)
        {
            int TestAppointmentID = -1;

            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = @"Insert Into TestAppointments (TestTypeID,LocalDrivingLicenseApplicationID,AppointmentDate,PaidFees,CreatedByUserID,IsLocked,RetakeTestApplicationID)
                            Values (@TestTypeID,@LocalDrivingLicenseApplicationID,@AppointmentDate,@PaidFees,@CreatedByUserID,0,@RetakeTestApplicationID);
                
                            SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);


            command.Parameters.AddWithValue("@TestTypeID", testAppointment.TestTypeID);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", testAppointment.LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@AppointmentDate", testAppointment.AppointmentDate);
            command.Parameters.AddWithValue("@PaidFees", testAppointment.PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", testAppointment.CreatedByUserID);

            if (testAppointment.RetakeTestApplicationID == -1)

                command.Parameters.AddWithValue("@RetakeTestApplicationID", DBNull.Value);
            else
                command.Parameters.AddWithValue("@RetakeTestApplicationID", testAppointment.RetakeTestApplicationID);





            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    TestAppointmentID = insertedID;
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


            return TestAppointmentID;

        }
        public bool UpdateTestAppointment(TestAppointment testAppointment)
        {

            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = @"Update  TestAppointments  
                            set TestTypeID = @TestTypeID,
                                LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID,
                                AppointmentDate = @AppointmentDate,
                                PaidFees = @PaidFees,
                                CreatedByUserID = @CreatedByUserID,
                                IsLocked=@IsLocked,
                                RetakeTestApplicationID=@RetakeTestApplicationID
                                where TestAppointmentID = @TestAppointmentID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestAppointmentID", testAppointment.TestAppointmentID);
            command.Parameters.AddWithValue("@TestTypeID", testAppointment.TestTypeID);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", testAppointment.LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@AppointmentDate", testAppointment.AppointmentDate);
            command.Parameters.AddWithValue("@PaidFees", testAppointment.PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", testAppointment.CreatedByUserID);
            command.Parameters.AddWithValue("@IsLocked", testAppointment.IsLocked);

            if (testAppointment.RetakeTestApplicationID == -1)

                command.Parameters.AddWithValue("@RetakeTestApplicationID", DBNull.Value);
            else
                command.Parameters.AddWithValue("@RetakeTestApplicationID", testAppointment.RetakeTestApplicationID);

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
        public int GetTestID(int TestAppointmentID)
        {
            int TestID = -1;
            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = @"select TestID from Tests where TestAppointmentID=@TestAppointmentID;";

            SqlCommand command = new SqlCommand(query, connection);


            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);


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
        public bool UpdateTestAppointmentDate(int TestAppointmentID, DateTime newDate)
        {

            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = @"Update  TestAppointments  
                        set 
                            AppointmentDate = @AppointmentDate
                            where TestAppointmentID = @TestAppointmentID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            command.Parameters.AddWithValue("@AppointmentDate", newDate);

   
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

    }
}
