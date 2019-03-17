import { Component, OnInit } from '@angular/core';
import { PatientsService } from 'app/services/patients.services';
import CustomStore from 'devextreme/data/custom_store';
import { MaritalStatusService } from 'app/services/maritalStatus.services';

declare var $: any;
declare var jQuery: any;

@Component({
  selector: 'fea-patient-list',
  templateUrl: './patient.list.html',
  providers: [
      PatientsService,
      MaritalStatusService
  ]
})

export class PatientList implements OnInit {
  
    patients: CustomStore;

    maritalStatuses: CustomStore;

    indentificationTypes: any[];

    genres: any[];

    constructor(private patientService: PatientsService, private maritalStatusService: MaritalStatusService) {
        this.indentificationTypes = [
            {
                id: "CC",
                name: "Cédula de Ciudadanía"
            }
        ];

        this.genres = [
            {
                id: "M",
                name: "Masculino"
            },
            {
                id: "F",
                name: "Femenino"
            }
        ];

        this.maritalStatuses = maritalStatusService.store;
    }

    ngOnInit() {
        this.patients = this.patientService.store;
    }
}