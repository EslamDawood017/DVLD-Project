import { Component, OnInit } from '@angular/core';
import { TestType } from '../../../Models/TestType';
import { ActivatedRoute, Router } from '@angular/router';
import { TestTypeService } from '../../../services/Test Type/test-type.service';
import Swal from 'sweetalert2';
import { NgIf } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-update',
  imports: [NgIf , FormsModule ],
  templateUrl: './update.component.html',
  styleUrl: './update.component.css'
})
export class UpdateTestTypeComponent implements OnInit{


  TestType: TestType | null = null;
    
  
   
    constructor(private route :ActivatedRoute , 
      private router : Router , 
      private testTypeService : TestTypeService) {}
  
    ngOnInit(): void {
      const id = Number(this.route.snapshot.paramMap.get("id")!);
      this.fetchData(id);
    }
  
    fetchData(id : number){
      this.testTypeService.getTestTypeById(id).subscribe({
        next:(data)=>{
          this.TestType = data;
        },
        error:(error) => {
          console.error("Get test type by id error : " , error);
        }
      })
    }
  
    onSubmit(): void {
      if (this.TestType) {
        this.testTypeService.UpdateTestType(this.TestType).subscribe(
          () => {
            Swal.fire({
              title: 'Success!',
              text: 'Test type updated successfully!',
              icon: 'success',
              confirmButtonColor: '#007bff',
              confirmButtonText: 'OK'
            }).then(() => {
              this.router.navigate(['/Test-Type-List']);
            });
          },
          (error) => {
            console.error('Error updating test type:', error);
            Swal.fire({
              title: 'Error!',
              text: 'Failed to update test type. Please try again later.',
              icon: 'error',
              confirmButtonColor: '#d33',
              confirmButtonText: 'OK'
            });
          }
        );
      }
    }
  
    onCancel(): void {
      this.router.navigate(['/Test-Type-List']);
    }
}
