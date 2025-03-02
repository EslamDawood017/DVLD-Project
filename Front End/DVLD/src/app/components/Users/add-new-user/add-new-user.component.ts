import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { UserService } from '../../../services/User/user.service';
import Swal from 'sweetalert2';
import { NgFor, NgIf } from '@angular/common';
import { Router, RouterLink } from '@angular/router';
import { User } from '../../../Models/User';
import { PersonService } from '../../../services/Person/person.service';
import { Person } from '../../../Models/Person';
import { debounceTime } from 'rxjs';

@Component({
  selector: 'app-add-new-user',
  imports: [NgIf , ReactiveFormsModule  , NgFor],
  templateUrl: './add-new-user.component.html',
  styleUrl: './add-new-user.component.css'
})
export class AddNewUserComponent implements OnInit{

  people : Person[] = [] ;
  addUserForm! : FormGroup;
  usernameExists = false ;
  personAssigned = false ;


  constructor(private fb : FormBuilder , 
    private userService : UserService,
    private personService : PersonService , 
    private router : Router
  ) {   
  }

  ngOnInit(): void {
    this.initializePeople();
    this.initializeForm();

    this.addUserForm.get('userName')?.valueChanges.pipe(debounceTime(500)).subscribe(username => {
      if (username) {
        this.userService.checkUsernameExists(username).subscribe(res => {
          this.usernameExists = res.exists;
        });
      }
    });

    // Listen for changes in person selection & check if assigned
    this.addUserForm.get('personID')?.valueChanges.subscribe(personId => {
      if (personId) {
        this.userService.checkPersonAssigned(personId).subscribe(res => {
          this.personAssigned = res.assigned;
        });
      }
    });
  }


  initializePeople(){
    this.personService.getAllPeople().subscribe({
      next : (data) => {
        this.people = data;
      },
      error:(error) => {
      }
    })
  }
  initializeForm(){

    this.addUserForm = this.fb.group({
      userName: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(6)]],
      personID: ['', Validators.required],
      role: ['', Validators.required],
      isActive: [true]
    });
  }
  onSubmit(): void {

    if (this.usernameExists || this.personAssigned) {
      return; // Prevent form submission if validation fails
    }

    if (this.addUserForm.valid) {
      this.userService.AddNewUser(this.addUserForm.value).subscribe({
        next: () => {
          Swal.fire({
            icon: 'success',
            title: 'User Added',
            text: 'The new user has been added successfully!',
          });
          this.addUserForm.reset(); // Clear form after success
          this.router.navigateByUrl("/UsersList");
        },
        error: (err) => {
          console.error('Error adding user:', err);
          Swal.fire({
            icon: 'error',
            title: 'Error',
            text: 'Failed to add user. Please try again.',
          });
        }
      });
    }
  }

}
