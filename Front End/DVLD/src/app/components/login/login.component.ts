import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthServiceService } from '../../services/Auth/auth-service.service';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';
import { NgIf } from '@angular/common';

@Component({
  selector: 'app-login',
  imports: [ReactiveFormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {

  loginForm!: FormGroup;

  constructor(private fb: FormBuilder, private authService: AuthServiceService, private router: Router) {
    this.loginForm = this.fb.group({
      userName: ['', Validators.required],
      password: ['', Validators.required]
    });
  }

  onSubmit() :void {
    if (this.loginForm.valid) {
      this.authService.login(this.loginForm.value).subscribe({
        next: (res) => {
          localStorage.setItem('token', res.token);
          localStorage.setItem('userName', res.userName);
          localStorage.setItem('role', res.role);
          localStorage.setItem('UserId', res.userId);

          Swal.fire('Success!', 'Login successful', 'success');
          this.router.navigate(['/Home']);
        },
        error: () => {
          Swal.fire('Error!', 'Invalid username or password', 'error');
        }
      });
    }
  }
}
