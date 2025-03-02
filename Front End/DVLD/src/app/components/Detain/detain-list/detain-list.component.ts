import { Component } from '@angular/core';
import { Detention, DetentionList } from '../../../Models/Detain';
import { DetainService } from '../../../services/Detain/detain.service';
import { Router, RouterLink } from '@angular/router';
import { DatePipe, NgFor } from '@angular/common';
import { FormsModule } from '@angular/forms';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-detain-list',
  imports: [NgFor , FormsModule , RouterLink, DatePipe],
  templateUrl: './detain-list.component.html',
  styleUrl: './detain-list.component.css'
})
export class DetainListComponent {
  detections: DetentionList[] = []; // Full list of dete
  filteredetains: DetentionList[] = []; // Filtered list of users based on search
  searchQuery: string = ''; // Search query


  constructor(private DetainService : DetainService , 
    private  router : Router) {}

  ngOnInit(): void {
    this.fetchDetainList();
  }

  // Fetch all users
  fetchDetainList(): void {
    this.DetainService.GetAllDetainedLicenses().subscribe({
      next: (res: DetentionList[]) => {
        this.detections = res;
        this.filteredetains = res ;
      },
      error: (error) => {
        console.error('Error fetching detained Licenses:', error);
      },
    });
  }

  // Filter users based on search query
  filterDetains(): void {
    if (this.searchQuery) {
      this.filteredetains = this.detections.filter((detain) =>
        detain.nationalNo.toLowerCase().includes(this.searchQuery.toLowerCase())
      );
    }
    else {
      this.filteredetains = this.detections; // Show all detection if search query is empty
    }
  }

  ReleaseLicense(LicenseId : number) {  

      const createdByUserID = Number(localStorage.getItem("UserId"));

      this.DetainService.ReleaseLicense(LicenseId , createdByUserID).subscribe({
        next: (res) => {
          Swal.fire({
            title: 'Success!',
            text: 'The license has been Released successfully.',
            icon: 'success',
            confirmButtonText: 'OK'
          });
        },
        error: (err) => {
          Swal.fire({
            title: 'Error!',
            text: 'An error occurred while releasing the license. Please try again.',
            icon: 'error',
            confirmButtonText: 'OK'
          });
          console.error(err);
        }
      });
      this.fetchDetainList();
    } 

    
  

}
