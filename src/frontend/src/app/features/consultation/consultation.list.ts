import { Component, OnInit } from '@angular/core';
import CustomStore from 'devextreme/data/custom_store';
import { ConsultationsService } from 'app/services/consultations.services';
import { PatientsService } from 'app/services/patients.services';
import { DoctorsService } from 'app/services/doctors.services';
import {Router} from "@angular/router";
import {Consultation} from "../../models/consultation";

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

    consultations: Consultation[];

    patients: CustomStore;

    doctors: CustomStore;

    constructor(
        private router: Router,
        private consultationsService: ConsultationsService,
        private patientsService: PatientsService,
        private doctorsService: DoctorsService)
    {
        this.consultationsService.getAll().then(data => {
            this.consultations = data;
        });
        this.patients = this.patientsService.store;
        this.doctors = this.doctorsService.store;
    }

    ngOnInit() {

    }

    viewDetails(instance) {
        this.router.navigate(['/consultation/', instance.data.id]);
    }
}
