import { Component, OnInit } from '@angular/core';
import { Medication } from 'src/models/medication';
import { MedicationService } from '../services/medication.service';

@Component({
  selector: 'app-medications',
  templateUrl: './medications.component.html',
  styleUrls: ['./medications.component.css'],
})
export class MedicationsComponent implements OnInit {
  drugs: Medication[] = [];

  constructor(private medicationService: MedicationService) {}

  ngOnInit() {
    this.medicationService.medications$.subscribe((medications) => {
      this.drugs = medications;
    });
  }

  onDelete(id: string) {
    this.medicationService.deleteMedication(id);
  }
}
