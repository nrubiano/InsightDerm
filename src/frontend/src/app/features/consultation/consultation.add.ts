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
    PatientsService,
    MaritalStatusService
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

    patient: Patient;

    maritalStatuses: any;
    /**
     * Ctor
     */
    constructor(private patientsService : PatientsService, private maritalStatusService:MaritalStatusService) { }
    /**
     * Init Event
     */
    ngOnInit() {
        this.loadMaritalStatus();
    }

    loadMaritalStatus(){
        this.maritalStatusService
            .store
            .load()
            .then((res:any)=>{                 
                if(res.totalCount > 0){
                    this.maritalStatuses = res.data;
                }
            }, 
            () => { 
                 console.log("Some error happen loading the marital statuses"); 
            });
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
                        this.showFormPatient = true;
                        if(res.data) {
                            console.log(res.data);
                            this.patient = res.data[0];
                        }
                    }, 
                    () => {
                        console.log("error");
                    }
                );
    }

    createOrUpdatePatient(form) {        
        var patient = form.value;
        patient.bornDate = `${patient.bornDate}T00:00:00`;

        this.patientsService
                .store
                .insert(patient)
                .then(
                    (res) => {           
                        form.reset();             
                    },
                    () => {
                        console.log("error");
                    }
                );
    }
}