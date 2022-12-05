import {
  Component,
  EventEmitter,
  Input,
  Output,
  OnInit,
  OnChanges,
} from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { MedicationService } from 'src/shared/services/medication.service';
import { Medication } from 'src/models/medication';

@Component({
  selector: 'app-medication-form',
  templateUrl: './medication-form.component.html',
  styleUrls: ['./medication-form.component.css'],
})
export class MedicationFormComponent implements OnInit, OnChanges {
  @Input() medication: Medication = {} as Medication;
  @Input() title: String = '';
  @Output() submitEmitter = new EventEmitter<Medication>();
  medicationForm = this.fb.group<Medication>({
    id: '',
    name: '',
    capacity: 0,
    unit: '',
    stock: 0,
    price: 0,
  });

  constructor(private fb: FormBuilder) {}

  ngOnInit(): void {}

  ngOnChanges(changes: any) {
    this.medicationForm.setValue(changes.medication.currentValue);
  }

  onSubmit(): void {
    const formValues =
      this.medicationForm.getRawValue() as unknown as Medication;
    this.submitEmitter.emit(formValues);
  }
}
