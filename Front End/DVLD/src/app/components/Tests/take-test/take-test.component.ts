import { Component, OnInit } from '@angular/core';
import { TestAppointment } from '../../../Models/TestAppointment';
import { enTestType } from '../../../enums/enTestType';
import { ActivatedRoute, Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { TestRequest } from '@angular/common/http/testing';
import { TestResult } from '../../../Models/Test';
import { TestService } from '../../../services/Test/test.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-take-test',
  imports: [FormsModule],
  templateUrl: './take-test.component.html',
  styleUrl: './take-test.component.css'
})
export class TakeTestComponent implements OnInit {
 
  TestAppointment : any;
  TestTypeId : number = 0 ;
  imagePath:string = '';
  lebal : string = '' ;
  Result : number = 1 ;
  Note : string = '';
  Test! : TestResult ;
  app : any ;

  constructor(private route :ActivatedRoute ,
    private router : Router , 

    private TestService : TestService
  ) {}
  ngOnInit(): void {
    this.TestAppointment = history.state.appData;
    this.app = history.state.NavigationData 
    this.TestTypeId = Number(this.route.snapshot.paramMap.get("id"));
    this.manageImage();
  }
  manageImage(){
    switch (this.TestTypeId)
    {
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
  Save() {
    this.Test = {
      testID: 0,
      testAppointmentID: this.TestAppointment.TestAppointmentId,
      testResult: (this.Result == 1 )? 1 : 0  ,
      createdByUserID: Number(localStorage.getItem("UserId")),
      notes: this.Note
    };
    
    console.log(this.Test);

    this.TestService.addNewTest(this.Test).subscribe({
      next: (res) => {
        Swal.fire({
          icon: 'success',
          title: 'Success!',
          text: 'Test result saved successfully.',
          confirmButtonColor: '#3085d6'
        });

        this.router.navigate(["/TestInfo" , this.TestTypeId] , {state : {appData : this.app }}); 

      },
      error: (err) => {
        Swal.fire({
          icon: 'error',
          title: 'Error!',
          text: 'Failed to save test result. Please try again.',
          confirmButtonColor: '#d33'
        });

        console.error("take Test Error => " , err);
      }
    });
  }
  
}
