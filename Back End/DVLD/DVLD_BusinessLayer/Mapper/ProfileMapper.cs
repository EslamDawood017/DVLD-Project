using DVLD_DataAccessLayer.DTOS;
using DVLD_DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusinessLayer.Mapper
{
    public static class ProfileMapper
    {
        public static ProfileInfoDto GetProfileInfo(User user, Person person)
        {
            return new ProfileInfoDto
            {
                UserName = user.UserName,
                Role = user.Role,
                IsActive = user.IsActive,
                Address = person.Address,
                DateOfBirth = person.DateOfBirth,
                Email = person.Email,
                NationalNo = person.NationalNo,
                FirstName = person.FirstName,
                SecondName = person.SecondName,
                ThirdName = person.ThirdName,
                LastName = person.LastName,
                Gendor = person.Gendor,
                Phone = person.Phone,
                ImagePath = person.ImagePath

            };
        }
    }
}
