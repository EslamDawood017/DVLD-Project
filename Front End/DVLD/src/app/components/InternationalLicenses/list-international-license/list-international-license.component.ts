import { Component } from '@angular/core';
import { InternationalLicense } from '../../../Models/InternationalLicense';
import { InternationalLicenseService } from '../../../services/International License/international-license.service';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { DatePipe, NgFor } from '@angular/common';

@Component({
  selector: 'app-list-international-license',
  imports: [FormsModule , DatePipe , NgFor ],
  templateUrl: './list-international-license.component.html',
  styleUrl: './list-international-license.component.css'
})
export class ListInternationalLicenseComponent {

  InternationalLicenses: InternationalLicense[] = []; // Full list of licenes
  filterlicenes: InternationalLicense[] = []; // Filtered list of users based on search
  searchQuery!: number ; // Search query


  constructor(private InternationalLicensesService : InternationalLicenseService , 
    private  router : Router) {}

  ngOnInit(): void {
    this.fetchLicensesList();
  }

  // Fetch all users
  fetchLicensesList(): void {
    this.InternationalLicensesService.GetAllInternationalLicense().subscribe({
      next: (res) => {
        this.InternationalLicenses = res;
        this.filterlicenes = res ;
      },
      error: (error) => {
        console.error('Error fetching international Licenses:', error);
      },
    });
  }

  // Filter users based on search query
  filterDetains(): void {
    if (this.searchQuery) {
      this.filterlicenes = this.InternationalLicenses.filter((License) =>
        License.internationalLicenseID == this.searchQuery)
    }
    else {
      this.filterlicenes = this.InternationalLicenses; // Show all detection if search query is empty
    }
  }
  IssueNewLicense(){
    this.router.navigate(["/IssueInternationalLicense"]);
  }

}
