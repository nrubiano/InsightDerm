import { Patient } from "./patient";
import { Entity } from "./base";

export class Consultation extends Entity {
    id: string;
    patientId: string;
    creationDate: string;
    requestedById: string;
    reason: string;
    medicalBackground: string;
    physicalExam: string;
    patient: Patient
}