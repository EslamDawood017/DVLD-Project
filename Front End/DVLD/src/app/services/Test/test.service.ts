import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../Environment/environment';
import { TestResult } from '../../Models/Test';

@Injectable({
  providedIn: 'root'
})
export class TestService {

  constructor(private http : HttpClient) { }

  GetNumberOfPassedTest(LocalDrivingLicenseApplicationId : Number):Observable<number>{
    return this.http.get<number>(`${environment.apiUrl}Test/PassedTest?localDrivingLicenseApplication=${LocalDrivingLicenseApplicationId}`);
  }
  addNewTest(newTest : TestResult) : Observable<number>{
    return this.http.post<number>(`${environment.apiUrl}Test/addNew` , newTest);
  }
  GetLastTestByPersonAndTestTypeAndLicenseClass(personId : number , LicenseClassID : number , TestTypeID : number):Observable<boolean>{
    return this.http.get<boolean>(`${environment.apiUrl}Test/GetLastTestByPersonAndTestTypeAndLicenseClass?PersonID=${personId}&LicenseClassID=${LicenseClassID}&TestTypeID=${TestTypeID}`);
  }
}
