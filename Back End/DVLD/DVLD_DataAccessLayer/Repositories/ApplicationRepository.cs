using DVLD_DataAccessLayer.Interfaces;
using DVLD_DataAccessLayer.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DVLD_DataAccessLayer.Repositories
{
    public class ApplicationRepository : IApplicationRepository
    {
        private readonly IConfiguration _configuration;
        private string ConnectionString = "";
        public ApplicationRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            ConnectionString = _configuration.GetConnectionString("Default");
        }
        public int CreateNewApplication(Models.Application application)
        {
            int ApplicationID = -1;
            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = @"INSERT INTO Applications ( 
                            ApplicantPersonID,ApplicationDate,ApplicationTypeID,
                            ApplicationStatus,LastStatusDate,
                            PaidFees,CreatedByUserID)
                             VALUES (@ApplicantPersonID,@ApplicationDate,@ApplicationTypeID,
                                      @ApplicationStatus,@LastStatusDate,
                                      @PaidFees,   @CreatedByUserID);
                             SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicantPersonID", application.ApplicantPersonID);
            command.Parameters.AddWithValue("@ApplicationDate", application.ApplicationDate);
            command.Parameters.AddWithValue("@ApplicationTypeID",application.ApplicationTypeID );
            command.Parameters.AddWithValue("@ApplicationStatus", application.ApplicationStatus );
            command.Parameters.AddWithValue("@LastStatusDate", application.LastStatusDate);
            command.Parameters.AddWithValue("@PaidFees",application.PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID",application.CreatedByUserID );


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

        public List<Models.Application> GetAllApplications()
        {
            List<Models.Application> applications = new List<Models.Application>();

            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = "select * from Applications order by ApplicationDate desc";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var app = new Models.Application
                    {
                        ApplicationID = (int)reader["ApplicationID"],
                        ApplicantPersonID = (int)reader["ApplicantPersonID"],
                        ApplicationDate = (DateTime)reader["ApplicationDate"],
                        ApplicationTypeID = (int)reader["ApplicationTypeID"],
                        ApplicationStatus = (byte)reader["ApplicationStatus"],
                        LastStatusDate = (DateTime)reader["LastStatusDate"],
                        PaidFees = (decimal)reader["PaidFees"],
                        CreatedByUserID = (int)reader["CreatedByUserID"],
                    };
                    applications.Add(app);
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

            return applications;
        }

        public Models.Application GetApplicationById(int ApplicationId)
        {

            var application = new Models.Application();

            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = "SELECT * FROM Applications WHERE ApplicationID = @ApplicationID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationId);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    application.ApplicantPersonID = (int)reader["ApplicantPersonID"];
                    application.ApplicationDate = (DateTime)reader["ApplicationDate"];
                    application.ApplicationTypeID = (int)reader["ApplicationTypeID"];
                    application.ApplicationStatus = (byte)reader["ApplicationStatus"];
                    application.LastStatusDate = (DateTime)reader["LastStatusDate"];
                    application.PaidFees = (decimal)reader["PaidFees"];
                    application.CreatedByUserID = (int)reader["CreatedByUserID"];


                }
                else
                {
                    // The record was not found
                    application = null;
                }

                reader.Close();


            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                application = null;
            }
            finally
            {
                connection.Close();
            }

            return application;
        }

        public bool UpdateApplication(Models.Application application)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = @"Update  Applications  
                            set ApplicantPersonID = @ApplicantPersonID,
                                ApplicationDate = @ApplicationDate,
                                ApplicationTypeID = @ApplicationTypeID,
                                ApplicationStatus = @ApplicationStatus, 
                                LastStatusDate = @LastStatusDate,
                                PaidFees = @PaidFees,
                                CreatedByUserID=@CreatedByUserID
                            where ApplicationID=@ApplicationID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationID", application.ApplicationID);
            command.Parameters.AddWithValue("@ApplicantPersonID", application.ApplicantPersonID);
            command.Parameters.AddWithValue("@ApplicationDate", application.ApplicationDate);
            command.Parameters.AddWithValue("@ApplicationTypeID", application.ApplicationTypeID);
            command.Parameters.AddWithValue("@ApplicationStatus", application.ApplicationStatus);
            command.Parameters.AddWithValue("@LastStatusDate", application.LastStatusDate);
            command.Parameters.AddWithValue("@PaidFees", application.PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", application.CreatedByUserID);


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
        public bool DeleteApplication(int applicationID)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = @"Delete Applications 
                                where ApplicationID = @ApplicationID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationID", applicationID);

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

        public bool isApplicationExist(int applicationID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = "SELECT Found=1 FROM Applications WHERE ApplicationID = @ApplicationID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationID", applicationID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                isFound = reader.HasRows;

                reader.Close();
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public int GetActiveApplicationID(int PersonID, int ApplicationTypeID)
        {
            int ActiveApplicationID = -1;

            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = "SELECT ActiveApplicationID=ApplicationID FROM Applications WHERE ApplicantPersonID = @ApplicantPersonID and ApplicationTypeID=@ApplicationTypeID and ApplicationStatus=1";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicantPersonID", PersonID);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();


                if (result != null && int.TryParse(result.ToString(), out int AppID))
                {
                    ActiveApplicationID = AppID;
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                return ActiveApplicationID;
            }
            finally
            {
                connection.Close();
            }

            return ActiveApplicationID;
        }
        public bool UpdateStatus(int ApplicationId, int newStatus)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = @"Update  Applications  
                            set 
                                ApplicationStatus = @NewStatus, 
                                LastStatusDate = @LastStatusDate
                            where ApplicationID=@ApplicationID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationId);
            command.Parameters.AddWithValue("@NewStatus", newStatus);
            command.Parameters.AddWithValue("LastStatusDate", DateTime.Now);


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
