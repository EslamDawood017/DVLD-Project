import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { TestType } from '../../Models/TestType';
import { environment } from '../../Environment/environment';

@Injectable({
  providedIn: 'root'
})
export class TestTypeService {

   constructor(private http: HttpClient) { }
  
    getAllTestTypes(): Observable<TestType[]> {
      return this.http.get<TestType[]>(`${environment.apiUrl}TestType/getAll`);
    }
    UpdateTestType(TestType : TestType):Observable<any>{
      return this.http.put<any>(`${environment.apiUrl}TestType/Update` , TestType);
    }
    getTestTypeById(id : number):Observable<TestType>{
      return this.http.get<TestType>(`${environment.apiUrl}TestType/${id}`);
    }
}
