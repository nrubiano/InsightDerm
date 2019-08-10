import { Component, OnInit } from '@angular/core';
import { PatientsService } from '../../services/patients.services';
import { Patient } from '../../models/patient';
import { ConsultationsService } from 'app/services/consultations.services';

@Component({
    selector: 'fea-consultation-add',
    templateUrl: './consultation.add.html',
    providers: [
        PatientsService,
        ConsultationsService
    ]
})
export class ConsultationAdd implements OnInit {
    /**
     * Indicate if the search box is enabled
     */
    active = true;

    /**
     * Stores the patient's indentification number
     */
    patientId: string;

    showFormPatient = false;

    showBeginConsultation = false;

    patient: Patient;

    popupVisible = false;

    /**
     * Ctor
     */
    constructor(
        private patientsService: PatientsService
    ) { }
    /**
     * Init Event
     */
    ngOnInit() {}
    /**
     * Search a patient by his identification number
     */
    searchPatient(id = null) {
        const options = {
            filter: '["IdentificationNumber", "eq", "' + id + '"]'
        };

        this.patientsService
            .store
            .load(options)
            .then(
                (res: any) => {
                    if (res.totalCount === 1) {
                        this.showFormPatient = false;
                        this.patient = res.data[0];
                        this.showBeginConsultation = true;
                    } else {
                        this.patient = new Patient();
                        this.patient.identificationNumber = this.patientId;
                        this.showFormPatient = true;
                        this.showBeginConsultation = false;
                    }
                },
                () => {
                    console.log('error');
                }
            );
    }

    createOrUpdatePatient(patient: Patient) {
        this.patientsService
            .store
            .insert(patient)
            .then(
                (res) => {
                    this.searchPatient(patient.identificationNumber);
                },
                (res) => {
                    console.log(res);
                }
            );
    }
}
