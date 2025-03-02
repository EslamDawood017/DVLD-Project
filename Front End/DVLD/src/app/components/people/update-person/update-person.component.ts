import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { country } from '../../../Models/country';
import { CountryService } from '../../../services/Country/country.service';
import { PersonService } from '../../../services/Person/person.service';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';
import Swal from 'sweetalert2';
import { environment } from '../../../Environment/environment';
import { Person } from '../../../Models/Person';
import { NgFor, NgIf } from '@angular/common';

@Component({
  selector: 'app-update-person',
  imports: [NgIf , NgFor , ReactiveFormsModule],
  templateUrl: './update-person.component.html',
  styleUrl: './update-person.component.css'
})
export class UpdatePersonComponent {

  
    addPersonForm! : FormGroup ;
    countries! : country[] ;
    selectedFile :File | null = null;
    imageUrl: string | null = null ;
    PersonId : number = 0 ;
    PersonData! : Person ;
    url: string = environment.apiUrl;
  
    constructor(private fb: FormBuilder, private personService: PersonService,
      private countryService : CountryService , 
      private http : HttpClient , 
      private router : Router, 
      private activatedRoute : ActivatedRoute
    ) {}
    
    /**
     *
     */
    

    async ngOnInit() {
      this.addPersonForm = this.fb.group({
        firstName: ['', Validators.required],
        secondName: ['', Validators.required],
        thirdName: ['', Validators.required],
        lastName: ['', Validators.required],
        dateOfBirth: ['', Validators.required],
        gendor: ['', Validators.required],
        address: ['', Validators.required],
        phone: ['', Validators.required],
        email: ['', [Validators.required, Validators.email]],
        nationalityCountryID: ['', Validators.required],
        imagePath: [''], // Optional
        nationalNo: ['', Validators.required],
      });

      this.PersonId = Number(this.activatedRoute.snapshot.paramMap.get('PersonId')) ;

     

      await  this.fetchPersonData().then(() => {
        
      });

      
      
    }
  
    // Initialize the form with all person parameters
    initializeForm(): void {
      this.addPersonForm = this.fb.group({
       
        firstName: [this.PersonData.firstName, Validators.required],
        secondName: [this.PersonData.secondName, Validators.required],
        thirdName: [this.PersonData.thirdName, Validators.required],
        lastName: [this.PersonData.lastName, Validators.required],
        dateOfBirth: [this.formatDate(this.PersonData.dateOfBirth), Validators.required],
        gendor: [this.PersonData.gendor, Validators.required],
        address: [this.PersonData.address, Validators.required],
        phone: [this.PersonData.phone, Validators.required],
        email: [this.PersonData.email, [Validators.required, Validators.email]],
        nationalityCountryID: [this.PersonData.nationalityCountryID, Validators.required], 
        imagePath: [], // Optional 
        nationalNo: [this.PersonData.nationalNo ,  Validators.required],
      });

      this.imageUrl = this.PersonData.imagePath;
    }
  
    fetchCountriesData(){
      //Get Countries Data ;
      this.countryService.getAllCountries().subscribe({
        next : (Data) => {
          this.countries = Data;
        },
        error : (error) => {
          console.error("Countries Error : " , error);
        }
      })
    }
     fetchPersonData():Promise<void>{
      
      return new Promise((resolve , reject) => {
        this.personService.getPersonById(this.PersonId).subscribe({
          next : (data) => {
            this.PersonData = data ;
            this.fetchCountriesData();
            this.initializeForm();
            resolve();
          },
          error :(error) => {
            console.error("Get Person By Id error" , error);
            reject(error);
          }
        })
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
          text: 'Do you want to Update this person?',
          icon: 'question',
          showCancelButton: true,
          confirmButtonText: 'Yes, update it!',
          cancelButtonText: 'No, cancel!',
          confirmButtonColor: '#3085d6',
          cancelButtonColor: '#d33',
        }).then((result) => {
          if (result.isConfirmed) {
            // Prepare the person data
            const personData = {
              personID : this.PersonId,
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
            this.personService.updatePerson(personData).subscribe({
              next: () => {
                // Show success message
                Swal.fire({
                  icon: 'success',
                  title: 'Person updated',
                  text: `Person has been updated successfully!`,
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
                  text: 'Failed to update the  person. Please try again.',
                  confirmButtonColor: '#d33',
                });
    
                console.error('Failed to upda new person:', error);
              },
            });
          } else {
            // User canceled the confirmation
            Swal.fire({
              title: 'Cancelled',
              text: 'Person update was cancelled.',
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

    formatDate(date: string | Date): string {
      if (date instanceof Date) {
        return date.toISOString().split('T')[0]; // Format to yyyy-MM-dd
      } else {
        return date.split('T')[0]; // Handle ISO string
      }
    }

}
