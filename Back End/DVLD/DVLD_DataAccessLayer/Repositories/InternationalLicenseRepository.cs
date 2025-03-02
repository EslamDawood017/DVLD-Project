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
using static System.Net.Mime.MediaTypeNames;

namespace DVLD_DataAccessLayer.Repositories
{
    public class InternationalLicenseRepository : IInternationalLicenseRepository
    {
        private readonly IConfiguration _configuration;
        private string ConnectionString = "";
        public InternationalLicenseRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            ConnectionString = _configuration.GetConnectionString("Default");
        }
        public int AddNewInternationalLicense(InternationalLicense internationalLicense)
        {
            int InternationalLicenseID = -1;

            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = @"
                               Update InternationalLicenses 
                               set IsActive=0
                               where DriverID=@DriverID;

                             INSERT INTO InternationalLicenses
                               (
                                ApplicationID,
                                DriverID,
                                IssuedUsingLocalLicenseID,
                                IssueDate,
                                ExpirationDate,
                                IsActive,
                                CreatedByUserID)
                         VALUES
                               (@ApplicationID,
                                @DriverID,
                                @IssuedUsingLocalLicenseID,
                                @IssueDate,
                                @ExpirationDate,
                                @IsActive,
                                @CreatedByUserID);
                            SELECT SCOPE_IDENTITY();"
            ;
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationID", internationalLicense.ApplicationID);
            command.Parameters.AddWithValue("@DriverID", internationalLicense.DriverID);
            command.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", internationalLicense.IssuedUsingLocalLicenseID);
            command.Parameters.AddWithValue("@IssueDate", internationalLicense.IssueDate);
            command.Parameters.AddWithValue("@ExpirationDate", internationalLicense.ExpirationDate);
            command.Parameters.AddWithValue("@IsActive", internationalLicense.IsActive);
            command.Parameters.AddWithValue("@CreatedByUserID", internationalLicense.CreatedByUserID);



            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    InternationalLicenseID = insertedID;
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

            return InternationalLicenseID;
        }

        public int GetActiveInternationalLicenseIDByDriverID(int DriverID)
        {
            int InternationalLicenseID = -1;

            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = @"  
                            SELECT Top 1 InternationalLicenseID
                            FROM InternationalLicenses 
                            where DriverID=@DriverID and GetDate() between IssueDate and ExpirationDate 
                            order by ExpirationDate Desc;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@DriverID", DriverID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    InternationalLicenseID = insertedID;
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


            return InternationalLicenseID;
        }

        public List<InternationalLicense> GetAllInternationalLicenses()
        {
            List<InternationalLicense> internationalLicenses = new List<InternationalLicense>();

            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = @"
                SELECT InternationalLicenseID, ApplicationID,DriverID,
		                IssuedUsingLocalLicenseID , IssueDate, 
                        CreatedByUserID,
                        ExpirationDate, IsActive
		        from InternationalLicenses 
                order by IsActive, ExpirationDate desc";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while(reader.Read())
                {
                    internationalLicenses.Add(new InternationalLicense
                    {
                        InternationalLicenseID = (int)reader["InternationalLicenseID"],
                        ApplicationID = (int)reader["ApplicationID"],
                        CreatedByUserID = (int)reader["CreatedByUserID"],
                        DriverID = (int)reader["DriverID"],
                        IssuedUsingLocalLicenseID = (int)reader["IssuedUsingLocalLicenseID"],
                        IsActive = (bool)reader["IsActive"],
                        IssueDate = (DateTime)reader["IssueDate"],
                        ExpirationDate = (DateTime)reader["ExpirationDate"],
                    });
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

            return internationalLicenses;
        }

        public List<InternationalLicense> GetDriverInternationalLicenses(int DriverID)
        {
            List<InternationalLicense> internationalLicenses = new List<InternationalLicense>();

            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = @"
            SELECT    InternationalLicenseID, ApplicationID,
		                IssuedUsingLocalLicenseID , IssueDate, 
                        ExpirationDate, IsActive
		    from InternationalLicenses where DriverID=@DriverID
                order by ExpirationDate desc";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    internationalLicenses.Add(new InternationalLicense
                    {
                        InternationalLicenseID = (int)reader["InternationalLicenseID"],
                        ApplicationID = (int)reader["ApplicationID"],
                        CreatedByUserID = (int)reader["CreatedByUserID"],
                        DriverID = (int)reader["DriverID"],
                        IssuedUsingLocalLicenseID = (int)reader["IssuedUsingLocalLicenseID"],
                        IsActive = (bool)reader["IsActive"],
                        IssueDate = (DateTime)reader["IssueDate"],
                        ExpirationDate = (DateTime)reader["ExpirationDate"],
                    });
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

            return internationalLicenses;
        }

        public InternationalLicense GetInternationalLicenseInfoByID(int InternationalLicenseID)
        {
            InternationalLicense license = null;

            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = "SELECT * FROM InternationalLicenses WHERE InternationalLicenseID = @InternationalLicenseID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    // The record was found
                    license = new InternationalLicense {
                        ApplicationID = (int)reader["ApplicationID"],
                        DriverID = (int)reader["DriverID"],
                        IssuedUsingLocalLicenseID = (int)reader["IssuedUsingLocalLicenseID"],
                        IssueDate = (DateTime)reader["IssueDate"],
                        ExpirationDate = (DateTime)reader["ExpirationDate"],
                        IsActive = (bool)reader["IsActive"],
                        CreatedByUserID = (int)reader["DriverID"]
                    };


                }
                else
                {
                    // The record was not found
                    license = null;
                }

                reader.Close();


            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                license = null;
            }
            finally
            {
                connection.Close();
            }

            return license;
        }

        public bool UpdateInternationalLicense(InternationalLicense internationalLicense)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = @"UPDATE InternationalLicenses
                           SET 
                              ApplicationID=@ApplicationID,
                              DriverID = @DriverID,
                              IssuedUsingLocalLicenseID = @IssuedUsingLocalLicenseID,
                              IssueDate = @IssueDate,
                              ExpirationDate = @ExpirationDate,
                              IsActive = @IsActive,
                              CreatedByUserID = @CreatedByUserID
                         WHERE InternationalLicenseID=@InternationalLicenseID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@InternationalLicenseID", internationalLicense.InternationalLicenseID);
            command.Parameters.AddWithValue("@ApplicationID", internationalLicense.ApplicationID);
            command.Parameters.AddWithValue("@DriverID", internationalLicense.DriverID);
            command.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", internationalLicense.IssuedUsingLocalLicenseID);
            command.Parameters.AddWithValue("@IssueDate", internationalLicense.IssueDate);
            command.Parameters.AddWithValue("@ExpirationDate", internationalLicense.ExpirationDate);
            command.Parameters.AddWithValue("@IsActive", internationalLicense.IsActive);
            command.Parameters.AddWithValue("@CreatedByUserID", internationalLicense.CreatedByUserID);

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
