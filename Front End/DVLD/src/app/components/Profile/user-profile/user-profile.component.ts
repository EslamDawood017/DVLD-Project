import { Component, OnInit } from '@angular/core';
import { ProfileService } from '../../../services/Profile/profile.service';
import { ActivatedRoute } from '@angular/router';
import { NgClass } from '@angular/common';
import { environment } from '../../../Environment/environment';

@Component({
  selector: 'app-user-profile',
  imports: [NgClass],
  templateUrl: './user-profile.component.html',
  styleUrl: './user-profile.component.css'
})
export class UserProfileComponent implements OnInit {

  UserId:number = -1 ;
  userProfile :any ;
  apiUrl : string = environment.apiUrl;


  constructor(private ProfileService : ProfileService , 
          private route : ActivatedRoute ) { }

  ngOnInit(): void {

    this.UserId = Number(this.route.snapshot.paramMap.get("UserId"));

    this.ProfileService.getUserProfile(this.UserId).subscribe({
      next:(data) => {
        this.userProfile = data;
      },
      error :(err) => {
        console.error("Profile info error : " , err);
      }
    })
  }

  formatDate(dateString: string): string {
    if (!dateString) return '';
    return new Date(dateString).toLocaleDateString('en-US', { year: 'numeric', month: 'long', day: 'numeric' });
  }

}
