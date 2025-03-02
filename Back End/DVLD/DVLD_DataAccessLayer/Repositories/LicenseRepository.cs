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
using static System.Net.Mime.MediaTypeNames;

namespace DVLD_DataAccessLayer.Repositories
{
    public class LicenseRepository : ILicenseRepository
    {
        private readonly IConfiguration _configuration;
        private string ConnectionString = "";
        public LicenseRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            ConnectionString = _configuration.GetConnectionString("Default");
        }
        public License GetLicecnseInfoById(int LicenseID)
        {

            var license = new Models.License();

            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = "SELECT * FROM Licenses WHERE LicenseID = @LicenseID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseID", LicenseID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    license.LicenseID= (int)reader["LicenseID"];
                    license.ApplicationID = (int)reader["ApplicationID"];
                    license.DriverID = (int)reader["DriverID"];
                    license.LicenseClassID = (int)reader["LicenseClass"];
                    license.IssueDate = (DateTime)reader["IssueDate"];
                    license.ExpirationDate = (DateTime)reader["ExpirationDate"];

                    if (reader["Notes"] == DBNull.Value)
                        license.Notes = "";
                    else
                        license.Notes = (string)reader["Notes"];

                    license.PaidFees = Convert.ToDecimal(reader["PaidFees"]);
                    license.IsActive = (bool)reader["IsActive"];
                    license.IssueReason = (byte)reader["IssueReason"];
                    license.CreatedByUserID = (int)reader["DriverID"];


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
        public List<License> GetAllLicenses()
        {
            List<License> licenseList = new List<License>();

            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = "SELECT * FROM Licenses";

            SqlCommand command = new SqlCommand(query, connection);
            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                

                while(reader.Read())
                {
                    var license = new License();
                    license.LicenseID = (int)reader["LicenseID"];
                    license.ApplicationID = (int)reader["ApplicationID"];
                    license.DriverID = (int)reader["DriverID"];
                    license.LicenseClassID = (int)reader["LicenseClass"];
                    license.IssueDate = (DateTime)reader["IssueDate"];
                    license.ExpirationDate = (DateTime)reader["ExpirationDate"];

                    if (reader["Notes"] == DBNull.Value)
                        license.Notes = "";
                    else
                        license.Notes = (string)reader["Notes"];

                    license.PaidFees = Convert.ToDecimal(reader["PaidFees"]);
                    license.IsActive = (bool)reader["IsActive"];
                    license.IssueReason = (byte)reader["IssueReason"];
                    license.CreatedByUserID = (int)reader["DriverID"];

                    licenseList.Add(license);
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

            return licenseList;
        }
        public int AddNewLicense(License license)
        {
            int LicenseID = -1;

            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = @"
                              INSERT INTO Licenses
                               (ApplicationID,
                                DriverID,
                                LicenseClass,
                                IssueDate,
                                ExpirationDate,
                                Notes,
                                PaidFees,
                                IsActive,IssueReason,
                                CreatedByUserID)
                         VALUES
                               (
                               @ApplicationID,
                               @DriverID,
                               @LicenseClass,
                               @IssueDate,
                               @ExpirationDate,
                               @Notes,
                               @PaidFees,
                               @IsActive,@IssueReason, 
                               @CreatedByUserID);
                            SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", license.ApplicationID);
            command.Parameters.AddWithValue("@DriverID", license.DriverID);
            command.Parameters.AddWithValue("@LicenseClass", license.LicenseClassID);
            command.Parameters.AddWithValue("@IssueDate", license.IssueDate);

            command.Parameters.AddWithValue("@ExpirationDate", license.ExpirationDate);

            if (license.Notes == "")
                command.Parameters.AddWithValue("@Notes", DBNull.Value);
            else
                command.Parameters.AddWithValue("@Notes", license.Notes);

            command.Parameters.AddWithValue("@PaidFees", license.PaidFees);
            command.Parameters.AddWithValue("@IsActive", license.IsActive);
            command.Parameters.AddWithValue("@IssueReason", license.IssueReason);

            command.Parameters.AddWithValue("@CreatedByUserID", license.CreatedByUserID);
            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    LicenseID = insertedID;
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


            return LicenseID;
        }
        public bool UpdateLicense(License license)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = @"UPDATE Licenses
                           SET ApplicationID=@ApplicationID, DriverID = @DriverID,
                              LicenseClass = @LicenseClass,
                              IssueDate = @IssueDate,
                              ExpirationDate = @ExpirationDate,
                              Notes = @Notes,
                              PaidFees = @PaidFees,
                              IsActive = @IsActive,IssueReason=@IssueReason,
                              CreatedByUserID = @CreatedByUserID
                         WHERE LicenseID=@LicenseID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseID", license.LicenseID);
            command.Parameters.AddWithValue("@ApplicationID", license.ApplicationID);
            command.Parameters.AddWithValue("@DriverID", license.DriverID);
            command.Parameters.AddWithValue("@LicenseClass", license.LicenseClassID);
            command.Parameters.AddWithValue("@IssueDate", license.IssueDate);
            command.Parameters.AddWithValue("@ExpirationDate", license.ExpirationDate);

            if (license.Notes == "")
                command.Parameters.AddWithValue("@Notes", DBNull.Value);
            else
                command.Parameters.AddWithValue("@Notes", license.Notes);

            command.Parameters.AddWithValue("@PaidFees", license.PaidFees);
            command.Parameters.AddWithValue("@IsActive", license.IsActive);
            command.Parameters.AddWithValue("@IssueReason", license.IssueReason);
            command.Parameters.AddWithValue("@CreatedByUserID", license.CreatedByUserID);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                return false;
            }

            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }
        public int GetActiveLicenseIDByPersonID(int PersonID, int LicenseClassID)
        {
            int LicenseID = -1;

            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = @"SELECT        Licenses.LicenseID
                            FROM Licenses INNER JOIN
                                                     Drivers ON Licenses.DriverID = Drivers.DriverID
                            WHERE  
                             
                             Licenses.LicenseClass = @LicenseClass 
                              AND Drivers.PersonID = @PersonID
                              And IsActive=1;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@LicenseClass", LicenseClassID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    LicenseID = insertedID;
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
            return LicenseID;
        }
        public  bool DeactivateLicense(int LicenseID)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = @"UPDATE Licenses
                           SET 
                              IsActive = 0
                             
