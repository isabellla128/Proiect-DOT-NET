import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MedicAppointmentComponent } from './medic-appointment.component';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { MatSnackBar } from '@angular/material/snack-bar';
import { OverlayModule } from '@angular/cdk/overlay';
import { ActivatedRoute } from '@angular/router';

describe('MedicAppointmentComponent', () => {
  let component: MedicAppointmentComponent;
  let fixture: ComponentFixture<MedicAppointmentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HttpClientModule, OverlayModule],
      providers: [HttpClient, MatSnackBar, ActivatedRoute],
      declarations: [MedicAppointmentComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(MedicAppointmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
