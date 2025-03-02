import { DatePipe, NgClass } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { LocalDrivingLicenseApplication } from '../../../Models/LocalDrivingLicenseApplication';

@Component({
  selector: 'app-driving-license-details',
  imports: [DatePipe],
  templateUrl: './driving-license-details.component.html',
  styleUrl: './driving-license-details.component.css'
})
export class DrivingLicenseDetailsComponent implements OnInit {
  localDrivingLicenseApp! : LocalDrivingLicenseApplication ;
  ngOnInit(): void {
    this.localDrivingLicenseApp = history.state.appData ;
  }
  

}
