import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListInternationalLicenseComponent } from './list-international-license.component';

describe('ListInternationalLicenseComponent', () => {
  let component: ListInternationalLicenseComponent;
  let fixture: ComponentFixture<ListInternationalLicenseComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ListInternationalLicenseComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ListInternationalLicenseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
