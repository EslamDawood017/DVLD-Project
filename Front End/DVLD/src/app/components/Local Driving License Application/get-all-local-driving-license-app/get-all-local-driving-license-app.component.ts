import { Component, HostListener, OnInit, ViewChild } from '@angular/core';
import { LocalDrivingLicenseApplicationService } from '../../../services/Local Driving License Application/local-driving-license-application.service';
import { CommonModule, NgIf } from '@angular/common';
import { LocalDrivingLicenseApplication } from '../../../Models/LocalDrivingLicenseApplication';
import { FormsModule } from '@angular/forms';
import { ContextMenuComponent } from "../context-menu/context-menu.component";
import { Router } from '@angular/router';
import { enTestType } from '../../../enums/enTestType';
import { enStatus } from '../../../enums/enApplicationStaus';
import { TestService } from '../../../services/Test/test.service';
import { LicenseService } from '../../../services/License/license.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-get-all-local-driving-license-app',
  imports: [NgIf, CommonModule, FormsModule, ContextMenuComponent],
  templateUrl: './get-all-local-driving-license-app.component.html',
  styleUrl: './get-all-local-driving-license-app.component.css'
})
export class GetAllLocalDrivingLicenseAppComponent implements OnInit {

  showContextMenu = false;
  contextMenuX = 0;
  contextMenuY = 0;
  selectedApplication: any = null;

  selectedRowIndex: number | null = null;

  applications: LocalDrivingLicenseApplication[] = [];
  isLoading: boolean = true;
  filteredApplications: LocalDrivingLicenseApplication[] = []; // Filtered list of users based on search
  searchQuery: string = ''; // Search query
  PassedTest:number = 0 ; 
  LicenseExists! : boolean ;


  constructor(private LocalLicenseAppService : LocalDrivingLicenseApplicationService , 
    private router : Router,
    private LicenseService : LicenseService,
    private TestService : TestService
  ) {}

  ngOnInit(): void {
    this.fetchApplications(); 
  }

  fetchApplications() {
    this.LocalLicenseAppService.getAllLocalDrivingLicenseApplications().subscribe({
      next: (res) => {
        this.applications = res;
        this.filteredApplications = res;
        this.isLoading = false;
      },
      error: (err) => {
        console.error('Error fetching applications:', err);
        this.isLoading = false;
      }
    });
  }

  filterUsers(){
    if (this.searchQuery) {
      this.filteredApplications = this.applications.filter((app) =>
        app.nationalNo.toLowerCase().includes(this.searchQuery.toLowerCase())
      );
    } else {
      this.filteredApplications = this.applications; // Show all users if search query is empty
    }
  }

  

  @HostListener('document:click')
  closeContextMenu() {
    this.showContextMenu = false;
    this.selectedRowIndex = null;
  }

  onRightClick(event: MouseEvent, app: any, index: number) {
    event.preventDefault(); // Prevent the default browser context menu
    this.showContextMenu = true;
    this.contextMenuX = event.clientX; // X coordinate of the mouse click
    this.contextMenuY = event.clientY; // Y coordinate of the mouse click
    this.selectedApplication = app; // Store the selected application
    this.selectedRowIndex = index;
  }

  onContextMenuOptionClicked(option: string) {
    this.showContextMenu = false;
    switch (option) {
      case 'Vision':
        this.VissionTestApplication(this.selectedApplication);
        break;
      case 'Written':
        this.WrittenTestApplication(this.selectedApplication);
        break;
      case 'Practical':
        this.PracticalTestApplication(this.selectedApplication); 
        break;
      case 'Cancel':
        this.CancelApplicationStatus(this.selectedApplication); 
        break;
      case 'ViewDetails':
        this.viewDetails(this.selectedApplication); 
        break;
      case 'IssueFirstTime':
        this.IssueFirstTime(this.selectedApplication); 
        break;
      case 'ShowLicenseInfo':
        this.ShowLicenseInfo(this.selectedApplication); 
        break;
      case 'ShowLicenseInfoHistory':
        this.ShowLicenseInfoHistory(this.selectedApplication); 
        break;
    }
  }
  IssueFirstTime(app: LocalDrivingLicenseApplication) {
    this.PassedTest = app.passedTestCount ;
    this.getLicenseExist(app.localDrivingLicenseApplicationID , app.className);

    
      if(app.status == "Completed" || app.status == "Cancelled" ){
      Swal.fire({
        icon: 'error',
        title: 'Error!',
        text: 'this application is compeleted Or cancelled!!',
        confirmButtonColor: '#d33'
      });
    }
    else if(this.PassedTest < 3 ){
      Swal.fire({
        icon: 'warning',
        title: 'warning!',
        text: 'you must pass all tests !!',
        confirmButtonColor: '#d33'
      });
    }
    else if(this.LicenseExists){
      Swal.fire({
        icon: 'error',
        title: 'Error!',
        text: 'this Person alraedy have a License!!',
        confirmButtonColor: '#d33'
      });
    }
    
    else{
      this.router.navigate(["/IssueFirstTime"],{state : {appData : app}}); 
        console.log(this.LicenseExists);
    }
  }
  VissionTestApplication(app: LocalDrivingLicenseApplication) {
    this.router.navigate(["/TestInfo" , enTestType.VisionTest ] , {state : {appData : app}}); 
  }
  ShowLicenseInfo(app: LocalDrivingLicenseApplication) {
    this.router.navigate(["/LicenseInfo" , app.localDrivingLicenseApplicationID ]); 
  }
  WrittenTestApplication(app: LocalDrivingLicenseApplication) {
    this.router.navigate(["/TestInfo" , enTestType.WrittenTest ] , {state : {appData : app}}); 
  }
  PracticalTestApplication(app: LocalDrivingLicenseApplication) {
    this.router.navigate(["/TestInfo" , enTestType.PracticalTest ] , {state : {appData : app}}); 
  }
  CancelApplicationStatus(app: any) {
    console.log('Cancel Application:', app);
    this.LocalLicenseAppService.UpdateLocalDrivingLicenseApplication(app.localDrivingLicenseApplicationID , enStatus.Cancelled).subscribe({
      next :() => {
        this.fetchApplications();
      }, 
      error:(err) => {
        console.error(err)
      }
    });
  }
  viewDetails(app: any) {
    this.router.navigate(["/LocalDrivingLicenseApplicationDetails"] , {state : {appData : app}}); 
  }
  ShowLicenseInfoHistory(LocalDrivingLicenseApplication : LocalDrivingLicenseApplication){
    this.router.navigate(["/licenseHistory" , LocalDrivingLicenseApplication.nationalNo]);

  }

  getPassedTest(localDrivingLicenseApplicationID : number){
    this.TestService.GetNumberOfPassedTest(localDrivingLicenseApplicationID )
    .subscribe({
      next:(res) => {
        this.PassedTest = res ;
      }
    })
  }
  getLicenseExist(localDrivingLicenseApplicationID : number , LicenseClassName : string){
     this.LicenseService.IsLicenseExistByLocalDrivingLicenseApplicationID(localDrivingLicenseApplicationID  , LicenseClassName).subscribe({
      next:(res)=> {
        this.LicenseExists = res;
      },
      error:(err) => {
        console.error(err);
      }
    })
  }

  
}


