import { Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { PeopleComponent } from './components/people/GetAll/people.component'; 
import { AddnewPersonComponent } from './components/people/addnew-person/addnew-person.component';
import { UpdatePersonComponent } from './components/people/update-person/update-person.component';
import { UsersListComponent } from './components/Users/users-list/users-list.component';
import { AddNewUserComponent } from './components/Users/add-new-user/add-new-user.component';
import { UpdateUserComponent } from './components/Users/update-user/update-user.component';
import { LoginComponent } from './components/login/login.component';
import { UserProfileComponent } from './components/Profile/user-profile/user-profile.component';
import { ChangePasswordComponent } from './components/Users/change-password/change-password.component';
import { ApplicationTypesComponent } from './components/application-types/All Application Types/application-types.component';
import { UpdateApplicationTypeComponent } from './components/application-types/update-application-type/update-application-type.component';
import { TestTypeListComponent } from './components/Test Types/test-type-list/test-type-list.component';
import { UpdateTestTypeComponent } from './components/Test Types/update/update.component';
import { AddNewLDLApplicationComponent } from './components/Local Driving License Application/add-new-ldlapplication/add-new-ldlapplication.component';
import { GetAllLocalDrivingLicenseAppComponent } from './components/Local Driving License Application/get-all-local-driving-license-app/get-all-local-driving-license-app.component';
import { ScheduleTestComponent } from './components/Tests/schedule-test/schedule-test.component';
import { TestInfoComponent } from './components/Tests/test-info/test-info.component';
import { UpdateAppointmentComponent } from './components/Tests/update-appointment/update-appointment.component';
import { TakeTestComponent } from './components/Tests/take-test/take-test.component';
import { DrivingLicenseDetailsComponent } from './components/Local Driving License Application/driving-license-details/driving-license-details.component';
import { IssueFirstTimeComponent } from './components/Licenses/issue-first-time/issue-first-time.component';
import { DriverLicenseInfoComponent } from './components/Licenses/driver-license-info/driver-license-info.component';
import { RenewComponent } from './components/Licenses/renew/renew.component';
import { ListDriversComponent } from './components/Driver/list-drivers/list-drivers.component';
import { PersonInfoComponent } from './components/people/person-info/person-info.component';
import { ReplacementComponent } from './components/Licenses/replacement/replacement.component';
import { LicenseHistoryComponent } from './components/Licenses/license-history/license-history.component';
import { DetainLicenseComponent } from './components/Detain/detain-license/detain-license.component';
import { ReleaseComponent } from './components/Detain/release/release.component';
import { DetainListComponent } from './components/Detain/detain-list/detain-list.component';
import { NewInternationalLicenseComponent } from './components/InternationalLicenses/new-international-license/new-international-license.component';
import { ListInternationalLicenseComponent } from './components/InternationalLicenses/list-international-license/list-international-license.component';

export const routes: Routes = [
    {path : '' , component : HomeComponent},
    {path : "People" , component : PeopleComponent },
    {path : "UpdatePerson/:PersonId" , component : UpdatePersonComponent},
    {path : "People/New-Person" , component : AddnewPersonComponent},
    {path : "UsersList" , component : UsersListComponent},
    {path : "AddNewUser" , component :AddNewUserComponent},
    {path : "UpdateUser/:UserId" , component : UpdateUserComponent},
    {path : 'profile/:UserId', component: UserProfileComponent },
    {path : "Login" , component : LoginComponent},
    {path : 'changePassword', component: ChangePasswordComponent },
    {path : 'application-types', component: ApplicationTypesComponent },
    {path : 'update-application-type/:id', component: UpdateApplicationTypeComponent },
    {path : "Test-Type-List"  , component : TestTypeListComponent},
    {path : "update-Test-type/:id" , component : UpdateTestTypeComponent},
    {path : "add-new-LDLA" , component : AddNewLDLApplicationComponent},
    {path : "getAllLDLA" , component : GetAllLocalDrivingLicenseAppComponent},
    {path : "TestInfo/:id" , component : TestInfoComponent},
    {path : "ScheduleTest/:id" , component : ScheduleTestComponent},
    {path : "TakeTest/:id" , component : TakeTestComponent},
    {path : "IssueFirstTime" , component : IssueFirstTimeComponent},
    {path : "IssueFirstTime" , component : IssueFirstTimeComponent},
    {path : "Replacement" , component : ReplacementComponent},
    {path : "licenseHistory/:nationalNo" , component : LicenseHistoryComponent},
    {path : "Renew" , component : RenewComponent},
    {path : "DetainLicense" , component : DetainLicenseComponent},
    {path : "ReleaseLicense" , component : ReleaseComponent},
    {path : "ReleaseList" , component : DetainListComponent},
    {path : "List-Drivers" , component : ListDriversComponent},
    {path : "PersonInfo/:id" ,component : PersonInfoComponent},
    {path : "LicenseInfo/:id" , component: DriverLicenseInfoComponent},
    {path : "ListInternational" ,component : ListInternationalLicenseComponent},
    {path : "IssueInternationalLicense" ,component : NewInternationalLicenseComponent},
    {path : "LocalDrivingLicenseApplicationDetails" , component : DrivingLicenseDetailsComponent},
    {path : "UpdateTestAppointmentDate/:id" , component : UpdateAppointmentComponent},
    {path : "Home" , component : HomeComponent},
    
];
