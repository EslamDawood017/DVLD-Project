import { Component, OnInit } from '@angular/core';
import { enTestType } from '../../../enums/enTestType';
import { ActivatedRoute, Router } from '@angular/router';
import { LocalDrivingLicenseApplicationService } from '../../../services/Local Driving License Application/local-driving-license-application.service';
import { LocalDrivingLicenseApplication } from '../../../Models/LocalDrivingLicenseApplication';
import { ApplicationInfo } from '../../../Models/ApplicationInfo';
import { CommonModule, NgClass, NgFor } from '@angular/common';
import { TestAppointmentService } from '../../../services/Test Appointment/test-appointment.service';
import { TestService } from '../../../services/Test/test.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-test-info',
  imports: [CommonModule , NgFor , NgClass , CommonModule],
  templateUrl: './test-info.component.html',
  styleUrl: './test-info.component.css'
})
export class TestInfoComponent implements OnInit{
  

  TestTypeId : enTestType = enTestType.VisionTest ;
  lebal : string = '';
  localDrivingLicenseApp! : LocalDrivingLicenseApplication;
  BaseApplication!:ApplicationInfo;
  allTestAppointment : any ;
  imagePath! : string;
  PassedTest :number = 0 ;
  PersonId : number = 0 ;
  LicenseClassId! : number ;

  constructor(private route : ActivatedRoute , 
    private router : Router,
    private TestService : TestService,
    private TestAppointmentService : TestAppointmentService ,
    private localDrivingApplicationService : LocalDrivingLicenseApplicationService
  ) {}

  ngOnInit(): void {
    
    this.localDrivingLicenseApp = history.state.appData;
    this.TestTypeId = Number(this.route.snapshot.paramMap.get("id"));

    this.getBaseApplicationData();
    this.getAllTestAppointment();
    this.manageLabel();
    this.getPassedTest();
    this.getPersonId();
    this.getLicenseClassID()
  }
  getPersonId(){
    this.localDrivingApplicationService.getPersonIdForLocalDrivingLicenseApp(this.localDrivingLicenseApp.localDrivingLicenseApplicationID).subscribe({
      next:(res) => {
        this.PersonId = res ;
      },
      error : (err)=> {
        console.error(err);
      }
    })
  }
  getLicenseClassID(){
    this.localDrivingApplicationService.getLocalDrivingLicenseApplicationById(this.localDrivingLicenseApp.localDrivingLicenseApplicationID).subscribe({
      next:(res)=> {
        this.LicenseClassId = res.licenseClassID ;
      }, 
      error:(err)=> {
        console.error(err);
      }
    })
  }
  manageLabel(){
    switch(this.TestTypeId){
      case enTestType.VisionTest :
      {
        this.lebal = 'Vision';
        this.imagePath = "/assets/img/Vision512.png";
        break;
      }
      case enTestType.WrittenTest :
      {
        this.lebal = 'Written';
        this.imagePath = "/assets/img/Written Test 512.png";
        break;
      }
      case enTestType.PracticalTest :
      {
        this.lebal = 'Practical';
        this.imagePath = "/assets/img/driving-test 512.png";
        break;
      }
    }
  }
  getPassedTest(){
    this.TestService.GetNumberOfPassedTest(this.localDrivingLicenseApp.localDrivingLicenseApplicationID )
    .subscribe({
      next:(res) => {
        this.PassedTest = res ;
      }
    })
  }
  getAllTestAppointment(){
    this.TestAppointmentService.getAllTestAppointmentPerTestType(this.localDrivingLicenseApp.localDrivingLicenseApplicationID , this.TestTypeId)
    .subscribe({
      next :(res)=> {
        this.allTestAppointment = res;
      }, 
      error:(err)=> {
        console.error("All Test Appointment error => " , err);
      }
    })
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
  AddNew(){

    if(this.allTestAppointment.length == 0){
      this.router.navigate(["/ScheduleTest" , this.TestTypeId ] , {state : {appData : this.localDrivingLicenseApp}}); 
      return ;
    }

    this.TestService.GetLastTestByPersonAndTestTypeAndLicenseClass(this.PersonId , this.LicenseClassId, this.TestTypeId).subscribe({
      next:(res)=> {
        if(res == false){
          console.log(this.PersonId  +  "+++ " +this.LicenseClassId +  "+++ " + this.TestTypeId);
          this.router.navigate(["/ScheduleTest" , this.TestTypeId ] , {state : {appData : this.localDrivingLicenseApp}}); 
        }
        else{
          Swal.fire({
          icon: 'error',
          title: 'Error!',
          text: 'this person has passed this test before !!.',
          confirmButtonColor: '#d33'
        });
        }
      }
    })
    
  }
  UpdateAppointmentDate(){
    this.router.navigate(["/UpdateTestAppointmentDate" , this.TestTypeId ] , {state : {appData : this.localDrivingLicenseApp}}); 
  }
  TakeTest(app:any){
    const data = {
      LocalDrivingLicenseApplicationId : this.localDrivingLicenseApp.localDrivingLicenseApplicationID ,
      LicenseClass : this.localDrivingLicenseApp.className , 
      TestDate : app.appointmentDate ,
      TestFees : app.paidFees, 
      TestAppointmentId : app.testAppointmentID, 
      PersonName : this.localDrivingLicenseApp.fullName
      
    }
    this.router.navigate(["/TakeTest" , this.TestTypeId] , {state : {appData : data , NavigationData : this.localDrivingLicenseApp }});
  }

  Cancel(){
    this.router.navigateByUrl("/getAllLDLA")
  }

}
