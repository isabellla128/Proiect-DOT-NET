import { Component, OnInit } from '@angular/core';
import { Doctor } from 'src/models/doctor';
import { Medication } from 'src/models/medication';
import { DoctorService } from '../services/doctor.service';
import { MedicationService } from '../services/medication.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit {
  doctors: Doctor[] = [];

  drugs: Medication[] = [];

  constructor(
    private doctorService: DoctorService,
    private medicationService: MedicationService
  ) {}

  ngOnInit() {
    this.doctorService.getAllDoctors().subscribe(
      (response) => {
        console.log(response);
        this.doctors = response;
      },
      (error) => {
        console.error(error);
      }
    );

    this.medicationService.getAllMedications().subscribe(
      (response) => {
        console.log(response);
        this.drugs = response;
      },
      (error) => {
        console.error(error);
      }
    );
  }
}
