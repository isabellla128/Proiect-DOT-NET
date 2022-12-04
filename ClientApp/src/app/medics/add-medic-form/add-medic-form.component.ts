import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Doctor } from 'src/models/doctor';
import { DoctorService } from 'src/shared/services/doctor.service';

@Component({
  selector: 'app-add-medic-form',
  templateUrl: './add-medic-form.component.html',
  styleUrls: ['./add-medic-form.component.css'],
})
export class AddMedicFormComponent {
  addMedicForm = this.fb.group({
    firstName: [null, Validators.required],
    lastName: [null, Validators.required],
    title: [null, Validators.required],
    specialization: [null, Validators.required],
    profession: [null, Validators.required],
    email: [null, Validators.required],
    phone: [null, Validators.required],
    location: [null, Validators.required],
    grade: [null, Validators.required],
    reviews: [null, Validators.required],
  });

  constructor(private fb: FormBuilder, private doctorService: DoctorService) {}

  onSubmit(): void {
    const formValues = this.addMedicForm.getRawValue() as unknown as Doctor;
    this.doctorService.post(formValues);
  }
}
