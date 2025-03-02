import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../Environment/environment';
import { LocalDrivingLicenseApplication } from '../../Models/LocalDrivingLicenseApplication';
import { ApplicationInfo } from '../../Models/ApplicationInfo';
import { enStatus } from '../../enums/enApplicationStaus';

@Injectable({
  providedIn: 'root'
})
export class LocalDrivingLicenseApplicationService {

  constructor(private http : HttpClient) { }

  // Add new local driving license application
  addNewApplication(data: any): Observable<any> {
    return this.http.post<any>(`${environment.apiUrl}LocalDrivingLicenseApplication/AddNew`, data);
  }
  IsThereAnActiveApplication(personId : number , LicenseClassId : number ): Observable<boolean>{
    return this.http.get<boolean>(`${environment.apiUrl}LocalDrivingLicenseApplication/IsThereAnActiveApplication?PersonId=${personId}&licenseClassId=${LicenseClassId}`);
  }
  getAllLocalDrivingLicenseApplications():Observable<LocalDrivingLicenseApplication[]>{
    return this.http.get<LocalDrivingLicenseApplication[]>(`${environment.apiUrl}LocalDrivingLicenseApplication/getAll`);
  }
  DoesPersonAttendTestType(LocalDrivingLicenseApplicationID : number , TestTypeId : number):Observable<boolean>{
    return this.http.get<boolean>(`${environment.apiUrl}LocalDrivingLicenseApplication/DoesAttendTestType?LocalDrivingLicenseApplicationID=${LocalDrivingLicenseApplicationID}&TestTypeID=${TestTypeId}`);
  }
  getTotalNumberOfTrails(LocalDrivingLicenseApplicationID : number , TestTypeId : number):Observable<number>{
    return this.http.get<number>(`${environment.apiUrl}LocalDrivingLicenseApplication/TotalTrialPerTest?LocalDrivingLicenseApplicationID=${LocalDrivingLicenseApplicationID}&TestTypeID=${TestTypeId}`);
  }
  getPersonIdForLocalDrivingLicenseApp(LocalDrivingLicenseApplicationID:number):Observable<number>{
    return this.http.get<number>(`${environment.apiUrl}LocalDrivingLicenseApplication/GetPersonIdForLocalDrivingLicense?LocalDrivingLicenseApplicationID=${LocalDrivingLicenseApplicationID}`);
  }
  getBaseApplication(localdrivingLicenseId : number):Observable<ApplicationInfo>{
    return this.http.get<ApplicationInfo>(`${environment.apiUrl}LocalDrivingLicenseApplication/getBaseApplicationForLocalDrivingLicenseApplication?LocalDrivingLicenseApplicationID=${localdrivingLicenseId}`);
  }
  getLocalDrivingLicenseApplicationById(id : number):Observable<any>{
    return this.http.get<any>(`${environment.apiUrl}LocalDrivingLicenseApplication/GetLocalDrivingLicenseAppById/${id}`);
  }
  UpdateLocalDrivingLicenseApplication(LocalDrivingLicenseApplicationID : number , Status : enStatus):Observable<any>{
    return this.http.put<any>(`${environment.apiUrl}Application/UpdateStatusByLocalDrivingLicenseAppId?LocalDrivingLicenseAppId=${LocalDrivingLicenseApplicationID}&NewStatus=${Status}`, {});
  }

}
