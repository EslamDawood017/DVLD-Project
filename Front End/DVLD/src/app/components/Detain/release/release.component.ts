import { Component } from '@angular/core';
import { LicenseInfo } from '../../../Models/LicenseInfo';
import { DetainService } from '../../../services/Detain/detain.service';
import { Router } from '@angular/router';
import { LicenseService } from '../../../services/License/license.service';
import { DatePipe, NgIf } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Detention } from '../../../Models/Detain';
import { AapplicationTypeService } from '../../../services/App Type/aapplication-type.service';
import { ApplicationType } from '../../../Models/ApplicationType';
import { enApplicationType } from '../../../enums/enApplicationType';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-release',
  imports: [NgIf, FormsModule, DatePipe ],
  templateUrl: './release.component.html',
  styleUrl: './release.component.css'
})
export class ReleaseComponent {
  LicenseId! : number  ;
  licenseInfo : LicenseInfo | null = null ;
  isExpired : boolean = true;
  found : boolean = true;
  detention! : Detention ; 
  ApplicationTypeInfo! : ApplicationType;
  isDetained : boolean = true;

  constructor(private DetainService : DetainService , 
    private LicenseService:LicenseService,
    private ApplicationTypeService : AapplicationTypeService,
    private router : Router){}

  ngOnInit(): void {
    this.ApplicationTypeService.getAppTypeById(Number(enApplicationType.ReleaseDetainedDrivingLicense)).subscribe({
      next:(res)=> {
        this.ApplicationTypeInfo = res;
      },
      error :(err) => {
        console.error(err);
      }
    })
  }

  getData(){
    this.getDetentionInfo();
    this.getLicenseInfo();
    this.isLicenseDetained();
  }

  getLicenseInfo(){
    this.LicenseService.getLicenseInfoByLicenseID(this.LicenseId).subscribe({
      next:(res)=> {
        this.licenseInfo = res;
        this.found = true ;
        this.isExpired = new Date(res.expirationDate).getTime() < Date.now() ? true : false ;
      },
      error:(err) =>{
         this.licenseInfo = null ;
         this.found = false;
      }
    })
  }

  getDetentionInfo(){
    this.DetainService.GetDetainedLicenseByLicenseId(this.LicenseId).subscribe({
      next : (res) => {
        this.detention = res ;
      },
      error :(err) => {
        console.error(err);
      }
    })
  }

  Close(){
    this.router.navigate(["/Home"])
  }

  isLicenseDetained(){
    this.DetainService.isLicenseDetained(this.LicenseId).subscribe({
      next:(res)=> {
        this.isDetained = res.isDetained;
      },
      error:(err)=>{
        console.error(err);
      }
    })
  }
  Release() {
    
    this.LicenseId = Number(this.licenseInfo?.licenseID)
    const createdByUserID = Number(localStorage.getItem("UserId"))
    

    console.log(this.LicenseId , createdByUserID);

    this.DetainService.ReleaseLicense(this.LicenseId , createdByUserID).subscribe({
      next: (res) => {
        Swal.fire({
          title: 'Success!',
          text: 'The license has been Released successfully.',
          icon: 'success',
          confirmButtonText: 'OK'
        }).then(() => {
            this.router.navigate(["/Home"]); 
          });
      },
      error: (err) => {
        Swal.fire({
          title: 'Error!',
          text: 'An error occurred while releasing the license. Please try again.',
          icon: 'error',
          confirmButtonText: 'OK'
        });
        console.error(err);
      }
    });
  } 
}
