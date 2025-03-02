using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessLayer.Models
{
    public class InternationalLicense
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InternationalLicenseID { get; set; }

        [Required]
        public int ApplicationID { get; set; }

        [Required]
        public int DriverID { get; set; }

        [Required]
        public int IssuedUsingLocalLicenseID { get; set; }

        [Required]
        [Column(TypeName = "smalldatetime")]
        public DateTime IssueDate { get; set; }

        [Required]
        [Column(TypeName = "smalldatetime")]
        public DateTime ExpirationDate { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public int CreatedByUserID { get; set; }
    }
}
