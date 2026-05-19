using DVLD_BusinessLayer.Services;
using DVLD_DataAccessLayer.Interfaces;
using DVLD_DataAccessLayer.Models;
using Moq;

namespace DVLD.Test;

public class CountryServiceTests
{
    [Fact]
    public void getAllCountry_ReturnCountyList()
    {
        //Arrange
        var mockRepository = new Mock<ICountryRepository>();

        //create fake data
        var fakeCountries = new List<Country>
        {
            new Country {CountryID = 1 , CountryName = "Egypt"},
            new Country {CountryID = 2 , CountryName = "Jerman"},
            new Country {CountryID = 3 , CountryName = "America"}
        };

        //Setup the mock to return fake data when GetAllCountry is called
        mockRepository.Setup(repo => repo.GetAllCountries())
            .Returns(fakeCountries);

        var service = new CountryService(mockRepository.Object);

        //Act 
        var Countries = service.getAllCountry();

        //Assert

        Assert.Equal(3, Countries.Count);
        Assert.Contains(Countries, p => p.CountryName == "Egypt");
        Assert.Contains(Countries, p => p.CountryName == "Jerman");
        Assert.Contains(Countries, p => p.CountryName == "America");
    }
}