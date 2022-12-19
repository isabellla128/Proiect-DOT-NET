import { TestBed } from '@angular/core/testing';

import { AppointmentsService } from './appointments.service';
import { HttpClient, HttpClientModule } from '@angular/common/http';

describe('AppointmentsService', () => {
  let service: AppointmentsService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientModule],
      providers: [HttpClient],
    });
    service = TestBed.inject(AppointmentsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
