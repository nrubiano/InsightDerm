import { Patient } from "./patient";
import { Entity } from "./base";
export interface MedicalExamInfo {
    date: any;
    description: any;
}

export class MedicalExam {
    medicalExamId?: any;
    result?: MedicalExamInfo;
}

export class Treatment {
    date: any;
    description: string;
    by: string;
    medicalExams: MedicalExam[]
}

export class Consultation extends Entity {
    id: string;
    patientId: string;
    creationDate: string;
    requestedById: string;
    reason: string;
    medicalBackground: string;
    physicalExam: string;
    patient: Patient;
    treatments: string[];
}
