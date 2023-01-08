import { Component, OnInit } from '@angular/core';
import { Doctor } from 'src/models/doctor';
import { Patient } from 'src/models/patient';
import { Prescription } from 'src/models/prescription';
import { DoctorService } from 'src/shared/services/doctor.service';
import { MedicationService } from 'src/shared/services/medication.service';
import { PatientService } from 'src/shared/services/patient.service';
import { PrescriptionService } from 'src/shared/services/prescription.service';

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
    private prescriptionService: PrescriptionService,
    private doctorService: DoctorService,
    private medicationService: MedicationService,
    private patientService: PatientService
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
      console.log(prescriptions);
      console.log(this.doctors);
    });
  }
}
