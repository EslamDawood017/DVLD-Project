import { Component } from '@angular/core';
import { LicenseInfo } from '../../../Models/LicenseInfo';
import { InternationalLicenseService } from '../../../services/International License/international-license.service';
import { LicenseService } from '../../../services/License/license.service';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';
import { DatePipe, NgIf } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-new-international-license',
  imports: [NgIf ,FormsModule , DatePipe],
  templateUrl: './new-international-license.component.html',
  styleUrl: './new-international-license.component.css'
})
export class NewInternationalLicenseComponent {
  LicenseId! : number  ;
  licenseInfo : LicenseInfo | null = null ;
  found : boolean = true;
  IsValidClass :boolean = true;

  constructor(private InternationalLicenseService : InternationalLicenseService , 
    private LicenseService:LicenseService,
    private router : Router){}

  ngOnInit(): void {}

  getLicenseInfo(){
    this.LicenseService.getLicenseInfoByLicenseID(this.LicenseId).subscribe({
      next:(res)=> {
        this.licenseInfo = res;
        this.found = true ;
        if(this.licenseInfo.className == "Class 3 - Ordinary driving license")
          this.IsValidClass = true;
        else
          this.IsValidClass = false;
      },
      error:(err) =>{
         this.licenseInfo = null ;
         this.found = false;
      }
    })
  }

  Close(){
    this.router.navigate(["/Home"])
  }

  IssueInternationalLicense() {

    const createdByUserID = Number(localStorage.getItem("UserId"));

    this.InternationalLicenseService.CreateNewInternationalLicense(this.LicenseId ,createdByUserID ).subscribe({
      next: (res) => {
        Swal.fire({
          title: 'Success!',
          text: 'The International license has been Issued successfully.',
          icon: 'success',
          confirmButtonText: 'OK'
        });
      },
      error: (err) => {
        Swal.fire({
          title: 'Error!',
          text: 'An error occurred while Issuing the license. Please try again.',
          icon: 'error',
          confirmButtonText: 'OK'
        });
        console.error(err);
      }
    });
  } 
}
