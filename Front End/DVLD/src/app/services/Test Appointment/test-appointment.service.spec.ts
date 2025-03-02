import { TestBed } from '@angular/core/testing';

import { TestAppointmentService } from './test-appointment.service';

describe('TestAppointmentService', () => {
  let service: TestAppointmentService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TestAppointmentService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
