import { Component, OnInit } from '@angular/core';
import { Doctor } from 'src/models/doctor';
import { Medication, MedicationDosages } from 'src/models/medication';
import { Patient } from 'src/models/patient';
import { Prescription } from 'src/models/prescription';
import { DoctorService } from 'src/shared/services/doctor.service';
import { MedicationService } from 'src/shared/services/medication.service';
import { PatientService } from 'src/shared/services/patient.service';
import { PrescriptionService } from 'src/shared/services/prescription.service';
import { ShoppingCartService } from 'src/shared/services/shopping-cart.service';

@Component({
  selector: 'app-prescriptions',
  templateUrl: './prescriptions.component.html',
  styleUrls: ['./prescriptions.component.css'],
})
export class PrescriptionsComponent implements OnInit {
  prescriptions: Prescription[] = [];
  doctors: Doctor[] = [];
  patients: Patient[] = [];
  constructor(
    public medicationService: MedicationService,
    private prescriptionService: PrescriptionService,
    private doctorService: DoctorService,
    private patientService: PatientService,
    private shoppingCartService: ShoppingCartService
  ) {}

  ngOnInit() {
    this.prescriptionService.collection$.subscribe((prescriptions) => {
      this.prescriptions = prescriptions;
      prescriptions.forEach((prescption) => {
        this.doctors.push(
          this.doctorService.getOne(prescption.doctorId) || ({} as Doctor)
        );
      });
      this.patientService.collection$.subscribe(
        (patients) => (this.patients = patients)
      );
    });
  }

  getDate(date: string) {
    const dateObj = new Date(date);
    return dateObj.toLocaleDateString('en-GB');
  }

  addToCart(dosages: MedicationDosages[] | undefined) {
    dosages?.forEach((dosage) => {
      const medication = this.medicationService.getOne(dosage.medicationId);
      for (let index = 0; index < dosage.quantity; index++)
        this.shoppingCartService.addToCard(medication || ({} as Medication));
    });
  }
}
