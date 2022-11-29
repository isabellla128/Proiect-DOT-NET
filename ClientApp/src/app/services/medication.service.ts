import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BASE_API_URL } from 'src/global';
import { Medication } from 'src/models/medication';

@Injectable({
  providedIn: 'root',
})
export class MedicationService {
  url = BASE_API_URL + 'Medications';
  constructor(private http: HttpClient) {}

  getAllMedications(): Observable<Medication[]> {
    return this.http.get<Medication[]>(this.url);
  }

  postMedication(medication: Medication): Observable<Medication> {
    return this.http.post<Medication>(this.url, medication);
  }
}
