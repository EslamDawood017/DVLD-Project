import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LicenseHistoryComponent } from './license-history.component';

describe('LicenseHistoryComponent', () => {
  let component: LicenseHistoryComponent;
  let fixture: ComponentFixture<LicenseHistoryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LicenseHistoryComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LicenseHistoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
