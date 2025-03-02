using DVLD_DataAccessLayer.DTOS;
using DVLD_DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusinessLayer.interfaces
{
    public interface IUserService
    {
        public List<getUserDto> getAllUsers();
        public bool DeleteUser(int UserID);
        public bool IsUserExist(int UserID);
        public bool IsUserExist(string UserName);
        public bool ChangePassword(int UserID, string NewPassword);
        public User GetUserInfoByUserID(int UserID);
        public User GetUserInfoByPersonID(int PersonID);
        public int AddNewUser(User newUser);
        public bool UpdateUser(User updatedUser);
        public bool IsUserExistForPersonID(int PersonID);
        public User GetUserInfoByUsernameAndPassword(string UserName, string Password);

    }
}
