import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Person } from '../../Models/Person';
import { environment } from '../../Environment/environment';
import { AnyCatcher } from 'rxjs/internal/AnyCatcher';


@Injectable({
  providedIn: 'root'
})
export class PersonService {

  constructor(private http : HttpClient) { }

  getAllPeople():Observable<Person[]>{
    return this.http.get<Person[]>(`${environment.apiUrl}Person/getAll`);
  }
  addNewPerson(person : any):Observable<number>{
    return this.http.post<number>(`${environment.apiUrl}Person/AddNew` , person);
  }
  deletePerson(personId : number):Observable<any>{
    return this.http.delete(`${environment.apiUrl}Person?personId=${personId}`);
  }
  getPersonById(personId : number):Observable<Person>{
    return this.http.get<Person>(`${environment.apiUrl}Person/${personId}`)
  }
  updatePerson(updatedPerson: any):Observable<any>{
    return this.http.put<any>(`${environment.apiUrl}Person/Update` , updatedPerson);
  }
  getPersonByNationalNumber(NationalNum : string):Observable<Person>{
    return this.http.get<Person>(`${environment.apiUrl}Person/nationalNo?NationalNo=${NationalNum}`);
  }
}
