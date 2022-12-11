import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MedicAppointmentComponent } from './medic-appointment.component';

describe('MedicAppointmentComponent', () => {
  let component: MedicAppointmentComponent;
  let fixture: ComponentFixture<MedicAppointmentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MedicAppointmentComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MedicAppointmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
