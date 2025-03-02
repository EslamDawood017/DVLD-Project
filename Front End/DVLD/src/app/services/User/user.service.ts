import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from '../../Models/User';
import { environment } from '../../Environment/environment';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http : HttpClient) { }

  // Get all users
  getAllUsers(): Observable<User[]> {
    return this.http.get<User[]>(`${environment.apiUrl}User/GetAll`);
  }

  //update User 
  updateUser(updatedUser : any):Observable<any>{
    return this.http.put<any>(`${environment.apiUrl}User/Update` , updatedUser)
  }
  getUserById(UserId : number):Observable<any>{
    return this.http.get<any>(`${environment.apiUrl}User/${UserId}`);
  }
  // Delete a user
  deleteUser(userID: number): Observable<void> {
    return this.http.delete<void>(`${environment.apiUrl}User/DeleteUser?id=${userID}`);
  }
  AddNewUser(user : User):Observable<any>{
    return this.http.post<any>(`${environment.apiUrl}User/AddNew` , user);
  }
  checkUsernameExists(userName : string) :Observable<{ exists: boolean }>{
    return this.http.get<{ exists: boolean }>(`${environment.apiUrl}User/CheckUsernameExists/${userName}`)
  }
  checkPersonAssigned(personId: number) :Observable<{ assigned: boolean }>{
    return this.http.get<{ assigned: boolean }>(`${environment.apiUrl}User/CheckPersonAssigned/${personId}`)
  }
  changePassword(data: { userId: number; oldPassword: string; newPassword: string }):Observable<any>{
    return this.http.post<any>(`${environment.apiUrl}Login/ChangePassword` , data);
  }
}
