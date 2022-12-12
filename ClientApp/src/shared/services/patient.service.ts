import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { BASE_API_URL } from 'src/environments/global';
import { Patient } from 'src/models/patent';
import AbstractRestService from '../abstracts/AbstractRestService';

@Injectable({
  providedIn: 'root',
})
export class PatientService extends AbstractRestService<Patient> {
  patientId: String = '5049a678-9524-48df-99f2-cecf3e105b83';
  constructor(private http: HttpClient) {
    super(http, BASE_API_URL + 'Patients', new BehaviorSubject<Patient[]>([]));
  }
}
