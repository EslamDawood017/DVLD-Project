import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GetAllLocalDrivingLicenseAppComponent } from './get-all-local-driving-license-app.component';

describe('GetAllLocalDrivingLicenseAppComponent', () => {
  let component: GetAllLocalDrivingLicenseAppComponent;
  let fixture: ComponentFixture<GetAllLocalDrivingLicenseAppComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [GetAllLocalDrivingLicenseAppComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(GetAllLocalDrivingLicenseAppComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
