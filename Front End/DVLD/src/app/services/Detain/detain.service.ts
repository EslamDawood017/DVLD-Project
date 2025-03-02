import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../Environment/environment';
import { Detention, DetentionList } from '../../Models/Detain';

@Injectable({
  providedIn: 'root'
})
export class DetainService {

  constructor(private http:HttpClient){}

  AddNewDetain(detain : any):Observable<any>{
    return this.http.post<any>(`${environment.apiUrl}DetainedLicense/add` , detain );
  }
  ReleaseLicense(LicenseId : number , ReleasedByUserID : number):Observable<any>{
    return this.http.put<any>(`${environment.apiUrl}DetainedLicense/release/${LicenseId}?ReleasedByUserId=${ReleasedByUserID}` , {});
  }
  GetDetainedLicenseByLicenseId(LicenseID : number):Observable<Detention>{
    return this.http.get<Detention>(`${environment.apiUrl}DetainedLicense/license/${LicenseID}`);
  }
  isLicenseDetained(LicenseId : number):Observable<{isDetained: boolean}>{
    return this.http.get<{isDetained: boolean}>(`${environment.apiUrl}DetainedLicense/isDetained/${LicenseId}`);
  }
  GetAllDetainedLicenses():Observable<DetentionList[]>{
    return this.http.get<DetentionList[]>(`${environment.apiUrl}DetainedLicense/all`);
  }
}
