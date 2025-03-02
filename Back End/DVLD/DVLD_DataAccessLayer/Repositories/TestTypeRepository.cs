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
    public class TestTypeRepository : ITestTypeRepository
    {
        private readonly IConfiguration _configuration;
        private string ConnectionString = "";
        public TestTypeRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            ConnectionString = _configuration.GetConnectionString("Default");
        }
        public int AddNewTestType(TestType Testtype)
        {
            int TestTypeID = -1;
            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = @"Insert Into TestTypes (TestTypeTitle,TestTypeDescription,TestTypeFees)
                            Values (@TestTypeTitle,@TestTypeDescription,@ApplicationFees)
                           
                            SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestTypeTitle", Testtype.TestTypeTitle);
            command.Parameters.AddWithValue("@TestTypeDescription", Testtype.TestTypeDescription);
            command.Parameters.AddWithValue("@ApplicationFees", Testtype.TestTypeFees);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    TestTypeID = insertedID;
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


            return TestTypeID;
        }

        public List<TestType> GetAllTestTypes()
        {
            List<TestType> TestTypes = new List<TestType>();

            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = "SELECT * FROM TestTypes order by TestTypeID";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var TestType = new TestType
                    {
                        TestTypeID = (int)reader["TestTypeID"],
                        TestTypeTitle  = (string)reader["TestTypeTitle"],
                        TestTypeDescription = (string)reader["TestTypeDescription"],
                        TestTypeFees = (decimal)reader["TestTypeFees"]
                    };

                    TestTypes.Add(TestType);
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

            return TestTypes;
        }

        public TestType GetTestTypeInfoById(int TestTypeId)
        {
            TestType testType = new TestType();

            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = "SELECT * FROM TestTypes WHERE TestTypeID = @TestTypeID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestTypeID", TestTypeId);


            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    // The record was found

                    testType.TestTypeID = (int)reader["TestTypeID"];
                    testType.TestTypeTitle = (string)reader["TestTypeTitle"];
                    testType.TestTypeDescription = (string)reader["TestTypeDescription"];
                    testType.TestTypeFees = (decimal)reader["TestTypeFees"];




                }
                else
                {
                    // The record was not found
                    testType = null;
                }

                reader.Close();


            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                testType = null;
            }
            finally
            {
                connection.Close();
            }

            return testType;
        }

        public bool UpdateTestType(TestType Testtype)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = @"Update  TestTypes  
                            set TestTypeTitle = @TestTypeTitle,
                                TestTypeDescription=@TestTypeDescription,
                                TestTypeFees = @TestTypeFees
                                where TestTypeID = @TestTypeID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestTypeID", Testtype.TestTypeID);
            command.Parameters.AddWithValue("@TestTypeTitle", Testtype.TestTypeTitle);
            command.Parameters.AddWithValue("@TestTypeDescription", Testtype.TestTypeDescription);
            command.Parameters.AddWithValue("@TestTypeFees", Testtype.TestTypeFees);

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
