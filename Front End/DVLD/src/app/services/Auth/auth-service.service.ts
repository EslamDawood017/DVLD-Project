import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../Environment/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthServiceService {

  constructor(private http : HttpClient) { }

  login(UserData : { userName: string; password: string } ):Observable<any>{
    return this.http.post<any>(`${environment.apiUrl}User/login` , UserData);
  }
  
  isAuthenticated(): boolean {
    return !!localStorage.getItem('token'); // Check if token exists
  }

  logout(): void {
    localStorage.removeItem('token');
    localStorage.removeItem('userName');
    localStorage.removeItem('role');
    localStorage.removeItem('UserId');
  }
}
