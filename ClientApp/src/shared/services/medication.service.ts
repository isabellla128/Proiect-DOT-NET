import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { BehaviorSubject } from 'rxjs';
import { BASE_API_URL } from 'src/environments/global';
import { Medication } from 'src/models/medication';
import AbstractRestService from '../abstracts/AbstractRestService';

@Injectable({
  providedIn: 'root',
})
export class MedicationService extends AbstractRestService<Medication> {
  // url = BASE_API_URL + 'Medications';
  constructor(private http: HttpClient) {
    super(
      http,
      BASE_API_URL + 'Medications',
      new BehaviorSubject<Medication[]>([])
    );
  }
}
