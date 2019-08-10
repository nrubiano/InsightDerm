import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {Patient} from "../../models/patient";
import notify from "devextreme/ui/notify";
import {Consultation} from "../../models/consultation";
import {MedicalBackground} from "../../models/medical-background";
import {PhysicalExam} from "../../models/physical-exam";
import {FileUploadControl} from "@iplab/ngx-file-upload";
import {ConsultationsService} from "../../services/consultations.services";
import {Router} from "@angular/router";

@Component({
    selector: 'app-consultant-info',
    templateUrl: './consultant-info.component.html',
    styleUrls: ['./consultant-info.component.css'],
    providers:[
        ConsultationsService
    ]
})
export class ConsultantInfoComponent implements OnInit {
    @Input('isVisible') isVisible: boolean = true;
    @Input('infoTitle') infoTitle: string = 'Iniciar Consulta';
    @Input('saveText') saveText: string = 'Guardar Interconsulta';
    @Input('patientId') patientId: any = null;
    @Input('consultantData') consultantData: Consultation = null;
    @Input('editMode') editMode: boolean = false;
    @Output() canceled = new EventEmitter();

    public fileUploadControl = new FileUploadControl();
    public uploadedFiles: Array<File> = [];

    consultation: Consultation;
    medicalBackground: MedicalBackground;

    physicalExam: PhysicalExam;

    message: string;
    loading: boolean = false;

    constructor(
        private consultationsService : ConsultationsService,
    ) { }

    ngOnInit() {
        this.consultation = new Consultation();
        this.medicalBackground = new MedicalBackground();
        this.physicalExam = new PhysicalExam();

        this.fileUploadControl.setListVisibility(false);

        if (this.consultantData) {
            this.consultation.reason = this.consultantData.reason;
            this.updateExistingEntity(this.medicalBackground, this.consultantData.medicalBackground);
            this.updateExistingEntity(this.physicalExam, this.consultantData.physicalExam);
        }
    }

    updateExistingEntity(entity, existingData) {
        Object.keys(JSON.parse(existingData)).forEach((key, item) => entity[key] = item);
    }

    saveConsultation() {
        this.loading = true;
        this.consultation.medicalBackground = this.medicalBackground.json();
        this.consultation.physicalExam = this.physicalExam.json();
        this.consultation.patientId = this.patientId;

        let requestFn = !this.editMode ? this.consultationsService.store.insert(this.consultation) : this.consultationsService.store.update(this.consultantData, this.consultation);

        requestFn.then(value => {
            this.message = `La consulta fue ${!this.editMode ? 'creada' : 'actualizada'} correctamente!!`;
            notify(this.message, "success", 2500);

            if (!this.editMode) {
                this.restore();
            }

            this.loading = false;
        }).catch(error => {
            this.message = `No se logro procesar la consulta, por favor intente de nuevo más tarde.`;
            notify(this.message, "error", 2500);
            this.loading = false;
        });
    }

    cancel() {
        this.canceled.emit();
    }

    restore() {
        this.consultation = new Consultation();
        this.medicalBackground = new MedicalBackground();
        this.physicalExam = new PhysicalExam();
        this.isVisible = false;
    }
}
