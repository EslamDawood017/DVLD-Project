import { Component } from '@angular/core';
import { Driver } from '../../../Models/Driver';
import { DriverService } from '../../../services/Driver/driver.service';
import { Router } from '@angular/router';
import { CommonModule, DatePipe } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-list-drivers',
  imports: [CommonModule , FormsModule , DatePipe],
  templateUrl: './list-drivers.component.html',
  styleUrl: './list-drivers.component.css'
})
export class ListDriversComponent {

    Drivers: Driver[] = [];
    filteredDrivers: Driver[] = []; 
    searchQuery: string = ''; 
  
  
    constructor(private DriverService : DriverService , 
      private  router : Router) { }
  
    ngOnInit(): void {
      this.fetchDrivers();
    }
  
    

    // Fetch all Drivers
    fetchDrivers(): void {
      this.DriverService.GetAllDrivers().subscribe({
        next:(data)=>{
          this.Drivers = data;
          this.filteredDrivers = data ;
        },
        error:(error)=> {
          console.error('Error fetching Driver:', error);
        }
      });
    }
  





     
    // Filter users based on search query
    filterDrivers(): void {
      if (this.searchQuery) {
        this.filteredDrivers = this.Drivers.filter((driver) =>
          driver.nationalNo.toLowerCase().includes(this.searchQuery.toLowerCase())
        );
      } else {
        this.filteredDrivers = this.Drivers; // Show all users if search query is empty
      }
    }

    
    ShowPersonInfo(PersonId : number){
      this.router.navigate(["/PersonInfo" , PersonId]);
    }
    ShowPersonLicenseHistory(nationalNo : string){
      this.router.navigate(["/licenseHistory" , nationalNo]);

    }
}
