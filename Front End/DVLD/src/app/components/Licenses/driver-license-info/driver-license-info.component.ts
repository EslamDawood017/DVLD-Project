import { Component, OnInit } from '@angular/core';
import { LicenseInfo } from '../../../Models/LicenseInfo';
import { LicenseService } from '../../../services/License/license.service';
import { ActivatedRoute, Router } from '@angular/router';
import { environment } from '../../../Environment/environment';
import { DatePipe, NgIf } from '@angular/common';

@Component({
  selector: 'app-driver-license-info',
  imports: [DatePipe , NgIf],
  templateUrl: './driver-license-info.component.html',
  styleUrl: './driver-license-info.component.css'
})
export class DriverLicenseInfoComponent implements OnInit {

  license! : LicenseInfo ;
  LocalDrivingLicenseApplicatioId : number = 0 ;
  host = environment.apiUrl ;
  DoesPersonHaveAlicense : boolean = false;

  constructor(private licenseService : LicenseService , 
    private route : ActivatedRoute , 
    private router : Router
  ) {}

  ngOnInit(): void {
    this.LocalDrivingLicenseApplicatioId = Number(this.route.snapshot.paramMap.get("id"));
    this.getLicenseInfo();
  }
  getLicenseInfo(){
    this.licenseService.getLicenseInfo(this.LocalDrivingLicenseApplicatioId).subscribe({
      next: (res)=> {
        this.license = res;
        this.DoesPersonHaveAlicense = true;
      }, 
      error:(err)=>{
        console.error(err);
        this.DoesPersonHaveAlicense = false ;
        console.log(this.DoesPersonHaveAlicense);
      }
    })
  }
  Close(){
    this.router.navigate(["getAllLDLA"]);
  }
  
}
