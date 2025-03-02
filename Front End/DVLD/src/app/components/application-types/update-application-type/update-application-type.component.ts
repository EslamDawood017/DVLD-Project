import { Component, OnInit } from '@angular/core';
import { ApplicationType } from '../../../Models/ApplicationType';
import { ActivatedRoute, Router } from '@angular/router';
import { AapplicationTypeService } from '../../../services/App Type/aapplication-type.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-update-application-type',
  imports: [CommonModule , FormsModule],
  templateUrl: './update-application-type.component.html',
  styleUrl: './update-application-type.component.css'
})
export class UpdateApplicationTypeComponent implements OnInit {

  applicationType: ApplicationType | null = null;
  

 
  constructor(private route :ActivatedRoute , 
    private router : Router , 
    private AppTypeService : AapplicationTypeService
  ) {
    
    
  }

  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get("id")!);
    this.fetchData(id);
  }

  fetchData(id : number){
    this.AppTypeService.getAppTypeById(id).subscribe({
      next:(data)=>{
        this.applicationType = data;
      },
      error:(error) => {
        console.error("Get App Type by id error : " , error);
      }
    })
  }

  onSubmit(): void {
    if (this.applicationType) {
      this.AppTypeService.UpdateApplicationType(this.applicationType).subscribe(
        () => {
          Swal.fire({
            title: 'Success!',
            text: 'Application type updated successfully!',
            icon: 'success',
            confirmButtonColor: '#007bff',
            confirmButtonText: 'OK'
          }).then(() => {
            this.router.navigate(['/application-types']);
          });
        },
        (error) => {
          console.error('Error updating application type:', error);
          Swal.fire({
            title: 'Error!',
            text: 'Failed to update application type. Please try again later.',
            icon: 'error',
            confirmButtonColor: '#d33',
            confirmButtonText: 'OK'
          });
        }
      );
    }
  }

  onCancel(): void {
    this.router.navigate(['/application-types']);
  }
  
}
