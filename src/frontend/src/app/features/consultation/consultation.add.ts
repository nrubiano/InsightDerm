import { Component, OnInit } from '@angular/core';
import { PatientsService } from '../../services/patients.services';
import { Patient } from '../../models/patient';
import { _getComponentHostLElementNode } from '@angular/core/src/render3/instructions';
import { Consultation } from 'app/models/consultation';
import { MedicalBackground } from 'app/models/medical-background';
import { PhysicalExam } from 'app/models/physical-exam';
import { ConsultationsService } from 'app/services/consultations.services';
import notify from 'devextreme/ui/notify';


declare var $: any;
declare var jQuery: any;

@Component({
  selector: 'fea-consultation-add',
  templateUrl: './consultation.add.html',  
  providers:[
    PatientsService,
    ConsultationsService
  ]
})
export class ConsultationAdd implements OnInit 
{
    /**
     * Indicate if the search box is enabled
     */
    active: boolean = true;
    /**
     * Stores the patient's indentification number 
     */
    patientId: string;

    showFormPatient: boolean = false;
    
    showBeginConsultation: boolean = false;

    patient: Patient;

    consultation: Consultation;

    medicalBackground: MedicalBackground;
    
    physicalExam: PhysicalExam;

    popupVisible: boolean = false;

    message: string;
    /**
     * Ctor
     */
    constructor(
        private patientsService : PatientsService,
        private consultationsService : ConsultationsService
    ) { }
    /**
     * Init Event
     */
    ngOnInit() {
        this.consultation = new Consultation();
        this.medicalBackground = new MedicalBackground();
        this.physicalExam = new PhysicalExam();
    }
    /**
     * Search a patient by his identification number
     */
    searchPatient() {
        var options = {
            filter:'["IdentificationNumber", "eq", "'+this.patientId+'"]'
        };

        this.patientsService
                .store
                .load(options)
                .then(
                    (res: any) => {                                                                
                        if(res.totalCount == 1) {
                            this.showFormPatient = false;
                            this.showBeginConsultation = true;
                            this.patient = res.data[0];
                        } else {
                            this.showFormPatient = true;
                            this.showBeginConsultation = false;
                        }
                    }, 
                    () => {
                        console.log("error");
                    }
                );
    }

    createOrUpdatePatient(patient: Patient) {                
        this.patientsService
                .store
                .insert(patient)
                .then(
                    (res) => {           
                        this.searchPatient();
                    },
                    (res) => {
                        console.log(res);
                    }
                );
    }

    saveConsultation() {
        this.consultation.medicalBackground = this.medicalBackground.json();
        this.consultation.physicalExam = this.physicalExam.json();
        this.consultation.patientId = this.patient.id;     
        
        this.consultationsService
            .store
            .insert(this.consultation)
            .then(value => {                
                this.message = `La consulta fue creada correctamente!!`;
                notify(this.message, "success", 2500);
                this.restore();
            }).catch(error => {
                this.message = `No se logro procesar la consulta, por favor intente de nuevo más tarde.`;
                notify(this.message, "error", 2500);
            });
    }

    restore() {
        this.consultation = new Consultation();
        this.medicalBackground = new MedicalBackground();
        this.physicalExam = new PhysicalExam();
        this.showBeginConsultation = false;
    }
}