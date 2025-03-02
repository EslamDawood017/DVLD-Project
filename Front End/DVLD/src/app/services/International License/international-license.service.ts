import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../Environment/environment';
import { InternationalLicense } from '../../Models/InternationalLicense';

@Injectable({
  providedIn: 'root'
})
export class InternationalLicenseService {

  constructor(private http : HttpClient) { }

  CreateNewInternationalLicense(LocalLicenseId :number , CreatedByUserId : number):Observable<any>{
    return this.http.post<any>(`${environment.apiUrl}InternationalLicense/Add/${LocalLicenseId}?CreatedByUserId=${CreatedByUserId}` , {})
  }

  GetAllInternationalLicense():Observable<InternationalLicense[]>{
    return this.http.get<InternationalLicense[]>(`${environment.apiUrl}InternationalLicense/All` );
  }

}
