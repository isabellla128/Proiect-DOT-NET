import { Doctor } from './doctor';
import { Patient } from './patent';

export interface Appointment {
  id: string;
  doctorId: string;
  doctor?: Doctor;
  patientId: string;
  patient?: Patient;
  startTime: string;
  endTime: string;
}
