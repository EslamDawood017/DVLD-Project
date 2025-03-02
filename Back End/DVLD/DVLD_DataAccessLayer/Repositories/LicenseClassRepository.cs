using DVLD_DataAccessLayer.Interfaces;
using DVLD_DataAccessLayer.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessLayer.Repositories
{
    public class LicenseClassRepository : ILicenseClassRepository
    {
        private readonly IConfiguration _configuration;
        private string ConnectionString = "";
        public LicenseClassRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            ConnectionString = _configuration.GetConnectionString("Default");
        }
        public List<LicenseClass> getAllLicenseClass()
        {

            List<LicenseClass> LicenseClasses = new List<LicenseClass>();

            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = "select * from LicenseClasses ;";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {

                    LicenseClasses.Add(new LicenseClass
                    {
                        LicenseClassID = (int)reader["LicenseClassID"],
                        ClassDescription = (string)reader["ClassDescription"],
                        ClassFees = (decimal)reader["ClassFees"],
                        ClassName = (string)reader["ClassName"],
                        DefaultValidityLength = (byte)reader["DefaultValidityLength"],
                        MinimumAllowedAge = (byte)reader["MinimumAllowedAge"]
                    });
                }

                reader.Close();


            }

            catch (Exception ex)
            {
                LicenseClasses = null;
            }
            finally
            {
                connection.Close();
            }

            return LicenseClasses;
        } 
        public int AddNewLicenseClass(LicenseClass licenseClass)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string query = @"
                INSERT INTO LicenseClasses (ClassName, ClassDescription, MinimumAllowedAge, DefaultValidityLength, ClassFees) 
                VALUES (@ClassName, @ClassDescription, @MinimumAllowedAge, @DefaultValidityLength, @ClassFees);
                SELECT SCOPE_IDENTITY();"; 

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ClassName", licenseClass.ClassName);
                    command.Parameters.AddWithValue("@ClassDescription", licenseClass.ClassDescription);
                    command.Parameters.AddWithValue("@MinimumAllowedAge", licenseClass.MinimumAllowedAge);
                    command.Parameters.AddWithValue("@DefaultValidityLength", licenseClass.DefaultValidityLength);
                    command.Parameters.AddWithValue("@ClassFees", licenseClass.ClassFees);

                    connection.Open();

                    return Convert.ToInt32(command.ExecuteScalar()); // Return the new ID
                }
            }
        }
        public LicenseClass? GetLicenseClassInfoByClassName(string className)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string query = "SELECT * FROM LicenseClasses WHERE ClassName = @ClassName";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ClassName", className);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new LicenseClass
                            {
                                LicenseClassID = reader.GetInt32(reader.GetOrdinal("LicenseClassID")),
                                ClassName = reader.GetString(reader.GetOrdinal("ClassName")),
                                ClassDescription = reader.GetString(reader.GetOrdinal("ClassDescription")),
                                MinimumAllowedAge = reader.GetByte(reader.GetOrdinal("MinimumAllowedAge")),
                                DefaultValidityLength = reader.GetByte(reader.GetOrdinal("DefaultValidityLength")),
                                ClassFees = reader.GetDecimal(reader.GetOrdinal("ClassFees"))
                            };
                        }
                    }
                }
            }
            return null; 
        }
        public LicenseClass? GetLicenseClassInfoByID(int licenseClassID)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string query = "SELECT * FROM LicenseClasses WHERE LicenseClassID = @LicenseClassID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@LicenseClassID", licenseClassID);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new LicenseClass
                            {
                                LicenseClassID = reader.GetInt32(reader.GetOrdinal("LicenseClassID")),
                                ClassName = reader.GetString(reader.GetOrdinal("ClassName")),
                                ClassDescription = reader.GetString(reader.GetOrdinal("ClassDescription")),
                                MinimumAllowedAge = reader.GetByte(reader.GetOrdinal("MinimumAllowedAge")),
                                DefaultValidityLength = reader.GetByte(reader.GetOrdinal("DefaultValidityLength")),
                                ClassFees = reader.GetDecimal(reader.GetOrdinal("ClassFees"))
                            };
                        }
                    }
                }
            }
            return null; // Return null if no class found
        }
        public bool UpdateLicenseClass(LicenseClass licenseClass)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string query = @"
                UPDATE LicenseClasses 
                SET ClassName = @ClassName, 
                    ClassDescription = @ClassDescription, 
                    MinimumAllowedAge = @MinimumAllowedAge, 
                    DefaultValidityLength = @DefaultValidityLength, 
                    ClassFees = @ClassFees
                WHERE LicenseClassID = @LicenseClassID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ClassName", licenseClass.ClassName);
                    command.Parameters.AddWithValue("@ClassDescription", licenseClass.ClassDescription);
                    command.Parameters.AddWithValue("@MinimumAllowedAge", licenseClass.MinimumAllowedAge);
                    command.Parameters.AddWithValue("@DefaultValidityLength", licenseClass.DefaultValidityLength);
                    command.Parameters.AddWithValue("@ClassFees", licenseClass.ClassFees);
                    command.Parameters.AddWithValue("@LicenseClassID", licenseClass.LicenseClassID);

                    connection.Open();
                    return command.ExecuteNonQuery() > 0; // Returns true if update is successful
                }
            }
        }
    }
    
}
