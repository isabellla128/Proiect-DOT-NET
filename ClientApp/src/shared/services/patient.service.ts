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
  patientId: string = '27f2c8a3-03dd-4584-b7b2-0376799e92e7';
  constructor(private http: HttpClient) {
    super(http, BASE_API_URL + 'Patients', new BehaviorSubject<Patient[]>([]));
  }
}
