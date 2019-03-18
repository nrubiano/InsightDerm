import { Component, OnInit } from '@angular/core';
import CustomStore from 'devextreme/data/custom_store';
import { ConsultationsService } from 'app/services/consultations.services';
import { PatientsService } from 'app/services/patients.services';
import { DoctorsService } from 'app/services/doctors.services';

declare var $: any;
declare var jQuery: any;

@Component({
  selector: 'fea-consultation-list',
  templateUrl: './consultation.list.html',
  providers: [
      ConsultationsService,
      PatientsService,
      DoctorsService
  ]
})
export class ConsultationList implements OnInit {
  
    consultations: CustomStore;

    patients: CustomStore;

    doctors: CustomStore;

    constructor(private consultationsService: ConsultationsService,
                private patientsService: PatientsService,
                private doctorsService: DoctorsService) {
        this.consultations = this.consultationsService.store;
        this.patients = this.patientsService.store;
        this.doctors = this.doctorsService.store;
    }

    ngOnInit() {
        
    }

    showDialogToAdd() {
        
    }
}