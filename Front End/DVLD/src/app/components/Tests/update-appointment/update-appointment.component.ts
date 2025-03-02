import { Component, OnInit } from '@angular/core';
import { enMode } from '../../../enums/enMode';
import { enCreationMode } from '../../../enums/enCreationMode';
import { enTestType } from '../../../enums/enTestType';
import { LocalDrivingLicenseApplication } from '../../../Models/LocalDrivingLicenseApplication';
import { TestAppointment } from '../../../Models/TestAppointment';
import { TestTypeService } from '../../../services/Test Type/test-type.service';
import { TestType } from '../../../Models/TestType';
import { LocalDrivingLicenseApplicationService } from '../../../services/Local Driving License Application/local-driving-license-application.service';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { TestAppointmentService } from '../../../services/Test Appointment/test-appointment.service';
import Swal from 'sweetalert2';
import { CommonModule, NgIf } from '@angular/common';
import { AapplicationTypeService } from '../../../services/App Type/aapplication-type.service';
import { enApplicationType } from '../../../enums/enApplicationType';
import { ApplicationService } from '../../../services/Application/application.service';


@Component({
  selector: 'app-update-appointment',
  imports: [NgIf , CommonModule , FormsModule],
  templateUrl: './update-appointment.component.html',
  styleUrl: './update-appointment.component.css'
})

export class UpdateAppointmentComponent implements OnInit {

  
    private _Mode:enMode = enMode.addNew ;
    private CreationMode : enCreationMode = enCreationMode.firstTimeSchedule ;
    private _TestTypeId! : enTestType ;
    localDrivingLicenseApplication! : LocalDrivingLicenseApplication ; // from sanp shot 
    TestAppointment! : TestAppointment ;
    private _TestAppointmentId : number = -1 ; 
    TestType! : TestType ;
    DoesAttendTestType! : boolean 
    NumberOfTrail : number = 0 ;
    currentDate : string  = '';
    Label : string = '' ;
    userId! : number ;
    RetakeTestApplicationId : number = 0 ;
    RetakeTestFees : number = 0 ;
    PersonId! : number ;
    minDate! : string ;
    
  
  
  
    constructor(private TestTypeService : TestTypeService , 
      private testAppointmentService : TestAppointmentService,
      private route : ActivatedRoute,
      private router : Router,
      private ApplicationTypeService : AapplicationTypeService,
      private LocalDrivingLicenseApplicationService : LocalDrivingLicenseApplicationService)  {}
  
  
    ngOnInit(): void {
  
      const testData = history.state.appData;

      this.localDrivingLicenseApplication = testData ;
  
      this.userId = Number(localStorage.getItem("UserId"));
  
      const testAppointmentID = this.route.snapshot.paramMap.get("id");
  
      this._TestTypeId = Number(testAppointmentID);
    
      this.FetchAllData();

      
    }
  
    manageComponentLabel()
    {
      switch(this._TestTypeId)
      {
        case enTestType.VisionTest :
        {
          this.Label = "Vision Test" ;
          break;
        }
        case enTestType.WrittenTest :
        {
          this.Label = "Written Test" ;
          break;
        }
        case enTestType.PracticalTest :
        {
          this.Label = "Practical Test" ;
          break;
        }
        
          
  
      }
    }
    FetchAllData(){
      this.fetchTestType();
      this.DoesPesonAttendTestType();
      this.getTotalNumberOfTrail();
      this.manageComponentLabel();
      this.getAppointmentInfo();   
      this.manageMinDate();
    }
    manageMinDate(){
      const today = new Date();
      this.minDate = today.toISOString().split('T')[0];
      this.currentDate = this.minDate;
    }
    getAppointmentInfo(){
      this.testAppointmentService.getTestAppointmentInfo(this.localDrivingLicenseApplication.localDrivingLicenseApplicationID , this._TestTypeId).subscribe({
        next:(res)=>{
          this.TestAppointment = res ;
          this.currentDate = this.formatDate(this.TestAppointment.appointmentDate);
        },
        error:(err) => {
          console.error("Test Appointment error => " , err);
        }
      })
    }
  
    GetRetakeTestApplicationInfo(){
      this.ApplicationTypeService.getAppTypeById(enApplicationType.RetakeTest).subscribe({
        next:(res) => {
          this.RetakeTestFees = res.applicationFees ;
        },
        error:(err)=>{
          console.error(err);
        }
      })
    }
  
    fetchTestType(){
      this.TestTypeService.getTestTypeById(this._TestTypeId).subscribe({
        next : (res) => {
          this.TestType = res ;
        },
        error : (error) => {
           console.error("Test Type error" , error);
        }
      })
    }
  
    getPersonId(){
      this.LocalDrivingLicenseApplicationService.getPersonIdForLocalDrivingLicenseApp(this.localDrivingLicenseApplication.localDrivingLicenseApplicationID).subscribe({
        next: (res) => {
          this.PersonId = res;
        },
        error:(err) => {
          console.log(err);
        }
        
      })
    }

    DoesPesonAttendTestType(){
      this.LocalDrivingLicenseApplicationService.DoesPersonAttendTestType(this.localDrivingLicenseApplication.localDrivingLicenseApplicationID , this._TestTypeId).subscribe({
        next: (res)=> {
          this.DoesAttendTestType = res ;
          
          if(this.DoesAttendTestType)
          {
            this.CreationMode = enCreationMode.retakeTestSchedule ;
            this.GetRetakeTestApplicationInfo();
            this.getPersonId();
          } 
          else
            this.CreationMode = enCreationMode.firstTimeSchedule
        },
        error : (error) => {
          console.error("DoesPesonAttendTestType => " , error);
        }
      }) 
    }
  
    getTotalNumberOfTrail(){
      this.LocalDrivingLicenseApplicationService.getTotalNumberOfTrails(this.localDrivingLicenseApplication.localDrivingLicenseApplicationID , this._TestTypeId).subscribe({
        next: (res)=> {
          this.NumberOfTrail = Number(res) ;
       
        },
        error : (error) => {
          console.error("DoesPesonAttendTestType => " , error);
        }
      });
    } 
  
    formatDate(date: any): string {
      if (!date) {
        return ''; // Handle null or undefined
      }
    
      // If it's a string, convert it to a Date object
      if (typeof date === 'string') {
        date = new Date(date);
      }
    
      // Check if the date is valid
      if (date instanceof Date && !isNaN(date.getTime())) {
        const year = date.getFullYear();
        const month = (date.getMonth() + 1).toString().padStart(2, '0'); // Months are 0-based
        const day = date.getDate().toString().padStart(2, '0');
        return `${year}-${month}-${day}`; // Format as 'yyyy-MM-dd'
      }
    
      return ''; // Fallback for invalid dates
    }
    
    Update(): void {
      console.log(new Date(this.currentDate));
      this.testAppointmentService
        .UpdateTestAppointment(this.TestAppointment.testAppointmentID, new Date(this.currentDate).toISOString())
        .subscribe({
          next: (res) => {

            Swal.fire({
              title: 'Success!',
              text: 'Test appointment updated successfully.',
              icon: 'success',
              confirmButtonText: 'OK'
            }).then(() => {
              // Optional: Reload or Navigate After Success
              this.router.navigate(["/TestInfo" , this._TestTypeId ] , {state : {appData : this.localDrivingLicenseApplication}}); 
            });
          },
          error: (err) => {
            console.error('Update Test Appointment error =>', err);

            Swal.fire({
              title: 'Error!',
              text: 'Failed to update test appointment. Please try again.',
              icon: 'error',
              confirmButtonText: 'OK'
            });
          }
        });
    }  
    
  
    
}
