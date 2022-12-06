import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { BASE_API_URL } from 'src/environments/global';
import { Doctor } from 'src/models/doctor';
import AbstractRestService from '../abstracts/AbstractRestService';

@Injectable({
  providedIn: 'root',
})
export class DoctorService extends AbstractRestService<Doctor> {
  constructor(private http: HttpClient) {
    super(http, BASE_API_URL + 'Doctors', new BehaviorSubject<Doctor[]>([]));
  }
}
