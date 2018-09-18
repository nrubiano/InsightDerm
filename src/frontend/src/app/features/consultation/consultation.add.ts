import { Component, OnInit } from '@angular/core';
import { PatientsService } from '../../services/patients.services';
import { MaritalStatusService } from '../../services/maritalStatus.services';
import { Patient } from '../../models/patient';

declare var $: any;
declare var jQuery: any;

@Component({
  selector: 'fea-consultation-add',
  templateUrl: './consultation.add.html',  
  providers:[
    PatientsService
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
    /**
     * Ctor
     */
    constructor(private patientsService : PatientsService) { }
    /**
     * Init Event
     */
    ngOnInit() {
        
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
}