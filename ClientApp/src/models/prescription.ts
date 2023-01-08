import { MedicationDosages } from './medication';

export interface Prescription {
  doctorId: string;
  patientId: string;
  medicationDosages: MedicationDosages[];
}

export interface PrescriptinResponse {
  doctorId: string;
  patiententId: string;
  medicationDosagePrescription: MedicationDosages[];
}
