import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BASE_API_URL } from 'src/global';
import { Doctor } from 'src/models/doctor';

@Injectable({
  providedIn: 'root',
})
export class DoctorService {
  url = BASE_API_URL + 'Doctors';
  constructor(private http: HttpClient) {}

  getAllDoctors(): Observable<Doctor[]> {
    return this.http.get<Doctor[]>(this.url);
  }
}
