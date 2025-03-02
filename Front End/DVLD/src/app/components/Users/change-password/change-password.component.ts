import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { UserService } from '../../../services/User/user.service';
import Swal from 'sweetalert2';
import { NgIf } from '@angular/common';

@Component({
  selector: 'app-change-password',
  imports: [NgIf ,ReactiveFormsModule],
  templateUrl: './change-password.component.html',
  styleUrl: './change-password.component.css'
})
export class ChangePasswordComponent  implements OnInit{
  changePasswordForm!: FormGroup;
  userId!: number;


  constructor(private userService : UserService , private fb : FormBuilder) {}

  ngOnInit(): void {
    const storedUserId = localStorage.getItem("UserId");
    this.userId = storedUserId ? Number(storedUserId) : -1;

    this.changePasswordForm = this.fb.group({
      oldPassword: ['', Validators.required],
      newPassword: ['', [Validators.required, Validators.minLength(6)]],
      confirmNewPassword: ['', Validators.required]
    }, { validator: this.passwordsMatch });
  }

  passwordsMatch(form: FormGroup) {
    return form.get('newPassword')?.value === form.get('confirmNewPassword')?.value
      ? null : { mismatch: true };
  }

  onSubmit(): void {
    if (this.changePasswordForm.valid) {
      const data = {
        userId: this.userId,
        oldPassword: this.changePasswordForm.value.oldPassword,
        newPassword: this.changePasswordForm.value.newPassword
      };

      this.userService.changePassword(data).subscribe({
        next: () => {
          Swal.fire('Success!', 'Password changed successfully', 'success');
          this.changePasswordForm.reset();
        },
        error: (err) => {
          Swal.fire('Error!', err.error, 'error');
        }
      });
    }
}}
