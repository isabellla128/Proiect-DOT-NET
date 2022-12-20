import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { Patient } from 'src/models/patent';
import { PatientService } from 'src/shared/services/patient.service';

@Component({
  selector: 'app-settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.css'],
})
export class SettingsComponent implements OnInit {
  patients: Patient[] = [];
  patientControl = new FormControl<Patient | null>(null, Validators.required);

  constructor(private patientService: PatientService) {}

  ngOnInit(): void {
    this.patientService.collection$.subscribe((patients) => {
      this.patients = patients;
    });

    this.patientService.currentPatient$.subscribe((patient) => {
      this.patientControl.setValue(patient);
    });

    this.patientControl.valueChanges.subscribe((value) => {
      this.patientService.currentPatient$.next(value || ({} as Patient));
    });
  }
}
