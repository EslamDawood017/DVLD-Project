import { Component, OnInit } from '@angular/core';
import Swal from 'sweetalert2';
import { LicenseClassService } from '../../../services/Licenses Class/license-class.service';
import { PersonService } from '../../../services/Person/person.service';
import { LocalDrivingLicenseApplicationService } from '../../../services/Local Driving License Application/local-driving-license-application.service';
import { Person } from '../../../Models/Person';
import { LicenseClass } from '../../../Models/LicenseClass';
import { NgFor, NgIf } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-add-new-ldlapplication',
  imports: [NgIf , NgFor , FormsModule ],
  templateUrl: './add-new-ldlapplication.component.html',
  styleUrl: './add-new-ldlapplication.component.css'
})
export class AddNewLDLApplicationComponent implements OnInit{
  
  nationalNo: string = '';
  person! : Person | null;
  licenseClasses: LicenseClass[] = [];
  selectedLicenseClassId: number = 0;
  createdByUserId: number = 0;

 
  constructor(private LicenseClassService : LicenseClassService ,
    private personService: PersonService , 
    private localDrivingLicenseApplicationService : LocalDrivingLicenseApplicationService
  ) { }

  ngOnInit(): void {
    this.loadLicenseClasses();
    this.getCreatedByUserId();
  }
  getCreatedByUserId() {
    const storedUserId = localStorage.getItem("UserId");
    this.createdByUserId = storedUserId ? Number(storedUserId) : 0;
  }
  loadLicenseClasses() {
    this.LicenseClassService.getAllLicenseClasses().subscribe({
      next : (data )=> {
        this.licenseClasses = data ;
      },
      error : (err) => {
        console.error("License Classes Error => " , err);
      }
    })
  }
  // Search for a person using national number
  searchPerson(): void {
    if (!this.nationalNo.trim()) {
      Swal.fire('Warning', 'Please enter a valid national number.', 'warning');
      return;
    }
    this.personService.getPersonByNationalNumber(this.nationalNo).subscribe({
      next: (res) => this.person = res,
      error: () => {
        this.person = null;
        Swal.fire('Not Found', 'No person found with this national number.', 'error');
      }
    });
  }
 // Submit new application
 submitApplication(): void {

  if (!this.person || this.selectedLicenseClassId === 0) {
    Swal.fire('Warning', 'Please complete all fields before submitting.', 'warning');
    return;
  }

  const newApplication = {
    personId: this.person.personID,
    createdByUserId: this.createdByUserId,
    lisenceClassId: this.selectedLicenseClassId
  };


   this.localDrivingLicenseApplicationService.IsThereAnActiveApplication(this.person.personID , this.selectedLicenseClassId).subscribe({
    next : (x)=> {
      this.addApplication(newApplication);
    },
    error : ()=> Swal.fire('Error', 'there is an Active Application For this User.', 'error')

   })
  
 }

 addApplication(data :any){
  this.localDrivingLicenseApplicationService.addNewApplication(data).subscribe({
    next: () => {
      Swal.fire('Success', 'Application submitted successfully!', 'success');
      this.resetForm();
    },
    error: (err) => {
      Swal.fire('Error', 'Failed to submit the application.', 'error');
    }
  });
 }
  resetForm() {
    this.nationalNo = '';
    this.person = null;
    this.selectedLicenseClassId = 0;
  }

 

}
