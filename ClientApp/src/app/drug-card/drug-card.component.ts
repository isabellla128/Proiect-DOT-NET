import { outputAst } from '@angular/compiler';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Medication } from 'src/models/medication';

@Component({
  selector: 'app-drug-card',
  templateUrl: './drug-card.component.html',
  styleUrls: ['./drug-card.component.css'],
})
export class DrugCardComponent {
  @Input() drug = {} as Medication;
  @Input() editable = false;
  @Output() delete = new EventEmitter<string>();
  onDelete(event: any) {
    this.delete.emit(this.drug.id);
  }
}
