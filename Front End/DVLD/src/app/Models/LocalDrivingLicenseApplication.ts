export interface LocalDrivingLicenseApplication {
    localDrivingLicenseApplicationID: number;
    className: string;
    nationalNo: string;
    fullName: string;
    applicationDate: string; // or use Date if you plan to parse it into a Date object
    passedTestCount: number;
    status: string;
}