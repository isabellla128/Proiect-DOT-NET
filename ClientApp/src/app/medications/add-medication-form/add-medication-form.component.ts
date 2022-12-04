import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { MedicationService } from 'src/shared/services/medication.service';
import { Medication } from 'src/models/medication';

@Component({
  selector: 'app-add-medication-form',
  templateUrl: './add-medication-form.component.html',
  styleUrls: ['./add-medication-form.component.css'],
})
export class AddMedicationFormComponent {
  addressForm = this.fb.group({
    name: [null, Validators.required],
    capacity: [null, Validators.required],
    unit: [null, Validators.required],
    stock: [null, Validators.required],
    price: [null, Validators.required],
  });

  constructor(
    private fb: FormBuilder,
    private medicationService: MedicationService
  ) {}

  onSubmit(): void {
    const formValues = this.addressForm.getRawValue() as unknown as Medication;
    this.medicationService.post(formValues);
  }
}
