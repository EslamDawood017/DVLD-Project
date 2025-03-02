import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Driver } from '../../Models/Driver';
import { environment } from '../../Environment/environment';

@Injectable({
  providedIn: 'root'
})
export class DriverService {

  constructor(private http : HttpClient) { }

  GetAllDrivers():Observable<Driver[]>{
    return this.http.get<Driver[]>(`${environment.apiUrl}Driver/all`)
  }
}
