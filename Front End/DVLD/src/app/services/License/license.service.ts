import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../Environment/environment';
import { LicenseInfo } from '../../Models/LicenseInfo';
import { RenewLicense } from '../../Models/RenewLicense';

@Injectable({
  providedIn: 'root'
})
export class LicenseService {

  constructor(private http : HttpClient) { }

  IsLicenseExistByLocalDrivingLicenseApplicationID(LocalDrivingLicenseAppId :number , LicenseClassName : string):Observable<boolean>{
    return this.http.get<boolean>(`${environment.apiUrl}License/IsLicenseExistByLocalDrivingLicenseAppID?LocalDrivingLicenseAppID=${LocalDrivingLicenseAppId}&LicenseClassName=${LicenseClassName}`);
  }
  IssueForFirstTime(LicenseData : any) : Observable<number>{
    return this.http.post<number>(`${environment.apiUrl}License/IssueForFirstTime` , LicenseData);
  }
  getLicenseInfo(LocalDrivngLicenseApplicationId : number):Observable<LicenseInfo>{
    return this.http.get<LicenseInfo>(`${environment.apiUrl}License/${LocalDrivngLicenseApplicationId}`);
  }
  getLicenseInfoByLicenseID(LicenseId : number):Observable<LicenseInfo>{
    return this.http.get<LicenseInfo>(`${environment.apiUrl}License/LicenseId/${LicenseId}`);
  }
  RenewLicense(Data :RenewLicense ):Observable<number>{
    return this.http.post<number>(`${environment.apiUrl}License/RenewLicense/${Data.licenseId}` , Data);
  }
  ReplaceLicense(Data :any ):Observable<number>{
    return this.http.post<number>(`${environment.apiUrl}License/ReplaceForLostOrDamageLicense` , Data);
  }
  GetAllLicenseForPerosn(NationalNo : string):Observable<LicenseInfo[]>{
    return this.http.get<LicenseInfo[]>(`${environment.apiUrl}License/All/Person/${NationalNo}`);
  }

}
