import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../Environment/environment';
import { Observable } from 'rxjs';
import { ApplicationType } from '../../Models/ApplicationType';

@Injectable({
  providedIn: 'root'
})
export class AapplicationTypeService {

  constructor(private http: HttpClient) { }

  getAllApplicationTypes(): Observable<ApplicationType[]> {
    return this.http.get<ApplicationType[]>(`${environment.apiUrl}AppType/getAll`);
  }
  UpdateApplicationType(AppType : ApplicationType):Observable<any>{
    return this.http.put<any>(`${environment.apiUrl}AppType/Update` , AppType);
  }
  getAppTypeById(id : number):Observable<ApplicationType>{
    return this.http.get<ApplicationType>(`${environment.apiUrl}AppType/${id}`);
  }
}
