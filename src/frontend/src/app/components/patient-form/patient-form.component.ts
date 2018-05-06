import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { DxDateBoxModule } from 'devextreme-angular';
import { Patient } from 'app/models/patient';
import { MaritalStatusService } from 'app/services/maritalStatus.services';

@Component({
    selector: 'patient-form',
    templateUrl: 'patient-form.component.html',        
    providers:[
        MaritalStatusService
    ],
})
export class PatientFormComponent implements OnInit {
    
    @Input("patient") 
    patient: Patient;
    
    @Input("show") 
    showForm: boolean;

    @Output() 
    submit = new EventEmitter();

    maritalStatuses: any;

    constructor(private maritalStatusService: MaritalStatusService) { 
    }
    
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

    onSubmit(form) {
        this.patient = form.value;
        this.patient.bornDate = `${this.patient.bornDate}T00:00:00`;
        this.submit.emit(this.patient);
    }
}