import { TestBed } from '@angular/core/testing';

import { DetainService } from './detain.service';

describe('DetainService', () => {
  let service: DetainService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DetainService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
