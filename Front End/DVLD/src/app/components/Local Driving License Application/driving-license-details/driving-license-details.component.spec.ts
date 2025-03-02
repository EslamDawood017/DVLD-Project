import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DrivingLicenseDetailsComponent } from './driving-license-details.component';

describe('DrivingLicenseDetailsComponent', () => {
  let component: DrivingLicenseDetailsComponent;
  let fixture: ComponentFixture<DrivingLicenseDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DrivingLicenseDetailsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DrivingLicenseDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
