import { Injectable } from '@angular/core';
import AbstractRestService from '../abstracts/AbstractRestService';
import { Presctiption } from 'src/models/prescription';
import { HttpClient } from '@angular/common/http';
import { BASE_API_URL } from 'src/environments/global';
import { BehaviorSubject } from 'rxjs';
import { MedicationDosages } from 'src/models/medication';

@Injectable({
  providedIn: 'root',
})
export class PrescriptionService extends AbstractRestService<Presctiption> {
  constructor(private http: HttpClient) {
    super(
      http,
      BASE_API_URL + 'Prescriptions',
      new BehaviorSubject<Presctiption[]>([])
    );
  }

  getMedicationDosages(prescriptionId: string) {
    return this.http.get<MedicationDosages[]>(
      this._url + '/' + prescriptionId + '/medicationDosages'
    );
  }
}
