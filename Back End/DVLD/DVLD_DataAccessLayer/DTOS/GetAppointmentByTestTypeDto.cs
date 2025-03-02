﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessLayer.DTOS
{
    public class GetAppointmentByTestTypeDto
    {
        public int TestAppointmentID { get; set; }
        
        public DateTime AppointmentDate { get; set; }
        public decimal PaidFees { get; set; }
        public bool IsLocked { get; set; }
    }
}
