import { HttpClient } from '@angular/common/http';
import { Injectable, Testability } from '@angular/core';
import { TestAppointment } from '../../Models/TestAppointment';
import { Observable } from 'rxjs';
import { environment } from '../../Environment/environment';
import { LocalDrivingLicenseApplicationService } from '../Local Driving License Application/local-driving-license-application.service';

@Injectable({
  providedIn: 'root'
})
export class TestAppointmentService {

  constructor(private http : HttpClient) { }

  addNewTestAppointmnet(testAppoinment : TestAppointment):Observable<any>{
    return this.http.post<any>(`${environment.apiUrl}TestAppointment/AddNewTestAppointment` , testAppoinment);
  }
  isThereActiveAppointmentPerTest(LocalDrivingLicenseApplicationId : number , TestType : number):Observable<any>{
    return this.http.get<any>(`${environment.apiUrl}LocalDrivingLicenseApplication/IsThereAnActiveScheduledTest?LocalDrivingLicenseApplicationID=${LocalDrivingLicenseApplicationId}&TestTypeID=${TestType}`);
  }
  getAllTestAppointmentPerTestType(LocalDrivingLicenseApplicationId : number , TestTypeId : number):Observable<any>{
    return this.http.get<any>(`${environment.apiUrl}TestAppointment/getTestAppintmentPerTestType?LocalDrivingLicenseApplicationId=${LocalDrivingLicenseApplicationId}&TestType=${TestTypeId}`);
  }
  getTestAppointmentInfo(LocalDrivingLicenseApplicationId : number , TestTypeId : number):Observable<TestAppointment>{
    return this.http.get<TestAppointment>(`${environment.apiUrl}TestAppointment/getLastTestAppointment?LocalDrivingLicenseApplicationId=${LocalDrivingLicenseApplicationId}&TestTypeId=${TestTypeId}`);
  }
  UpdateTestAppointment(TestAppointmentId : number ,newDate : string):Observable<any>{
    return this.http.post<any>(`${environment.apiUrl}TestAppointment/UpdateTestAppointmentDate?TestAppointmentId=${TestAppointmentId}&NewTestAppointmentDate=${newDate}` , {});
  }
}
