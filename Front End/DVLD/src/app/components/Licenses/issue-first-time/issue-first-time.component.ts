import { Component, OnInit } from '@angular/core';
import { LocalDrivingLicenseApplication } from '../../../Models/LocalDrivingLicenseApplication';
import { ApplicationInfo } from '../../../Models/ApplicationInfo';
import { LocalDrivingLicenseApplicationService } from '../../../services/Local Driving License Application/local-driving-license-application.service';
import { CommonModule, DatePipe } from '@angular/common';
import { TestService } from '../../../services/Test/test.service';
import { LicenseService } from '../../../services/License/license.service';
import { FormsModule } from '@angular/forms';
import Swal from 'sweetalert2';
import { Router } from '@angular/router';
@Component({
  selector: 'app-issue-first-time',
  imports: [DatePipe , CommonModule , FormsModule],
  templateUrl: './issue-first-time.component.html',
  styleUrl: './issue-first-time.component.css'
})
export class IssueFirstTimeComponent implements OnInit {

  BaseApplication!:ApplicationInfo;
  PassedTest : number = 0 ;
  Note : string = '' ;
  localDrivingLicenseApp! : LocalDrivingLicenseApplication;


  constructor(private localDrivingApplicationService: LocalDrivingLicenseApplicationService ,
    private TestService : TestService , 
    private LicenseService : LicenseService, 
    private router : Router
   ) {}
  ngOnInit(): void {
    this.localDrivingLicenseApp = history.state.appData;
    this.getBaseApplicationData();
    this.getPassedTest();
  }

  getBaseApplicationData(){
    this.localDrivingApplicationService.getBaseApplication(this.localDrivingLicenseApp.localDrivingLicenseApplicationID).subscribe({
      next:(res)=>{
        this.BaseApplication = res ;
      },
      error:(err)=>{
        console.error("Base Application Error => " , err);
      }
    });
  }
  getPassedTest(){
    this.TestService.GetNumberOfPassedTest(this.localDrivingLicenseApp.localDrivingLicenseApplicationID )
    .subscribe({
      next:(res) => {
        this.PassedTest = res ;
      }
    })
  }
  Close(){
    this.router.navigate(['/getAllLDLA']); 
  }
  IssueFirstTime() {
    const LicenseData = {
      localDrivingLicenseApplicationId: this.localDrivingLicenseApp.localDrivingLicenseApplicationID,
      createdByUserId: Number(localStorage.getItem("UserId")),
      note: this.Note
    };

    this.LicenseService.IssueForFirstTime(LicenseData).subscribe({
      next: (res) => {
        Swal.fire({
          icon: 'success',
          title: 'Success!',
          text: `License issued successfully with id ${res}.`,
          confirmButtonColor: '#3085d6',
          confirmButtonText: 'OK'
        }).then(() => {
          
          this.router.navigate(['/getAllLDLA']); 
        });
      },
      error: (err) => {
        Swal.fire({
          icon: 'error',
          title: 'Oops...',
          text: 'Failed to issue license. Please try again later.',
          confirmButtonColor: '#d33',
          confirmButtonText: 'OK'
        });
      }
    });
  }



}
