import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DetainLicenseComponent } from './detain-license.component';

describe('DetainLicenseComponent', () => {
  let component: DetainLicenseComponent;
  let fixture: ComponentFixture<DetainLicenseComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DetainLicenseComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DetainLicenseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
