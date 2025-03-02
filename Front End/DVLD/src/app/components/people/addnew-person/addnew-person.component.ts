import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { PersonService } from '../../../services/Person/person.service';
import { NgFor, NgIf } from '@angular/common';
import { country } from '../../../Models/country';
import { CountryService } from '../../../services/Country/country.service';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../Environment/environment';
import Swal from 'sweetalert2'
import { Person } from '../../../Models/Person';
import { Router } from '@angular/router';

@Component({
  selector: 'app-addnew-person',
  imports: [NgIf ,ReactiveFormsModule , NgFor],
  templateUrl: './addnew-person.component.html',
  styleUrl: './addnew-person.component.css'
})
export class AddnewPersonComponent {

  addPersonForm! : FormGroup ;
  countries! : country[] ;
  selectedFile :File | null = null;
  imageUrl: string | null = null ;

  constructor(private fb: FormBuilder, private personService: PersonService,
    private countryService : CountryService , 
    private http : HttpClient , 
    private router : Router
  ) {}

  ngOnInit(): void {
    this.initializeForm();
    this.fetchData();
  }

  // Initialize the form with all person parameters
  initializeForm(): void {
    this.addPersonForm = this.fb.group({
      nationalNo: ['', Validators.required],
      firstName: ['', Validators.required],
      secondName: ['', Validators.required],
      thirdName: ['', Validators.required],
      lastName: ['', Validators.required],
      dateOfBirth: ['', Validators.required],
      gendor: [0, Validators.required],
      address: ['', Validators.required],
      phone: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      nationalityCountryID: [0, Validators.required], 
      imagePath: [''], // Optional 
    });
  }

  fetchData(){
    this.countryService.getAllCountries().subscribe({
      next : (Data) => {
        this.countries = Data;
      },
      error : (error) => {
        console.error("Countries Error : " , error);
      }
    })
  }

  onImageSelected(event : Event){
    const fileInput = event.target as HTMLInputElement;
    if (fileInput.files && fileInput.files.length > 0) {
      this.selectedFile = fileInput.files[0];
    }
    // Step 1: upload person image 
    this.uploadImage();
  }

  uploadImage(): void {
    if (this.selectedFile) {
      const formData = new FormData();
      formData.append('imageFile', this.selectedFile);

      this.http.post<{ filePath: string }>(`${environment.apiUrl}ManageImages/Upload`, formData)
        .subscribe( {
          next:(response) => {
            this.imageUrl = response.filePath; // Store the uploaded image URL
            console.log('Image uploaded successfully:', this.imageUrl);
          },
          error : (error) => {
            console.error('Error uploading image:', error);
          }
        });
    }
  }

  
  onSubmit(): void {
    if (this.addPersonForm.valid) {
      // Display a confirmation dialog before submitting
      Swal.fire({
        title: 'Are you sure?',
        text: 'Do you want to add this person?',
        icon: 'question',
        showCancelButton: true,
        confirmButtonText: 'Yes, add it!',
        cancelButtonText: 'No, cancel!',
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
      }).then((result) => {
        if (result.isConfirmed) {
          // Prepare the person data
          const personData = {
            nationalNo: this.addPersonForm.value.nationalNo,
            firstName: this.addPersonForm.value.firstName,
            secondName: this.addPersonForm.value.secondName,
            thirdName: this.addPersonForm.value.thirdName,
            lastName: this.addPersonForm.value.lastName,
            dateOfBirth: this.addPersonForm.value.dateOfBirth,
            gendor: this.addPersonForm.value.gendor,
            address: this.addPersonForm.value.address,
            phone: this.addPersonForm.value.phone,
            email: this.addPersonForm.value.email,
            nationalityCountryID: this.addPersonForm.value.nationalityCountryID,
            imagePath: this.imageUrl,
          };
  
          // Send data to the backend
          this.personService.addNewPerson(personData).subscribe({
            next: (PersonId) => {
              // Show success message
              Swal.fire({
                icon: 'success',
                title: 'Person Added',
                text: `Person has been added successfully! Person ID: ${PersonId}`,
                confirmButtonColor: '#3085d6',
              });
  
              // Reset form after successful submission
              this.addPersonForm.reset();
              this.imageUrl = null; // Reset the image URL if necessary
              this.router.navigateByUrl("/People");
            },
            error: (error) => {
              // Show error message
              Swal.fire({
                icon: 'error',
                title: 'Error',
                text: 'Failed to add the new person. Please try again.',
                confirmButtonColor: '#d33',
              });
  
              console.error('Failed to add new person:', error);
            },
          });
        } else {
          // User canceled the confirmation
          Swal.fire({
            title: 'Cancelled',
            text: 'Person addition was cancelled.',
            icon: 'info',
            confirmButtonColor: '#3085d6',
          });
        }
      });
    } else {
      // Display an error alert for invalid form
      Swal.fire({
        icon: 'error',
        title: 'Invalid Form',
        text: 'Please fill out the form correctly before submitting.',
        confirmButtonColor: '#d33',
      });
    }
  }
  }

