using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessLayer.Models
{
    public class User
    {
        public int UserID { get; set; }
        public int PersonID { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public bool IsActive { get; set; }
        public string? Role { get; set; } = "User";
    }
}
