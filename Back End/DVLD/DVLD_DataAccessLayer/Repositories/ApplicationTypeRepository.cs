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
    public class ApplicationTypeRepository : IApplicationTypeRepository
    {
        private readonly IConfiguration _configuration;
        private string ConnectionString = "";

        public ApplicationTypeRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            ConnectionString = _configuration.GetConnectionString("Default");
        }
        public int AddNewApplicationType(ApplicationType applicationType)
        {
            int ApplicationTypeID = -1;
            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = @"Insert Into ApplicationTypes (ApplicationTypeTitle,ApplicationFees)
                            Values (@ApplicationTypeTitle,@ApplicationFees)
                            
                            SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationTypeTitle", applicationType.ApplicationTypeTitle);
            command.Parameters.AddWithValue("@ApplicationFees", applicationType.ApplicationFees);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    ApplicationTypeID = insertedID;
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


            return ApplicationTypeID;
        }

        public List<ApplicationType> GetAllApplicationTypes()
        {
            List<ApplicationType> applicationTypes = new List<ApplicationType>();

            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = "SELECT * FROM ApplicationTypes order by ApplicationTypeTitle";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var ApplicationType = new ApplicationType
                    {
                        ApplicationTypeID = (int)reader["ApplicationTypeID"],
                        ApplicationTypeTitle = (string)reader["ApplicationTypeTitle"],
                        ApplicationFees = (decimal)reader["ApplicationFees"]
                    };

                    applicationTypes.Add(ApplicationType);
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

            return applicationTypes;
        }

        public ApplicationType GetApplicationTypeInfoById(int AppId)
        {
            ApplicationType ApplicationType = new ApplicationType();

            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = "SELECT * FROM ApplicationTypes WHERE ApplicationTypeID = @ApplicationTypeID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationTypeID", AppId);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    // The record was found

                    ApplicationType.ApplicationTypeID = (int)reader["ApplicationTypeID"];
                    ApplicationType.ApplicationTypeTitle = (string)reader["ApplicationTypeTitle"];
                    ApplicationType.ApplicationFees = (decimal)reader["ApplicationFees"];





                }
                else
                {
                    // The record was not found
                    ApplicationType = null;
                }

                reader.Close();


            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                ApplicationType = null;
            }
            finally
            {
                connection.Close();
            }

            return ApplicationType;
        }

        public bool UpdateApplicationType(ApplicationType applicationType)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = @"Update  ApplicationTypes  
                            set ApplicationTypeTitle = @Title,
                                ApplicationFees = @Fees
                                where ApplicationTypeID = @ApplicationTypeID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationTypeID", applicationType.ApplicationTypeID);
            command.Parameters.AddWithValue("@Title", applicationType.ApplicationTypeTitle);
            command.Parameters.AddWithValue("@Fees", applicationType.ApplicationFees);

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
