import { DatePipe, NgIf } from '@angular/common';
import { Component } from '@angular/core';
import { LicenseInfo } from '../../../Models/LicenseInfo';
import { LicenseService } from '../../../services/License/license.service';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-replacement',
  imports: [NgIf , FormsModule , DatePipe],
  templateUrl: './replacement.component.html',
  styleUrl: './replacement.component.css'
})
export class ReplacementComponent {

  LicenseId! : number  ;
  LicenseInfo : LicenseInfo | null = null ;
  isExpired : boolean = true;
  note : string = '';
  found : boolean = true;
  reasonId : number  = 3 ;

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
  Replace() {
    const data = {
      licenseId: this.LicenseId ,
      createdByUserId: Number(localStorage.getItem("UserId")),
      note: this.note,
      reasonId: this.reasonId

    }

    console.log(data);
    this.LicenseService.ReplaceLicense(data).subscribe({
      next: (res) => {
        Swal.fire({
          title: 'Success!',
          text: 'The license has been replaced successfully.',
          icon: 'success',
          confirmButtonText: 'OK'
        }).then(() => {
            this.router.navigate(["/Home"]); 
          });
      },
      error: (err) => {
        Swal.fire({
          title: 'Error!',
          text: 'An error occurred while replacing the license. Please try again.',
          icon: 'error',
          confirmButtonText: 'OK'
        });
        console.error(err);
      }
    });
  } 
}
