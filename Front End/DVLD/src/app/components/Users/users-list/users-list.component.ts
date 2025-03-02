import { Component, OnInit } from '@angular/core';
import { User } from '../../../Models/User';
import { UserService } from '../../../services/User/user.service';
import { NgClass, NgFor } from '@angular/common';
import { FormsModule } from '@angular/forms';
import Swal from 'sweetalert2';
import { Router, RouterLink } from '@angular/router';

@Component({
  selector: 'app-users-list',
  imports: [NgFor , FormsModule , RouterLink],
  templateUrl: './users-list.component.html',
  styleUrl: './users-list.component.css'
})
export class UsersListComponent implements OnInit {
  
  users: User[] = []; // Full list of users
  filteredUsers: User[] = []; // Filtered list of users based on search
  searchQuery: string = ''; // Search query


  constructor(private userService : UserService , 
    private  router : Router
  ) {
      
  }

  ngOnInit(): void {
    this.fetchUsers();
  }

  // Fetch all users
  fetchUsers(): void {
    this.userService.getAllUsers().subscribe({
      next: (data: User[]) => {
        this.users = data;
        this.filteredUsers = data ;
      },
      error: (error) => {
        console.error('Error fetching users:', error);
      },
    });
  }

  // Filter users based on search query
  filterUsers(): void {
    if (this.searchQuery) {
      this.filteredUsers = this.users.filter((user) =>
        user.userName.toLowerCase().includes(this.searchQuery.toLowerCase())
      );
    } else {
      this.filteredUsers = this.users; // Show all users if search query is empty
    }
  }
  DeleteUser(userId : number){
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
            this.userService.deleteUser(userId).subscribe({
              next: () => {
                // Show success message
                Swal.fire({
                  icon: 'success',
                  title: 'Deleted!',
                  text: 'The user has been deleted successfully.',
                  confirmButtonColor: '#3085d6',
                });
      
                // Optionally, update the UI (e.g., refresh the person list)
                this.fetchUsers();
              },
              error: (error) => {
                // Show error message
                Swal.fire({
                  icon: 'error',
                  title: 'Error',
                  text: 'Failed to delete the person. Please try again later.',
                  confirmButtonColor: '#d33',
                });
      
                console.error('Failed to delete user:', error);
              },
            });
          } else {
            // User canceled the deletion
            Swal.fire({
              title: 'Cancelled',
              text: 'The user was not deleted.',
              icon: 'info',
              confirmButtonColor: '#3085d6',
            });
          }
        });
  }
  goToUpdatePage(userId : number){
    this.router.navigate(["/UpdateUser" , userId])
  }

}
