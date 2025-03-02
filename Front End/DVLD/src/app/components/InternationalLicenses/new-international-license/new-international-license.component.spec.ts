import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NewInternationalLicenseComponent } from './new-international-license.component';

describe('NewInternationalLicenseComponent', () => {
  let component: NewInternationalLicenseComponent;
  let fixture: ComponentFixture<NewInternationalLicenseComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [NewInternationalLicenseComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(NewInternationalLicenseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
