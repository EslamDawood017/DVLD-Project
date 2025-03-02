using DVLD_DataAccessLayer.DTOS;
using DVLD_DataAccessLayer.Interfaces;
using DVLD_DataAccessLayer.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessLayer.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration configuration;
        private string  ConnectionString = "" ;

        public UserRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
            ConnectionString = configuration.GetConnectionString("Default");
        }
        public User GetUserInfoByUserID(int UserID)
        {
           

            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = "SELECT * FROM Users WHERE UserID = @UserID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@UserID", UserID);

            var user = new User();

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                

                if (reader.Read())
                {
                    user.UserID = (int)reader["UserID"];
                    user.PersonID = (int)reader["PersonID"];
                    user.UserName = (string)reader["UserName"];
                    user.Password = (string)reader["Password"];
                    user.IsActive = (bool)reader["IsActive"];
                    user.Role = reader["Role"] == DBNull.Value ? "" : (string)reader["Role"];

                }
                else
                {

                    user = null;
                }

                reader.Close();


            }
            catch (Exception ex)
            {
                user = null;
            }
            finally
            {
                connection.Close();
            }

            return user;
        }
        public User GetUserInfoByPersonID(int PersonID)
        {


            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = "SELECT * FROM Users WHERE PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);

            var user = new User();

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();



                if (reader.Read())
                {
                    user.UserID = (int)reader["UserID"];
                    user.PersonID = (int)reader["PersonID"];
                    user.UserName = (string)reader["UserName"];
                    user.Password = (string)reader["Password"];
                    user.IsActive = (bool)reader["IsActive"];
                    user.Role = reader["Role"] == DBNull.Value ? "" : (string)reader["Role"];

                }
                else
                {

                    user = null;
                }

                reader.Close();


            }
            catch (Exception ex)
            {
                user = null;
            }
            finally
            {
                connection.Close();
            }

            return user;
        }
        public User GetUserInfoByUsernameAndPassword(string UserName, string Password)
        {


            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = "SELECT * FROM Users WHERE Username = @Username and Password=@Password;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@Username", UserName);
            command.Parameters.AddWithValue("@Password", Password);


            var user = new User();

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();



                if (reader.Read())
                {
                    user.UserID = (int)reader["UserID"];
                    user.PersonID = (int)reader["PersonID"];
                    user.UserName = (string)reader["UserName"];
                    user.Password = (string)reader["Password"];
                    user.IsActive = (bool)reader["IsActive"];
                    user.Role = reader["Role"] == DBNull.Value ? "" : (string)reader["Role"];

                }
                else
                {

                    user = null;
                }

                reader.Close();


            }
            catch (Exception ex)
            {
                user = null;
            }
            finally
            {
                connection.Close();
            }

            return user;
        }
        public List<getUserDto> getAllUsers()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = @"SELECT  Users.UserID, Users.PersonID,
                            FullName = People.FirstName + ' ' + People.SecondName + ' ' + ISNULL( People.ThirdName,'') +' ' + People.LastName,
                             Users.UserName, Users.IsActive , Users.Role
                             FROM  Users INNER JOIN
                                    People ON Users.PersonID = People.PersonID";

            List<getUserDto> users = new List<getUserDto>();

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while(reader.Read())

                {
                    getUserDto user = new getUserDto
                    {
                        UserID = (int)reader["UserID"],
                        PersonID = (int)reader["PersonID"],
                        UserName = (string)reader["UserName"],
                        FullName = (string)reader["FullName"],
                        IsActive = (bool)reader["IsActive"],
                        Role = reader["Role"] == DBNull.Value? "" : (string)reader["Role"]


                    };
                    users.Add(user);
                }

                reader.Close();


            }
            catch (Exception ex)
            {
                
            }
            finally
            {
                connection.Close();
            }

            return users;
        }
        public bool DeleteUser(int UserID)
        {

            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = @"Delete Users 
                                where UserID = @UserID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@UserID", UserID);

            try
            {
                connection.Open();

                rowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                
            }
            finally
            {

                connection.Close();

            }

            return (rowsAffected > 0);

        }
        public  bool IsUserExist(int UserID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = "SELECT Found=1 FROM Users WHERE UserID = @UserID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@UserID", UserID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                isFound = reader.HasRows;

                reader.Close();
            }
            catch (Exception ex)
            {
                
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }
        public  bool IsUserExist(string UserName)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = "SELECT Found=1 FROM Users WHERE UserName = @UserName";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@UserName", UserName);

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
        public  bool IsUserExistForPersonID(int PersonID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = "SELECT Found=1 FROM Users WHERE PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                isFound = reader.HasRows;

                reader.Close();
            }
            catch (Exception ex)
            {
                
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }
        public  bool ChangePassword(int UserID, string NewPassword)
        {

            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = @"Update  Users  
                            set Password = @Password
                            where UserID = @UserID";

            SqlCommand command = new SqlCommand(query, connection);


            command.Parameters.AddWithValue("@UserID", UserID);
            command.Parameters.AddWithValue("@Password", NewPassword);



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
        public  int AddNewUser(User newUser)
        {
            //this function will return the new person id if succeeded and -1 if not.
            int UserID = -1;

            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = @"INSERT INTO Users (PersonID,UserName,Password,IsActive , Role)
                             VALUES (@PersonID, @UserName,@Password,@IsActive , @Role);
                             SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", newUser.PersonID);
            command.Parameters.AddWithValue("@UserName", newUser.UserName);
            command.Parameters.AddWithValue("@Password", newUser.Password);
            command.Parameters.AddWithValue("@IsActive", newUser.IsActive);
            if(newUser.Role == "")
            {
                command.Parameters.AddWithValue("@Role", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@Role", newUser.Role);
            }
            


            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    UserID = insertedID;
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

            return UserID;
        }
        public  bool UpdateUser(User updatedUser)
        {

            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = @"Update  Users  
                            set PersonID = @PersonID,
                                UserName = @UserName,
                                Password = @Password,
                                IsActive = @IsActive , 
                                Role     = @Role
                                where UserID = @UserID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", updatedUser.PersonID);
            command.Parameters.AddWithValue("@UserName", updatedUser.UserName);
            command.Parameters.AddWithValue("@Password", updatedUser.Password);
            command.Parameters.AddWithValue("@IsActive", updatedUser.IsActive);
            command.Parameters.AddWithValue("@UserID", updatedUser.UserID);
            if (updatedUser.Role == "")
            {
                command.Parameters.AddWithValue("@Role", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@Role", updatedUser.Role);
            }



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
