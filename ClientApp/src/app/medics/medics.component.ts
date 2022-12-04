import { Component, OnInit } from '@angular/core';
import { Doctor } from 'src/models/doctor';
import { DoctorService } from '../../shared/services/doctor.service';

@Component({
  selector: 'app-medics',
  templateUrl: './medics.component.html',
  styleUrls: ['./medics.component.css'],
})
export class MedicsComponent implements OnInit {
  doctors: Doctor[] = [];

  constructor(private doctorService: DoctorService) {}

  ngOnInit() {
    this.doctorService.getAll();
    this.doctorService.collection$.subscribe(
      (response) => {
        this.doctors = response;
      },
      (error) => {
        console.error(error);
      }
    );
  }

  onDelete(id: string) {
    this.doctorService.delete(id);
  }
}
