import { Component, OnInit } from '@angular/core';
import { TestType } from '../../../Models/TestType';
import { TestTypeService } from '../../../services/Test Type/test-type.service';
import { Router } from '@angular/router';
import { NgFor, NgIf } from '@angular/common';

@Component({
  selector: 'app-test-type-list',
  imports: [NgIf, NgFor],
  templateUrl: './test-type-list.component.html',
  styleUrl: './test-type-list.component.css'
})
export class TestTypeListComponent implements OnInit {

   TestTypes: TestType[] = [];
  
    constructor(private TestTypeService: TestTypeService,
      private router : Router) { }
  
    ngOnInit(): void {
      this.fetchTestTypes();
    }
  
    fetchTestTypes(): void {
      this.TestTypeService.getAllTestTypes().subscribe(
        (data) => {
          this.TestTypes = data;
          console.log(this.TestTypes)
        },
        (error) => {
          console.error('Error fetching test types:', error);
        }
      );
    }
    onUpdate(testType : TestType){
      this.router.navigate(['/update-Test-type', testType.testTypeID]);
    }

}
