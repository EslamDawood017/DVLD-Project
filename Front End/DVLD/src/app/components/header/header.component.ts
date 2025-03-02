import { NgIf } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { AuthServiceService } from '../../services/Auth/auth-service.service';

@Component({
  selector: 'app-header',
  imports: [RouterLink , NgIf],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent implements OnInit {

  UserId : number  = -1 ;

  constructor(public authService : AuthServiceService , private router : Router
  ) {}

  ngOnInit(): void {

    this.getUserId(); 
  }
  logout(){
    this.authService.logout();
  }
  goToProfile(){
    this.getUserId();
    if(this.UserId != -1){
      this.router.navigate(["/profile" , this.UserId]);
    }
    else{
      this.router.navigate(["/Login"]);
    }
      
  }

  changePassword(){
    this.router.navigateByUrl("/changePassword");
  }
  getUserId(){
    const storedUserId = localStorage.getItem('UserId');
    if(storedUserId){
      this.UserId = Number(storedUserId);
    } 
    else{
      this.UserId = -1 ;
    }  
  }

  

}
