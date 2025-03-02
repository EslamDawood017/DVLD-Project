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
import { NgIf } from '@angular/common';
import { AapplicationTypeService } from '../../../services/App Type/aapplication-type.service';
import { enApplicationType } from '../../../enums/enApplicationType';
import { ApplicationService } from '../../../services/Application/application.service';

@Component({
  selector: 'app-schedule-test',
  imports: [FormsModule , NgIf],
  templateUrl: './schedule-test.component.html',
  styleUrl: './schedule-test.component.css'
})
export class ScheduleTestComponent implements OnInit {
 


  private _Mode:enMode = enMode.addNew ;

  private CreationMode : enCreationMode = enCreationMode.firstTimeSchedule ;
  private _TestTypeId! : enTestType ;
  localDrivingLicenseApplication! : LocalDrivingLicenseApplication ; // from sanp shot 
  private _TestAppointment! : TestAppointment ;
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
  minDate!: string;



  constructor(private TestTypeService : TestTypeService , 
    private testAppointmentService : TestAppointmentService,
    private ApplicationService : ApplicationService,
    private route : ActivatedRoute,
    private router : Router ,
    private ApplicationTypeService : AapplicationTypeService,
    private LocalDrivingLicenseApplicationService : LocalDrivingLicenseApplicationService)  {}


  ngOnInit(): void {

    const testData = history.state.appData;
    this.localDrivingLicenseApplication = testData ;

    this.userId = Number(localStorage.getItem("UserId"));

    const testAppointmentID = this.route.snapshot.paramMap.get("id");
    
    console.log("Test Type => " + testAppointmentID )

    this._TestTypeId = Number(testAppointmentID);

    this.currentDate = this.formatDate(new Date()); // Set default value to today

    this.FetchAllData();
  }

  manageMinDate(){
    const today = new Date();
    this.minDate = today.toISOString().split('T')[0];
    this.currentDate = this.minDate;
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
    this.manageMinDate();
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

  createRetakeTestApplication(){

    const UserId = Number(localStorage.getItem("UserId"));

    const Application = {
      personId : this.PersonId,
      applicationTypeId : enApplicationType.RetakeTest ,
      createdByUserId : UserId 
    }
    this.ApplicationService.addNewApplication(Application).subscribe({
      error:(err)=>{
        console.error(err);
        Swal.fire({
          title: 'Error!',
          text: 'Failed to add test appointment. Please try again.',
          icon: 'error',
          confirmButtonText: 'OK'
        });
        return ;
      }
    });
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

  formatDate(date: Date): string {
    return date.toISOString().split('T')[0]; // Convert to "YYYY-MM-DD"
  }
  
  Save():void{

    this._TestAppointment  = {
      testAppointmentID : 0 ,
      localDrivingLicenseApplicationID : this.localDrivingLicenseApplication.localDrivingLicenseApplicationID ,
      testTypeID : this._TestTypeId , 
      appointmentDate : new Date(this.currentDate) , 
      paidFees : this.TestType.testTypeFees ,
      createdByUserID : this.userId  , 
      isLocked : false ,
      retakeTestApplicationID : -1 
    }


    this.testAppointmentService.isThereActiveAppointmentPerTest(this._TestAppointment.localDrivingLicenseApplicationID , this._TestAppointment.testTypeID).subscribe({
      next:(res) => {
        this.addNewAppointment();
      },
      error:(err)=> {
        Swal.fire({
          title: 'Error!',
          text: 'There Is An Active Test Appointment.',
          icon: 'error',
          confirmButtonText: 'OK'
        });
        console.error(err);
      }
    })
  }

  addNewAppointment() {

    if(this.CreationMode == enCreationMode.retakeTestSchedule){
      this.createRetakeTestApplication();
    }

    this.testAppointmentService.addNewTestAppointmnet(this._TestAppointment).subscribe({
      next: (res) => {
        this._TestAppointmentId = res;
        Swal.fire({
          title: 'Success!',
          text: 'Test appointment added successfully.',
          icon: 'success',
          confirmButtonText: 'OK'
        });

        this.router.navigate(["/TestInfo" , this._TestTypeId ] , {state : {appData : this.localDrivingLicenseApplication}}); 

      },
      error: (err) => {
        console.error("Add new Test Appointment error =>", err);
        Swal.fire({
          title: 'Error!',
          text: 'Failed to add test appointment. Please try again.',
          icon: 'error',
          confirmButtonText: 'OK'
        });
      }
    });
  }
}

 


