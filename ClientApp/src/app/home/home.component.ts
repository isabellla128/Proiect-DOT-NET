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
        this.doctors = this.getRandomFromArray(response, 4);
      },
      (error) => {
        console.error(error);
      }
    );

    this.medicationService.getAllMedications();
    this.medicationService.medications$.subscribe(
      (result) => (this.drugs = this.getRandomFromArray(result, 3))
    );
  }

  getRandomFromArray(array: any[], n: number): any[] {
    const shuffled = [...array].sort(() => 0.5 - Math.random());

    return shuffled.slice(0, n);
  }
}
