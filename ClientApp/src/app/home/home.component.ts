import { Component } from '@angular/core';
import { Doctor } from 'src/models/doctor';
import { Medication } from 'src/models/medication';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent {
  doctors: Doctor[] = [
    {
      title: 'Conf. Univ. Dr.',
      name: 'Chelaru Radu',
      profession: 'MEDIC SPECIALIST',
      specialities: ['ECOGRAFIE', 'ENDOSCOPRIE DIGESTIVA'],
      grade: 9.07,
      reviews: 107,
      locations: ['Policlinica Iasi', 'Iasi Campus Medical'],
    },
    {
      title: 'Dr.',
      name: 'Chelaru Radu',
      profession: 'NUTRITIONIST',
      specialities: ['DIABET ZAHARAT', 'NUTRITIE SI BOLI METABOLICE'],
      grade: 9.07,
      reviews: 107,
      locations: ['Policlinica Iasi', 'Iasi Campus Medical'],
    },
    {
      title: 'Dr.',
      name: 'Chelaru Radu',
      profession: 'MEDIC PRIMAR',
      specialities: ['ECOGRAFIE', 'ENDOSCOPRIE DIGESTIVA'],
      grade: 9.07,
      reviews: 107,
      locations: ['Policlinica Iasi', 'Iasi Campus Medical'],
    },
    {
      title: 'Dr.',
      name: 'Chelaru Radu',
      profession: 'PSIHOLOG',
      specialities: [],
      grade: 9.07,
      reviews: 107,
      locations: ['Policlinica Iasi', 'Iasi Campus Medical'],
    },
  ];

  drugs: Medication[] = [
    {
      id: 1,
      name: 'Paracetamol',
      stock: 3,
      unit: 'comprimate',
      capacity: 20,
      price: 35.3,
    },
    {
      id: 1,
      name: 'Nurofen',
      stock: 3,
      unit: 'comprimate',
      capacity: 35,
      price: 20.01,
    },
    {
      id: 1,
      name: 'Placebo',
      stock: 3,
      unit: 'mL',
      capacity: 20,
      price: 35.3,
    },
  ];
}
