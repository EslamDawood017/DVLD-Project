using DVLD_BusinessLayer.interfaces;
using DVLD_DataAccessLayer.DTOS;
using DVLD_DataAccessLayer.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;


namespace DVLD_DataAccessLayer.Repositories
{
    public class DetainedLicenseRepository : IDetainedRepository
    {
        private readonly IConfiguration _configuration;
        private string ConnectionString = "";
        public DetainedLicenseRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            ConnectionString = _configuration.GetConnectionString("Default");
        }
        public int AddNewDetainedLicense(NewDetainDto requist)
        {
            int DetainID = -1;

            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = @"INSERT INTO dbo.DetainedLicenses
                               (LicenseID,
                               DetainDate,
                               FineFees,
                               CreatedByUserID,
                               IsReleased
                               )
                            VALUES
                               (@LicenseID,
                               @DetainDate, 
                               @FineFees, 
                               @CreatedByUserID,
                               0
                             );
                            
                            SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseID", requist.LicenseID);
            command.Parameters.AddWithValue("@DetainDate", DateTime.Now);
            command.Parameters.AddWithValue("@FineFees", requist.FineFees);
            command.Parameters.AddWithValue("@CreatedByUserID", requist.CreatedByUserID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    DetainID = insertedID;
                }
            }
            catch (Exception ex){}

            finally
            {
                connection.Close();
            }


            return DetainID;
        }
        public List<DetainedLicenseDto> GetAllDetainedLicenses()
        {
            List<DetainedLicenseDto> detailedLicenses = new List<DetainedLicenseDto>();

            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = "select * from detainedLicenses_View order by IsReleased ,DetainID;";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while(reader.Read())
                {
                    detailedLicenses.Add(new DetainedLicenseDto
                    {
                        DetainID = reader.GetInt32(reader.GetOrdinal("DetainID")),
                        LicenseID = reader.GetInt32(reader.GetOrdinal("LicenseID")),
                        DetainDate = reader.GetDateTime(reader.GetOrdinal("DetainDate")),
                        IsReleased = reader.GetBoolean(reader.GetOrdinal("IsReleased")),
                        FineFees = reader.GetDecimal(reader.GetOrdinal("FineFees")),
                        ReleaseDate = reader.IsDBNull(reader.GetOrdinal("ReleaseDate"))
                            ? (DateTime?)null
                            : reader.GetDateTime(reader.GetOrdinal("ReleaseDate")),
                        NationalNo = reader.GetString(reader.GetOrdinal("NationalNo")),
                        FullName = reader.GetString(reader.GetOrdinal("FullName")),
                        ReleaseApplicationID = reader.IsDBNull(reader.GetOrdinal("ReleaseApplicationID"))
                            ? (int?)null
                            : reader.GetInt32(reader.GetOrdinal("ReleaseApplicationID"))
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

            return detailedLicenses;

        }        public DetailedLicense GetDetainedLicenseInfoByID(int detainId)
        {
            

            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = "SELECT * FROM DetainedLicenses WHERE DetainID = @DetainID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@DetainID", detainId);

            DetailedLicense? detailedLicense = null;

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    detailedLicense = new DetailedLicense
                    {
                        DetainID = (int)reader["DetainID"],
                        LicenseID = (int)reader["LicenseID"],
                        DetainDate = (DateTime)reader["DetainDate"],
                        FineFees = Convert.ToDecimal(reader["FineFees"]),
                        CreatedByUserID = (int)reader["CreatedByUserID"],
                        IsReleased = (bool)reader["IsReleased"],
                        ReleaseDate = (reader["ReleaseDate"] == DBNull.Value) ? DateTime.MaxValue : (DateTime)reader["ReleaseDate"],
                        ReleasedByUserID = (reader["ReleasedByUserID"] == DBNull.Value) ? -1 : (int)reader["ReleasedByUserID"],
                        ReleaseApplicationID = (reader["ReleaseApplicationID"] == DBNull.Value) ? -1 : (int)reader["ReleaseApplicationID"]
                    };

                }
                else
                {
                    // The record was not found
                    detailedLicense = null ;
                }

                reader.Close();


            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                detailedLicense = null;
            }
            finally
            {
                connection.Close();
            }

            return detailedLicense ;
        }
        public DetailedLicense GetDetainedLicenseInfoByLicenseID(int licenseID)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = "SELECT * FROM DetainedLicenses WHERE LicenseID = @LicenseID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseID", licenseID);

            DetailedLicense? detailedLicense = null;

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    detailedLicense = new DetailedLicense
                    {
                        DetainID = (int)reader["DetainID"],
                        LicenseID = (int)reader["LicenseID"],
                        DetainDate = (DateTime)reader["DetainDate"],
                        FineFees = Convert.ToDecimal(reader["FineFees"]),
                        CreatedByUserID = (int)reader["CreatedByUserID"],
                        IsReleased = (bool)reader["IsReleased"],
                        ReleaseDate = (reader["ReleaseDate"] == DBNull.Value) ? DateTime.MaxValue : (DateTime)reader["ReleaseDate"],
                        ReleasedByUserID = (reader["ReleasedByUserID"] == DBNull.Value) ? -1 : (int)reader["ReleasedByUserID"],
                        ReleaseApplicationID = (reader["ReleaseApplicationID"] == DBNull.Value) ? -1 : (int)reader["ReleaseApplicationID"]
                    };

                }
                else
                {
                    // The record was not found
                    detailedLicense = null ;
                }

                reader.Close();


            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                detailedLicense = null;
            }
            finally
            {
                connection.Close();
            }

            return detailedLicense ;
        }
        public bool IsLicenseDetained(int licenseID)
        {
            bool IsDetained = false;

            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = @"select IsDetained=1 
                            from detainedLicenses 
                            where 
                            LicenseID=@LicenseID 
                            and IsReleased=0;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseID", licenseID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null)
                {
                    IsDetained = Convert.ToBoolean(result);
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


            return IsDetained;
            ;
        }
        public bool ReleaseDetainedLicense(int LicenseId , int ReleasedByUserId , int ReleaseApplicationID)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = @"UPDATE dbo.DetainedLicenses
                              SET IsReleased = 1, 
                              ReleasedByUserID = @ReleasedByUserID,
                              ReleaseDate = @ReleaseDate, 
                              ReleaseApplicationID = @ReleaseApplicationID   
                              WHERE LicenseID=@LicenseID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseID", LicenseId);
            command.Parameters.AddWithValue("@ReleasedByUserID", ReleasedByUserId);
            command.Parameters.AddWithValue("@ReleaseApplicationID", ReleaseApplicationID);
            command.Parameters.AddWithValue("@ReleaseDate", DateTime.Now);
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
        public bool UpdateDetainedLicense(NewDetainDto requist)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = @"UPDATE dbo.DetainedLicenses
                              SET LicenseID = @LicenseID, 
                              FineFees = @FineFees,
                              CreatedByUserID = @CreatedByUserID  
                              WHERE DetainID=@DetainID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@DetainID", requist.DetainId);
            command.Parameters.AddWithValue("@LicenseID", requist.LicenseID);
            command.Parameters.AddWithValue("@FineFees", requist.FineFees);
            command.Parameters.AddWithValue("@CreatedByUserID", requist.CreatedByUserID);


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
