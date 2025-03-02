export interface TestAppointment {
    testAppointmentID: number;
    localDrivingLicenseApplicationID: number;
    testTypeID: number;
    appointmentDate: Date; 
    paidFees: number;
    createdByUserID: number;
    isLocked: boolean;
    retakeTestApplicationID: number;
  }