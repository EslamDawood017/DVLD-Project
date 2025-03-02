export class LicenseInfo {
    className: string;
    fullName: string;
    licenseID: string;
    nationalNo: string;
    issueDate: Date;
    gender: string;
    imagePath: string;
    expirationDate: Date;
    issueReason: string;
    notes: string;
    isActive: string;
    dateOfBirth: Date;
    driverID: string;
  
    constructor(
      className: string,
      fullName: string,
      licenseID: string,
      nationalNo: string,
      issueDate: Date,
      gender: string,
      imagePath: string,
      expirationDate: Date,
      issueReason: string,
      notes: string,
      isActive: string,
      dateOfBirth: Date,
      driverID: string
    ) {
      this.className = className;
      this.fullName = fullName;
      this.licenseID = licenseID;
      this.nationalNo = nationalNo;
      this.issueDate = issueDate;
      this.gender = gender;
      this.imagePath = imagePath;
      this.expirationDate = expirationDate;
      this.issueReason = issueReason;
      this.notes = notes;
      this.isActive = isActive;
      this.dateOfBirth = dateOfBirth;
      this.driverID = driverID;
    }
  }