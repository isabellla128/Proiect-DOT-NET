import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { BASE_API_URL } from 'src/environments/global';
import { Appointment } from 'src/models/appointment';
import { Doctor } from 'src/models/doctor';
import AbstractRestService from '../abstracts/AbstractRestService';

@Injectable({
  providedIn: 'root',
})
export class DoctorService extends AbstractRestService<Doctor> {
  constructor(private http: HttpClient) {
    super(http, BASE_API_URL + 'Doctors', new BehaviorSubject<Doctor[]>([]));
  }

  getAppointments(doctorId: string) {
    return this.http.get<Appointment[]>(
      this._url + '/' + doctorId + '/appointments'
    );
  }

  postAppointment(doctorId: string, appointment: Appointment) {
    this.http
      .post<Appointment>(this._url + '/' + doctorId + '/appointments', [
        {
          startTime: appointment.startTime,
          endTime: appointment.endTime,
          patientId: appointment.patientId,
          doctorId: doctorId,
        },
      ])
      .subscribe({
        next: (result) => console.log(result),
        error: (error) => console.log(error),
        complete: () => {},
      });
  }
}
