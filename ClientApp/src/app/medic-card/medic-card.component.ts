import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Doctor } from 'src/models/doctor';

@Component({
  selector: 'app-medic-card',
  templateUrl: './medic-card.component.html',
  styleUrls: ['./medic-card.component.css'],
})
export class MedicCardComponent {
  @Input() doctor: Doctor = {} as Doctor;
  @Input() editable = false;
  @Output() delete = new EventEmitter<string>();

  onDelete(event: any) {
    this.delete.emit(this.doctor.id);
  }
}
