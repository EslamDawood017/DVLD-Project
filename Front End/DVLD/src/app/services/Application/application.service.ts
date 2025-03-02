import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Application } from '../../Models/Application';
import { Observable } from 'rxjs';
import { environment } from '../../Environment/environment';

@Injectable({
  providedIn: 'root'
})
export class ApplicationService {

  constructor(private http : HttpClient) { }

  addNewApplication(application : Application):Observable<number>{
    return this.http.post<number>(`${environment.apiUrl}Application/Addnew` , application);
  }
  
}
