import { DatePipe, NgIf } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { LicenseInfo } from '../../../Models/LicenseInfo';
import { Router } from '@angular/router';
import { DetainService } from '../../../services/Detain/detain.service';
import { LicenseService } from '../../../services/License/license.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-detain-license',
  imports: [NgIf , FormsModule , DatePipe],
  templateUrl: './detain-license.component.html',
  styleUrl: './detain-license.component.css'
})
export class DetainLicenseComponent {

  LicenseId! : number  ;
  licenseInfo : LicenseInfo | null = null ;
  isExpired : boolean = true;
  FineFees! : number ;
  found : boolean = true;

  constructor(private DetainService : DetainService , 
    private LicenseService:LicenseService,
    private router : Router){}

  ngOnInit(): void {}

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

  Close(){
    this.router.navigate(["/Home"])
  }

  Detain() {
    const data = {
      detainId: 0,
      licenseID: this.licenseInfo?.licenseID,
      fineFees: this.FineFees,
      createdByUserID: Number(localStorage.getItem("UserId"))
    }

    console.log(data);

    this.DetainService.AddNewDetain(data).subscribe({
      next: (res) => {
        Swal.fire({
          title: 'Success!',
          text: 'The license has been Detained successfully.',
          icon: 'success',
          confirmButtonText: 'OK'
        });
      },
      error: (err) => {
        Swal.fire({
          title: 'Error!',
          text: 'An error occurred while Detaining the license. Please try again.',
          icon: 'error',
          confirmButtonText: 'OK'
        });
        console.error(err);
      }
    });
  } 
}
