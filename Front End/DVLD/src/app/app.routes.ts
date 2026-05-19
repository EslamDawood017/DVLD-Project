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
import { AuthGuard } from './guard/auth.guard';

export const routes: Routes = [
    {path : '' , component : HomeComponent},
    {path : "People" , component : PeopleComponent , canActivate:[AuthGuard]},
    {path : "UpdatePerson/:PersonId" , component : UpdatePersonComponent, canActivate:[AuthGuard]},
    {path : "People/New-Person" , component : AddnewPersonComponent, canActivate:[AuthGuard]},
    {path : "UsersList" , component : UsersListComponent, canActivate:[AuthGuard]},
    {path : "AddNewUser" , component :AddNewUserComponent, canActivate:[AuthGuard]},
    {path : "UpdateUser/:UserId" , component : UpdateUserComponent, canActivate:[AuthGuard]},
    {path : 'profile/:UserId', component: UserProfileComponent, canActivate:[AuthGuard] },
    {path : "Login" , component : LoginComponent},
    {path : 'changePassword', component: ChangePasswordComponent , canActivate:[AuthGuard]},
    {path : 'application-types', component: ApplicationTypesComponent , canActivate:[AuthGuard] },
    {path : 'update-application-type/:id', component: UpdateApplicationTypeComponent, canActivate:[AuthGuard] },
    {path : "Test-Type-List"  , component : TestTypeListComponent, canActivate:[AuthGuard]},
    {path : "update-Test-type/:id" , component : UpdateTestTypeComponent, canActivate:[AuthGuard]},
    {path : "add-new-LDLA" , component : AddNewLDLApplicationComponent, canActivate:[AuthGuard]},
    {path : "getAllLDLA" , component : GetAllLocalDrivingLicenseAppComponent, canActivate:[AuthGuard]},
    {path : "TestInfo/:id" , component : TestInfoComponent, canActivate:[AuthGuard]},
    {path : "ScheduleTest/:id" , component : ScheduleTestComponent, canActivate:[AuthGuard]},
    {path : "TakeTest/:id" , component : TakeTestComponent, canActivate:[AuthGuard]},
    {path : "IssueFirstTime" , component : IssueFirstTimeComponent, canActivate:[AuthGuard]},
    {path : "IssueFirstTime" , component : IssueFirstTimeComponent, canActivate:[AuthGuard]},
    {path : "Replacement" , component : ReplacementComponent, canActivate:[AuthGuard]},
    {path : "licenseHistory/:nationalNo" , component : LicenseHistoryComponent, canActivate:[AuthGuard]},
    {path : "Renew" , component : RenewComponent, canActivate:[AuthGuard]},
    {path : "DetainLicense" , component : DetainLicenseComponent, canActivate:[AuthGuard]},
    {path : "ReleaseLicense" , component : ReleaseComponent, canActivate:[AuthGuard]},
    {path : "ReleaseList" , component : DetainListComponent, canActivate:[AuthGuard]},
    {path : "List-Drivers" , component : ListDriversComponent, canActivate:[AuthGuard]},
    {path : "PersonInfo/:id" ,component : PersonInfoComponent, canActivate:[AuthGuard]},
    {path : "LicenseInfo/:id" , component: DriverLicenseInfoComponent, canActivate:[AuthGuard]},
    {path : "ListInternational" ,component : ListInternationalLicenseComponent, canActivate:[AuthGuard]},
    {path : "IssueInternationalLicense" ,component : NewInternationalLicenseComponent, canActivate:[AuthGuard]},
    {path : "LocalDrivingLicenseApplicationDetails" , component : DrivingLicenseDetailsComponent, canActivate:[AuthGuard]},
    {path : "UpdateTestAppointmentDate/:id" , component : UpdateAppointmentComponent, canActivate:[AuthGuard]},
    {path : "Home" , component : HomeComponent},
    
];
