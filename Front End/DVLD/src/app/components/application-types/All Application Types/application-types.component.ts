import { Component } from '@angular/core';
import { ApplicationType } from '../../../Models/ApplicationType';
import { AapplicationTypeService } from '../../../services/App Type/aapplication-type.service';
import { NgFor, NgIf } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-application-types',
  imports: [NgIf , NgFor],
  templateUrl: './application-types.component.html',
  styleUrl: './application-types.component.css'
})
export class ApplicationTypesComponent {

  applicationTypes: ApplicationType[] = [];
  isLoading: boolean = true; // Add loading state

  constructor(private applicationTypeService: AapplicationTypeService,
    private router : Router
  ) { }

  ngOnInit(): void {
    this.fetchApplicationTypes();
  }

  fetchApplicationTypes(): void {
    this.applicationTypeService.getAllApplicationTypes().subscribe(
      (data) => {
        this.applicationTypes = data;
        this.isLoading = false; // Data loaded
      },
      (error) => {
        console.error('Error fetching application types:', error);
        this.isLoading = false; // Stop loading on error
      }
    );
  }
  onUpdate(applicationTypes : ApplicationType){
    this.router.navigate(['/update-application-type', applicationTypes.applicationTypeID]);
  }
}
