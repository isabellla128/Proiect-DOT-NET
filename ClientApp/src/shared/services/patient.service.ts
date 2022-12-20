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
  currentPatient$ = new BehaviorSubject<Patient>({} as Patient);
  constructor(private http: HttpClient) {
    super(http, BASE_API_URL + 'Patients', new BehaviorSubject<Patient[]>([]));
    this.getAll();
  }
}
