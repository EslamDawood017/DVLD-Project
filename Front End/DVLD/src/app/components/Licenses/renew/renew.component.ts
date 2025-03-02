import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { LicenseInfo } from '../../../Models/LicenseInfo';
import { LicenseService } from '../../../services/License/license.service';
import { DatePipe, NgIf } from '@angular/common';
import { Router } from '@angular/router';
import { RenewLicense } from '../../../Models/RenewLicense';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-renew',
  imports: [FormsModule , DatePipe , NgIf],
  templateUrl: './renew.component.html',
  styleUrl: './renew.component.css'
})
export class RenewComponent implements OnInit{
  

  LicenseId! : number  ;
  LicenseInfo : LicenseInfo | null = null ;
  isExpired : boolean = true;
  note : string = '';
  found : boolean = true;

  constructor(private LicenseService : LicenseService , 
    private router : Router
  ) {}

  ngOnInit(): void {}

  getLicenseInfo(){
    this.LicenseService.getLicenseInfoByLicenseID(this.LicenseId).subscribe({
      next:(res)=> {
        this.LicenseInfo = res;
        this.found = true ;
        this.isExpired = new Date(res.expirationDate).getTime() < Date.now() ? true : false ;
      },
      error:(err) =>{
         this.LicenseInfo = null ;
         this.found = false;
      }
    })
  }
  Close(){
    this.router.navigate(["/Home"])
  }
  Renew() {
    const data = {
      licenseId: this.LicenseId ,
      createdByUserId: Number(localStorage.getItem("UserId")),
      note: this.note
    }
    this.LicenseService.RenewLicense(data).subscribe({
      next: (res) => {
        Swal.fire({
          title: 'Success!',
          text: 'The license has been renewed successfully.',
          icon: 'success',
          confirmButtonText: 'OK'
        }).then(() => {
            this.router.navigate(["/Home"]); 
          });
      },
      error: (err) => {
        Swal.fire({
          title: 'Error!',
          text: 'An error occurred while renewing the license. Please try again.',
          icon: 'error',
          confirmButtonText: 'OK'
        });
      }
    });
  } 
}

