import { CommonModule, NgFor } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Person } from '../../../Models/Person'; 
import { PersonService } from '../../../services/Person/person.service';
import { Router, RouterLink } from '@angular/router';
import Swal from 'sweetalert2';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-people',
  imports: [NgFor , CommonModule , RouterLink , FormsModule],
  templateUrl: './people.component.html',
  styleUrl: './people.component.css',
  
})
export class PeopleComponent implements OnInit {
  people:Person[] = [] ;
   filteredPeople: Person[] = []; // Filtered list of users based on search
  searchQuery: string = ''; // Search query
  

  constructor(private personService : PersonService ,
    private router : Router
  ) { }  

  ngOnInit(): void {
    this.FetchData();
  }
  FetchData(){
    this.personService.getAllPeople().subscribe({
      next: (result:Person[]) => {
        this.people = result;
        this.filteredPeople = result;
        
      },
      error:(error) =>{
        console.error("get all people error:" , error);
      }
    })
  }
  // Filter people based on search query
  filterPeople(): void {
    if (this.searchQuery) {
      this.filteredPeople = this.people.filter((person) =>
        person.nationalNo.toLowerCase().includes(this.searchQuery.toLowerCase())
      );
    } else {
      this.filteredPeople = this.people; // Show all people if search query is empty
    }
  }
  

  // Delete Person
  deletePerson(personId: number): void {
    // Show a confirmation dialog before deletion
    Swal.fire({
      title: 'Are you sure?',
      text: 'Do you really want to delete this person? This action cannot be undone.',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonText: 'Yes, delete it!',
      cancelButtonText: 'No, cancel!',
      confirmButtonColor: '#d33',
      cancelButtonColor: '#3085d6',
    }).then((result) => {
      if (result.isConfirmed) {
        // Proceed with the deletion
        this.personService.deletePerson(personId).subscribe({
          next: () => {
            // Show success message
            Swal.fire({
              icon: 'success',
              title: 'Deleted!',
              text: 'The person has been deleted successfully.',
              confirmButtonColor: '#3085d6',
            });
  
            // Optionally, update the UI (e.g., refresh the person list)
            this.FetchData();
          },
          error: (error) => {
            // Show error message
            Swal.fire({
              icon: 'error',
              title: 'Error',
              text: 'Failed to delete the person. Please try again later.',
              confirmButtonColor: '#d33',
            });
  
            console.error('Failed to delete person:', error);
          },
        });
      } else {
        // User canceled the deletion
        Swal.fire({
          title: 'Cancelled',
          text: 'The person was not deleted.',
          icon: 'info',
          confirmButtonColor: '#3085d6',
        });
      }
    });
  }
  GoToUpdatePersonCompoent(personId : number) {
    this.router.navigate(["/UpdatePerson" ,personId])
  }

}
