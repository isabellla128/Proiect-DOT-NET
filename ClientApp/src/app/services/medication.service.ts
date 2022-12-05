import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { BASE_API_URL } from 'src/global';
import { Medication } from 'src/models/medication';

@Injectable({
  providedIn: 'root',
})
export class MedicationService {
  url = BASE_API_URL + 'Medications';
  medications$ = new BehaviorSubject<Medication[]>([]);
  constructor(private http: HttpClient) {
    this.getAllMedications();
  }

  getAllMedications() {
    this.http.get<Medication[]>(this.url).subscribe(
      (result) => this.medications$.next(result),
      (error) => console.log(error)
    );
  }

  postMedication(medication: Medication) {
    this.http.post<Medication>(this.url, medication).subscribe(
      (result) => this.medications$.next(this.addMedication(medication)),
      (error) => console.log(error)
    );
  }

  deleteMedication(medicationId: string) {
    this.deleteMedicationLocaly(medicationId);
    this.http.delete(this.url + '/' + medicationId).subscribe(
      (result) => console.log(result),
      (error) => console.log(error)
    );
  }

  private addMedication(medication: Medication): Medication[] {
    const medications = this.medications$.getValue();
    medications.push(medication);
    return medications;
  }

  private deleteMedicationLocaly(id: string) {
    this.medications$.next(
      this.medications$.getValue().filter((medication) => medication.id !== id)
    );
  }
}
