import { TestBed } from '@angular/core/testing';

import { AapplicationTypeService } from './aapplication-type.service';

describe('AapplicationTypeService', () => {
  let service: AapplicationTypeService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AapplicationTypeService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
