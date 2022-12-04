import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddMedicFormComponent } from './add-medic-form.component';

describe('AddMedicFormComponent', () => {
  let component: AddMedicFormComponent;
  let fixture: ComponentFixture<AddMedicFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddMedicFormComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddMedicFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
