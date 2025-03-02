import { TestBed } from '@angular/core/testing';

import { LicenseClassService } from './license-class.service';

describe('LicenseClassService', () => {
  let service: LicenseClassService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(LicenseClassService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
