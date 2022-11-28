import { Component, Input } from '@angular/core';
import { Medication } from 'src/models/medication';

@Component({
  selector: 'app-drug-card',
  templateUrl: './drug-card.component.html',
  styleUrls: ['./drug-card.component.css'],
})
export class DrugCardComponent {
  @Input() drug: Medication = {} as Medication;
}
