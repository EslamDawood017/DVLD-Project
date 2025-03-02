import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { country } from '../../Models/country';
import { environment } from '../../Environment/environment';

@Injectable({
  providedIn: 'root'
})
export class CountryService {

  constructor(private http : HttpClient) { }

  getAllCountries():Observable<country[]>{
    return this.http.get<country[]>(`${environment.apiUrl}Country`);
  }
}
