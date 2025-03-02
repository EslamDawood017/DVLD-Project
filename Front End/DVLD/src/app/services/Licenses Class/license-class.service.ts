import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { LicenseClass } from '../../Models/LicenseClass';
import { environment } from '../../Environment/environment';

@Injectable({
  providedIn: 'root'
})
export class LicenseClassService {

  constructor(private http : HttpClient) { }

  getAllLicenseClasses():Observable<LicenseClass[]>{
    return this.http.get<LicenseClass[]>(`${environment.apiUrl}LicenseClass/getAll`);
  }
}
