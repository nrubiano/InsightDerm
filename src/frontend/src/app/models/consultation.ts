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

export class MedicalLaboratory {
    id?: string;
    consultationDiagnosisId: string;
    requestedDate: string;
    requestedById?: string;
    typeId?: string;
}

export class Diagnosis {
    id?: string;
    consultationId: any;
    byId: string;
    description: string;
    creationDate: any;
    updateDate?: any;
    exams?: any[]
    treatments?: any[]
    currentTreatment?: string
}

export class Treatment {
    id?: string;
    consultationDiagnosisId: any;
    description: string;
    byId: string;
    creationDate: any
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
    diagnostics: string[];
    status: string;
}
