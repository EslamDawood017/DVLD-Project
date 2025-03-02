using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessLayer.DTOS
{
    public class getUserDto
    {
        public int UserID { set; get; }
        public int PersonID { set; get; }
        public string UserName { set; get; }
        public string FullName { set; get; }
        public bool IsActive { set; get; } 
        public string? Role { set; get; }
    }
}
