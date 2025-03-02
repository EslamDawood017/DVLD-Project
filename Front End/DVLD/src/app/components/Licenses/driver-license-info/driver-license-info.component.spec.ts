import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DriverLicenseInfoComponent } from './driver-license-info.component';

describe('DriverLicenseInfoComponent', () => {
  let component: DriverLicenseInfoComponent;
  let fixture: ComponentFixture<DriverLicenseInfoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DriverLicenseInfoComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DriverLicenseInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
