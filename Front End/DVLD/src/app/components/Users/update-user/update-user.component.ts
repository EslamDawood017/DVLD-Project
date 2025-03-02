import { NgFor, NgIf } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Person } from '../../../Models/Person';
import { UserService } from '../../../services/User/user.service';
import { ActivatedRoute, Router } from '@angular/router';
import Swal from 'sweetalert2';
import { PersonService } from '../../../services/Person/person.service';

@Component({
  selector: 'app-update-user',
  imports: [NgFor , ReactiveFormsModule , NgIf],
  templateUrl: './update-user.component.html',
  styleUrl: './update-user.component.css'
})
export class UpdateUserComponent implements OnInit {

  updateUserForm!: FormGroup;
  userId!: number;
  persons: Person[] = [];
  usernameExists = false;
  personAssigned = false;
  selectedUser! : {
    "userID": number,
    "personID": number,
    "userName": string,
    "password": string,
    "isActive": true,
    "role": string
  }
  constructor(
    private fb:FormBuilder,
    private userService : UserService ,
    private route : ActivatedRoute , 
    private router : Router , 
    private personService : PersonService
  ) {}

  ngOnInit(): void {
    this.userId = Number(this.route.snapshot.paramMap.get('UserId'));
    this.fetchPeopleData();
    this.initializeForm();
    this.loadUserDetails();
  }
  fetchPeopleData(){
    this.personService.getAllPeople().subscribe({
      next :(data) => {
        this.persons = data
      },
      error: (err)=>{
        console.error(err);
      }
    })
  }

  initializeForm(): void {

    this.updateUserForm = this.fb.group({
      userName: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(6)]],
      personID: ['', Validators.required],
      role: ['', Validators.required],
      isActive: [true]
    });

  }

  loadUserDetails(): void{

      this.userService.getUserById(this.userId).subscribe({
        next : (user) => {
          this.selectedUser = user ;
          this.updateUserForm.patchValue({
            userName: user.userName,
            password: user.password,
            personID: user.personID,
            role: user.role,
            isActive: user.isActive
          })
        }
      })

      // Validate username and person ID in real-time

      this.updateUserForm.get('userName')?.valueChanges.subscribe(username => {
        if (username) {
          if(username != this.selectedUser.userName ){
            this.userService.checkUsernameExists(username).subscribe(res => {
              this.usernameExists = res.exists;
            });
          }
            
        }
      });
  
      this.updateUserForm.get('personID')?.valueChanges.subscribe(personId => {
        if (personId) {
          if(personId != this.selectedUser.personID){
            this.userService.checkPersonAssigned(personId).subscribe(res => {
              this.personAssigned = res.assigned;
            });
          }
          
        }
      });
  }

  onSubmit(): void {

    if (this.usernameExists || this.personAssigned) {
      return;
    }

    const UserData = {
      userID: this.userId,
      userName : this.updateUserForm.value.userName,
      password : this.updateUserForm.value.password,
      personID : this.updateUserForm.value.personID ,
      role : this.updateUserForm.value.role ,
      isActive : this.updateUserForm.value.isActive  }
    
    this.userService.updateUser(UserData).subscribe({
      next: () => {
        Swal.fire('Updated!', 'User updated successfully.', 'success');
        this.router.navigate(['/UsersList']);
      },
      error: (err) => {
        Swal.fire('Error!', 'Failed to update user.', 'error');
        console.error("Update User Error : " , err)
      }
    })

  }


}
