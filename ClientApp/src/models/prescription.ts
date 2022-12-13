export interface MedicationDosages {
  medicationId: string;
  startDate: string;
  endDate: string;
  quantity: number;
  frequency: number;
}

export interface Presctiption {
  doctorId: string;
  pacientId: string;
  medicationDosages: MedicationDosages[];
}
