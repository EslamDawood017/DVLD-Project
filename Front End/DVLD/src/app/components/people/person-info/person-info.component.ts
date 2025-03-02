import { Component } from '@angular/core';
import { Person } from '../../../Models/Person';
import { environment } from '../../../Environment/environment';
import { PersonService } from '../../../services/Person/person.service';
import { ActivatedRoute, Router } from '@angular/router';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-person-info',
  imports: [DatePipe],
  templateUrl: './person-info.component.html',
  styleUrl: './person-info.component.css'
})
export class PersonInfoComponent {
  Person! : Person ;
  PersonId : number = 0 ;
  host = environment.apiUrl ;
  DoesPersonHaveAlicense : boolean = false;

  constructor(private personService : PersonService , 
    private route : ActivatedRoute , 
    private router : Router
  ) {}

  ngOnInit(): void {
    this.PersonId = Number(this.route.snapshot.paramMap.get("id"));
    this.getPersonInfo();
  }
  getPersonInfo(){
    this.personService.getPersonById(this.PersonId).subscribe({
      next: (res)=> {
        this.Person = res;
      }, 
      error:(err)=>{
        console.error(err);  
      }
    })
  }
  Close(){
    this.router.navigate(["List-Drivers"]);
  }
}
