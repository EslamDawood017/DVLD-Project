import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../Environment/environment';

@Injectable({
  providedIn: 'root'
})
export class ProfileService {

  constructor(private http : HttpClient) { }

  getUserProfile(userId : number) : Observable<any>{
    return this.http.get<any>(`${environment.apiUrl}Login/${userId}`);
  }
}
