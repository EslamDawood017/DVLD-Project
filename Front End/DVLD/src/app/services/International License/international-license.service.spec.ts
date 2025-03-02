import { TestBed } from '@angular/core/testing';

import { InternationalLicenseService } from './international-license.service';

describe('InternationalLicenseService', () => {
  let service: InternationalLicenseService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(InternationalLicenseService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