                         WHERE LicenseID=@LicenseID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseID", LicenseID);


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
        public LicenseInfoDto GetLicenseInfo(int LocalDrivingLicenseApplicationId)
        {
            LicenseInfoDto license = null;

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                string query = "SELECT * FROM vw_LicenseInfo WHERE LocalDrivingLicenseApplicationID = @ApplicationID";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ApplicationID", LocalDrivingLicenseApplicationId);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    license = new LicenseInfoDto
                    {
                        ClassName =(string)reader["ClassName"],
                        FullName = (string)reader["FullName"],
                        LicenseID = (int)reader["LicenseID"],
                        NationalNo = (string)reader["NationalNo"],
                        IssueDate = Convert.ToDateTime(reader["IssueDate"]),
                        Gender = (string)reader["Gendor"],
                        ImagePath = (string)reader["ImagePath"],
                        ExpirationDate = Convert.ToDateTime(reader["ExpirationDate"]),
                        IssueReason = (string)reader["IssueReason"],
                        Notes = (string)reader["Note"],
                        IsActive = (string)reader["IsActive"],
                        DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]),
                        DriverID = (int)reader["DriverID"]
                    };
                }
            }

            return license;
        }
        public LicenseInfoDto GetLicenseInfoDToByLicenseId(int LicenseId)
        {
            LicenseInfoDto license = null;

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                string query = "SELECT * FROM vw_LicenseInfoByLicenseID WHERE LicenseID = @LicenseID";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@LicenseID", LicenseId);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    license = new LicenseInfoDto
                    {
                        ClassName = (string)reader["ClassName"],
                        FullName = (string)reader["FullName"],
                        LicenseID = (int)reader["LicenseID"],
                        NationalNo = (string)reader["NationalNo"],
                        IssueDate = Convert.ToDateTime(reader["IssueDate"]),
                        Gender = (string)reader["Gendor"],
                        ImagePath = (string)reader["ImagePath"],
                        ExpirationDate = Convert.ToDateTime(reader["ExpirationDate"]),
                        IssueReason = (string)reader["IssueReason"],
                        Notes = (string)reader["Note"],
                        IsActive = (string)reader["IsActive"],
                        DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]),
                        DriverID = (int)reader["DriverID"]
                    };
                }
            }
            return license;
        }

        public List<LicenseInfoDto> GetAllLicenseForPerosnByNationalNo(string NationalNo)
        {
            List<LicenseInfoDto> licenses = new List<LicenseInfoDto>();

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                string query = "SELECT * FROM vw_LicenseInfoByLicenseID WHERE NationalNo = @NationalNo";

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@NationalNo", NationalNo);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while(reader.Read())
                {               
                    licenses.Add(new LicenseInfoDto
                    {
                        ClassName = (string)reader["ClassName"],
                        FullName = (string)reader["FullName"],
                        LicenseID = (int)reader["LicenseID"],
                        NationalNo = (string)reader["NationalNo"],
                        IssueDate = Convert.ToDateTime(reader["IssueDate"]),
                        Gender = (string)reader["Gendor"],
                        ImagePath = (string)reader["ImagePath"],
                        ExpirationDate = Convert.ToDateTime(reader["ExpirationDate"]),
                        IssueReason = (string)reader["IssueReason"],
                        Notes = (string)reader["Note"],
                        IsActive = (string)reader["IsActive"],
                        DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]),
                        DriverID = (int)reader["DriverID"]
                    });
                }
            }
            return licenses;
        }
    }
}
