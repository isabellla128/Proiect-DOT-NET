import { TestBed } from '@angular/core/testing';

import { MedicationService } from './medication.service';
import { HttpClient, HttpClientModule } from '@angular/common/http';

describe('MedicationService', () => {
  let service: MedicationService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientModule],
      providers: [HttpClient],
    });
    service = TestBed.inject(MedicationService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
