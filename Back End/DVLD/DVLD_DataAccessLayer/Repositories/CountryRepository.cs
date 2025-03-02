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
    public class CountryRepository : ICountryRepository
    {
        private readonly IConfiguration _configuration;
        string connectionString = "";
        public CountryRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = configuration.GetConnectionString("Default");
        }
        public List<Country> GetAllCountries()
        {

            List<Country> Countries = new List<Country>();

            SqlConnection connection = new SqlConnection(connectionString);

            string query = "SELECT * FROM Countries order by CountryName";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())

                {
                    var Country = new Country
                    {
                        CountryID = (int)reader["CountryID"],
                        CountryName = (string)reader["CountryName"]
                    };
                    Countries.Add(Country);

         

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

            return Countries;

        }
    }
}
