import { TestBed } from '@angular/core/testing';

import { PatientService } from './patient.service';
import { HttpClient, HttpClientModule } from '@angular/common/http';

describe('PatientService', () => {
  let service: PatientService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientModule],
      providers: [HttpClient],
    });
    service = TestBed.inject(PatientService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
