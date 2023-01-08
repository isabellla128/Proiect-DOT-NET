import { MedicationDosages } from './medication';

export interface Presctiption {
  doctorId: string;
  pacientId: string;
  medicationDosages: MedicationDosages[];
}
