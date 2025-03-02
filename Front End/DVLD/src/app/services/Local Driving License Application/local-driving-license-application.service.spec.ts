import { TestBed } from '@angular/core/testing';

import { LocalDrivingLicenseApplicationService } from './local-driving-license-application.service';

describe('LocalDrivingLicenseApplicationService', () => {
  let service: LocalDrivingLicenseApplicationService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(LocalDrivingLicenseApplicationService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
