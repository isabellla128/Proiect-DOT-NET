import { TestBed } from '@angular/core/testing';

import { DoctorService } from './doctor.service';
import { HttpClient, HttpClientModule } from '@angular/common/http';

describe('DoctorService', () => {
  let service: DoctorService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientModule],
      providers: [HttpClient],
    });
    service = TestBed.inject(DoctorService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
