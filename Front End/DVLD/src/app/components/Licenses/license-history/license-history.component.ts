import { Component, OnInit } from '@angular/core';
import { Person } from '../../../Models/Person';
import { LicenseInfo } from '../../../Models/LicenseInfo';
import { LicenseService } from '../../../services/License/license.service';
import { ActivatedRoute, Router } from '@angular/router';
import { PersonService } from '../../../services/Person/person.service';
import { DatePipe, NgClass, NgFor } from '@angular/common';

@Component({
  selector: 'app-license-history',
  imports: [ NgClass, NgFor , DatePipe],
  templateUrl: './license-history.component.html',
  styleUrl: './license-history.component.css'
})
export class LicenseHistoryComponent implements OnInit {

  PersonInfo! : Person ;
  LicensesInfo : LicenseInfo []= [];
  NationalNo : string = '' ;

  constructor(private personService : PersonService ,
    private LicenseService : LicenseService , 
    private route : ActivatedRoute , 
    private router : Router ) {}

  ngOnInit(): void {
    this.NationalNo = String(this.route.snapshot.paramMap.get("nationalNo"));

    this.FetchPersonInfo();
    this.FetchLicenesInfo();
  }

  FetchPersonInfo(){
    this.personService.getPersonByNationalNumber(this.NationalNo).subscribe({
      next:(res) => {
        this.PersonInfo = res ;
      },
      error :(err) => {
        console.error(err);
      }
    });
  }

  FetchLicenesInfo(){
    this.LicenseService.GetAllLicenseForPerosn(this.NationalNo).subscribe({
      next:(res) => {
        this.LicensesInfo = res ;
      },
      error :(err) => {
        console.error(err);
      }
    });
  }
  Cancel(){
    this.router.navigate(["List-Drivers"]);
  }

}
