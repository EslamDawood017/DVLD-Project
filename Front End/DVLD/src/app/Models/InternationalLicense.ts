export interface InternationalLicense {
    internationalLicenseID: number;
    applicationID: number;
    driverID: number;
    issuedUsingLocalLicenseID: number;
    issueDate: Date;  // ISO date string
    expirationDate: Date;  // ISO date string
    isActive: boolean;
    createdByUserID: number;
  }
  