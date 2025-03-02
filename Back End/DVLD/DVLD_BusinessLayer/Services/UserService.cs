using DVLD_BusinessLayer.interfaces;
using DVLD_DataAccessLayer.DTOS;
using DVLD_DataAccessLayer.Interfaces;
using DVLD_DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusinessLayer.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        { 
            this.userRepository = userRepository;
        }
        public List<getUserDto> getAllUsers()
        {
            return userRepository.getAllUsers();
        }
        public bool DeleteUser(int UserID)
        {
            return userRepository.DeleteUser(UserID);
        }
        public bool IsUserExist(int UserID)
        {
            return userRepository.IsUserExist(UserID);
        }
        public bool IsUserExist(string UserName)
        {
            return userRepository.IsUserExist(UserName);
        }
        public bool ChangePassword(int UserID, string NewPassword)
        {
            return userRepository.ChangePassword(UserID, NewPassword);
        }
        public User GetUserInfoByUserID(int UserID)
        {
            return userRepository.GetUserInfoByUserID(UserID);
        }
        public User GetUserInfoByPersonID(int PersonID)
        {
            return userRepository.GetUserInfoByPersonID(PersonID);
        }
        public int AddNewUser(User newUser)
        {
            return userRepository.AddNewUser(newUser);
        }
        public User GetUserInfoByUsernameAndPassword(string UserName, string Password)
        {
            return userRepository.GetUserInfoByUsernameAndPassword(UserName,Password);
        }
        public bool UpdateUser(User updatedUser)
        {
            return userRepository.UpdateUser(updatedUser);
        }
        public bool IsUserExistForPersonID(int PersonID)
        {
            return userRepository.IsUserExistForPersonID(PersonID);
        }

    }
}
