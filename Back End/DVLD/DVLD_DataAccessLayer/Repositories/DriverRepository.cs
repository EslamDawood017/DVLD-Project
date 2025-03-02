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
    public class DriverRepository : IdriverRepository
    {
        private readonly IConfiguration _configuration;
        private string ConnectionString = "";
        public DriverRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            ConnectionString = this._configuration.GetConnectionString("Default");
        }
        public int AddNewDriver(int PersonID, int CreatedByUserID)
        {
            int DriverID = -1;

            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = @"Insert Into Drivers (PersonID,CreatedByUserID,CreatedDate)
                            Values (@PersonID,@CreatedByUserID,@CreatedDate);
                          
                            SELECT SCOPE_IDENTITY();";


            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    DriverID = insertedID;
                }
            }

            catch (Exception ex)
            {
               

            }

            finally
            {
                connection.Close();
            }


            return DriverID;
        }
        public List<DriverDto> GetAllDrivers()
        {
            List<DriverDto> list = new List<DriverDto>();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string query = "SELECT DriverID, PersonID, NationalNo, FullName, CreatedDate, NumberOfActiveLicenses FROM Drivers_View ORDER BY FullName";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                DriverDto driver = new DriverDto
                                {
                                    DriverID = reader.GetInt32(reader.GetOrdinal("DriverID")),
                                    PersonID = reader.GetInt32(reader.GetOrdinal("PersonID")),
                                    NationalNo = reader.GetString(reader.GetOrdinal("NationalNo")),
                                    FullName = reader.GetString(reader.GetOrdinal("FullName")),
                                    CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate")),
                                    NumberOfActiveLicenses = reader.GetInt32(reader.GetOrdinal("NumberOfActiveLicenses"))
                                };

                                list.Add(driver);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                    }
                }
            }

            return list;
        }
        public DriverDto GetDriverInfoByDriverID(int driverId)
        {
            DriverDto? driver = null;

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string query = @"SELECT DriverID, PersonID, NationalNo, FullName, CreatedDate, NumberOfActiveLicenses 
                                 FROM Drivers_View 
                                 WHERE DriverID = @DriverID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@DriverID", driverId);

                    try
                    {
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                driver = new DriverDto
                                {
                                    DriverID = reader.GetInt32(reader.GetOrdinal("DriverID")),
                                    PersonID = reader.GetInt32(reader.GetOrdinal("PersonID")),
                                    NationalNo = reader.GetString(reader.GetOrdinal("NationalNo")),
                                    FullName = reader.GetString(reader.GetOrdinal("FullName")),
                                    CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate")),
                                    NumberOfActiveLicenses = reader.GetInt32(reader.GetOrdinal("NumberOfActiveLicenses"))
                                };
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                    }
                }
            }

            return driver;
        }
        public DriverDto GetDriverInfoByPersonID(int PersonID)
        {
            DriverDto? driver = null;

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string query = @"SELECT DriverID, PersonID, NationalNo, FullName, CreatedDate, NumberOfActiveLicenses 
                                 FROM Drivers_View 
                                 WHERE PersonID = @PersonID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PersonID", PersonID);

                    try
                    {
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                driver = new DriverDto
                                {
                                    DriverID = reader.GetInt32(reader.GetOrdinal("DriverID")),
                                    PersonID = reader.GetInt32(reader.GetOrdinal("PersonID")),
                                    NationalNo = reader.GetString(reader.GetOrdinal("NationalNo")),
                                    FullName = reader.GetString(reader.GetOrdinal("FullName")),
                                    CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate")),
                                    NumberOfActiveLicenses = reader.GetInt32(reader.GetOrdinal("NumberOfActiveLicenses"))
                                };
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                    }
                }
               
            }
            return driver;
        }
        public bool UpdateDriver(int DriverID , int PersonID , int CreatedByUserID)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(ConnectionString);
            

            string query = @"Update  Drivers  
                            set PersonID = @PersonID,
                                CreatedByUserID = @CreatedByUserID
                                where DriverID = @DriverID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

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
