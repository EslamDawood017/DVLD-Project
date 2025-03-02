using DVLD_DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using DVLD_DataAccessLayer.Models;
using DVLD_BusinessLayer.DTOS;
using System.Net;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace DVLD_DataAccessLayer.Repositories
{
    public class PersonRespository : IPersonRepository
    {
        private readonly IConfiguration _configuration;
        string connectionString = "";
        public PersonRespository(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = configuration.GetConnectionString("Default");
        }

        public int AddNewPerson(Person person)
        {
            int PersonId = -1;

            SqlConnection connection = new SqlConnection(connectionString);

            string query = @"INSERT INTO People (FirstName, SecondName, ThirdName,LastName,NationalNo,
                                                   DateOfBirth,Gendor,Address,Phone, Email, NationalityCountryID,ImagePath)
                             VALUES (@FirstName, @SecondName,@ThirdName, @LastName, @NationalNo,
                                     @DateOfBirth,@Gendor,@Address,@Phone, @Email,@NationalityCountryID,@ImagePath);
                             SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@FirstName", person.FirstName);
            command.Parameters.AddWithValue("@SecondName", person.SecondName);
            if (person.ThirdName != "" || person.ThirdName != null)
                command.Parameters.AddWithValue("@ThirdName", person.ThirdName);
            else
                command.Parameters.AddWithValue("@ThirdName", System.DBNull.Value);
            command.Parameters.AddWithValue("@LastName", person.LastName);
            command.Parameters.AddWithValue("@NationalNo", person.NationalNo);
            command.Parameters.AddWithValue("@DateOfBirth", person.DateOfBirth);
            command.Parameters.AddWithValue("@Gendor", person.Gendor);
            command.Parameters.AddWithValue("@Address", person.Address);
            command.Parameters.AddWithValue("@Phone", person.Phone);

            if (person.Email != "" && person.Email != null)
                command.Parameters.AddWithValue("@Email", person.Email);
            else
                command.Parameters.AddWithValue("@Email", System.DBNull.Value);

            command.Parameters.AddWithValue("@NationalityCountryID", person.NationalityCountryID);

            if (person.ImagePath != "" && person.ImagePath != null)
                command.Parameters.AddWithValue("@ImagePath", person.ImagePath);
            else
                command.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);

            try
            {
                connection.Open();

                object Result = command.ExecuteScalar();

                if (Result != null && int.TryParse(Result.ToString(), out int insertedId))
                {
                    PersonId = insertedId;
                }
            }
            catch (Exception ex)
            {

            }
            finally 
            { 
                connection.Close(); 
            }


            return PersonId;

        }

        public bool DeletePerson(int id)
        {
            int RowAffected = 0;
            
            SqlConnection connection = new SqlConnection(connectionString);

            string query = @"Delete People 
                                where PersonID = @PersonID";
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", id);

            try
            {
                connection.Open();
                RowAffected = command.ExecuteNonQuery();
            }
            catch
            {
                return false;
            }
            finally 
            { 
                connection.Close() ;
            }

            return RowAffected > 0;


        }

        public List<PersonDTO> GetAllPeople()
        {
            List<PersonDTO> result = new List<PersonDTO>();

            SqlConnection connection = new SqlConnection(connectionString);

            string query = @"SELECT People.PersonID, People.NationalNo,
                              People.FirstName, People.SecondName, People.ThirdName, People.LastName,
			                  People.DateOfBirth, People.Gendor,  
				                  CASE
                                  WHEN People.Gendor = 0 THEN 'Male'

                                  ELSE 'Female'

                                  END as GendorCaption ,
			                  People.Address, People.Phone, People.Email, 
                              People.NationalityCountryID, Countries.CountryName, People.ImagePath
                              FROM            People INNER JOIN
                                         Countries ON People.NationalityCountryID = Countries.CountryID
                                ORDER BY People.FirstName"; 

            SqlCommand Command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = Command.ExecuteReader();

                while (reader.Read())
                {
                    
                    PersonDTO person = new PersonDTO
                    { 
                        
                        PersonID = (int)reader["PersonID"],
                        NationalNo = reader.GetString(reader.GetOrdinal("NationalNo")),
                        FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                        SecondName = reader.GetString(reader.GetOrdinal("SecondName")),
                        ThirdName = (reader["ThirdName"] == DBNull.Value )? "" : reader.GetString(reader.GetOrdinal("ThirdName")),
                        LastName = reader.GetString(reader.GetOrdinal("LastName")),
                        DateOfBirth = reader.GetDateTime(reader.GetOrdinal("DateOfBirth")),
                        Gendor = (byte)reader["Gendor"],
                        Address = reader.GetString(reader.GetOrdinal("Address")),
                        Phone = reader.GetString(reader.GetOrdinal("Phone")),
                        Email = (reader["Email"] == DBNull.Value) ? "" : (string)reader["Email"],
                        NationalityCountryID = reader.GetInt32(reader.GetOrdinal("NationalityCountryID")),
                        ImagePath = (reader["ImagePath"] == DBNull.Value) ? "" : (string)reader["ImagePath"],
                        CountryName = (string)reader["CountryName"],
                        GendorCaption = (string)reader["GendorCaption"]
                    };
                    result.Add(person);
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally 
            {
                connection.Close();
            }
            return result;
        }

        public Person GetPersonById(int id)
        {

            bool isFound = false;

            SqlConnection connection = new SqlConnection(connectionString);

            string query = "Select * from People where PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", id);

            Person person = new Person();
            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;

                    person.PersonID = id;
                    person.FirstName = (string)reader["FirstName"];
                    person.SecondName = (string)reader["SecondName"];
                    if (reader["ThirdName"] != DBNull.Value)
                        person.ThirdName = (string)reader["ThirdName"];
                    
                    else
                        person.ThirdName = "";
                    person.LastName = (string)reader["LastName"];
                    person.LastName = (string)reader["LastName"];
                    person.NationalNo = (string)reader["NationalNo"];
                    person.DateOfBirth = (DateTime)reader["DateOfBirth"];
                    person.Gendor = (byte)reader["Gendor"];
                    person.Address = (string)reader["Address"];
                    person.Phone = (string)reader["Phone"];
                    if (reader["Email"] != DBNull.Value)
                    {
                        person.Email = (string)reader["Email"];
                    }
                    else
                    {
                        person.Email = "";
                    }

                    person.NationalityCountryID = (int)reader["NationalityCountryID"];

                    //ImagePath: allows null in database so we should handle null
                    if (reader["ImagePath"] != DBNull.Value)
                    {
                        person.ImagePath = (string)reader["ImagePath"];
                    }
                    else
                    {
                        person.ImagePath = "";
                    }
                    reader.Close();
                }
                else
                {
                    person = null;
                }
                
            }
            catch (Exception ex)
            {
                person = null;
            }
            finally 
            {
                connection.Close();
            }

            return person;

        }
        public Person GetPersonByNationalNo(string NationalNo)
        {

            bool isFound = false;

            SqlConnection connection = new SqlConnection(connectionString);

            string query = "Select * from People where NationalNo = @NationalNo";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@NationalNo", NationalNo);

            Person person = new Person();
            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;

                    person.PersonID = (int)reader["PersonID"];
                    person.FirstName = (string)reader["FirstName"];
                    person.SecondName = (string)reader["SecondName"];
                    if (reader["ThirdName"] != DBNull.Value)
                        person.ThirdName = (string)reader["ThirdName"];

                    else
                        person.ThirdName = "";
                    person.LastName = (string)reader["LastName"];
                    person.LastName = (string)reader["LastName"];
                    person.NationalNo = (string)reader["NationalNo"];
                    person.DateOfBirth = (DateTime)reader["DateOfBirth"];
                    person.Gendor = (byte)reader["Gendor"];
                    person.Address = (string)reader["Address"];
                    person.Phone = (string)reader["Phone"];
                    if (reader["Email"] != DBNull.Value)
                    {
                        person.Email = (string)reader["Email"];
                    }
                    else
                    {
                        person.Email = "";
                    }

                    person.NationalityCountryID = (int)reader["NationalityCountryID"];

                    //ImagePath: allows null in database so we should handle null
                    if (reader["ImagePath"] != DBNull.Value)
                    {
                        person.ImagePath = (string)reader["ImagePath"];
                    }
                    else
                    {
                        person.ImagePath = "";
                    }
                    reader.Close();
                }
                else
                {
                    person = null;
                }

            }
            catch (Exception ex)
            {
                person = null;
            }
            finally
            {
                connection.Close();
            }

            return person;

        }

        public bool IsPersonExistById(int id)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(connectionString);

            string query = "SELECT Found=1 FROM People WHERE PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", id);
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
                connection.Close( );
            }
            return isFound;

        }
        public bool IsPersonExistByNationalNo(string nationalNo)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(connectionString);

            string query = "SELECT Found=1 FROM People WHERE NationalNo = @nationalNo";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@nationalNo", nationalNo);
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

       

        public bool UpdatePerson(Person UpdatedPerson)
        {
            int RowAffected = 0;

            SqlConnection connection = new SqlConnection(connectionString);

            string query = @"Update  People  
                            set FirstName = @FirstName,
                                SecondName = @SecondName,
                                ThirdName = @ThirdName,
                                LastName = @LastName, 
                                NationalNo = @NationalNo,
                                DateOfBirth = @DateOfBirth,
                                Gendor=@Gendor,
                                Address = @Address,  
                                Phone = @Phone,
                                Email = @Email, 
                                NationalityCountryID = @NationalityCountryID,
                                ImagePath =@ImagePath
                                where PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", UpdatedPerson.PersonID);
            command.Parameters.AddWithValue("@FirstName", UpdatedPerson.FirstName);
            command.Parameters.AddWithValue("@SecondName", UpdatedPerson.SecondName);

            if (UpdatedPerson.ThirdName != "" && UpdatedPerson.ThirdName != null)
                command.Parameters.AddWithValue("@ThirdName", UpdatedPerson.ThirdName);
            else
                command.Parameters.AddWithValue("@ThirdName", System.DBNull.Value);


            command.Parameters.AddWithValue("@LastName", UpdatedPerson.LastName);
            command.Parameters.AddWithValue("@NationalNo", UpdatedPerson.NationalNo);
            command.Parameters.AddWithValue("@DateOfBirth", UpdatedPerson.DateOfBirth);
            command.Parameters.AddWithValue("@Gendor", UpdatedPerson.Gendor);
            command.Parameters.AddWithValue("@Address", UpdatedPerson.Address);
            command.Parameters.AddWithValue("@Phone", UpdatedPerson.Phone);

            if (UpdatedPerson.Email != "" && UpdatedPerson.Email != null)
                command.Parameters.AddWithValue("@Email", UpdatedPerson.Email);
            else
                command.Parameters.AddWithValue("@Email", System.DBNull.Value);

            command.Parameters.AddWithValue("@NationalityCountryID", UpdatedPerson.NationalityCountryID);

            if (UpdatedPerson.ImagePath != "" && UpdatedPerson.ImagePath != null)
                command.Parameters.AddWithValue("@ImagePath", UpdatedPerson.ImagePath);
            else
                command.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);
            try
            {
                connection.Open();
                RowAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex) 
            {
                return false;
            }

            return (RowAffected > 0 )? true : false;

        }
    }
}
